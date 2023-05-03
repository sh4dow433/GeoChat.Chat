using GeoChat.Chat.Core.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoChat.Chat.Infra.DbAccess;

public static class ServiceCollectionExtensions
{
    public static void RegisterDbAndRepos(this IServiceCollection services, IConfiguration configuration)
    {
        // register db context
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ChatDb"));
        });

        // register UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}

