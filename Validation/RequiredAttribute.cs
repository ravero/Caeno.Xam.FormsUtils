using System;

namespace FormsUtils.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}
