using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Staff",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Staff",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DepartmentFaculty",
                columns: table => new
                {
                    DepartmentsDepartmentId = table.Column<int>(type: "int", nullable: false),
                    FacultiesFacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentFaculty", x => new { x.DepartmentsDepartmentId, x.FacultiesFacultyId });
                    table.ForeignKey(
                        name: "FK_DepartmentFaculty_Departments_DepartmentsDepartmentId",
                        column: x => x.DepartmentsDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Department_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentFaculty_Faculty_FacultiesFacultyId",
                        column: x => x.FacultiesFacultyId,
                        principalTable: "Faculty",
                        principalColumn: "Faculty_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_DepartmentId",
                table: "Staff",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_FacultyId",
                table: "Staff",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentFaculty_FacultiesFacultyId",
                table: "DepartmentFaculty",
                column: "FacultiesFacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Departments_DepartmentId",
                table: "Staff",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Department_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Faculty_FacultyId",
                table: "Staff",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Faculty_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Departments_DepartmentId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Faculty_FacultyId",
                table: "Staff");

            migrationBuilder.DropTable(
                name: "DepartmentFaculty");

            migrationBuilder.DropIndex(
                name: "IX_Staff_DepartmentId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_FacultyId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Staff");
        }
    }
}
