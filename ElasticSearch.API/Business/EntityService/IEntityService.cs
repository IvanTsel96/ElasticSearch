using ElasticSearch.API.Business.EntityService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.API.Business.EntityService
{
    public interface IEntityService
    {
        Task<List<EntityResponse>> Get();
        Task<EntityResponse> GetById(int id);
        Task Save(EntityRequest request);
        Task Update(int id, EntityRequest request);
        Task Delete(int id);
    }
}
