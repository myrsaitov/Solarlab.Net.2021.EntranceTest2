using Microsoft.EntityFrameworkCore.Migrations;

namespace WidePictBoard.Infrastructure.Migrations
{
    public partial class AddIdentity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "4203ca41-693c-49fb-b576-a8c798408ef2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "81957380-475c-4b8d-bb89-f113ebbe654d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f8773b9-6d03-4f59-86b4-ebfe03484365", "AQAAAAEAACcQAAAAEHLuIVyqLz8imBr7BQeLvtfIFozA7XeWKzu/Yh2oZ1leuuws2pakc5HH772q1TYZqg==", "0a98876e-c893-4495-92fc-46f44aab7548" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "56393bab-066f-439a-b3b6-513dcdb2a2a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "af60dcc1-ddf2-4d56-bf32-c34c3e5dbd1d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "868a4f37-2244-4552-aef2-47e9bbf15031", "AQAAAAEAACcQAAAAEMdRg0VK/DJgjoTaCwp0LpEv6kYiurFMQV+VgXfo/+g+kgtljPejql7dRHBPY+zEUA==", "91831f4c-dde8-4806-a829-809c3e76eadd" });
        }
    }
}
