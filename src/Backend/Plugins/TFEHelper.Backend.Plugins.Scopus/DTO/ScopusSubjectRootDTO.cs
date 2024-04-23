using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.DTO
{
    public class ScopusSubjectRootDTO
    {
        [JsonPropertyName("subject-classifications")]
        public ScopusSubjectNestedRootDTO Info { get; set; }
    }

    public class ScopusSubjectNestedRootDTO
    {
        [JsonPropertyName("subject-classification")]
        public List<ScopusSubjectDTO> Subjects { get; set; }
    }
}
