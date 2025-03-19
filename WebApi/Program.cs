using ApplicationCore.Application.Commons;
using ApplicationCore.Application.Services;
using Infrastructer.Memory;
using Scalar.AspNetCore;

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
            o.ReturnHttpNotAcceptable = true;
            o.RespectBrowserAcceptHeader = true;
        }).AddXmlSerializerFormatters()
        .AddNewtonsoftJson();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173", "https://localhost:5173") // Dodaj adresy localhost, z których będą dozwolone żądania
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(o =>
            {
                o.Theme = ScalarTheme.Mars;
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors("AllowLocalhost");

        app.MapControllers();
        app.Seed();
        app.Run();
    }
}