using ApplicationCore.Application.Commons;
using ApplicationCore.Application.Exceptions;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Infrastructure.Memory;
using Scalar.AspNetCore;
using WebApi.Handlers;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddSingleton(typeof(IGenericRepositoryAsync<>), typeof(MemoryGenericRepositoryAsync<>));
        builder.Services.AddSingleton<IMovieServiceAsync, MovieServiceAsync>();
        builder.Services.AddControllers(o =>
            {
                o.RespectBrowserAcceptHeader = true;
                o.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson()
            .AddXmlSerializerFormatters();
        builder.Services.AddOpenApi();
        //builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();      
        builder.Services.AddProblemDetails();
        var app = builder.Build();
        //app.UseExceptionHandler();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(o =>
            {
                o.Theme = ScalarTheme.Mars;
            });
        }
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.MapControllers();
        app.Seed();
        app.Run();
    }
}