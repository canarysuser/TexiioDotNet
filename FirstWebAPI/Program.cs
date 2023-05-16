
using FirstWebAPI.Infrastructure;
using FirstWebAPI.Models;
using FirstWebApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FirstWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //extract the connectionstring from the appsettings.json file. 
            var northwindConnStr = builder.Configuration.GetConnectionString("NorthwindConnection");
            var authConnStr = builder.Configuration.GetConnectionString("AuthConnection");

            //Extract the custom configuration section - AppSettings 
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            //Configure the DbContext objects with the connection strings 
            builder.Services.AddDbContext<FirstWebAPI.Infrastructure.NorthwindContext>(options =>
            {
                options.UseSqlServer(connectionString: northwindConnStr);
            });
            builder.Services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(connectionString: authConnStr);
            });
            
            //Configure the DI Objects mapping the Interfaces to the Classes
            builder.Services.AddScoped<IAsyncRepository<Product, int>, ProductEFRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            //configure the JWT bearer token services 
            //Dont have to configure as we will be providing our own class implementation 


            //rest of service configuration 
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(policy=>
            {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            // app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}