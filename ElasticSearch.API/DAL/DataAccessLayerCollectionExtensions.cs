using ElasticSearch.API.DAL.ElasticSearch;
using ElasticSearch.API.DAL.EntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace ElasticSearch.API.DAL
{
    public static class DataAccessLayerCollectionExtensions
    {
        public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IElasticSearchProvider, ElasticSearchProvider>();
            services.AddScoped<IEntityRepository, EntityRepository.EntityRepository>();

            services.AddPostgreSql(configuration)
                    .AddElasticSearch(configuration);

            return services;
        }

        private static IServiceCollection AddPostgreSql(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSql");

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(connectionString)
                       .UseSnakeCaseNamingConvention();
            });

            return services;
        }

        private static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ElasticSearch");
            var elasticSearchUri = new Uri(connectionString);
            var connectionSettings = new ConnectionSettings(elasticSearchUri);

            var client = new ElasticClient(connectionSettings);

            services.AddSingleton<IElasticClient>(client);

            return services;
        }
    }
}
