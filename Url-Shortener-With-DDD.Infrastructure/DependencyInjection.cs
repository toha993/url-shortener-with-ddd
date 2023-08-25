using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortenerWithDDD.Application.Interfaces;
using UrlShortenerWithDDD.Infrastructure.Data.Database;

namespace UrlShortenerWithDDD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10),
                        null);
                    builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
                }), ServiceLifetime.Transient);
        
        services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>() ?? throw new InvalidOperationException());
        return services;

    }
    
}