using Microsoft.EntityFrameworkCore.Migrations;

namespace AutosApi.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarcaName",
                table: "marcas");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "marcas",
                newName: "MarcaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "autos",
                newName: "AutoId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "marcas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "marcas");

            migrationBuilder.RenameColumn(
                name: "MarcaId",
                table: "marcas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AutoId",
                table: "autos",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "MarcaName",
                table: "marcas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
