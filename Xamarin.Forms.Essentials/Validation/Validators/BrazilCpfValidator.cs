using System;
using System.Text.RegularExpressions;

namespace Xamarin.Forms.Essentials.Validation.Validators
{
    public class BrazilCpfValidator : IValidator<string>
    {
        readonly static int[] digit1Multipliers = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        readonly static int[] digit2Multipliers = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        readonly string errorMessage;

        public int Order { get; }

        public BrazilCpfValidator(string errorMessage, int order = 0) {
            this.errorMessage = errorMessage;
            Order = order;
        }

        public (bool IsValid, string ErrorMessage) Validate(string value) {
            value = value.Trim().Replace(".", "").Replace("-", "");
            if (value.Length != 11)
                return (false, errorMessage);

            //
            // Validate same digits string
            for (int i = 0; i <= 9; i++) {
                var invalidNumber = new string(i.ToString()[0], 11);
                if (value == invalidNumber)
                    return (false, errorMessage);
            }

            //
            // Validate digits
            var tempCpf = value.Substring(0, 9);
            var sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * digit1Multipliers[i];

            var remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            var digit = remainder.ToString();
            tempCpf += digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * digit2Multipliers[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit += remainder.ToString();

            return value.EndsWith(digit, StringComparison.Ordinal) ? (true, null) : (false, errorMessage);
        }
    }
}
