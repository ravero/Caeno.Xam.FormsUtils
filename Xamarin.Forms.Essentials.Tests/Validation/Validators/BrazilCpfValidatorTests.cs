using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation.Validators;

namespace Validation.Validators
{
    [TestFixture]
    public class BrazilCpfValidatorTests
    {
        const string ErrorMessage = "invalid cpf number";

        [TestCase("30642010854")]
        [TestCase("306420108-54")]
        public void Validator_ValidCpf_ReturnsTrue(string cpf) {
            var validator = CreateValidator();
            var (success, _) = validator.Validate(cpf);
            Assert.IsTrue(success);
        }


        [TestCase("11111111111")]
        [TestCase("12345678910")]
        [TestCase("1023021931")]
        public void Validator_InvalidCpf_ReturnsFalse(string cpf) {
            var validator = CreateValidator();
            var (success, _) = validator.Validate(cpf);
            Assert.IsFalse(success);
        }

        [TestCase("11111111111")]
        [TestCase("12345678910")]
        [TestCase("1023021931")]
        public void Validator_InvalidCpf_ReturnsMessage(string cpf) {
            var validator = CreateValidator();
            var (_, message) = validator.Validate(cpf);
            Assert.AreEqual(ErrorMessage, message);
        }

        BrazilCpfValidator CreateValidator() => new BrazilCpfValidator(ErrorMessage);
    }
}
