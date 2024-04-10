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
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Infrastructure.Database.Extensions;

namespace TFEHelper.Backend.Infrastructure.Database.Implementations
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
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
#warning investigar por qué al debuggear, la base de datos se busca en la raíz del proyecto TFEHelper.Backend.API! (hint: https://stackoverflow.com/a/65093101/14042736)
#warning otro hint (medio pedorro): https://stackoverflow.com/questions/49569055/store-entity-framework-core-sqlite-file-in-project-relative-subdirectory
