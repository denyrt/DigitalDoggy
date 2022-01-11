using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalDoggy.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DogEntities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TailLength = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogEntities", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DogEntities");
        }
    }
}
