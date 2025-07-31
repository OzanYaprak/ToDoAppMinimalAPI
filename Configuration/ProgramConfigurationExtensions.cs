using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using ToDoAppMinimalAPI.Entities;
using ToDoAppMinimalAPI.Exceptions.BaseExceptions;
using ToDoAppMinimalAPI.Interfaces;
using ToDoAppMinimalAPI.Repositories;
using ToDoAppMinimalAPI.Services;
using ToDoAppMinimalAPI.Validators;

namespace ToDoAppMinimalAPI.Configuration
{
    public static class ProgramConfigurationExtensions
    {
        public static void UseCustomExceptionHandler(this WebApplication app) 
        {
            app.UseExceptionHandler((appError =>
            {
                appError.Run(async (context) => 
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Varsayılan olarak yanıt durum kodunu 500 Internal Server Error olarak ayarlıyor.
                    context.Response.ContentType = "application/json"; // Varsayılan olarak yanıt içeriğinin JSON formatında olduğunu belirtiyor.

                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound, // Custom Exception
                            BadRequestException => StatusCodes.Status400BadRequest, // Custom Exception
                            NoContentException => StatusCodes.Status204NoContent, // Custom Exception

                            ArgumentOutOfRangeException => StatusCodes.Status400BadRequest, 
                            KeyNotFoundException => StatusCodes.Status404NotFound, 
                            ArgumentException => StatusCodes.Status400BadRequest, 
                            ValidationException => StatusCodes.Status422UnprocessableEntity,
                            _ => StatusCodes.Status500InternalServerError, // Diğer tüm durumlarda 500 Internal Server Error döndürüyoruz.
                        };

                        await context.Response.WriteAsJsonAsync((new ErrorDetails
                        {
                            ErrorDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                            Message = contextFeature.Error.Message,
                            StatusCode = context.Response.StatusCode
                        }).ToString()); // ToString metodu, ErrorDetails sınıfını JSON formatında serileştirir. // Bu sayede hata detaylarını JSON formatında döndürürüz.
                    }
                });
            }));
        }
        public static IServiceCollection AddCustomCors(this IServiceCollection services) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
                options.AddPolicy("AllowSpecificOrigins", builder =>  { builder.WithOrigins("https://localhost:7206", "http://localhost:5116").AllowAnyMethod().AllowAnyHeader().AllowCredentials(); });
            });

            return services;
        }
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services) 
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ToDoAppMinimalAPI",
                    Version = "v1",
                    Description = "A simple example ASP.NET Core Minimal Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Ozan Yaprak",
                        Email = "oznyprk@gmail.com",
                        Url = new Uri("https://github.com/OzanYaprak")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/license/mit/")
                    },
                    TermsOfService = new Uri("https://www.google.com.tr")
                });
                //x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                //{
                //    In = ParameterLocation.Header,
                //    Description = "JWT Authorization header using the Bearer scheme.",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});
                //x.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            }
                //        },
                //        new string[] {}
                //    }
                //});
            });
            return services;
        }
        public static IServiceCollection CustomIocRegisters(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            services.AddScoped<CustomValidator>(); // Custom Validator for DTOs
            return services;
        }
        public static IServiceCollection UseSqlServerContext(this IServiceCollection services, IConfiguration configuration)
        {
            {
                services.AddDbContext<ToDoAppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
                return services;
            }
        }
        public static IServiceCollection RepositoryIocRegisters(this IServiceCollection services) 
        {
            services.AddScoped<ToDoAppRepository>(); 

            return services; 
        }
        public static IServiceCollection ServicesIocRegisters(this IServiceCollection services) 
        {
            services.AddScoped<IToDoAppService, ToDoAppService>(); 

            return services; 
        }
    }
}
