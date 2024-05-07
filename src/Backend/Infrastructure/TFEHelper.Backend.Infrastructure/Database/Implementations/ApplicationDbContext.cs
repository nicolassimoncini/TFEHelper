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

            return new SqliteParameter(parameter.Name, parameter.Value);
        }
    }
}