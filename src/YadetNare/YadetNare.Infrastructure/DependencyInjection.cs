using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YadetNare.Infrastructure.Common.Persistence;

namespace YadetNare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(x =>
            x.UseSqlServer(Environment.GetEnvironmentVariable("Yadet_Nare_ConnectionString") ??
                           throw new InvalidOperationException("Add Yadet_Nare_ConnectionString Environment Variable")));
        return services;
    }

}