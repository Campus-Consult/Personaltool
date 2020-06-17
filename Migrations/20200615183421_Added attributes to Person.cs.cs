using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Personaltool.Migrations
{
    public partial class AddedattributestoPersoncs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Persons_PersonID",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Person");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Person",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Person",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdressCity",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdressNr",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdressStreet",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdressZIP",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Person",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmailAssociaton",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailPrivate",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Person",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MobilePrivate",
                table: "Person",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "AdressCity",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "AdressNr",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "AdressStreet",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "AdressZIP",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "EmailAssociaton",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "EmailPrivate",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "MobilePrivate",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Persons",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Persons",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Persons_PersonID",
                table: "AspNetUsers",
                column: "PersonID",
                principalTable: "Persons",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
