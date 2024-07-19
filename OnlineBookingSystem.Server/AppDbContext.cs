using Microsoft.EntityFrameworkCore;
using OnlineBookingSystem.Server.Models;
using System.Text;

namespace OnlineBookingSystem.Server
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Car
            modelBuilder.Entity<Car>().HasKey(e => e.CarId);
            modelBuilder.Entity<Car>().Property(e => e.CarId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Car>()
                .Property(p => p.PricePerDay)
                .HasColumnType("decimal(18,2)");

            // Booking
            modelBuilder.Entity<Booking>().HasKey(e => e.BookingId);
            modelBuilder.Entity<Booking>().Property(e => e.BookingId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Booking>()
                .Property(p => p.RentalRate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Booking>()
                .Property(p => p.TotalCost)
                .HasColumnType("decimal(18,2)");

            // Define relationships
            modelBuilder.Entity<Booking>()
                .HasOne(e => e.User)
                .WithMany(c => c.Bookings)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(e => e.Car)
                 .WithMany(c => c.Bookings)
                .HasForeignKey(e => e.CarId)
                .OnDelete(DeleteBehavior.Cascade);


            // User
            modelBuilder.Entity<User>().HasKey(e => e.UserId);
            modelBuilder.Entity<User>().Property(e => e.UserId).ValueGeneratedOnAdd();

            // Role
            modelBuilder.Entity<Role>().HasKey(e => e.RoleId);
            modelBuilder.Entity<Role>().Property(e => e.RoleId).ValueGeneratedOnAdd();

            // UserRole
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


            // Seed data for Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "User" }
            );

            // Seed data for Users
            // Create password hashes and salts for default users

            byte[] adminPasswordHash, adminPasswordSalt;
            CreatePasswordHash("admin123", out adminPasswordHash, out adminPasswordSalt);

            byte[] userPasswordHash, userPasswordSalt;
            CreatePasswordHash("user123", out userPasswordHash, out userPasswordSalt);

            // Seed data for users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "admin",
                    PasswordHash = adminPasswordHash,
                    PasswordSalt = adminPasswordSalt,
                    FirstName = "admin",
                    LastName = "",
                    Email = "admin@mail.com",
                    Phone = ""
                },
                new User
                {
                    UserId = 2,
                    UserName = "user",
                    PasswordHash = userPasswordHash,
                    PasswordSalt = userPasswordSalt,
                    FirstName = "user",
                    LastName = "",
                    Email = "user@mail.com",
                    Phone = ""
                }
            );


            // Seed data for UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 }, // Admin user as Admin
                new UserRole { UserId = 2, RoleId = 2 }  // User as User
            );


            base.OnModelCreating(modelBuilder);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
