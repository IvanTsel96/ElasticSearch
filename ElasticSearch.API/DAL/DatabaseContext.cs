using ElasticSearch.API.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ElasticSearch.API.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity>().ToTable("entities").HasKey(x => x.Id);
            modelBuilder.Entity<Entity>().Property(x => x.CreateDate).HasDefaultValueSql("current_timestamp");

            modelBuilder.Entity<Entity>().HasData(GetDefaultData());
        }

        private static List<Entity> GetDefaultData()
        {
            return new List<Entity>
            {
                new Entity{Id = 1, Name="Автомобиль", Description = "Атомобиль марки Honda. Два хозяина. Состояние хорошее.", CreateDate = DateTime.UtcNow },
                new Entity{Id = 2, Name="Наручные часы", Description = "Наручные часы фирмы Rolex.", CreateDate = DateTime.UtcNow },
                new Entity{Id = 3, Name="Жидкоть для снятия лака", Description = "Жидкость для снятия лака. Объем 0.7 мл.", CreateDate = DateTime.UtcNow },
                new Entity{Id = 4, Name="Щетка для атомобиля", Description = "Щетка двухсторонняя для чистки от снега и наледи", CreateDate = DateTime.UtcNow },
                new Entity{Id = 5, Name="Лак бесцветны", Description = "Лак бесцветный. Объем 0.2 мл.", CreateDate = DateTime.UtcNow },
                new Entity{Id = 6, Name="Электронные наручные часы", Description = "Часы наручные с электронным циферблатом.", CreateDate = DateTime.UtcNow },
                new Entity{Id = 7, Name="Ремешок для часов", Description = "Кожанный ремешок для наручных часов", CreateDate = DateTime.UtcNow },
            };
        }
    }
}
