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
        public virtual DbSet<BookTeacherStudent> BookTeacherStudents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookTeacherStudent>()
                .HasKey(bt => new { bt.teacherId, bt.bookId, bt.StudentId});
            modelBuilder.Entity<BookTeacherStudent>()
                .HasOne(t => t.teachers)
                .WithMany(tb => tb.BookTeacherStudents)
                .HasForeignKey(t => t.teacherId);
            modelBuilder.Entity<BookTeacherStudent>()
                .HasOne(b => b.books)
                .WithMany(tb => tb.BookTeacherStudents)
                .HasForeignKey(b => b.bookId);
            modelBuilder.Entity<BookTeacherStudent>()
                .HasOne(s => s.student)
                .WithMany(bt => bt.BookTeacherStudents)
                .HasForeignKey(s => s.StudentId);
        } 
    }
}
