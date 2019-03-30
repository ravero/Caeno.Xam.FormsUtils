using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation;
using FormsUtils.Validation.Validators;
using System.Linq;

namespace Validation
{
    [TestFixture]
    public class ValidatableBuilderTests
    {
        [Test]
        public void Builder_Build_WithDefaultState() {
            var validatable = ValidatableBuilder.Create<string>().Build();
            Assert.IsNull(validatable.Value);
            Assert.IsTrue(validatable.IsValid);
        }

        [Test]
        public void Builder_Build_WithValidator() {
            var validatable = ValidatableBuilder.Create<string>()
                .WithValidator(new RequiredValidator("Required field."))
                .Build();

            Assert.AreEqual(1, validatable.Validators.Count());
        }

        [Test]
        public void Builder_Build_WithRequired() {
            var validatable = ValidatableBuilder.Create<string>()
                .WithRequired("Required field.")
                .Build();

            Assert.AreEqual(1, validatable.Validators.Count());
            Assert.Contains(typeof(RequiredValidator), 
                validatable.Validators.Select(v => v.GetType()).ToList());
        }

        [Test]
        public void Builder_BuildTextValidatable_WithDefaultState() {
            var validatable = ValidatableBuilder.CreateText().Build();
            Assert.IsNull(validatable.Value);
            Assert.IsTrue(validatable.IsValid);
        }

        [Test]
        public void Builder_BuildTextValidatable_WithValidator() {
            var validatable = ValidatableBuilder.CreateText()
                .WithValidator(new RequiredValidator("Required field."))
                .Build();

            Assert.AreEqual(1, validatable.Validators.Count());
        }

        [Test]
        public void Builder_BuildTextValidatable_WithRequired() {
            var validatable = ValidatableBuilder.CreateText()
                .WithRequired("Required field.")
                .Build();

            Assert.AreEqual(1, validatable.Validators.Count());
            Assert.Contains(typeof(RequiredValidator),
                validatable.Validators.Select(v => v.GetType()).ToList());
        }
    }
}
