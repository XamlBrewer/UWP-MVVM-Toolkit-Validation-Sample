using System;
using System.ComponentModel.DataAnnotations;

namespace Mvvm
{
    public sealed class GreaterThanAttribute : ValidationAttribute
    {
        private string _errorMessage;

        public GreaterThanAttribute(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            _errorMessage = errorMessage;
        }

        public string PropertyName { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var instance = validationContext.ObjectInstance;
            var otherValue = instance.GetType().GetProperty(PropertyName).GetValue(instance);

            if (((IComparable)value).CompareTo(otherValue) > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(_errorMessage);
        }
    }
}
