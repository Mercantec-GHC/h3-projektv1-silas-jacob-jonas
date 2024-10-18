
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add 

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDataProtection().UseCryptographicAlgorithms(
            new AuthenticatedEncryptorConfiguration
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            });

            IConfiguration Configuration = builder.Configuration;
            var a = Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DefaultConnection");
            var b = Configuration["JwtSettings:Issuer"] ?? Environment.GetEnvironmentVariable("Issuer");
            var c = Configuration["JwtSettings:Audience"] ?? Environment.GetEnvironmentVariable("Audience");
            var d = Configuration["JwtSettings:Key"] ?? Environment.GetEnvironmentVariable("Key");
            
            Console.WriteLine("CONNECTION: " + a);
            Console.WriteLine("Issuer: " + b);
            Console.WriteLine("Audience: " + c);
            Console.WriteLine("Key: " + d);


            string connectionString = Configuration.GetConnectionString("DefaultConnection")
                                        ?? Environment.GetEnvironmentVariable("DefaultConnection");

            builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseNpgsql(connectionString));

            // Configure JWT Authentication
            builder.Services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item =>
            {
                item.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["JwtSettings:Issuer"]
                                    ?? Environment.GetEnvironmentVariable("Issuer"),
                    ValidAudience = Configuration["JwtSettings:Audience"]
                                      ?? Environment.GetEnvironmentVariable("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Key"] ?? Environment.GetEnvironmentVariable("Key"))),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            var app = builder.Build();

            // TODO: LAV S� DEN GIVER RIGTIG HEADER ISTEDET FOR AT TILLADE ALT
            // Allows all headers for CORS
            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

                app.MapScalarApiReference(options =>
                {   
                    options
                        .WithDefaultHttpClient(ScalarTarget.C, ScalarClient.Libcurl)
                        .OpenApiRoutePattern = "/swagger/v1/swagger.json";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}
