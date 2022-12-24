using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace Library.Data.Migrations
{
    public partial class AddSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "EngSearchVector",
                table: "Books",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Name", "Author" });

  
            migrationBuilder.CreateIndex(
                name: "IX_Books_EngSearchVector",
                table: "Books",
                column: "EngSearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_EngSearchVector",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "EngSearchVector",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Books");
        }
    }
}
