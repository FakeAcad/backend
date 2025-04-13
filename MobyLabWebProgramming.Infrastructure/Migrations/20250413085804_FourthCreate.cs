using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FourthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_University_Name",
                table: "University",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Professor_FirstName_LastName",
                table: "Professor",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Complaint_Name",
                table: "Complaint",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Article_Title",
                table: "Article",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_University_Name",
                table: "University");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Professor_FirstName_LastName",
                table: "Professor");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Complaint_Name",
                table: "Complaint");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Article_Title",
                table: "Article");
        }
    }
}
