 using System;

namespace FormsUtils.Models
{
    public class Validatable<T> : ObservableBase
    {
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

        public virtual void Validate() { }
    }
}
