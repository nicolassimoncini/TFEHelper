using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Tools.ComponentModel
{
    public class ModelValidationResult
    {
        public List<ValidationResult> Results { get; set; }
        public bool IsValid { get; set; }

        public ModelValidationResult()
        {
            Results = new List<ValidationResult>();
            IsValid = false;
        }
    }
}
