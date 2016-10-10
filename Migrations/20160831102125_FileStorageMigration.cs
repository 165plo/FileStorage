using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileStorageMVC.Migrations
{
    public partial class FileStorageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Deleted = table.Column<bool>(nullable: false),
                    MarkedDeleted = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Payload = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    FileKey = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Tags_Files_FileKey",
                        column: x => x.FileKey,
                        principalTable: "Files",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_FileKey",
                table: "Tags",
                column: "FileKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
