using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WidePictBoard.Infrastructure.Migrations
{
    public partial class CommentsWereAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "0028aa66-b44c-4b0d-9148-2df1207f824a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "28301b7e-4e8a-402c-b744-6ea8c234c121");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8ad3755-fffd-4218-b280-7ac63ac932c7", "AQAAAAEAACcQAAAAEOUN5UeRglTquiO/EhQ5YuVjq4y74iHzXN5uKkzLiFqy72VR2ZSquEl2UN8CN1v9WA==", "4f503a9a-fd7f-4d09-b42b-e11c4cc0327c" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 20, 56, 57, 946, DateTimeKind.Utc).AddTicks(5542));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 20, 56, 57, 946, DateTimeKind.Utc).AddTicks(6516));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 20, 56, 57, 946, DateTimeKind.Utc).AddTicks(6520));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 20, 56, 57, 946, DateTimeKind.Utc).AddTicks(6522));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 20, 56, 57, 946, DateTimeKind.Utc).AddTicks(6523));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 20, 56, 57, 946, DateTimeKind.Utc).AddTicks(6524));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Comment");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TagText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "377e4fab-3c86-449d-9226-c7aff9ee8ee9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "c6d756ac-ae8c-45f5-bb06-6e9c3abf1a5d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54c3181b-e33f-4f7b-a830-f14484491240", "AQAAAAEAACcQAAAAEIBvqFFGuDbB4q0rM2BxGvG/obcg3h1k0e5J+qpLaczxy1lppFli4jJ44H8ZsV0IvA==", "03e9c671-00ce-462f-ac2a-ec0b94bac207" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 14, 5, 47, 356, DateTimeKind.Utc).AddTicks(5824));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 14, 5, 47, 356, DateTimeKind.Utc).AddTicks(6631));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 14, 5, 47, 356, DateTimeKind.Utc).AddTicks(6635));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 14, 5, 47, 356, DateTimeKind.Utc).AddTicks(6636));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 14, 5, 47, 356, DateTimeKind.Utc).AddTicks(6637));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 25, 14, 5, 47, 356, DateTimeKind.Utc).AddTicks(6638));

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ContentId",
                table: "Tag",
                column: "ContentId");
        }
    }
}
