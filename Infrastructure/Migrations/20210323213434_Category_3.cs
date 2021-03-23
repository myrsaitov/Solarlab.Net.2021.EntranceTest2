using Microsoft.EntityFrameworkCore.Migrations;

namespace WidePictBoard.Infrastructure.Migrations
{
    public partial class Category_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId_",
                table: "Categories",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCategoryId_",
                table: "Categories");

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
    }
}
