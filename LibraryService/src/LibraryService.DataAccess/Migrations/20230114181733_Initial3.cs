using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySevice.DataAccess.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicationDate",
                value: new DateTime(2023, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicationDate",
                value: new DateTime(2023, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicationDate",
                value: new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicationDate",
                value: new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
