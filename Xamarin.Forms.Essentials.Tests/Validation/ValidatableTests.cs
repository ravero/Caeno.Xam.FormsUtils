using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation;
using FormsUtils.Validation;

namespace Validation
{
    [TestFixture]
    public class ValidatableTests
    {
        [Test]
        public void Validatable_ByDefault_ValueNull() {
            var validatable = MakeValidator();
            Assert.IsNull(validatable.Value);
        }

        [Test]
        public void Validatable_ByDefault_IsValidTrue() {
            var validatable = MakeValidator();
            Assert.IsTrue(validatable.IsValid);
        }

        [Test]
        public void Validatable_Validate_ChangeIsValid() {
            throw new NotImplementedException();
        }

        [Test]
        public void Validatable_ValidateMultiple_ChangeIsValid() {
            throw new NotImplementedException();
        }

        [Test]
        public void Validatable_ValidateMultiple_CorrectOrder() {
            throw new NotImplementedException();
        }

        Validatable<object> MakeValidator() => ValidatableBuilder.Create<object>().Build();
    }
}
