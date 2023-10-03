﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjeAkademisyenProfil.Migrations
{
    /// <inheritdoc />
    public partial class Guncellemeuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryTitle",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryTitle",
                table: "Categories");
        }
    }
}
