using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Helpers;
using Server.Interfaces;

namespace Server.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<AppealContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("AppealConnection"));
            });

            services.AddDbContext<PorticoContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("PorticoConnection"));
            });

            return services;
        }
    }
}