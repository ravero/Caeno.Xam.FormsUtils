using System;

namespace Xamarin.Forms.Essentials.Validation.Validators
{
    public class CompareValidator : IValidator<string>
    {
       readonly TextValidatable compareToValidatable;
       readonly string errorMessage;

        public CompareValidator(TextValidatable compareToValidatable, string errorMessage, int order = 0) {
            this.compareToValidatable = compareToValidatable ?? throw new ArgumentNullException(nameof(compareToValidatable));
            this.errorMessage = errorMessage;
            Order = order;
        }

        public int Order { get; }

        public (bool IsValid, string ErrorMessage) Validate(string value) => 
            (value == compareToValidatable.Value) ? (true, null) : (false, errorMessage);
    }
}
