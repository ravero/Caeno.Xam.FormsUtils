using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation.Validators;

namespace Validation.Validators
{
    [TestFixture]
    public class RequiredValidatorTests
    {
        const string ErrorMessage = "Field required.";

        [Test]
        public void Validator_ValidateFilled_ReturnsTrue() {
            var validator = CreateValidator();
            var value = "test content";
            var (success, _) = validator.Validate(value);

            Assert.IsTrue(success);
        }

        [Test]
        public void Validator_ValidateNull_ReturnsFalse() {
            var validator = CreateValidator();
            var value = (string)null;
            var (success, _) = validator.Validate(value);

            Assert.IsFalse(success);
        }

        [Test]
        public void Validator_ValidateEmpty_ReturnsFalse() {
            var validator = CreateValidator();
            var value = string.Empty;
            var (success, _) = validator.Validate(value);

            Assert.IsFalse(success);
        }

        [Test]
        public void Validator_ValidateWhiteSpace_ReturnsFalse() {
            var validator = CreateValidator();
            var value = "    ";
            var (success, _) = validator.Validate(value);

            Assert.IsFalse(success);
        }

        [Test]
        public void Validator_Invalid_ReturnsMessage() {
            var validator = CreateValidator();
            var value = (string)null;
            var (_, message) = validator.Validate(value);

            Assert.AreEqual(ErrorMessage, message);
        }

        RequiredValidator CreateValidator() => new RequiredValidator(ErrorMessage);
    }
}
