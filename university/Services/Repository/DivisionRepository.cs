using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;
using university.Services.IRepository;

namespace university.Services.Repository
{
    public class DivisionRepository : IDivisionRepository
    {
        private UniversityDbContext _universityDbContext;
        public DivisionRepository(UniversityDbContext universityDbContext)
        {
            _universityDbContext = universityDbContext;
        }
        public bool CreateDivision(Division division, List<int> studentId)
        {
            var students = _universityDbContext.Students.Where(s => studentId.Contains(s.Id)).ToList();

            foreach(var student in students)
            {
                var divisionStudent = new DivisionStudent()
                {
                    Division = division,
                    Students = student
                };
                _universityDbContext.Add(divisionStudent);
            }
            _universityDbContext.Add(division);
            return Save();
        }

        public bool DeleteDivision(Division division)
        {
            _universityDbContext.Remove(division);
            return Save();
        }

        public bool DeleteDivisions(List<Division> divisions)
        {
            _universityDbContext.RemoveRange(divisions);
            return Save();
        }

        public bool DepulicatDivision(long divisionNo, int Id)
        {
            var depulicate = _universityDbContext.Divisions.Where(d => d.DivisionNo == divisionNo &&
                                                                  d.Id != Id).FirstOrDefault();
            return depulicate == null ? false : true;
        }

        public bool Exists(int divisionId)
        {
            return _universityDbContext.Divisions.Any(d => d.Id == divisionId);
        }

        public Books GetBookByDivision(int divisionId)
        {
            var book = _universityDbContext.Divisions.Where(d => d.Id == divisionId).Select(b => b.Books.Id).FirstOrDefault();
            return _universityDbContext.Books.Where(b => b.Id == book).FirstOrDefault();
        }

        public Division GetDivision(int divisionId)
        {
            return _universityDbContext.Divisions.Where(d => d.Id == divisionId).FirstOrDefault();
        }

        public ICollection<Division> GetDivisions()
        {
            return _universityDbContext.Divisions.OrderBy(d => d.DivisionNo).ToList();
        }

        public ICollection<Division> GetDivisionsByBook(int bookId)
        {
            return _universityDbContext.Divisions.Where(b => b.Books.Id == bookId).ToList();
        }

        public Teachers GetTeacherByDivision(int divisionId)
        {
            var teacher = _universityDbContext.Divisions.Where(d => d.Id == divisionId).Select(t => t.Teachers.Id).FirstOrDefault();
            return _universityDbContext.Teachers.Where(t => t.Id == teacher).FirstOrDefault();
        }

        public bool Save()
        {
            var save = _universityDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateDivision(Division division, List<int> studentId)
        {
            var students = _universityDbContext.Students.Where(s => studentId.Contains(s.Id)).ToList();
            var deleteDivisionStudent = _universityDbContext.DivisionStudents.Where(d => d.Division.Id == division.Id);

            _universityDbContext.RemoveRange(deleteDivisionStudent);

            foreach (var student in students)
            {
                var divisionStudent = new DivisionStudent()
                {
                    Division = division,
                    Students = student
                };
                _universityDbContext.Add(divisionStudent);
            }
            _universityDbContext.Update(division);
            return Save();
        }
    }
}
