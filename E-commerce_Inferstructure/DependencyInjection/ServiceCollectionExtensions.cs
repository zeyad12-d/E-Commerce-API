using E_commerce_Core.Entityes;
using E_commerce_Core.MappingProfile;
using E_commerce_Core.UnitOfWork;
using E_commerce_Inferstructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace E_commerce_Inferstructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDBcontext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            // Ensure the required Identity services are registered
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBcontext>()
                .AddDefaultTokenProviders();

            return services;
        }
        public static IServiceCollection AddCorsService(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, builder =>
                {
                    builder.AllowAnyOrigin();// add ui url
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            return services;

        }

        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();


            string[] roleNames = { "Admin", "Customer", "Vendor" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            var adminEmail = "admin@system.com";
            var adminUserName = "admin";
            var adminPassword = "Admin@123"; 

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Failed to create admin user: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }

        public static void addCustomJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                options.RequireHttpsMetadata = false; // Set to true in production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                };


            });




        }
        public static void addUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<UnitOfWork>();

        }
        public static void addAtuomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));

        }
    }
}