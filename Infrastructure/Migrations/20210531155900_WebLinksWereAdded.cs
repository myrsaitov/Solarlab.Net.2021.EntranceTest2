using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SL2021.Infrastructure.Migrations
{
    public partial class WebLinksWereAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebLinks_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebLinks_DomainUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "DomainUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebLinkWebLink",
                columns: table => new
                {
                    ChildPagesId = table.Column<int>(type: "int", nullable: false),
                    ParentPagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebLinkWebLink", x => new { x.ChildPagesId, x.ParentPagesId });
                    table.ForeignKey(
                        name: "FK_WebLinkWebLink_WebLinks_ChildPagesId",
                        column: x => x.ChildPagesId,
                        principalTable: "WebLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebLinkWebLink_WebLinks_ParentPagesId",
                        column: x => x.ParentPagesId,
                        principalTable: "WebLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "aae46164-86d9-4d04-8042-5a2619cd9107");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "05642974-f1b8-4ebc-9bb6-98d6ef8710de");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90579177-a832-4bf6-a379-1b39aff6f37a", "AQAAAAEAACcQAAAAEDEmd+gETiqWheLOK+mCjJu0XbLfFR71tY1y/jKDOCu/pW/BxtZl3cGGRMp19CihUw==", "0ba65aca-705d-4fdd-820b-f0b3c60a3c83" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 15, 58, 59, 676, DateTimeKind.Utc).AddTicks(8273));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 15, 58, 59, 676, DateTimeKind.Utc).AddTicks(9132));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 15, 58, 59, 676, DateTimeKind.Utc).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 15, 58, 59, 676, DateTimeKind.Utc).AddTicks(9137));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 15, 58, 59, 676, DateTimeKind.Utc).AddTicks(9138));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 15, 58, 59, 676, DateTimeKind.Utc).AddTicks(9139));

            migrationBuilder.CreateIndex(
                name: "IX_WebLinks_ContentId",
                table: "WebLinks",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_WebLinks_OwnerId",
                table: "WebLinks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WebLinkWebLink_ParentPagesId",
                table: "WebLinkWebLink",
                column: "ParentPagesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebLinkWebLink");

            migrationBuilder.DropTable(
                name: "WebLinks");

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
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(5506));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6388));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6391));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6392));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6393));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 25, 9, 4, 46, 13, DateTimeKind.Utc).AddTicks(6394));
        }
    }
}
