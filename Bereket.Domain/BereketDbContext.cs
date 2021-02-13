using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Bereket.Domain.Entity;

namespace Bereket.Domain
{

    public class BereketDbContextFactory: IDesignTimeDbContextFactory<BereketDbContext>{

        internal static string ConnectionString =
            "User ID=postgres;Password=P@ssw0rd;Server=localhost;Port=5432;Database=bereket;Integrated Security=true;Pooling=true;";
        public BereketDbContext CreateDbContext(string[] args){
            var optionsBuilder = new DbContextOptionsBuilder<BereketDbContext>();
            optionsBuilder.UseNpgsql(ConnectionString);
            return new BereketDbContext(optionsBuilder.Options);
        }
    }

   public class BereketDbContext:DbContext
    {
        public BereketDbContext(DbContextOptions ctx):base(ctx){

        } 

        public DbSet<Product> Products{get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder){
            if(!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseNpgsql(BereketDbContextFactory.ConnectionString);
            }
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder ModelBuilder){
            // ModelBuilder.Entity<Product>().HasKey(e=>e.Id);
        }
    }
}
