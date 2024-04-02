using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Domain.Classes.Database
{
    /// <summary>
    /// Parameter entity to be used when running a database query or command
    /// </summary>
    public sealed class DatabaseParameter : IDatabaseParameter
    {
        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc/>
        public DatabaseParameterType? Type { get; set; }

        /// <inheritdoc/>
        public object Value { get; set; }

        /// <inheritdoc/>
        public int? Size { get; set; }

        /// <inheritdoc/>
        public ParameterDirection Direction { get; set; }


        /// <summary>Creates an instance of <see cref="DatabaseParameter"/>.<br>Note: <see cref="Direction"/> is set to <see cref="ParameterDirection.Input"/> by default.</br></summary>
        public DatabaseParameter()
        {
            Direction = ParameterDirection.Input;
            Value = new object();
        }

        /// <summary>
        /// Creates an instance of <see cref="DatabaseParameter"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <returns>an instance of <see cref="DatabaseParameter"/>.</returns>
        public static DatabaseParameter Create(string name, object value, DatabaseParameterType? type = null, ParameterDirection direction = ParameterDirection.Input, int? size = null)
        {
            return new DatabaseParameter()
            {
                Name = name,
                Type = type,
                Value = value,
                Direction = direction,
                Size = size
            };
        }
    }
}
