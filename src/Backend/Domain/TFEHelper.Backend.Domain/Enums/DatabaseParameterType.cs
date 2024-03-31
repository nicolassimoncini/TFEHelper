using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Enums
{
    /// <summary>
    /// Database parameter type used by <see cref="IDatabaseParameter"/>.
    /// </summary>
    public enum DatabaseParameterType
    {
        /// <summary>Integer type<br></br>Note: this is the default value if none is defined.</summary>
        Int,
        /// <summary>Floal point type.</summary>
        Real,
        /// <summary>String type.</summary>
        String,
        /// <summary>Boolean type.</summary>
        Boolean,
        /// <summary>Date and time type.</summary>
        DateTime
    }
}
