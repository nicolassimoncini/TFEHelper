using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Services.Contracts.Attributes;

namespace TFEHelper.Backend.Services.Contracts.DTO.API
{
    public class NarrowingExpressionDTO
    {
        [Required(AllowEmptyStrings = false)]
        [MustBeAnAttributeOf(typeof(PublicationDTO), typeof(string))]
        public string FieldName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstSentence { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string SecondSentence { get; set; }

        [Range(0, 10)]
        public int MinimumDistance { get; set; }
    }
}
