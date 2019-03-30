using System;
using FormsUtils.Validation;
using FormsUtils.Validation.Validators;

namespace Xamarin.Forms.Essentials.Validation
{
    public static class ValidatableBuilder
    {
        public static ValidatableBuilder<T> Create<T>() => new ValidatableBuilder<T>(new Validatable<T>());

        public static ValidatableBuilder<string> CreateText() => new ValidatableBuilder<string>(new Validatable<string>());

        public static ValidatableBuilder<string> WithRequired(this ValidatableBuilder<string> validatable, string errorMessage) =>
            validatable.WithValidator(new RequiredValidator(errorMessage));

        public static ValidatableBuilder<string> WithEmail(this ValidatableBuilder<string> validatable, string errorMessage) =>
            validatable.WithValidator(new EmailValidator(errorMessage));
    }

    public class ValidatableBuilder<T>
    {
        Validatable<T> validatable;

        public ValidatableBuilder(Validatable<T> validatable) {
            this.validatable = validatable;
        }

        public ValidatableBuilder<T> WithValidator(IValidator<T> validator) {
            validatable.AddValidator(validator);
            return this;
        }

        public Validatable<T> Build() => validatable;
    }
}
