using System;

namespace FormsUtils.Validators
{
    public interface IValidator<T>
    {
        (bool IsValid, string ErrorMessage) Validate(T value);
    }
}
