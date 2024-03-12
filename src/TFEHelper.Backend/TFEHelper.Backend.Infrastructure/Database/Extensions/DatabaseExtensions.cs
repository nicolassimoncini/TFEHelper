using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Infrastructure.Database.Extensions
{
    internal static class DatabaseExtensions
    {
        internal static SqliteType ToSqliteType(this DatabaseParameterType paramType)
        {
            return paramType switch
            {
                DatabaseParameterType.Int => SqliteType.Integer,
                DatabaseParameterType.Real => SqliteType.Real,
                DatabaseParameterType.String => SqliteType.Text,
                DatabaseParameterType.Boolean => SqliteType.Integer,
                DatabaseParameterType.DateTime => SqliteType.Real,
                _ => throw new ArgumentException($"Invalid DatabaseParameterType argument", paramType.ToString()),
            };
        }
    }
}
