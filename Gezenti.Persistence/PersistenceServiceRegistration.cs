using Gezenti.Application.Services;
using Gezenti.Application.Services.Repositories;
using Gezenti.Persistence.Contexts;
using Gezenti.Persistence.Jwt;
using Gezenti.Persistence.Repositories;
using Gezenti.Persistence.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gezenti.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<GezentiDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<IPlaceService, EfPlaceRepository>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<IMailService, MailManager>();

            return services;
        }
    }
}
