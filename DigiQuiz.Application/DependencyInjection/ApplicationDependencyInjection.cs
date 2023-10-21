using DigiQuiz.Application.ApiServices.Commands;
using DigiQuiz.Application.ApiServices.Queries;
using DigiQuiz.Application.ApiServices.Responses;
using DigiQuiz.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DigiQuiz.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IGetDigimonsServiceHandler, GetDigimonsServiceHandler>()
            .AddTransient<IPostDigimonServiceHandler, PostDigimonServiceHandler>();
        return services;
    }
}