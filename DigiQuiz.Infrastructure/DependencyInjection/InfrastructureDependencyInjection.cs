using DigiQuiz.Application.Interfaces;
using DigiQuiz.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiQuiz.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDigimonRepository, DigimonRepository>();
        services.AddHttpClient();
        return services;
    }
}