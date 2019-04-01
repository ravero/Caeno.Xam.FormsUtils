using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation;
using Xamarin.Forms.Essentials.Tests.Fakes;

namespace Validation
{
    [TestFixture]
    public class TextValidatableTests
    {
        private const string ErrorMessage = "Validated on Change";

        [Test]
        public void TextValidatable_OnValueChanged_Validate() {
            var validator = new FakeValidator<string> {
                Message = ErrorMessage,
                IsValid = false,
            };
            var validatable = MakeValidatable(validator, true);

            // Asserts the initial state
            Assert.IsTrue(validatable.IsValid);

            // Change the value and check if state has changed
            validatable.Value = "test content";
            Assert.IsFalse(validatable.IsValid);
            Assert.AreEqual(ErrorMessage, validatable.ErrorMessage);
        }

        TextValidatable MakeValidatable(IValidator<string> validator, bool validateOnChange) => 
            ValidatableBuilder.CreateText()
                .WithValidator(validator)
                .SetValidateOnValueChanged(validateOnChange)
                .Build();
    }
}
