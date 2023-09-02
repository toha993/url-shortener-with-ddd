using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using UrlShortenerWithDDD.Application.Helpers;
using UrlShortenerWithDDD.Application.Services;

namespace UrlShortenerWithDDD.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IUrlService, UrlService>();
        services.AddTransient<IRandomCodeGenerator, RandomCodeGenerator>();
        return services;
    }
}