using System;

namespace FormsUtils.Validation.Validators
{
    public class RequiredValidator : IValidator<string>
    {
        readonly string errorMessage;

        public RequiredValidator(string errorMessage) => 
            this.errorMessage = errorMessage ?? 
                throw new ArgumentNullException(nameof(errorMessage));

        public (bool IsValid, string ErrorMessage) Validate(string value) =>
            string.IsNullOrWhiteSpace(value) ? (false, errorMessage) : (true, null);
    }
}
