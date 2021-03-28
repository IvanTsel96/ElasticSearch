using ElasticSearch.API.Business.EntityService;
using ElasticSearch.API.DAL.ElasticSearch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElasticSearch.API.Controllers
{
    [ApiController]
    [Route("entities/index")]
    public class EntitiesIndexingController
    {
        private readonly IEntityService _entityService;
        private readonly IElasticSearchProvider _elasticSearchProvider;

        public EntitiesIndexingController(
            IEntityService entityService,
            IElasticSearchProvider elasticSearchProvider)
        {
            _entityService = entityService;
            _elasticSearchProvider = elasticSearchProvider;
        }

        [HttpPost]
        public async Task Index()
        {
            var entities = await _entityService.Get();

            await _elasticSearchProvider.IndexDocuments(entities);
        }

        [HttpPost("{id}")]
        public async Task IndexById([FromRoute] int id)
        {
            var entity = await _entityService.GetById(id);

            await _elasticSearchProvider.IndexDocument(entity);
        }
    }
}
