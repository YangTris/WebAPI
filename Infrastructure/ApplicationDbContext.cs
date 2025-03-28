using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Numerics;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronics" },
                new Category { CategoryId = 2, Name = "Clothing" },
                new Category { CategoryId = 3, Name = "Books" }
            );

            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "fff5caad-d740-48f7-abdc-03ae0635c08b",
                Name = "Admin",
                NormalizedName = "ADMIN".ToUpper()
            },
            new IdentityRole
            {
                Id = "be3e451c-0914-443c-897e-cba2eb45b564",
                Name = "Staff",
                NormalizedName = "MANAGER".ToUpper()
            },
            new IdentityRole
            {
                Id = "6bc135f7-455c-4b04-b301-f32642221dea",
                Name = "Customer",
                NormalizedName = "CUSTOMER".ToUpper()
            }
            );

            var hasher= new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "c28305c3-93f5-4490-ae59-05d0401bcee3",
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
            },
            new IdentityUser
            {
                Id = "c28305c3-93f5-4490-ae59-05d0401bcee4",
                UserName = "user@gmail.com",
                NormalizedUserName = "USER@GMAIL.COM".ToUpper(),
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string>
                {
                    UserId = "c28305c3-93f5-4490-ae59-05d0401bcee3",
                    RoleId = "fff5caad-d740-48f7-abdc-03ae0635c08b"
                },
                new IdentityUserRole<string>
                {
                    UserId = "c28305c3-93f5-4490-ae59-05d0401bcee3",
                    RoleId = "be3e451c-0914-443c-897e-cba2eb45b564"
                }
            );
        }

    }
}
