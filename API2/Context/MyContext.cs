using API2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Profilling> Profilling { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profilling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profilling>(b => b.NIK);

            modelBuilder.Entity<Education>()
                .HasOne(c => c.University)
                .WithMany(e => e.Educations);

            modelBuilder.Entity<Education>()
               .HasOne(a => a.Profilling)
               .WithOne(b => b.Education)
               .HasForeignKey<Profilling>(b => b.Education_Id);


        }
    }
}
