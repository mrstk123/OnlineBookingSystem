﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineBookingSystem.Server;

#nullable disable

namespace OnlineBookingSystem.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240719070736_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BookingNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RentalEndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RentalRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RentalStartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"));

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<string>("CarBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Transmission")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "admin@mail.com",
                            FirstName = "admin",
                            LastName = "",
                            PasswordHash = new byte[] { 158, 2, 221, 18, 185, 47, 162, 199, 33, 139, 191, 244, 99, 127, 77, 206, 23, 208, 79, 37, 171, 122, 111, 208, 92, 89, 240, 107, 79, 236, 108, 29, 109, 72, 46, 229, 140, 175, 41, 35, 40, 101, 166, 226, 96, 41, 155, 13, 191, 173, 180, 147, 218, 134, 232, 59, 123, 136, 44, 38, 139, 44, 16, 145 },
                            PasswordSalt = new byte[] { 243, 39, 118, 135, 47, 140, 88, 109, 49, 1, 132, 115, 185, 31, 181, 38, 1, 45, 35, 131, 194, 65, 210, 144, 241, 163, 64, 148, 181, 72, 167, 15, 194, 171, 101, 168, 224, 227, 206, 227, 234, 156, 28, 79, 67, 37, 234, 5, 138, 19, 252, 111, 88, 141, 147, 124, 64, 82, 187, 169, 250, 218, 89, 221, 16, 97, 142, 148, 250, 245, 254, 23, 21, 202, 105, 92, 134, 14, 199, 136, 155, 161, 188, 105, 67, 242, 87, 3, 224, 124, 145, 117, 218, 49, 176, 85, 243, 201, 28, 5, 37, 136, 156, 187, 90, 153, 244, 77, 6, 129, 126, 189, 32, 96, 132, 3, 166, 82, 174, 248, 27, 107, 154, 233, 138, 159, 175, 233 },
                            Phone = "",
                            UserName = "admin"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "user@mail.com",
                            FirstName = "user",
                            LastName = "",
                            PasswordHash = new byte[] { 70, 200, 61, 29, 220, 5, 134, 139, 127, 56, 226, 52, 71, 120, 173, 188, 28, 159, 15, 251, 159, 65, 251, 82, 88, 7, 98, 81, 154, 53, 238, 186, 40, 151, 67, 107, 70, 57, 38, 70, 241, 77, 36, 193, 64, 84, 221, 107, 167, 21, 82, 88, 8, 230, 165, 116, 232, 180, 228, 236, 125, 27, 3, 156 },
                            PasswordSalt = new byte[] { 227, 167, 187, 244, 130, 121, 13, 41, 255, 160, 170, 70, 62, 41, 134, 51, 195, 36, 241, 154, 242, 240, 141, 75, 34, 54, 80, 132, 26, 155, 165, 39, 183, 248, 34, 207, 47, 96, 193, 137, 27, 113, 113, 138, 242, 184, 13, 252, 18, 206, 3, 167, 11, 191, 229, 197, 69, 118, 28, 14, 67, 230, 152, 250, 244, 215, 125, 57, 40, 142, 68, 49, 166, 207, 76, 251, 0, 58, 150, 3, 79, 103, 141, 235, 18, 117, 227, 204, 241, 241, 127, 15, 114, 44, 95, 255, 152, 84, 142, 97, 219, 24, 14, 44, 13, 122, 211, 205, 161, 158, 188, 120, 89, 121, 26, 93, 23, 28, 76, 217, 255, 218, 6, 99, 120, 120, 250, 41 },
                            Phone = "",
                            UserName = "user"
                        });
                });

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.Booking", b =>
                {
                    b.HasOne("OnlineBookingSystem.Server.Models.Car", "Car")
                        .WithMany("Bookings")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineBookingSystem.Server.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.UserRole", b =>
                {
                    b.HasOne("OnlineBookingSystem.Server.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineBookingSystem.Server.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.Car", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("OnlineBookingSystem.Server.Models.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
