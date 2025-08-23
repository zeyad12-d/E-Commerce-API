using E_commerc_Servers.Services;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.SendEmails;
using E_commerce_Inferstructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace E_Commerce_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(o=>o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "E-Commerce API",
                    Version = "v1",
                    Description = "API for E-Commerce application",
                    Contact = new OpenApiContact {
                    
                        Email="zeadyasser054@gmail.com",
                        Name="Eng:Zeyad"


                    }


                });
                op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name= "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat= "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT Key",

                });
                op.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{

                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In= ParameterLocation.Header

                        },
                        new List<string>{}
                    }
                });
            });

            // configure CORS, Identity, and JWT authentication,
            builder.Services
                .AddDbContextService(builder.Configuration)
                .AddIdentityService()
                .AddCorsService("CoresPolicy")
                .addCustomJWT(builder.Configuration);

            builder.Services.Configure<EmailSettings>(
                builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()).addUnitOfWork();
              builder.Services.AddScoped<IProductServices,ProductServices>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddScoped<IAddressServices, AddressServices>();
            builder.Services.AddScoped<ICartServices,ShoppingCartServices>();

            builder.Services.AddScoped<IEmailService, EmailService>();


            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseCors("CoresPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
         

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // Initialize the database and roles
                    await E_commerce_Inferstructure.DependencyInjection.ServiceCollectionExtensions.InitializeAsync(services);
                }
                catch (Exception ex)
                {
                    // Handle exceptions during initializ
                    Console.WriteLine($"An error occurred while initializing the application: {ex.Message}");
                }
              ;
            }
            app.Run();
        }
    }
}
