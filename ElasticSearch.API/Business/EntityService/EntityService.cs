using AutoMapper;
using ElasticSearch.API.DAL.EntityRepository;
using ElasticSearch.API.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch.API.Business.EntityService.Dtos;

namespace ElasticSearch.API.Business.EntityService
{
    public class EntityService : IEntityService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IMapper _mapper;

        public EntityService(
            IEntityRepository entityRepository,
            IMapper mapper)
        {
            _entityRepository = entityRepository;
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

        public Task Save(EntityRequest request)
        {
            var entity = _mapper.Map<Entity>(request);

            return _entityRepository.Save(entity);
        }

        public Task Update(int id, EntityRequest request)
        {
            var entity = new Entity { Id = id };

            _mapper.Map(request, entity);

            return _entityRepository.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _entityRepository.GetById(id);

            await _entityRepository.Delete(entity);
        }
    }
}
