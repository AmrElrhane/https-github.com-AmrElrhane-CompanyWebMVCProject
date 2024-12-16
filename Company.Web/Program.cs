using Company.Data.Contexts;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Repository;
using Company.Service.Interfaces;
using Company.Service.Mapping;
using Company.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CompanyDbContext>(option =>
                {
                    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });

            //builder.Services.AddScoped<IDepartmentReopsitory, DepartmentRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();


            builder.Services.AddAutoMapper(x => x.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(x => x.AddProfile(new DepartmentProfile()));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                config =>
                    {
                        config.Password.RequiredUniqueChars = 2;
                        config.Password.RequireDigit = true;
                        config.Password.RequireUppercase = true;
                        config.Password.RequireLowercase = true;
                        config.Password.RequireNonAlphanumeric = true;
                        config.Password.RequiredLength = 6;
                        config.User.RequireUniqueEmail = true;
                        config.Lockout.AllowedForNewUsers = true;
                        config.Lockout.MaxFailedAccessAttempts = 3;
                        config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);

                    }
                ).AddEntityFrameworkStores<CompanyDbContext>()
                    .AddDefaultTokenProviders();
            // el token el 7aga el bn7thgha fel authorization;

            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.HttpOnly = true; // cross site from HTTP only
                option.Cookie.SecurePolicy = CookieSecurePolicy.Always; // https only 
                option.ExpireTimeSpan = TimeSpan.FromMinutes(60);// hto3d 2d eh 
                option.SlidingExpiration = true;  // per request =>reset
                option.LoginPath = "/Account/Login";
                option.LogoutPath = "/Account/Logout";
                option.AccessDeniedPath = "/Account/AccessDenied";
                option.Cookie.Name = "AmCookie";
                option.Cookie.SameSite = SameSiteMode.Strict; // mfrsh cookie hyege mn bra 
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}");

            app.Run();
        }
    }
}
