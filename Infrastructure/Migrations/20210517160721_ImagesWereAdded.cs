using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SL2021.Infrastructure.Migrations
{
    public partial class ImagesWereAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "a0658ad1-2417-4b89-a244-1751ce082eb6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "405587b9-9ebc-4963-bae2-08c90f6d1b07");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1ed8a05-ebc2-4b8a-a3a5-29db27307554", "AQAAAAEAACcQAAAAEH4hmZBci9PVPiXVEvO1+nyBPAgYVaWVZhcnzb6bu8IL0C1SnuZbHumNskcOO18nmA==", "d36da77f-fdc8-49f6-a53b-3267e194d680" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 17, 16, 7, 20, 551, DateTimeKind.Utc).AddTicks(5993));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 17, 16, 7, 20, 551, DateTimeKind.Utc).AddTicks(7155));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 17, 16, 7, 20, 551, DateTimeKind.Utc).AddTicks(7159));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 17, 16, 7, 20, 551, DateTimeKind.Utc).AddTicks(7161));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 17, 16, 7, 20, 551, DateTimeKind.Utc).AddTicks(7162));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 17, 16, 7, 20, 551, DateTimeKind.Utc).AddTicks(7163));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "724022c8-08ef-48c4-b697-ef02fd026186");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "ae51265a-abc7-44c3-afe3-5953f9cbf979");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "336f0db1-9a8f-4bf3-b64e-3c4c88a3b905", "AQAAAAEAACcQAAAAEAARzaAqfJTSIQ3gfROqMLuZqBPIv5B8O9YqdMfvMz4OkTKqd+pLgwlF9cPdWftY/Q==", "1f1087fa-4ec5-4c55-a6c5-873432d770aa" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 13, 9, 59, 3, 97, DateTimeKind.Utc).AddTicks(442));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 13, 9, 59, 3, 97, DateTimeKind.Utc).AddTicks(1258));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 13, 9, 59, 3, 97, DateTimeKind.Utc).AddTicks(1261));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 13, 9, 59, 3, 97, DateTimeKind.Utc).AddTicks(1262));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 13, 9, 59, 3, 97, DateTimeKind.Utc).AddTicks(1264));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 13, 9, 59, 3, 97, DateTimeKind.Utc).AddTicks(1265));
        }
    }
}
