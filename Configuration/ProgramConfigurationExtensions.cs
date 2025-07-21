using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using ToDoAppMinimalAPI.Entities;
using ToDoAppMinimalAPI.Exceptions;

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
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    
                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound, // Custom Exception
                            BadRequestException => StatusCodes.Status400BadRequest, // Custom Exception

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
    }
}
