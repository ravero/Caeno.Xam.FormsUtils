using System;
namespace Xamarin.Forms.Essentials.Validation.Validators
{
    public class MinLengthValidator : IValidator<string>
    {
        readonly int minLength;
        readonly string errorMessage;

        public int Order { get; }

        public MinLengthValidator(int minLength, string errorMessage, int order = 0) {
            this.minLength = minLength;
            this.errorMessage = errorMessage;
            Order = order;
        }

        public (bool IsValid, string ErrorMessage) Validate(string value) =>
            (value.Length >= minLength) ? (true, null) : (false, errorMessage);
    }
}
