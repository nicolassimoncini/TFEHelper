using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.Database.Specifications;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Infrastructure.Database.Extensions;

namespace TFEHelper.Backend.Infrastructure.Database.Implementations
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Publication> Publications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbParameter CreateDbParameter(IDatabaseParameter parameter)
        {
            if (parameter.Direction != ParameterDirection.Input)
                throw new ArgumentException($"Invalid parameter direction type for {parameter.Name}.  SQLite provider only supports input parameter type.");

            var param = new SqliteParameter(parameter.Name, parameter.Value)
            {
                SqliteType = parameter.Type.GetValueOrDefault().ToSqliteType()
            };
            if (parameter.Size != null) param.Size = parameter.Size.GetValueOrDefault();
            return param;
        }
    }
}
