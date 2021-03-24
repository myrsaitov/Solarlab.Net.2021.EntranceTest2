using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WidePictBoard.Infrastructure.Migrations
{
    public partial class Category_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCategoryId_",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "787181ac-e80a-459b-a93c-bc0bd96abe62");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "d7166739-328c-419e-8594-e3d768b30697");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64957651-1e2d-405e-95f7-ba3e1b76418c", "AQAAAAEAACcQAAAAEDsarASvWP9gNXURUPWDyzE+pLdBrPlx8RtIUrjX8SqZIg2/wWdODp/o47tZqROxtA==", "bd8321f1-003e-4d80-b505-5f36e4ee3ee5" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 3, 24, 6, 25, 17, 443, DateTimeKind.Utc).AddTicks(4009), "Транспорт" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 3, 24, 6, 25, 17, 443, DateTimeKind.Utc).AddTicks(4460), "Недвижимость" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 3, 24, 6, 25, 17, 443, DateTimeKind.Utc).AddTicks(4463), "Мебель" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentCategoryId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, new DateTime(2021, 3, 24, 6, 25, 17, 443, DateTimeKind.Utc).AddTicks(4464), "Одежда", null, 0, null },
                    { 5, new DateTime(2021, 3, 24, 6, 25, 17, 443, DateTimeKind.Utc).AddTicks(4465), "Бытовая техника", null, 0, null },
                    { 6, new DateTime(2021, 3, 24, 6, 25, 17, 443, DateTimeKind.Utc).AddTicks(4466), "Книги", null, 0, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 3, 23, 22, 32, 24, 214, DateTimeKind.Utc).AddTicks(4827), "Автомобили" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 3, 23, 22, 32, 24, 214, DateTimeKind.Utc).AddTicks(5511), "Велосипеды" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 3, 23, 22, 32, 24, 214, DateTimeKind.Utc).AddTicks(5515), "Самокаты" });
        }
    }
}
