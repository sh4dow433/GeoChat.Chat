using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // TODO: register UnitOfWork

    }
}

