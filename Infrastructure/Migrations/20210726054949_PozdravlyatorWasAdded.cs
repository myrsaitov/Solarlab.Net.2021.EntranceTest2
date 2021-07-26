using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SL2021.Infrastructure.Migrations
{
    public partial class PozdravlyatorWasAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "Contents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "f241b10a-5ad0-41d7-b325-6f15faf2437e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "cecb3ad0-6f5c-46d9-8094-8ab03ac02575");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bca30efe-2309-4367-a022-10312c3807eb", "AQAAAAEAACcQAAAAEPLP4dCIXfKmBU0B7coRO9BlyyXzdNMS4WhK5uFILw4K4/kgoSj8xvFnR6xnJOsiNg==", "cc746adc-e19e-43c5-96fd-43ab5e060906" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 7, 26, 5, 49, 49, 459, DateTimeKind.Utc).AddTicks(9842), "Дни рождения" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 7, 26, 5, 49, 49, 460, DateTimeKind.Utc).AddTicks(656), "Дни свадьбы" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 7, 26, 5, 49, 49, 460, DateTimeKind.Utc).AddTicks(659), "Именины" });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_PersonId",
                table: "Contents",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_DomainUsers_PersonId",
                table: "Contents",
                column: "PersonId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_DomainUsers_PersonId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_PersonId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Contents");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Contents",
                type: "money",
                nullable: false,
                defaultValue: 0m);

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
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(5506), "Транспорт" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6388), "Недвижимость" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6391), "Мебель" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6392), false, "Одежда", null, null },
                    { 5, new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6393), false, "Бытовая техника", null, null },
                    { 6, new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6394), false, "Книги", null, null }
                });
        }
    }
}
