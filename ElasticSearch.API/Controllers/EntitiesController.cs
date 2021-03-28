using ElasticSearch.API.Business.EntityService;
using ElasticSearch.API.Business.EntityService.Dtos;
using ElasticSearch.API.DAL.ElasticSearch;
using ElasticSearch.API.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.API.Controllers
{
    [ApiController]
    [Route("entities")]
    public class EntitiesController : ControllerBase
    {
        private readonly IEntityService _entityService;
        private readonly IElasticSearchProvider _elasticSearchProvider;

        public EntitiesController(
            IEntityService entityService,
            IElasticSearchProvider elasticSearchProvider)
        {
            _entityService = entityService;
            _elasticSearchProvider = elasticSearchProvider;
        }

        [HttpGet]
        public Task<List<EntityResponse>> Get()
        {
            return _entityService.Get();
        }

        [HttpGet("{id}")]
        public Task<EntityResponse> GetById([FromRoute] int id)
        {
            return _entityService.GetById(id);
        }

        [HttpGet("search")]
        public Task<List<Entity>> Search([FromQuery] string searchPhrase)
        {
            return _elasticSearchProvider.Search(searchPhrase);
        }

        [HttpPost]
        public Task Save([FromBody] EntityRequest entity)
        {
            return _entityService.Save(entity);
        }

        [HttpPut("{id}")]
        public Task Update([FromRoute] int id, [FromBody] EntityRequest entity)
        {
            return _entityService.Update(id, entity);
        }

        [HttpDelete("{id}")]
        public Task Delete([FromRoute] int id)
        {
            return _entityService.Delete(id);
        }
    }
}
