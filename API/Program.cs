using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Domain;
using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSeeding((context, _) =>
           {
               // Resolve RoleManager and UserManager from the service provider
               var roleManager = context.GetService<RoleManager<IdentityRole>>();
               var userManager = context.GetService<UserManager<User>>();

               // Seed roles if none exist
               if (!context.Set<IdentityRole>().Any())
               {
                   var roles = new[] { "Admin", "Staff", "Customer" };
                   foreach (var role in roles)
                   {
                       roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                   }
                   context.SaveChanges();
               }

               // Seed users with roles if none exist
               if (!context.Set<User>().Any())
               {
                   var usersWithRoles = new[]
                   {
                       new { Email = "admin@gmail.com", Role = "admin" },
                       new { Email = "customer@gmail.com", Role = "customer" }
                   };

                   var hasher = new PasswordHasher<User>();
                   foreach (var userInfo in usersWithRoles)
                   {
                       var user = new User
                       {
                           Email = userInfo.Email,
                           UserName = userInfo.Email,
                           PasswordHash = hasher.HashPassword(null, "P@ssword")
                       };

                       // Create the user
                       context.Set<User>().Add(user);
                       context.SaveChanges();

                       // Assign the role to the user
                       userManager.AddToRoleAsync(user, userInfo.Role).GetAwaiter().GetResult();
                   }
               }
           })
           .UseAsyncSeeding(async (context, _, cancellationToken) =>
           {
               // Resolve RoleManager and UserManager from the service provider
               var roleManager = context.GetService<RoleManager<IdentityRole>>();
               var userManager = context.GetService<UserManager<User>>();

               // Seed roles if none exist (async)
               if (!await context.Set<IdentityRole>().AnyAsync(cancellationToken))
               {
                   var roles = new[] { "Admin", "Staff", "Customer" };
                   foreach (var role in roles)
                   {
                       await roleManager.CreateAsync(new IdentityRole(role));
                   }
                   await context.SaveChangesAsync(cancellationToken);
               }

               // Seed users with roles if none exist (async)
               if (!await context.Set<User>().AnyAsync(cancellationToken))
               {
                   var usersWithRoles = new[]
                   {
                       new { Email = "admin@gmail.com", Role = "admin" },
                       new { Email = "customer@gmail.com", Role = "customer" }
                   };

                   var hasher = new PasswordHasher<User>();
                   foreach (var userInfo in usersWithRoles)
                   {
                       var user = new User
                       {
                           Email = userInfo.Email,
                           UserName = userInfo.Email,
                           PasswordHash = hasher.HashPassword(null, "P@ssword")
                       };

                       // Create the user
                       context.Set<User>().Add(user);
                       await context.SaveChangesAsync(cancellationToken);

                       // Assign the role to the user
                       await userManager.AddToRoleAsync(user, userInfo.Role);
                   }
               }
           });
});

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication().AddGoogle(options =>
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            //options.CallbackPath = "/signin-google";
        });

builder.Services.AddScoped<IProductService,ProductService >();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

app.UseRouting();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapGet("/api/account/login/google", ([FromQuery] string returnUrl, LinkGenerator linkGenerator, SignInManager<User> SignInManager, HttpContext context ) =>
//{

app.Run();
