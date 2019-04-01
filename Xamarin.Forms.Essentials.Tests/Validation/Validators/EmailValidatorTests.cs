using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation.Validators;

namespace Validation.Validators
{
    [TestFixture]
    public class EmailValidatorTests
    {
        const string ErrorMessage = "Invalid email.";
        const string ValidEmailAddress = "test@test.com";
        const string InvalidEmailAddress = "t23$#test";

        [Test]
        public void Validator_ValidateCorrectEmail_ReturnsTrue() {
            var validator = CreateValidator();
            var (success, _) = validator.Validate(ValidEmailAddress);
            Assert.IsTrue(success);
        }

        [Test]
        public void Validator_ValidateIncorrectEmail_ReturnsFalse() {
            var validator = CreateValidator();
            var (success, _) = validator.Validate(InvalidEmailAddress);
            Assert.IsFalse(success);
        }

        [Test]
        public void Validator_ValidateIncorrectEmail_ReturnsMessage() {
            var validator = CreateValidator();
            var (_, message) = validator.Validate(InvalidEmailAddress);
            Assert.AreEqual(ErrorMessage, message);
        }

        EmailValidator CreateValidator() => new EmailValidator(ErrorMessage);
    }
}
