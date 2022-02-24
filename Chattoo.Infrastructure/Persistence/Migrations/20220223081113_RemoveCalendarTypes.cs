using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chattoo.Infrastructure.Persistence.Migrations
{
    public partial class RemoveCalendarTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvent_CalendarEventType_CalendarEventTypeId",
                table: "CalendarEvent");

            migrationBuilder.DropTable(
                name: "CalendarEventWishToCalendarEventType");

            migrationBuilder.DropTable(
                name: "CalendarEventType");

            migrationBuilder.DropIndex(
                name: "IX_CalendarEvent_CalendarEventTypeId",
                table: "CalendarEvent");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CalendarEventWish",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CalendarEventTypeId",
                table: "CalendarEvent",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalendarEventType",
                table: "CalendarEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CalendarEventWish");

            migrationBuilder.DropColumn(
                name: "CalendarEventType",
                table: "CalendarEvent");

            migrationBuilder.AlterColumn<string>(
                name: "CalendarEventTypeId",
                table: "CalendarEvent",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CalendarEventType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CalendarEventWishId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEventType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarEventType_CalendarEventWish_CalendarEventWishId",
                        column: x => x.CalendarEventWishId,
                        principalTable: "CalendarEventWish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEventWishToCalendarEventType",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WishId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEventWishToCalendarEventType", x => new { x.TypeId, x.WishId });
                    table.ForeignKey(
                        name: "FK_CalendarEventWishToCalendarEventType_CalendarEventType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CalendarEventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarEventWishToCalendarEventType_CalendarEventWish_WishId",
                        column: x => x.WishId,
                        principalTable: "CalendarEventWish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvent_CalendarEventTypeId",
                table: "CalendarEvent",
                column: "CalendarEventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventType_CalendarEventWishId",
                table: "CalendarEventType",
                column: "CalendarEventWishId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventWishToCalendarEventType_WishId",
                table: "CalendarEventWishToCalendarEventType",
                column: "WishId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvent_CalendarEventType_CalendarEventTypeId",
                table: "CalendarEvent",
                column: "CalendarEventTypeId",
                principalTable: "CalendarEventType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
