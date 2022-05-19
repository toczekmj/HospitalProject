using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalProject.Migrations
{
    public partial class add_doctor_department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctor_department",
                columns: table => new
                {
                    doctorId = table.Column<int>(type: "int", nullable: false),
                    departmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_doctor_department_department_departmentId",
                        column: x => x.departmentId,
                        principalTable: "department",
                        principalColumn: "departmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_doctor_department_doctor_doctorId",
                        column: x => x.doctorId,
                        principalTable: "doctor",
                        principalColumn: "doctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_doctor_department_departmentId",
                table: "doctor_department",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_department_doctorId",
                table: "doctor_department",
                column: "doctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doctor_department");
        }
    }
}
