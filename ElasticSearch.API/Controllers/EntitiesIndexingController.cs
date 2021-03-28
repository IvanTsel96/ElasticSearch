using ElasticSearch.API.Business.EntitySearchService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElasticSearch.API.Controllers
{
    [ApiController]
    [Route("entities/index")]
    public class EntitiesIndexingController
    {
        private readonly IEntitySearchService _entitySearchService;

        public EntitiesIndexingController(IEntitySearchService elasticSearchProvider)
        {
            _entitySearchService = elasticSearchProvider;
        }

        [HttpPost]
        public Task Index()
        {
            return _entitySearchService.Index();
        }

        [HttpPost("{id}")]
        public Task IndexById([FromRoute] int id)
        {
            return _entitySearchService.IndexById(id);
        }
    }
}
