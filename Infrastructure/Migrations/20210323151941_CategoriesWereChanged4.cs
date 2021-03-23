using Microsoft.EntityFrameworkCore.Migrations;

namespace WidePictBoard.Infrastructure.Migrations
{
    public partial class CategoriesWereChanged4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "aba15912-5562-4608-9ac7-b5f4bf714695");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "562e2e08-cfd8-4583-beb8-ee9ac72bfb06");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "221fdb91-a3d7-48b6-9b26-c2d8e6e3184c", "AQAAAAEAACcQAAAAEIh68B8JRjCW/Ib9S1QglQciHYDR0gmFoe0pCybmerv/kDTU3MdFe+fNZehs4l7QIw==", "986ba1df-5f97-4568-b9ff-40e8f7072dc5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Categories");

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
    }
}
