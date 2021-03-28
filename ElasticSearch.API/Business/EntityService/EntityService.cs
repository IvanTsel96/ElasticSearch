using AutoMapper;
using ElasticSearch.API.Business.EntityService.Dtos;
using ElasticSearch.API.DAL.ElasticSearch;
using ElasticSearch.API.DAL.EntityRepository;
using ElasticSearch.API.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.API.Business.EntityService
{
    public class EntityService : IEntityService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IElasticSearchProvider _elasticSearchProvider;
        private readonly IMapper _mapper;

        public EntityService(
            IEntityRepository entityRepository,
            IElasticSearchProvider elasticSearchProvider,
            IMapper mapper)
        {
            _entityRepository = entityRepository;
            _elasticSearchProvider = elasticSearchProvider;
            _mapper = mapper;
        }

        public async Task<List<EntityResponse>> Get()
        {
            var entities = await _entityRepository.Get();

            return _mapper.Map<List<EntityResponse>>(entities);
        }

        public async Task<EntityResponse> GetById(int id)
        {
            var entity = await _entityRepository.GetById(id);

            return _mapper.Map<EntityResponse>(entity);
        }

        public async Task Save(EntityRequest request)
        {
            var entity = _mapper.Map<Entity>(request);

            await _entityRepository.Save(entity);

            await _elasticSearchProvider.IndexDocument(entity);
        }

        public async Task Update(int id, EntityRequest request)
        {
            var entity = new Entity { Id = id };

            _mapper.Map(request, entity);

            await _entityRepository.Update(entity);

            await _elasticSearchProvider.IndexDocument(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _entityRepository.GetById(id);

            await _elasticSearchProvider.RemoveDocument<Entity>(id);

            await _entityRepository.Delete(entity);
        }
    }
}
