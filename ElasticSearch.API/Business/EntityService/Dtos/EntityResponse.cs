using System;

namespace ElasticSearch.API.Business.EntityService.Dtos
{
    public class EntityResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
