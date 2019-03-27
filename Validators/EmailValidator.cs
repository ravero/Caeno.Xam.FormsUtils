using System;
using System.Text.RegularExpressions;

namespace FormsUtils.Validators
{
    public class EmailValidator : IValidator<string>
    {
        readonly string errorMessage;

        public EmailValidator(string errorMessage) => 
            this.errorMessage = errorMessage ?? 
                throw new ArgumentNullException(nameof(errorMessage));

        public (bool IsValid, string ErrorMessage) Validate(string value) {
            if (value.Length == 0)
                return (true, null);

            return IsValidEmail(value) ? (true, null) : (false, errorMessage);
        }

        static bool IsValidEmail(string strIn) {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
