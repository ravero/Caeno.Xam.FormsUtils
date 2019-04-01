using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation.Validators;

namespace Validation.Validators
{
    [TestFixture]
    public class MinLengthValidatorTests
    {
        const int MinLength = 5;
        const string ErrorMessage = "Minimum length less than allowed.";
        const string ValidString = "123456789";
        const string InvalidString = "123";

        [Test]
        public void Validator_ValidateAboveMin_ReturnsTrue() {
            var validator = CreateValidator();
            var (success, _) = validator.Validate(ValidString);
            Assert.IsTrue(success);
        }

        [Test]
        public void Validator_ValidateBelowMin_ReturnsFalse() {
            var validator = CreateValidator();
            var (success, _) = validator.Validate(InvalidString);
            Assert.IsFalse(success);
        }

        [Test]
        public void Validator_ValidateBelowMin_ReturnsMessage() {
            var validator = CreateValidator();
            var (_, message) = validator.Validate(InvalidString);
            Assert.AreEqual(ErrorMessage, message);
        }

        MinLengthValidator CreateValidator() => new MinLengthValidator(MinLength, ErrorMessage);
    }
}
