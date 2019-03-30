using System;
using System.Collections.Generic;
using FormsUtils.Models;
using System.Linq;

namespace FormsUtils.Validation
{
    public class Validatable<T> : ObservableBase
    {
        readonly List<IValidator<T>> validators = new List<IValidator<T>>();

        bool _isValid = true;
        public bool IsValid {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        T _value;
        public T Value {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        string _errorMessage;
        public string ErrorMessage {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public void AddValidator(IValidator<T> validator) => validators.Add(validator);

        public virtual void Validate() {
            foreach (var validator in validators) {
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
