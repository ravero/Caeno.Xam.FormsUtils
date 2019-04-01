using System;
using System.ComponentModel;

namespace Xamarin.Forms.Essentials.Validation
{
    /// <summary>
    /// A Validatable that holds text content as string.
    /// </summary>
    public class TextValidatable : Validatable<string>
    {
        bool _validateOnValueChanged;
        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:Xamarin.Forms.Essentials.Validation.TextValidatable"/> validate on value changed.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Xamarin.Forms.Essentials.Validation.TextValidatable"/> class.
        /// </summary>
        /// <param name="validateOnValueChanged">If set to <c>true</c> validate when the Value property changes its content.</param>
        public TextValidatable(bool validateOnValueChanged = false) => ValidateOnValueChanged = validateOnValueChanged;

        void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(Value))
                Validate();
        }

        public override string ToString() => Value;

        public static implicit operator string(TextValidatable str) => str.Value;
    }
}
