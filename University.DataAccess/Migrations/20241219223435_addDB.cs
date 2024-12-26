using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Course_ID = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    Course_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Mark = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Lap_Hours = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Course_pk", x => x.Course_ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Department_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Building = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Department_pk", x => x.Department_ID);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    Faculty_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Faculty_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Faculty_pk", x => x.Faculty_ID);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Employee_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SSN = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Position = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Staff_pk", x => x.Employee_ID);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentCourses",
                columns: table => new
                {
                    Department_id = table.Column<int>(type: "int", nullable: false),
                    Course_id = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    Level = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Semester = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DepartmentCourses_pk", x => new { x.Department_id, x.Course_id });
                    table.ForeignKey(
                        name: "DepartmentCourses_D_FK",
                        column: x => x.Department_id,
                        principalTable: "Departments",
                        principalColumn: "Department_ID");
                    table.ForeignKey(
                        name: "DepartmentCourses_FK",
                        column: x => x.Course_id,
                        principalTable: "Courses",
                        principalColumn: "Course_ID");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Student_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Faculty_id = table.Column<int>(type: "int", nullable: false),
                    SSN = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthOfDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    enrollYear = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Students_PK", x => x.Student_ID);
                    table.ForeignKey(
                        name: "FK_Students_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Department_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Students_FK",
                        column: x => x.Faculty_id,
                        principalTable: "Faculty",
                        principalColumn: "Faculty_ID");
                });

            migrationBuilder.CreateTable(
                name: "Employing",
                columns: table => new
                {
                    Faculty_id = table.Column<int>(type: "int", nullable: false),
                    Department_id = table.Column<int>(type: "int", nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Employing_PK", x => new { x.Faculty_id, x.Department_id, x.Employee_id });
                    table.ForeignKey(
                        name: "Employing_D_FK",
                        column: x => x.Department_id,
                        principalTable: "Departments",
                        principalColumn: "Department_ID");
                    table.ForeignKey(
                        name: "Employing_F_FK",
                        column: x => x.Faculty_id,
                        principalTable: "Faculty",
                        principalColumn: "Faculty_ID");
                    table.ForeignKey(
                        name: "Employing_S_FK",
                        column: x => x.Employee_id,
                        principalTable: "Staff",
                        principalColumn: "Employee_ID");
                });

            migrationBuilder.CreateTable(
                name: "Teaching",
                columns: table => new
                {
                    Course_id = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    Staff_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Teach_PK", x => new { x.Course_id, x.Staff_id });
                    table.ForeignKey(
                        name: "Teach_C_FK",
                        column: x => x.Course_id,
                        principalTable: "Courses",
                        principalColumn: "Course_ID");
                    table.ForeignKey(
                        name: "Teach_S_FK",
                        column: x => x.Staff_id,
                        principalTable: "Staff",
                        principalColumn: "Employee_ID");
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Student_id = table.Column<int>(type: "int", nullable: false),
                    Course_id = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    Grade = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Enrollments_pk", x => new { x.Student_id, x.Course_id });
                    table.ForeignKey(
                        name: "Enrollments_C_FK",
                        column: x => x.Course_id,
                        principalTable: "Courses",
                        principalColumn: "Course_ID");
                    table.ForeignKey(
                        name: "Enrollments_S_FK",
                        column: x => x.Student_id,
                        principalTable: "Students",
                        principalColumn: "Student_ID");
                });

            migrationBuilder.CreateIndex(
                name: "Course_uq",
                table: "Courses",
                column: "Course_Name",
                unique: true,
                filter: "[Course_Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentCourses_Course_id",
                table: "DepartmentCourses",
                column: "Course_id");

            migrationBuilder.CreateIndex(
                name: "Department_uq",
                table: "Departments",
                column: "Department_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employing_Department_id",
                table: "Employing",
                column: "Department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employing_Employee_id",
                table: "Employing",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_Course_id",
                table: "Enrollments",
                column: "Course_id");

            migrationBuilder.CreateIndex(
                name: "Faculty_Email_uq",
                table: "Faculty",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Faculty_Location_uq",
                table: "Faculty",
                column: "Location",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Faculty_Name_uq",
                table: "Faculty",
                column: "Faculty_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Faculty_phone_uq",
                table: "Faculty",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Faculty_id",
                table: "Students",
                column: "Faculty_id");

            migrationBuilder.CreateIndex(
                name: "Students_email_uq",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Students_phone_uq",
                table: "Students",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Students_SSN",
                table: "Students",
                column: "SSN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teaching_Staff_id",
                table: "Teaching",
                column: "Staff_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentCourses");

            migrationBuilder.DropTable(
                name: "Employing");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Teaching");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Faculty");
        }
    }
}
