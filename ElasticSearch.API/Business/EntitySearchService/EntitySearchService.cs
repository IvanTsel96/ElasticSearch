using AutoMapper;
using ElasticSearch.API.Business.EntityService.Dtos;
using ElasticSearch.API.DAL.ElasticSearch;
using ElasticSearch.API.DAL.EntityRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.API.Business.EntitySearchService
{
    public class EntitySearchService : IEntitySearchService
    {
        private readonly IElasticSearchProvider _elasticSearchProvider;
        private readonly IEntityRepository _entityRepository;
        private readonly IMapper _mapper;

        public EntitySearchService(
            IElasticSearchProvider elasticSearchProvider,
            IEntityRepository entityRepository,
            IMapper mapper)
        {
            _elasticSearchProvider = elasticSearchProvider;
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        public async Task Index()
        {
            var entities = await _entityRepository.Get();

            await _elasticSearchProvider.IndexDocuments(entities);
        }

        public async Task IndexById(int id)
        {
            var entity = await _entityRepository.GetById(id);

            await _elasticSearchProvider.IndexDocument(entity);
        }

        public async Task<List<EntityResponse>> Search(string searchPhrase)
        {
            var entityIds = await _elasticSearchProvider.Search(searchPhrase);

            var entities = (await _entityRepository.GetByIds(entityIds))
                .OrderBy(x => entityIds.IndexOf(x.Id))
                .ToList();

            return _mapper.Map<List<EntityResponse>>(entities);
        }
    }
}
