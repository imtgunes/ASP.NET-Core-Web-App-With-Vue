using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjeAkademisyenProfil.Migrations
{
    /// <inheritdoc />
    public partial class Guncellemeiki : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EducationDate",
                table: "Educations",
                newName: "EducationDateStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "EducationDateEnd",
                table: "Educations",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationDateEnd",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "EducationDateStart",
                table: "Educations",
                newName: "EducationDate");
        }
    }
}
