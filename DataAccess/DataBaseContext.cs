
using Microsoft.EntityFrameworkCore;
using ProjectTracker_API.Models.Entities;
using System;

namespace ProjectTracker_API.DataAccess
{
    internal class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryUser> EntryUser { get; set; }
        public DbSet<ProjectRequest> ProjectRequests { get; set; }
        public DbSet<ProjectUser> ProjectUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbSource = Environment.GetEnvironmentVariable("Db_DatabaseSource");
            var userName = Environment.GetEnvironmentVariable("Db_DatabaseUserName");
            var password = Environment.GetEnvironmentVariable("Db_DatabasePassword");
            optionsBuilder.UseSqlServer($"Server={dbSource};Database=ProjectTracker;User Id={userName};Password={password};Trusted_Connection=False;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddRelations();
        }
    }

    public static class ModelBuilderExtensions
    {
        //public static void Seed(this ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<ExpenseType>().HasData( new User { Id = 1}, new User { Id = 2 });
        //}

        public static void AddRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>().HasOne(p => p.Project).WithMany().HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<ProjectUser>().HasOne(p => p.Project).WithMany().HasForeignKey(p => p.ProjectId);
            modelBuilder.Entity<ProjectUser>().HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);

            modelBuilder.Entity<ProjectRequest>().HasOne(p => p.Project).WithMany().HasForeignKey(p => p.ProjectId);
            modelBuilder.Entity<ProjectRequest>().HasOne(p => p.JoinerUser).WithMany().HasForeignKey(p => p.JoinerUserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProjectRequest>().HasOne(p => p.ApproverUser).WithMany().HasForeignKey(p => p.ApproverUserId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Entry>().HasOne(p => p.Project).WithMany().HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<EntryUser>().HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
            modelBuilder.Entity<EntryUser>().HasOne(p => p.Entry).WithMany().HasForeignKey(p => p.EntryId);
        }
    }
}