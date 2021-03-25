using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WidePictBoard.Infrastructure.Migrations
{
    public partial class CategoriesWereAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "f5019d67-f3cf-46c9-aa49-aad5d6d989ad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "7df67fd7-1914-429c-8c90-9b3b4a785e4b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d504488-ba47-427d-9baa-e20d2f7a11f4", "AQAAAAEAACcQAAAAEH89OxzwGgnqrSlltFCc3HZyoJP/X0gQhMfHW9WvXR0SdxoYbAhJVJibn1ENCFxXZA==", "864e04e7-e7f8-4a33-b670-9193c52fb5ce" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 3, 25, 9, 41, 14, 456, DateTimeKind.Utc).AddTicks(2723), "Транспорт" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentCategoryId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 2, new DateTime(2021, 3, 25, 9, 41, 14, 456, DateTimeKind.Utc).AddTicks(3505), "Недвижимость", null, "InUse", null },
                    { 3, new DateTime(2021, 3, 25, 9, 41, 14, 456, DateTimeKind.Utc).AddTicks(3545), "Мебель", null, "InUse", null },
                    { 4, new DateTime(2021, 3, 25, 9, 41, 14, 456, DateTimeKind.Utc).AddTicks(3547), "Одежда", null, "InUse", null },
                    { 5, new DateTime(2021, 3, 25, 9, 41, 14, 456, DateTimeKind.Utc).AddTicks(3548), "Бытовая техника", null, "InUse", null },
                    { 6, new DateTime(2021, 3, 25, 9, 41, 14, 456, DateTimeKind.Utc).AddTicks(3549), "Книги", null, "InUse", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ContentId",
                table: "Comment",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ContentId",
                table: "Tag",
                column: "ContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "c8d75b22-df9a-4834-845d-72258d0e4736");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "cb0d50bb-1a0d-4b97-99b8-8945ed7bbd35");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "151ad33f-3173-4722-ae55-e74cf2090c25", "AQAAAAEAACcQAAAAEP44bLoL7WEtCl2TBnFfwyEUALlUilYsu/f+syHOfxfs6sdWjb5eFNDiA9ciRe8BLw==", "a18560a5-1313-45a0-ac47-5763ad940183" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Автомобили" });
        }
    }
}
