﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CJMovieTracker.Migrations
{
    /// <inheritdoc />
    public partial class ReleaseYearAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReleaseYear",
                table: "Movies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Movies");
        }
    }
}
