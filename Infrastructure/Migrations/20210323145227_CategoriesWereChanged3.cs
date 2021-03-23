using Microsoft.EntityFrameworkCore.Migrations;

namespace WidePictBoard.Infrastructure.Migrations
{
    public partial class CategoriesWereChanged3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "2b7247fb-16b3-414a-aa14-55275e8cc23c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "36e6b379-5b79-4296-8fc3-e62e26654e6b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2516d8b-05c8-4a3a-a09b-944cf06a9b85", "AQAAAAEAACcQAAAAEP4ednvG56TfQHbMwVSM4/oy1DBlc6keEaZ2Lw099Dz9Qu63poWxvfaZ8FkzhW0o5A==", "17e00348-63c8-47a5-a748-b25d5669b70b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "b4e1f029-6fcb-4500-b41e-c666e0f9ef72");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "769b7468-2aac-469d-840a-154a311162c4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27879a8f-29db-4e3e-b774-0454476588f3", "AQAAAAEAACcQAAAAEOJhrIC9/dCnHEAIXKMb/iLKQ75TXdXYWxVJ6uv5YF6LjDnqr9AA5U+sXYbtfiIJVQ==", "a15d6151-cbf8-4f91-9a22-398e93a1fa08" });
        }
    }
}
