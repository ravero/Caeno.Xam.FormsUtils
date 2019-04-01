using System;
namespace Xamarin.Forms.Essentials.Validation.Validators
{
    public class MaxLengthValidator : IValidator<string>
    {
        readonly int maxLength;
        readonly string errorMessage;

        public int Order { get; }

        public MaxLengthValidator(int maxLength, string errorMessage, int order = 0) {
            this.maxLength = maxLength;
            this.errorMessage = errorMessage;
            Order = order;
        }

        public (bool IsValid, string ErrorMessage) Validate(string value) =>
            (value.Length <= maxLength) ? (true, null) : (false, errorMessage);
    }
}
