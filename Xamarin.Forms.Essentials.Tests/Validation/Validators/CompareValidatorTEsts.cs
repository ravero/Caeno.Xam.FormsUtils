using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation.Validators;
using Xamarin.Forms.Essentials.Validation;

namespace Validation.Validators
{
    [TestFixture]
    public class CompareValidatorTests
    {
        const string ErrorMessage = "values must match";

        [Test]
        public void Validator_SameString_ReturnsTrue() {
            var validatable = ValidatableBuilder.CreateText().Build();
            var validator = CreateValidator(validatable);

            validatable.Value = "test";
            var (success, _) = validator.Validate("test");
            Assert.IsTrue(success);
        }

        [Test]
        public void Validator_Empty_RetursTrue() {
            var validatable = ValidatableBuilder.CreateText().Build();
            var validator = CreateValidator(validatable);

            var (success, _) = validator.Validate(null);
            Assert.IsTrue(success);
        }

        [Test]
        public void Validator_DifferentString_ReturnsFalse() {
            var validatable = ValidatableBuilder.CreateText().Build();
            var validator = CreateValidator(validatable);

            validatable.Value = "test1";
            var (success, _) = validator.Validate("test");
            Assert.IsFalse(success);
        }

        [Test]
        public void Validator_DifferentString_ReturnsMessage() {
            var validatable = ValidatableBuilder.CreateText().Build();
            var validator = CreateValidator(validatable);

            validatable.Value = "test1";
            var (_, message) = validator.Validate("test");
            Assert.AreEqual(ErrorMessage, message);
        }

        CompareValidator CreateValidator(TextValidatable validatable) => new CompareValidator(validatable, ErrorMessage);
    }
}
