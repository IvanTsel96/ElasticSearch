using AutoMapper;
using ElasticSearch.API.Business.EntityService.Dtos;
using ElasticSearch.API.Domain;

namespace ElasticSearch.API.Business.EntityService.Mapping
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<EntityRequest, Entity>();
            CreateMap<Entity, EntityResponse>();
        }
    }
}
