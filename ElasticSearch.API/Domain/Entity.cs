using System;

namespace ElasticSearch.API.Domain
{
    public class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
