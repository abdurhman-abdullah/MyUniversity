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
                name: "BookTeacherStudents",
                columns: table => new
                {
                    teacherId = table.Column<int>(nullable: false),
                    bookId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTeacherStudents", x => new { x.teacherId, x.bookId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_BookTeacherStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTeacherStudents_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTeacherStudents_Teachers_teacherId",
                        column: x => x.teacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_specialtieId",
                table: "Books",
                column: "specialtieId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTeacherStudents_StudentId",
                table: "BookTeacherStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTeacherStudents_bookId",
                table: "BookTeacherStudents",
                column: "bookId");

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
                name: "BookTeacherStudents");

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
