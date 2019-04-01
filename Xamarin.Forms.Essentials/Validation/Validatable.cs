using System;
using System.Collections.Generic;
using FormsUtils.Models;
using System.Linq;

namespace Xamarin.Forms.Essentials.Validation
{
    /// <summary>
    /// Represents a Validatable Value.
    /// This class holds the value, the logic to validate its value and its validation state.
    /// </summary>
    public class Validatable<T> : ObservableBase
    {
        readonly List<IValidator<T>> validatorsList = new List<IValidator<T>>();
        public IEnumerable<IValidator<T>> Validators => validatorsList;

        bool _isValid = true;
        /// <summary>
        /// The validation state of this instace.
        /// This property implements Property Changed.
        /// </summary>
        /// <value><c>true</c> if is valid; otherwise, <c>false</c>.</value>
        public bool IsValid {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        T _value;
        /// <summary>
        /// The value hold by this validatable.
        /// This property implements Property Changed.
        /// </summary>
        public T Value {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        string _errorMessage;
        /// <summary>
        /// The error message that describes the current error state of this instance.
        /// This property implements Property Changed.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        /// <summary>
        /// Adds a new Validator to the collection of validators of this instance.
        /// </summary>
        /// <param name="validator">The Validator object.</param>
        internal void AddValidator(IValidator<T> validator) => validatorsList.Add(validator);

        /// <summary>
        /// Validates the current value holden by this instance.
        /// The default implementation applies the validators added to this instance.
        /// This method is virtual and may be overriden to provide an alternative implementation.
        /// </summary>
        public virtual void Validate() {
            foreach (var validator in Validators.OrderBy(v => v.Order)) {
                var validation = validator.Validate(Value);
                if (!validation.IsValid) {
                    IsValid = false;
                    ErrorMessage = validation.ErrorMessage;
                    return;
                }
            }
            IsValid = true;
        }
    }
}
