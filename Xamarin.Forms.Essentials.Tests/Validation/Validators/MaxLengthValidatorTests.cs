using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation.Validators;

namespace Validation.Validators
{
    [TestFixture]
    public class MaxLengthValidatorTests
    {
        const int MaxLength = 10;
        const string ErrorMessage = "Maximum length greater than allowed.";
        const string ValidString = "123456789";
        const string InvalidString = "12345678901";

        [Test]
        public void Validator_ValidateBelowMax_ReturnsTrue() {
            var validator = CreateValidator();
            var (success, _) = validator.Validate(ValidString);
            Assert.IsTrue(success);
        }

        [Test]
        public void Validator_ValidateAboveMax_ReturnsFalse() {
            var validator = CreateValidator();
            var (success, _) = validator.Validate(InvalidString);
            Assert.IsFalse(success);
        }

        [Test]
        public void Validator_ValidateAboveMax_ReturnsMessage() {
            var validator = CreateValidator();
            var (_, message) = validator.Validate(InvalidString);
            Assert.AreEqual(ErrorMessage, message);
        }

        MaxLengthValidator CreateValidator() => new MaxLengthValidator(MaxLength, ErrorMessage);
    }
}
