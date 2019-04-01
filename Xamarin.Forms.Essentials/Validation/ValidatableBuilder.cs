using System;
using System.Collections.Generic;
using Xamarin.Forms.Essentials.Validation.Validators;
using FormsUtils.Extensions;

namespace Xamarin.Forms.Essentials.Validation
{
    public static class ValidatableBuilder
    {
        public static ValidatableBuilder<T> Create<T>() => new ValidatableBuilder<T>();

        public static TextValidatableBuilder CreateText() => new TextValidatableBuilder();

        public static ValidatableBuilder<string> WithRequired(this ValidatableBuilder<string> validatable, string errorMessage, int order = 0) =>
            validatable.WithValidator(new RequiredValidator(errorMessage, order));

        public static ValidatableBuilder<string> WithEmail(this ValidatableBuilder<string> validatable, string errorMessage, int order) =>
            validatable.WithValidator(new EmailValidator(errorMessage, order));
    }

    public class ValidatableBuilder<T>
    {
        protected List<IValidator<T>> validators = new List<IValidator<T>>();

        public ValidatableBuilder<T> WithValidator(IValidator<T> validator) {
            validators.Add(validator);
            return this;
        }

        public virtual Validatable<T> Build() {
            var validatable = new Validatable<T>();
            validators.Iter(v => validatable.AddValidator(v));
            return validatable;
        }
    }

    public class TextValidatableBuilder : ValidatableBuilder<string>
    {
        bool isValidateOnValueChanged;

        public TextValidatableBuilder SetValidateOnValueChanged(bool isValidate) {
            isValidateOnValueChanged = isValidate;
            return this;
        }

        public new TextValidatableBuilder WithValidator(IValidator<string> validator) {
            base.WithValidator(validator);
            return this;
        }

        public new TextValidatable Build() {
            var validatable = new TextValidatable(isValidateOnValueChanged);
            validators.Iter(v => validatable.AddValidator(v));
            return validatable;
        }
    }
}
