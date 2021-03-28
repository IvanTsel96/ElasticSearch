using ElasticSearch.API.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.API.DAL.ElasticSearch
{
    public interface IElasticSearchProvider
    {
        Task IndexDocument<T>(T document) where T : class;
        Task IndexDocuments<T>(IList<T> document) where T : class;

        Task<List<Entity>> Search(string searchPhrase);
    }
}
