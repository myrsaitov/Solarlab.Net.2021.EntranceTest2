using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WidePictBoard.Infrastructure.Migrations
{
    public partial class Category_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "c0ac29f7-6b62-4b91-b8d9-86d776b0d39b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "de78b348-fa45-46c4-94fd-8533b5ed623c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4457a705-d4dd-4164-965b-44340badf061", "AQAAAAEAACcQAAAAEHNIg3Mb940IBoTUaxMv85aK+WWByA+EaSSg160ykXDc56I2ygy+V3tw/ZamBqGGtw==", "da2bc004-9e8d-4b51-9ec0-7fa6263780ed" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 22, 32, 24, 214, DateTimeKind.Utc).AddTicks(4827));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 22, 32, 24, 214, DateTimeKind.Utc).AddTicks(5511));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 22, 32, 24, 214, DateTimeKind.Utc).AddTicks(5515));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "f6eb8fe2-f16a-4ad5-9262-7539b478ea4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "2c944110-a080-4bee-abca-857a72b6210a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1838d16f-ac14-4918-8183-e1043e4d9f27", "AQAAAAEAACcQAAAAEPVmS683TT6LXK5WuTOrv6EdXFeRNXRTEEWclanHm2e7V4ZWBAOg52wj99LRDNwBRw==", "49298b7b-dd33-46ab-9674-e8a055557d22" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 22, 1, 11, 27, DateTimeKind.Utc).AddTicks(1655));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 22, 1, 11, 27, DateTimeKind.Utc).AddTicks(2778));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 22, 1, 11, 27, DateTimeKind.Utc).AddTicks(2790));
        }
    }
}
