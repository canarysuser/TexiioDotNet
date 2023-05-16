using FirstWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Infrastructure
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext( DbContextOptions<UsersDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Define a composite Primary key on the UserRoles table 
            modelBuilder.Entity<UserRole>().HasKey("UserId", "RoleId");

            //Initial Seed data for the User, Roles and UserRoles tables 
            modelBuilder.Seed();
        }
    }


    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Username = "admin",
                    Password = "admin",
                    EmailId = "admin@website.com"
                },
                new User { UserId=2, FirstName="User2", LastName="User2", Username="user2", Password="user2", EmailId="email"}
                );
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Administrator" },
                new Role { RoleId = 2, RoleName = "Operators" });
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { RoleId = 1, UserId = 1 },
                new UserRole { RoleId = 2, UserId = 2 }
                );
        }
    }
}
