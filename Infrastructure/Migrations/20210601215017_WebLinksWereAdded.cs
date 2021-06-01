using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SL2021.Infrastructure.Migrations
{
    public partial class WebLinksWereAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSearched = table.Column<bool>(type: "bit", nullable: false),
                    ReferedCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebLinks", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "5a18005a-a39d-456c-a3b8-49b8dc2ebc5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "4ada91bb-526f-43b5-abd3-3a28cff8b0fc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46306b52-b1ab-40b4-b70f-d8055066b7b4", "AQAAAAEAACcQAAAAENzdsn1BPjMLLSHZU3JjxYWIafEXZ9ew8ZzNtm/4n9twYRmI8Azp/7vMctU0YxQrGg==", "c9ac9241-f0a8-4eb7-8c35-904ab6db1d72" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 21, 50, 16, 809, DateTimeKind.Utc).AddTicks(2245));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 21, 50, 16, 809, DateTimeKind.Utc).AddTicks(3029));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 21, 50, 16, 809, DateTimeKind.Utc).AddTicks(3031));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 21, 50, 16, 809, DateTimeKind.Utc).AddTicks(3032));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 21, 50, 16, 809, DateTimeKind.Utc).AddTicks(3034));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 21, 50, 16, 809, DateTimeKind.Utc).AddTicks(3034));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebLinks");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "56c8f988-d952-4db3-8eff-758c0eb51d48");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "4489e755-db2d-4c3d-a780-63de74a3e098");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f4ef4f0-9564-4b41-b82a-2e154bb18f50", "AQAAAAEAACcQAAAAEOM3cBGXFFVRnCb93NA3wBeQon3GxhblBe/jR7qVZvSJ2Jr9lSUL4/aaZYtYB/rwSw==", "07ea4636-a8ff-4bcb-b21e-32db6da00d42" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(5506));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6388));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6391));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6392));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6393));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6394));
        }
    }
}
