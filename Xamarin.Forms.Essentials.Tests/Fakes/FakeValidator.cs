using System;
using Xamarin.Forms.Essentials.Validation;

namespace Xamarin.Forms.Essentials.Tests.Fakes
{
    public class FakeValidator<T> : IValidator<T>
    {
        public bool IsValid { get; set; }

        public int Order { get; set; }

        public string Message { get; set; }

        public (bool IsValid, string ErrorMessage) Validate(T value) => (IsValid, Message);
    }
}
