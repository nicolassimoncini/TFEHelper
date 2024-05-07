using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Contracts.Attributes
{
    /// <summary>
    /// Validates if a provided date value is greater than another.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class DateGreaterOrEqualsThan : ValidationAttribute
    {
        private readonly string _property;

        /// <summary></summary>
        public DateGreaterOrEqualsThan(string property)
            : base("{0} must be greater or equals than " + property)
        {
            _property = property;
        }

        /// <summary></summary>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = true;
            try
            {
                PropertyInfo property = validationContext.ObjectInstance.GetType().GetProperties().FirstOrDefault(p => p.Name == _property);
                result = ((DateOnly)value >= ((DateOnly)property.GetValue(validationContext.ObjectInstance)));
            }
            catch { }
            return (result) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
