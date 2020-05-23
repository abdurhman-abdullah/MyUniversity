using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;
using university.Services.IRepository;

namespace university.Services.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private UniversityDbContext _universityDbContext;
        public StudentRepository(UniversityDbContext universityDbContext)
        {
            _universityDbContext = universityDbContext;
        }
        public bool CreateStudent(Students students)
        {
            _universityDbContext.Add(students);
            return Save();
        }

        public bool DeleteStudents(Students students)
        {
            _universityDbContext.Remove(students);
            return Save();
        }

        public bool Exists(int studentId)
        {
           return _universityDbContext.Students.Any(s => s.Id == studentId);
        }

        public Specialties GetSpecialtiesByStudent(int studentId)
        {
            var specialties = _universityDbContext.Students.Where(s => s.Id == studentId).Select(s => s.specialtie.Id).FirstOrDefault();
            return _universityDbContext.Specialties.Where(s => s.Id == specialties).FirstOrDefault();
        }

        public Students GetStudentById(int studentId)
        {
            return _universityDbContext.Students.Where(s => s.Id == studentId).FirstOrDefault();
        }

        public ICollection<Students> GetStudents()
        {
            return _universityDbContext.Students.OrderBy(s => s.FirstName).ToList();
        }
        public bool Save()
        {
            var save = _universityDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateStudents(Students students)
        {
            _universityDbContext.Update(students);
            return Save();
        }
    }
}
