using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Tools.ComponentModel
{
    public static class ModelValidator
    {
        /// <summary>
        /// Validate the model and return a response, which includes any validation messages and an IsValid bit.
        /// </summary>
        public static ModelValidationResult Validate(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);

            var isValid = Validator.TryValidateObject(model, context, results, true);

            return new ModelValidationResult()
            {
                IsValid = isValid,
                Results = results
            };
        }

        /// <summary>
        /// Validate the model and return a bit indicating whether the model is valid or not.
        /// </summary>
        public static bool IsModelValid(object model)
        {
            var response = Validate(model);

            return response.IsValid;
        }
    }
}
