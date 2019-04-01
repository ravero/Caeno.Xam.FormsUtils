using System;

namespace Xamarin.Forms.Essentials.Validation
{
    /// <summary>
    /// Represents a Validator of the Specified Type.
    /// </summary>
    public interface IValidator<T>
    {
        /// <summary>
        /// Determines the Order that this Validator should be applied.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Validates the Value.
        /// </summary>
        /// <returns>
        /// A tuple where the first value indicates if the validation has succeeded 
        /// and the second parameter represents the error message.
        /// </returns>
        /// <param name="value">The value that will be validated.</param>
        (bool IsValid, string ErrorMessage) Validate(T value);
    }
}
