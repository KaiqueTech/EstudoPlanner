using EstudoPlanner.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EstudoPlanner.BLL.Configurations;

public static class DatabaseConfigurationExtensions
{
    public static IServiceCollection AddDatabaConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}