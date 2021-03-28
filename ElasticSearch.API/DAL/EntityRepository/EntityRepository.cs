using ElasticSearch.API.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.API.DAL.EntityRepository
{
    public class EntityRepository : IEntityRepository
    {
        private readonly DatabaseContext _context;

        public EntityRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Task<List<Entity>> Get()
        {
            return _context.Entities.ToListAsync();
        }

        public Task<Entity> GetById(int id)
        {
            return _context.Entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Entity>> GetByIds(IList<int> ids)
        {
            return _context.Entities.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public Task Save(Entity entity)
        {
            _context.Entities.Add(entity);

            return _context.SaveChangesAsync();
        }

        public Task Update(Entity entity)
        {
            _context.Set<Entity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return _context.SaveChangesAsync();
        }

        public Task Delete(Entity entity)
        {
            _context.Entities.Remove(entity);

            return _context.SaveChangesAsync();
        }

    }
}
