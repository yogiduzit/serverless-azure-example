using System;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public ApplicationDbContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(Config.Get("DefaultConnection"));

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>().HasData(
              new
              {
                  StudentId = Guid.NewGuid().ToString(),
                  FirstName = "Jane",
                  LastName = "Smith",
                  School = "Medicine"
              }, new
              {
                  StudentId = Guid.NewGuid().ToString(),
                  FirstName = "John",
                  LastName = "Fisher",
                  School = "Engineering"
              }, new
              {
                  StudentId = Guid.NewGuid().ToString(),
                  FirstName = "Pamela",
                  LastName = "Baker",
                  School = "Food Science"
              }, new
              {
                  StudentId = Guid.NewGuid().ToString(),
                  FirstName = "Peter",
                  LastName = "Taylor",
                  School = "Mining"
              }
            );
        }
    }
}