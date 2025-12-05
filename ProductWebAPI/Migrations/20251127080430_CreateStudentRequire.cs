using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateStudentRequire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorName = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorId);
                });

            migrationBuilder.CreateTable(
                name: "StudentEnrollment",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "varchar(50)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Sex = table.Column<string>(type: "varchar(10)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime", nullable: false),
                    Nationality = table.Column<string>(type: "varchar(50)", nullable: false),
                    Telegram = table.Column<string>(type: "varchar(50)", nullable: false),
                    FatherName = table.Column<string>(type: "varchar(100)", nullable: false),
                    MotherName = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEnrollment", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorId = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "MajorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactInformation",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "varchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    GuardianNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    EmergencyName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Relationship = table.Column<string>(type: "varchar(50)", nullable: false),
                    EmergencyContact = table.Column<string>(type: "varchar(20)", nullable: false),
                    EmergencyWorkplace = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformation", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_ContactInformation_StudentEnrollment_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentEnrollment",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentEducation",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "varchar(50)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false),
                    Education = table.Column<string>(type: "varchar(100)", nullable: false),
                    BacIIGrade = table.Column<string>(type: "char(2)", nullable: false),
                    BacIICertificateCode = table.Column<string>(type: "varchar(50)", nullable: false),
                    BacIIYear = table.Column<int>(type: "int", nullable: false),
                    HighSchoolName = table.Column<string>(type: "varchar(100)", nullable: false),
                    HighSchoolLocation = table.Column<string>(type: "varchar(100)", nullable: false),
                    CareerType = table.Column<string>(type: "varchar(50)", nullable: false),
                    AcademicUnit = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentEducation", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_CurrentEducation_StudentEnrollment_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentEnrollment",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermanentAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "varchar(50)", nullable: false),
                    Country = table.Column<string>(type: "varchar(50)", nullable: false),
                    Province = table.Column<string>(type: "varchar(50)", nullable: false),
                    District = table.Column<string>(type: "varchar(50)", nullable: false),
                    Commune = table.Column<string>(type: "varchar(50)", nullable: false),
                    Village = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermanentAddress", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_PermanentAddress_StudentEnrollment_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentEnrollment",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegisterInformation",
                columns: table => new
                {
                    RegisterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "varchar(50)", nullable: false),
                    MajorId = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RegisterType = table.Column<string>(type: "varchar(50)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterInformation", x => x.RegisterId);
                    table.ForeignKey(
                        name: "FK_RegisterInformation_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "MajorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisterInformation_StudentEnrollment_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentEnrollment",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_StudentId",
                table: "ContactInformation",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermanentAddress_StudentId",
                table: "PermanentAddress",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegisterInformation_MajorId",
                table: "RegisterInformation",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterInformation_StudentId",
                table: "RegisterInformation",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_MajorId",
                table: "Subjects",
                column: "MajorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInformation");

            migrationBuilder.DropTable(
                name: "CurrentEducation");

            migrationBuilder.DropTable(
                name: "PermanentAddress");

            migrationBuilder.DropTable(
                name: "RegisterInformation");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "StudentEnrollment");

            migrationBuilder.DropTable(
                name: "Majors");
        }
    }
}
