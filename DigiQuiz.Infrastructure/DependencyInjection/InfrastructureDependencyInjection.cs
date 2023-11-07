using DigiQuiz.Application.Interfaces;
using DigiQuiz.Infrastructure.Data.DbContexts;
using DigiQuiz.Infrastructure.Repositories;
using DigiQuiz.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiQuiz.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDigimonRepository, DigimonRepository>()
            .AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddHttpClient();
        var connectionstring = configuration.GetConnectionString("DatabaseConnection");
        services.AddDbContext<PlayerDbContext>(opt => opt.UseSqlServer(connectionstring));
        return services;
    }
}