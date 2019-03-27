using System;
namespace FormsUtils.Validatables
{
    public class CompareStringValidatable : StringValidatable
    {
        private readonly StringValidatable compareValidatable;
        private readonly string compareMessage;

        public CompareStringValidatable(string requiredMessage, StringValidatable compareValidatable, string compareMessage, bool validateOnValueChanged = true) : 
            base(requiredMessage, true, validateOnValueChanged) {
            this.compareValidatable = compareValidatable;
            this.compareMessage = compareMessage;

            compareValidatable.PropertyChanged += (sender, e) => {
                if (e.PropertyName == nameof(Value))
                    Validate();
            };
        }

        public override void Validate() {
            base.Validate();

            // Compare only if base validation is ok
            if (IsValid) {
                IsValid = Value == compareValidatable.Value;
                ErrorMessage = !IsValid ? compareMessage : null;
            }
        }
    }
}
