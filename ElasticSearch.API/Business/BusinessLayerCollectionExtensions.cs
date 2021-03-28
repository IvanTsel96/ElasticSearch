using ElasticSearch.API.Business.EntitySearchService;
using ElasticSearch.API.Business.EntityService;
using ElasticSearch.API.Business.EntityService.Mapping;
using ElasticSearch.API.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElasticSearch.API.Business
{
    public static class BusinessLayerCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEntitySearchService, EntitySearchService.EntitySearchService>();
            services.AddScoped<IEntityService, EntityService.EntityService>();

            services.AddAutoMapper(typeof(EntityProfile))
                    .AddDal(configuration);

            return services;
        }
    }
}
