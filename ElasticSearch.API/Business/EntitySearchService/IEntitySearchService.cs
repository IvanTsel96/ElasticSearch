using ElasticSearch.API.Business.EntityService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.API.Business.EntitySearchService
{
    public interface IEntitySearchService
    {
        Task Index();
        Task IndexById(int id);

        Task<List<EntityResponse>> Search(string searchPhrase);
    }
}
