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
                new Category { CategoryId = 1, Name = "Shirt" },
                new Category { CategoryId = 2, Name = "Coat" },
                new Category { CategoryId = 3, Name = "Pant" },
                new Category { CategoryId = 4, Name = "Sportswear" }
            );

            List<IdentityRole> roles = new()
            {
                new IdentityRole
                {
                    Id = Guid.Parse("fff5caad-d740-48f7-abdc-03ae0635c08b").ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN".ToUpper()
                },
                new IdentityRole
                {
                    Id = Guid.Parse("be3e451c-0914-443c-897e-cba2eb45b564").ToString(),
                    Name = "Staff",
                    NormalizedName = "MANAGER".ToUpper()
                },
                new IdentityRole
                {
                    Id = Guid.Parse("6bc135f7-455c-4b04-b301-f32642221dea").ToString(),
                    Name = "Customer",
                    NormalizedName = "CUSTOMER".ToUpper()
                }
            };

            //modelBuilder.Entity<IdentityRole>().HasData(roles);


            //var hasher= new PasswordHasher<IdentityUser>();
            //modelBuilder.Entity<IdentityUser>().HasData(
            //new IdentityUser
            //{
            //    Id = Guid.Parse("c28305c3-93f5-4490-ae59-05d0401bcee3").ToString(),
            //    UserName = "admin@gmail.com",
            //    NormalizedUserName = "admin@gmail.com".ToUpper(),
            //    Email = "admin@gmail.com",
            //    NormalizedEmail = "ADMIN@GMAIL.COM".ToUpper(),
            //    PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
            //},
            //new IdentityUser
            //{
            //    Id = Guid.Parse("c28305c3-93f5-4490-ae59-05d0401bcee4").ToString(),
            //    UserName = "user@gmail.com",
            //    NormalizedUserName = "USER@GMAIL.COM".ToUpper(),
            //    Email = "user@gmail.com",
            //    NormalizedEmail = "USER@GMAIL.COM".ToUpper(),
            //    PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
            //});

            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(

            //    new IdentityUserRole<string>
            //    {
            //        UserId = Guid.Parse("c28305c3-93f5-4490-ae59-05d0401bcee3").ToString(),
            //        RoleId = Guid.Parse("fff5caad-d740-48f7-abdc-03ae0635c08b").ToString()
            //    },
            //    new IdentityUserRole<string>
            //    {
            //        UserId = Guid.Parse("c28305c3-93f5-4490-ae59-05d0401bcee4").ToString(),
            //        RoleId = Guid.Parse("6bc135f7-455c-4b04-b301-f32642221dea").ToString()
            //    }
            //);
        }

    }
}
