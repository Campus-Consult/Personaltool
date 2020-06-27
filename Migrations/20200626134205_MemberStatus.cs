using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Personaltool.Migrations
{
    public partial class MemberStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberStatus",
                columns: table => new
                {
                    MemberStatusID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberStatus", x => x.MemberStatusID);
                });

            migrationBuilder.CreateTable(
                name: "PersonsMemberStatus",
                columns: table => new
                {
                    PersonsMemberStatusID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonID = table.Column<int>(nullable: false),
                    MemberStatusID = table.Column<int>(nullable: false),
                    Begin = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsMemberStatus", x => x.PersonsMemberStatusID);
                    table.ForeignKey(
                        name: "FK_PersonsMemberStatus_MemberStatus_MemberStatusID",
                        column: x => x.MemberStatusID,
                        principalTable: "MemberStatus",
                        principalColumn: "MemberStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonsMemberStatus_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonsMemberStatus_MemberStatusID",
                table: "PersonsMemberStatus",
                column: "MemberStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsMemberStatus_PersonID",
                table: "PersonsMemberStatus",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonsMemberStatus");

            migrationBuilder.DropTable(
                name: "MemberStatus");
        }
    }
}
