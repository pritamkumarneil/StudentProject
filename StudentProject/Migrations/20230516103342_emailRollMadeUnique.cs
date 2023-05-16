using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProject.Migrations
{
    /// <inheritdoc />
    public partial class emailRollMadeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmailId",
                table: "Students",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateIndex(
                name: "IX_Students_EmailId",
                table: "Students",
                column: "EmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_RollNo",
                table: "Students",
                column: "RollNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_EmailId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_RollNo",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "EmailId",
                table: "Students",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
