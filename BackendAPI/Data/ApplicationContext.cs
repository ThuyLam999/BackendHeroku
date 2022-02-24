using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BackendAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskModel> TaskModels { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });

            modelBuilder.Entity<Widget>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(e => e.Dashboard).WithMany(d => d.Widgets).HasForeignKey(d => d.DashboardId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Dashboard>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });

            modelBuilder.Entity<TaskModel>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(e => e.User).WithMany(d => d.RefreshTokens).HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
