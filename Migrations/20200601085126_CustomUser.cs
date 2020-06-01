using Microsoft.EntityFrameworkCore.Migrations;

namespace Personaltool.Migrations
{
    public partial class CustomUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonID",
                table: "AspNetUsers",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Person_PersonID",
                table: "AspNetUsers",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Person_PersonID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "AspNetUsers");
        }
    }
}
