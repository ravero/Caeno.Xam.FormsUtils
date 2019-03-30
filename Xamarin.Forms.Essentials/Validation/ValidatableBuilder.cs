using System;
using FormsUtils.Validation;

namespace Xamarin.Forms.Essentials.Validation
{
    public static class ValidatableBuilder
    {
        public static Validatable<T> Create<T>() => new Validatable<T>();
    }
}
