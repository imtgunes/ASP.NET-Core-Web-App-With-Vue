using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjeAkademisyenProfil.Migrations
{
    /// <inheritdoc />
    public partial class Guncellemebes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Infos",
                columns: table => new
                {
                    InfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicianId = table.Column<int>(type: "int", nullable: false),
                    InfoInstitute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoResearch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoBio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infos", x => x.InfoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Infos");
        }
    }
}
