using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ElasticSearch.API.DAL.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "current_timestamp")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entities", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "entities",
                columns: new[] { "id", "create_date", "description", "name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 3, 28, 23, 39, 15, 700, DateTimeKind.Utc).AddTicks(2986), "Атомобиль марки Honda. Два хозяина. Состояние хорошее.", "Автомобиль" },
                    { 2, new DateTime(2021, 3, 28, 23, 39, 15, 700, DateTimeKind.Utc).AddTicks(3333), "Наручные часы фирмы Rolex.", "Наручные часы" },
                    { 3, new DateTime(2021, 3, 28, 23, 39, 15, 700, DateTimeKind.Utc).AddTicks(3342), "Жидкость для снятия лака. Объем 0.7 мл.", "Жидкоть для снятия лака" },
                    { 4, new DateTime(2021, 3, 28, 23, 39, 15, 700, DateTimeKind.Utc).AddTicks(3343), "Щетка двухсторонняя для чистки от снега и наледи", "Щетка для атомобиля" },
                    { 5, new DateTime(2021, 3, 28, 23, 39, 15, 700, DateTimeKind.Utc).AddTicks(3345), "Лак бесцветный. Объем 0.2 мл.", "Лак бесцветны" },
                    { 6, new DateTime(2021, 3, 28, 23, 39, 15, 700, DateTimeKind.Utc).AddTicks(3348), "Часы наручные с электронным циферблатом.", "Электронные наручные часы" },
                    { 7, new DateTime(2021, 3, 28, 23, 39, 15, 700, DateTimeKind.Utc).AddTicks(3350), "Кожанный ремешок для наручных часов", "Ремешок для часов" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entities");
        }
    }
}
