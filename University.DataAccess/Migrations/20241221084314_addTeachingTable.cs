using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTeachingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachings",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<string>(type: "varchar(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachings", x => new { x.StaffId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Teachings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Course_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachings_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachings_CourseId",
                table: "Teachings",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teachings");
        }
    }
}
