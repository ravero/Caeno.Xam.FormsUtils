using System;
namespace FormsUtils.Validators
{
    public class RequiredStringValidator : IValidator<string>
    {
        readonly string errorMessage;

        public RequiredStringValidator(string errorMessage) => 
            this.errorMessage = errorMessage ?? 
                throw new ArgumentNullException(nameof(errorMessage));

        public (bool IsValid, string ErrorMessage) Validate(string value) =>
            string.IsNullOrWhiteSpace(value) ? (false, errorMessage) : (true, null);
    }
}
