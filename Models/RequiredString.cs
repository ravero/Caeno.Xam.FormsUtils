using System;
using System.ComponentModel;

namespace FormsUtils.Models
{
    public class RequiredString : Validatable<string>
    {
        bool _validateOnValueChanged;
        public bool ValidateOnValueChanged {
            get => _validateOnValueChanged;
            set {
                _validateOnValueChanged = value;
                if (value)
                    PropertyChanged += OnLocalPropertyChanged;
                else
                    PropertyChanged -= OnLocalPropertyChanged;
            }
        }

        public RequiredString(string errorMessage, bool validateOnValueChanged = false) {
            ErrorMessage = errorMessage;
            ValidateOnValueChanged = validateOnValueChanged;
        }

        void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(Value))
                Validate();
        }

        public override void Validate() => IsValid = !string.IsNullOrWhiteSpace(Value);
    }
}
