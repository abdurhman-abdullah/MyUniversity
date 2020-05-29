using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;

namespace university.Services
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) 
            : base(options)
        {
            Database.Migrate();
        }

        public virtual DbSet<TheSections> TheSections { get; set; }
        public virtual DbSet<Specialties> Specialties { get; set; }
        public virtual DbSet<DepartmentDirectors> DepartmentDirectors { get; set; }
        public virtual DbSet<Teachers> Teachers{ get; set; }
        public virtual DbSet<Supervisor> Supervisors { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<DivisionStudent> DivisionStudents { get; set; }
        public virtual DbSet<BooksTeachers> BooksTeachers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DivisionStudent>()
                .HasKey(ds => new { ds.DivisionId , ds.StudentId});
            modelBuilder.Entity<DivisionStudent>()
                .HasOne(d => d.Division)
                .WithMany(ds => ds.DivisionStudents)
                .HasForeignKey(d => d.DivisionId);
            modelBuilder.Entity<DivisionStudent>()
                .HasOne(s => s.Students)
                .WithMany(ds => ds.DivisionStudents)
                .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<BooksTeachers>()
                .HasKey(bt => new { bt.BookId, bt.TeacherId });
            modelBuilder.Entity<BooksTeachers>()
                .HasOne(b => b.Books)
                .WithMany(bt => bt.BooksTeachers)
                .HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BooksTeachers>()
                .HasOne(t => t.Teachers)
                .WithMany(bt => bt.BooksTeachers)
                .HasForeignKey(t => t.TeacherId);
        } 
    }
}
