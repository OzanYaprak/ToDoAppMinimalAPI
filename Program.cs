
using ToDoAppMinimalAPI.Configuration;

namespace ToDoAppMinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCustomCors(); // Custom CORS Middleware 
            builder.Services.AddCustomSwagger(); // Custom Swagger Middleware

            var app = builder.Build();




            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x=>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoAppMinimalAPI v1");
                    x.DocumentTitle = "ToDoAppMinimalAPI Documentation";
                    x.DisplayRequestDuration();
                    x.DefaultModelsExpandDepth(-1);
                });
            }

            app.UseCustomExceptionHandler(); // Custom Exception Handler Middleware
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
