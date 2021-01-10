using UserManagementSystem.DAL.Entities;
using System;
using UserManagementSystem.DAL.Helpers;
using Microsoft.EntityFrameworkCore;

namespace UserManagementSystem.DAL.Context
{
    public class UserManagementSystemContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserManagementSystemContext(DbContextOptions<UserManagementSystemContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasData(
                new User 
                {
                    Id = Guid.Parse("E0306550-C717-4FD0-98DA-08D8548AA120"),
                    Email = "admin@gmail.com",
                    Username = "admin",
                    Password = "Qwerty123",
                    FullName = "Admin Admin",
                    IsBlackListed = false,
                    Role = UserRole.Admin
                },
                new User
                {
                    Id = Guid.Parse("A21BCB79-67A8-426A-98D6-08D8548AA120"),
                    Email = "georgi@gmail.com",
                    Username = "georgi",
                    Password = "Qwerty123",
                    FullName = "Georgi Gerdzhikov",
                    IsBlackListed = false,
                    Role = UserRole.User
                },
                new User
                {
                    Id = Guid.Parse("CA0A6DD8-518E-4BA9-98D9-08D8548AA120"),
                    Email = "mariya@gmail.com",
                    Username = "mariya",
                    Password = "Qwerty123",
                    FullName = "Mariya Ivanova",
                    IsBlackListed = false,
                    Role = UserRole.User
                });
        }
    }
}
