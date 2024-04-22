using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.Enums
{
    public enum ScopusSubjectAreaType
    {
        [Display(Name = "Agricultural and Biological Sciences")]
        AGRI,
        [Display(Name = "Arts and Humanities")]
        ARTS,
        [Display(Name = "Biochemistry, Genetics and Molecular Biology")]
        BIOC,
        [Display(Name = "Business, Management and Accounting")]
        BUSI,
        [Display(Name = "Chemical Engineering")]
        CENG,
        [Display(Name = "Chemistry")]
        CHEM,
        [Display(Name = "Computer Science")]
        COMP,
        [Display(Name = "Decision Sciences")]
        DECI,
        [Display(Name = "Dentistry")]
        DENT,
        [Display(Name = "Earth and Planetary Sciences")]
        EART,
        [Display(Name = "Economics, Econometrics and Finance")]
        ECON,
        [Display(Name = "Energy")]
        ENER,
        [Display(Name = "Engineering")]
        ENGI,
        [Display(Name = "Environmental Science")]
        ENVI,
        [Display(Name = "Health Professions")]
        HEAL,
        [Display(Name = "Immunology and Microbiology")]
        IMMU,
        [Display(Name = "Materials Science")]
        MATE,
        [Display(Name = "Mathematics")]
        MATH,
        [Display(Name = "Medicine")]
        MEDI,
        [Display(Name = "Neuroscience")]
        NEUR,
        [Display(Name = "Nursing")]
        NURS,
        [Display(Name = "Pharmacology, Toxicology and Pharmaceutics")]
        PHAR,
        [Display(Name = "Physics and Astronomy")]
        PHYS,
        [Display(Name = "Psychology")]
        PSYC,
        [Display(Name = "Social Sciences")]
        SOCI,
        [Display(Name = "Veterinary")]
        VETE,
        [Display(Name = "Multidisciplinary")]
        MULT
    }
}
#warning traer estos valores dinámicamente desde https://api.elsevier.com/content/subject/scopus?httpAccept=application/json
