using Microsoft.Extensions.DependencyInjection;

namespace UrlShortenerWithDDD.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}