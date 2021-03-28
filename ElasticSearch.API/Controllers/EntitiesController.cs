using ElasticSearch.API.Business.EntitySearchService;
using ElasticSearch.API.Business.EntityService;
using ElasticSearch.API.Business.EntityService.Dtos;
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
        private readonly IEntitySearchService _entitySearchService;

        public EntitiesController(
            IEntityService entityService,
            IEntitySearchService entitySearchService)
        {
            _entityService = entityService;
            _entitySearchService = entitySearchService;
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
        public Task<List<EntityResponse>> Search([FromQuery] string searchPhrase)
        {
            return _entitySearchService.Search(searchPhrase);
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
