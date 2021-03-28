using ElasticSearch.API.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.API.DAL.EntityRepository
{
    public interface IEntityRepository
    {
        Task<List<Entity>> Get();
        Task<Entity> GetById(int id);
        Task<List<Entity>> GetByIds(IList<int> ids);
        Task Save(Entity entity);
        Task Update(Entity entity);
        Task Delete(Entity entity);
    }
}
