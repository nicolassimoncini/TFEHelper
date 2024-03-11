using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Attributes
{
    /// <summary>
    /// Indica si el atributo debe ser tomado como clave para la especificación BibTeX.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BibTeXKeyAttribute : Attribute
    {
    }
}
