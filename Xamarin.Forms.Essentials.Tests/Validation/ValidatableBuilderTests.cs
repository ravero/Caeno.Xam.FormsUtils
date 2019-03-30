using System;
using NUnit.Framework;
using Xamarin.Forms.Essentials.Validation;
using FormsUtils.Validation.Validators;

namespace Validation
{
    [TestFixture]
    public class ValidatableBuilderTests
    {
        [Test]
        public void Builder_Build_EmptyValidatable() {
            var validatable = ValidatableBuilder.Create<string>();
            Assert.IsNull(validatable.Value);
        }
    }
}
