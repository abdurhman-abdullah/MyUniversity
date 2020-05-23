using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;
using university.Services.IRepository;

namespace university.Services.Repository
{
    public class TeachersRepository : ITeachersRepository
    {
        private UniversityDbContext _universityDbContext;
        public TeachersRepository(UniversityDbContext universityDbContext)
        {
            _universityDbContext = universityDbContext;
        }
        public bool CreateTeacher(Teachers teachers)
        {
             _universityDbContext.Add(teachers);
            return Save();
        }

        public bool DeleteTeacher(Teachers teachers)
        {
            _universityDbContext.Remove(teachers);
            return Save();
        }

        public bool DublicateTeacher(string FirstName, string LastName, string NameFamily, int teacherId)
        {
          var duplicate =  _universityDbContext.Teachers.Where(t => t.FirstName.Trim().ToUpper() == FirstName.Trim().ToUpper() &&
                                                t.LastName.Trim().ToUpper() == LastName.Trim().ToUpper() &&
                                                t.NameFamily.Trim().ToUpper() == NameFamily.Trim().ToUpper() &&
                                                t.Id != teacherId).FirstOrDefault();
            return duplicate == null ? false : true;
        }

        public bool Exists(int teacherId)
        {
           return _universityDbContext.Teachers.Any(t => t.Id == teacherId);
        }
        public ICollection<Students> GetStudents(int teacherId)
        {
            var student = _universityDbContext.BookTeacherStudents.Where(t => t.teacherId == teacherId).Select(s => s.student.Id).FirstOrDefault();
            return _universityDbContext.Students.Where(s => s.Id == student).ToList();
        }

        public Teachers GetTeacher(int teacherId)
        {
            return _universityDbContext.Teachers.Where(t => t.Id == teacherId).FirstOrDefault();
        }

        public ICollection<Teachers> GetTeachers()
        {
           return _universityDbContext.Teachers.OrderBy(t => t.FirstName).ToList();
        }

        public ICollection<Teachers> GetTeachersByBook(int bookId)
        {
            var teacher = _universityDbContext.BookTeacherStudents.Where(b => b.bookId == bookId).Select(t => t.teachers.Id).FirstOrDefault();
            return _universityDbContext.Teachers.Where(t => t.Id == teacher).ToList();
        }
        public bool Save()
        {
            var save = _universityDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public Specialties specialtiesByTeacher(int teacherId)
        {
            var specialties = _universityDbContext.Teachers.Where(t => t.Id == teacherId).Select(s => s.Specialties.Id).FirstOrDefault();
            return _universityDbContext.Specialties.Where(s => s.Id == specialties).FirstOrDefault();
        }

        public bool UdateTeacher(Teachers teachers)
        {
            _universityDbContext.Update(teachers);
            return Save();
        }
    }
}
