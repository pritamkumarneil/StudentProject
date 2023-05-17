using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProject.Migrations
{
    /// <inheritdoc />
    public partial class TeacherStandardRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobNo",
                table: "Teachers",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "EmailId",
                table: "Teachers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<int>(
                name: "StandardId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_EmailId",
                table: "Teachers",
                column: "EmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_MobNo",
                table: "Teachers",
                column: "MobNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StandardId",
                table: "Teachers",
                column: "StandardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_standards_StandardId",
                table: "Teachers",
                column: "StandardId",
                principalTable: "standards",
                principalColumn: "StandardId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_standards_StandardId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_EmailId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_MobNo",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_StandardId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "StandardId",
                table: "Teachers");

            migrationBuilder.AlterColumn<string>(
                name: "MobNo",
                table: "Teachers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "EmailId",
                table: "Teachers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
