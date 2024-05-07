using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class MustBeAnAttributeOf : ValidationAttribute
    {
        private readonly Type _classType;
        private readonly Type _attributeType;

        /// <summary></summary>
        public MustBeAnAttributeOf(Type classType, Type attributeType)
            : base("{0} must be a string attribute of " + classType.Name)
        {
            _classType = classType;
            _attributeType = attributeType;
        }

        /// <summary></summary>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool result = false;
            try
            {
                PropertyInfo property = _classType.GetProperties().FirstOrDefault(p => p.Name == Convert.ToString(value));
                result = property != null && property.PropertyType == _attributeType;
            } catch { }            
            return (result) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
