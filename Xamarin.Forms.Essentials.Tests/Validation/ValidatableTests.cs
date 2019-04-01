using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation;

namespace Validation
{
    [TestFixture]
    public class ValidatableTests
    {
        [Test]
        public void Validatable_ByDefault_ValueNull() {
            var validatable = MakeValidatable();
            Assert.IsNull(validatable.Value);
        }

        [Test]
        public void Validatable_ByDefault_IsValidTrue() {
            var validatable = MakeValidatable();
            Assert.IsTrue(validatable.IsValid);
        }

        [Test]
        public void Validatable_Validate_ChangeIsValid() {
            var validatable = ValidatableBuilder.Create<object>()
                .WithValidator(new AlwaysFalseValidator())
                .Build();

            validatable.Validate();
            Assert.IsFalse(validatable.IsValid);
        }

        [Test]
        public void Validatable_ValidateMultiple_ChangeIsValid() {
            var validatable = ValidatableBuilder.Create<object>()
                .WithValidator(new AlwaysTrueValidator(1))
                .WithValidator(new AlwaysFalseValidator(null, 2))
                .Build();

            validatable.Validate();
            Assert.IsFalse(validatable.IsValid);
        }

        [Test]
        public void Validatable_ValidateMultiple_CorrectOrder() {
            var errorMessages = new[] {
                "ErrorMessage1",
                "ErrorMessage2",
                "ErrorMessage3",
            };
            var validators = new[] {
                new FakeValidator { Message = errorMessages[0], Order = 1 },
                new FakeValidator { Message = errorMessages[1], Order = 2 },
                new FakeValidator { Message = errorMessages[2], Order = 3 },
            };
            var validatable = ValidatableBuilder.Create<object>()
                .WithValidator(validators[0])
                .WithValidator(validators[1])
                .WithValidator(validators[2])
                .Build();

            for (int i = 0; i < validators.Length; i++) {
                validatable.Validate();
                Assert.IsFalse(validatable.IsValid);
                Assert.AreEqual(errorMessages[i], validatable.ErrorMessage);

                validators[i].IsValid = true;
            }
        }

        Validatable<object> MakeValidatable() => ValidatableBuilder.Create<object>().Build();

        #region Fakes
        class FakeValidator : IValidator<object>
        {
            public bool IsValid { get; set; }

            public int Order { get; set; }

            public string Message { get; set; }

            public (bool IsValid, string ErrorMessage) Validate(object value) => (IsValid, Message);
        }

        class AlwaysTrueValidator : IValidator<object>
        {
            public int Order { get; }

            public AlwaysTrueValidator(int order = 1) {
                Order = order;
            }

            public (bool IsValid, string ErrorMessage) Validate(object value) => (true, null);
        }

        class AlwaysFalseValidator : IValidator<object>
        {
            readonly string message;

            public int Order { get; }

            public AlwaysFalseValidator(string message = null, int order = 1) {
                this.message = message;
                Order = order;
            }

            public (bool IsValid, string ErrorMessage) Validate(object value) => (false, message);
        }
        #endregion
    }
}
