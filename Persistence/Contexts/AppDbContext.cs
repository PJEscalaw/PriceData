using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<PriceData> PriceData { get; set; }
        public DbSet<Result> Result { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory;
            dbContextOptionsBuilder.UseSqlite($"Data Source={dbPath}\\PriceData.db");
        }
    }
}
