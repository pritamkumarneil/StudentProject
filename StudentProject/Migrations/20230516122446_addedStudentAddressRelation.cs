using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace StudentProject.Migrations
{
    /// <inheritdoc />
    public partial class addedStudentAddressRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobNo",
                table: "Students",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateTable(
                name: "studentAddresses",
                columns: table => new
                {
                    StudentAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(type: "longtext", nullable: false),
                    Address2 = table.Column<string>(type: "longtext", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    State = table.Column<string>(type: "longtext", nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentAddresses", x => x.StudentAddressId);
                    table.ForeignKey(
                        name: "FK_studentAddresses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TeacherName = table.Column<string>(type: "longtext", nullable: false),
                    Age = table.Column<string>(type: "longtext", nullable: false),
                    EmailId = table.Column<string>(type: "varchar(255)", nullable: false),
                    MobNo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Subject = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.TeacherId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_studentAddresses_StudentId",
                table: "studentAddresses",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_EmailId",
                table: "Teacher",
                column: "EmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_MobNo",
                table: "Teacher",
                column: "MobNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "studentAddresses");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.AlterColumn<string>(
                name: "MobNo",
                table: "Students",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);
        }
    }
}
