using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Personaltool.Migrations
{
    public partial class CareerLevelDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CareerLevels",
                columns: table => new
                {
                    CareerLevelID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ShortName = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerLevels", x => x.CareerLevelID);
                });

            migrationBuilder.CreateTable(
                name: "PersonsCareerLevels",
                columns: table => new
                {
                    PersonsCareerLevelID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonID = table.Column<int>(nullable: false),
                    CareerLevelID = table.Column<int>(nullable: false),
                    Begin = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsCareerLevels", x => x.PersonsCareerLevelID);
                    table.ForeignKey(
                        name: "FK_PersonsCareerLevels_CareerLevels_CareerLevelID",
                        column: x => x.CareerLevelID,
                        principalTable: "CareerLevels",
                        principalColumn: "CareerLevelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonsCareerLevels_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonsCareerLevels_CareerLevelID",
                table: "PersonsCareerLevels",
                column: "CareerLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsCareerLevels_PersonID",
                table: "PersonsCareerLevels",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonsCareerLevels");

            migrationBuilder.DropTable(
                name: "CareerLevels");
        }
    }
}
