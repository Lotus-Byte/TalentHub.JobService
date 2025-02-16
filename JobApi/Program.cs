using JobApi.Infrastructure.Swagger;
using JobBll.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using static System.Net.Mime.MediaTypeNames;

namespace JobApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // External services
        builder.Services.AddJobBllService(builder.Configuration);

        // Internal services
        builder.Services.AddControllers();
        builder.Services.AddSwaggerConfiguration();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        // app.UseAuthorization();


        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    // using static System.Net.Mime.MediaTypeNames;
                    context.Response.ContentType = Text.Plain;

                    await context.Response.WriteAsync("An exception was thrown.");

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();
                });
            });

            app.UseHsts();
        }

        app.Run();
    }
}
