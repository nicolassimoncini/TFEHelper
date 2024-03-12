using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Domain.Classes.Database.Specifications
{
    /// <summary>
    /// Represents a database query or command parameter.
    /// </summary>
    public interface IDatabaseParameter
    {
        /// <summary>Parameter name</summary>
        public string Name { get; set; }

        /// <summary>Parameter type.<br>Note: if not defined, it could be infered by the implementer.</br></summary>        
        public DatabaseParameterType? Type { get; set; }

        /// <summary>Parameter value.<br>Note: if <see cref="Type"/> is defined, it must match with this value.</br></summary>        
        public object Value { get; set; }

        /// <summary>Parameter size.<br>Note: if not defined, itu could be infered by the implementer.</br></summary>        
        public int? Size { get; set; }

        /// <summary>Value used to instruct <c>DataSet</c> the direction for which the parameter has been set.</summary>        
        public ParameterDirection Direction { get; set; }
    }
}
