using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COURSES",
                columns: table => new
                {
                    COURSE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSES", x => x.COURSE_ID);
                });

            migrationBuilder.CreateTable(
                name: "GROUPS",
                columns: table => new
                {
                    GROUP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COURSE_ID = table.Column<int>(type: "int", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUPS", x => x.GROUP_ID);
                    table.ForeignKey(
                        name: "FK_GROUPS_COURSES_COURSE_ID",
                        column: x => x.COURSE_ID,
                        principalTable: "COURSES",
                        principalColumn: "COURSE_ID");
                });

            migrationBuilder.CreateTable(
                name: "STUDENTS",
                columns: table => new
                {
                    STUDENT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GROUP_ID = table.Column<int>(type: "int", nullable: true),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENTS", x => x.STUDENT_ID);
                    table.ForeignKey(
                        name: "FK_STUDENTS_GROUPS_GROUP_ID",
                        column: x => x.GROUP_ID,
                        principalTable: "GROUPS",
                        principalColumn: "GROUP_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GROUPS_COURSE_ID",
                table: "GROUPS",
                column: "COURSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENTS_GROUP_ID",
                table: "STUDENTS",
                column: "GROUP_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDENTS");

            migrationBuilder.DropTable(
                name: "GROUPS");

            migrationBuilder.DropTable(
                name: "COURSES");
        }
    }
}
