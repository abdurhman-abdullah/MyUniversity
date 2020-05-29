using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace university.Migrations
{
    public partial class InitialDatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentDirectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    NameFamily = table.Column<string>(nullable: false),
                    DatePublished = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentDirectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supervisors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    NameFamily = table.Column<string>(nullable: false),
                    DatePublished = table.Column<DateTime>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supervisors_DepartmentDirectors_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DepartmentDirectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TheSections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    DatePublished = table.Column<DateTime>(nullable: true),
                    DepartmentDirectorsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheSections_DepartmentDirectors_DepartmentDirectorsId",
                        column: x => x.DepartmentDirectorsId,
                        principalTable: "DepartmentDirectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    DatePublished = table.Column<DateTime>(nullable: true),
                    TheSectionsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specialties_TheSections_TheSectionsId",
                        column: x => x.TheSectionsId,
                        principalTable: "TheSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    DatePublished = table.Column<DateTime>(nullable: true),
                    specialtieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Specialties_specialtieId",
                        column: x => x.specialtieId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    NameFamily = table.Column<string>(nullable: false),
                    DatePublished = table.Column<DateTime>(nullable: true),
                    specialtieId = table.Column<int>(nullable: true),
                    supervisorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Specialties_specialtieId",
                        column: x => x.specialtieId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Supervisors_supervisorId",
                        column: x => x.supervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    NameFamily = table.Column<string>(nullable: false),
                    DatePublished = table.Column<DateTime>(nullable: true),
                    departmentDirectorId = table.Column<int>(nullable: true),
                    SpecialtiesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teachers_DepartmentDirectors_departmentDirectorId",
                        column: x => x.departmentDirectorId,
                        principalTable: "DepartmentDirectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BooksTeachers",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false),
                    BooksTeachersBookId = table.Column<int>(nullable: true),
                    BooksTeachersTeacherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksTeachers", x => new { x.BookId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_BooksTeachers_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksTeachers_BooksTeachers_BooksTeachersBookId_BooksTeachersTeacherId",
                        columns: x => new { x.BooksTeachersBookId, x.BooksTeachersTeacherId },
                        principalTable: "BooksTeachers",
                        principalColumns: new[] { "BookId", "TeacherId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DivisionNo = table.Column<long>(nullable: false),
                    SpecialtiesId = table.Column<int>(nullable: true),
                    BooksId = table.Column<int>(nullable: true),
                    TeachersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Divisions_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Divisions_Specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Divisions_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DivisionStudents",
                columns: table => new
                {
                    DivisionId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    DivisionStudentDivisionId = table.Column<int>(nullable: true),
                    DivisionStudentStudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionStudents", x => new { x.DivisionId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_DivisionStudents_Divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivisionStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivisionStudents_DivisionStudents_DivisionStudentDivisionId_DivisionStudentStudentId",
                        columns: x => new { x.DivisionStudentDivisionId, x.DivisionStudentStudentId },
                        principalTable: "DivisionStudents",
                        principalColumns: new[] { "DivisionId", "StudentId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_specialtieId",
                table: "Books",
                column: "specialtieId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksTeachers_TeacherId",
                table: "BooksTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksTeachers_BooksTeachersBookId_BooksTeachersTeacherId",
                table: "BooksTeachers",
                columns: new[] { "BooksTeachersBookId", "BooksTeachersTeacherId" });

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_BooksId",
                table: "Divisions",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_SpecialtiesId",
                table: "Divisions",
                column: "SpecialtiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_TeachersId",
                table: "Divisions",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionStudents_StudentId",
                table: "DivisionStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionStudents_DivisionStudentDivisionId_DivisionStudentStudentId",
                table: "DivisionStudents",
                columns: new[] { "DivisionStudentDivisionId", "DivisionStudentStudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_TheSectionsId",
                table: "Specialties",
                column: "TheSectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_specialtieId",
                table: "Students",
                column: "specialtieId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_supervisorId",
                table: "Students",
                column: "supervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_DepartmentId",
                table: "Supervisors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SpecialtiesId",
                table: "Teachers",
                column: "SpecialtiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_departmentDirectorId",
                table: "Teachers",
                column: "departmentDirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_TheSections_DepartmentDirectorsId",
                table: "TheSections",
                column: "DepartmentDirectorsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksTeachers");

            migrationBuilder.DropTable(
                name: "DivisionStudents");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Supervisors");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "TheSections");

            migrationBuilder.DropTable(
                name: "DepartmentDirectors");
        }
    }
}
