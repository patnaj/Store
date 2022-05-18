using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.Migrations
{
    public partial class StoreModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmploymentDate",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IssuedForGuid = table.Column<string>(type: "TEXT", nullable: true),
                    IssuedByGuid = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_IssuedByGuid",
                        column: x => x.IssuedByGuid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_IssuedForGuid",
                        column: x => x.IssuedForGuid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    Amount = table.Column<float>(type: "REAL", nullable: false),
                    VAT = table.Column<float>(type: "REAL", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_IssuedByGuid",
                table: "Invoices",
                column: "IssuedByGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_IssuedForGuid",
                table: "Invoices",
                column: "IssuedForGuid");

            migrationBuilder.CreateIndex(
                name: "IX_IvoiceItems_InvoiceId",
                table: "IvoiceItems",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IvoiceItems");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmploymentDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");
        }
    }
}
