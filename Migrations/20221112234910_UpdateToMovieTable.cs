using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CJMovieTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToMovieTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateWatchedDateType",
                table: "Movies",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgLink",
                table: "Movies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TmdbID",
                table: "Movies",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateWatchedDateType",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ImgLink",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TmdbID",
                table: "Movies");
        }
    }
}
