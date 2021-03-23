using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WidePictBoard.Infrastructure.Migrations
{
    public partial class Category_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "fc08dfda-b4c9-4106-864b-64d9daa648e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "a393283d-2fa5-433d-b19a-a5b338aa8d18");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6069f89a-1bed-43b4-9933-44437277e127", "AQAAAAEAACcQAAAAEGJOfVSqJfVuCf1jwiqRMh8/4PQ4P0j05znX1jpBH03enhjxlhyBhOugv6UM0GKbwQ==", "b03f5f32-fba0-4ca8-a96f-95698338e3e3" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
