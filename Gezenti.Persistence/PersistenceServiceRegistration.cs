using Gezenti.Application.Services;
using Gezenti.Application.Services.Repositories;
using Gezenti.Persistence.Contexts.EntityFramwork;
using Gezenti.Persistence.Contexts.Mongo;
using Gezenti.Persistence.Jwt;
using Gezenti.Persistence.Repositories;
using Gezenti.Persistence.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.EntityFrameworkCore.Extensions;

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

            // MongoDB DbContext Configuration
            var mongoUri = configuration.GetConnectionString("MongoDbConnection");
            var mongoDbName = configuration.GetSection("ConnectionStrings:MongoDbName").Value;

            if (!string.IsNullOrEmpty(mongoUri) && !string.IsNullOrEmpty(mongoDbName))
            {
                services.AddDbContext<GezentiMongoDbContext>(options =>
                    options.UseMongoDB(mongoUri, mongoDbName));
            }

            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<IPlaceService, EfPlaceRepository>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<IMailService, MailManager>();
            
            // MongoDB Repository
            services.AddScoped<IUserActivityRepository, MongoUserActivityRepository>();

            return services;
        }
    }
}
