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


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>( options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            .UseSeeding((context, _) =>
            {
                //var userManager = context.GetService<UserManager<IdentityUser>>();
                //var roleManager = context.GetService<RoleManager<IdentityRole>>();
                
                if (context.Set<IdentityRole>().FirstOrDefault(r => r.NormalizedName == "Admin") == null)
                {
                    context.Set<IdentityRole>().Add(new IdentityRole("Admin"));
                    context.SaveChanges();
                }

                if (context.Set<IdentityUser>().FirstOrDefault(r => r.Email == "admin@gmail.com") == null)
                {
                    context.Set<IdentityUser>().Add(new IdentityUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" });
                    context.SaveChanges();

                    //await userManager.CreateAsync(adminUser, "P@ssw0rd");
                    //await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            });
});

//builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

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

app.MapIdentityApi<IdentityUser>();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapGet("/api/account/login/google", ([FromQuery] string returnUrl, LinkGenerator linkGenerator, SignInManager<User> SignInManager, HttpContext context ) =>
//{

//});
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles= new[] { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
