using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using FormsUtils.Validators;

namespace FormsUtils.Validatables
{
    public class EmailValidatable : StringValidatable
    {
        public EmailValidatable(string invalidEmailMessage, bool isRequired = false, string requiredMessage = null, bool validateOnValueChanged = true) : base(requiredMessage, isRequired, validateOnValueChanged) {
            AddValidator(new EmailValidator(invalidEmailMessage));
        }
    }
}
