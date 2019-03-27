using System;
using System.ComponentModel;
using System.Collections.Generic;
using FormsUtils.Validators;
using System.Linq;

namespace FormsUtils.Validatables
{
    public class StringValidatable : Validatable<string>
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

        public StringValidatable(string requiredMessage = null, bool isRequired = true, bool validateOnValueChanged = true) {
            ValidateOnValueChanged = validateOnValueChanged;
            if (isRequired)
                AddValidator(new RequiredStringValidator(requiredMessage));
        }

        void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(Value))
                Validate();
        }

        public override string ToString() => Value;

        public static implicit operator string(StringValidatable str) => str.Value;
    }
}
