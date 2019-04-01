using System;

namespace Xamarin.Forms.Essentials.Validation.Validators
{
    public class RequiredValidator : IValidator<string>
    {
        readonly string errorMessage;

        public int Order { get; }

        public RequiredValidator(string errorMessage, int order = 0) {
            this.errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            Order = order;
        }

        public (bool IsValid, string ErrorMessage) Validate(string value) =>
            string.IsNullOrWhiteSpace(value) ? (false, errorMessage) : (true, null);
    }
}
