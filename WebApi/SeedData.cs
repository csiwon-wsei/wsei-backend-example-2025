

using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.Models;
using Infrastructer.Memory;

namespace WebApi;

public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var movieRepo = provider.GetService<IGenericRepositoryAsync<Movie>>();
            var userRepo = provider.GetService<IGenericRepositoryAsync<User>>();
            InitData.Init(movieRepo, userRepo);            
        }
    }
}