using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;
using university.Services.IRepository;

namespace university.Services.Repository
{
    public class SpecialtiesRepository : ISpecialtiesRepository
    {
        private UniversityDbContext _universityDbContext;
        public SpecialtiesRepository(UniversityDbContext universityDbContext)
        {
            _universityDbContext = universityDbContext;
        }
        public bool CreateSpecialties(Specialties specialties)
        {
            _universityDbContext.Add(specialties);
            return Save();
        }

        public bool DeleteSpecialties(Specialties specialties)
        {
            _universityDbContext.Remove(specialties);
            return Save();
        }

        public bool DuplicateSpecialties(string name, int specialtiesId)
        {
            var specialties = _universityDbContext.Specialties.Where(s => s.Name.Trim().ToUpper() == name.Trim().ToUpper() &&
                                                   s.Id != specialtiesId).FirstOrDefault();
            return specialties == null ? false : true;
        }

        public bool Exists(int specialtiesId)
        {
            return _universityDbContext.Specialties.Any(s => s.Id == specialtiesId);
        }

        public ICollection<Books> GetBooks(int specialtiesId)
        {
            return _universityDbContext.Books.Where(s => s.specialtie.Id == specialtiesId).ToList();
        }

        public ICollection<Division> GetDivisions(int specialtiesId)
        {
            return _universityDbContext.Divisions.Where(d => d.Specialties.Id == specialtiesId).ToList();
        }

        public ICollection<Specialties> GetSpecialties()
        {
            return _universityDbContext.Specialties.OrderBy(s => s.Name).ToList();
        }

        public Specialties GetSpecialty(int specialtiesId)
        {
            return _universityDbContext.Specialties.Where(s => s.Id == specialtiesId).FirstOrDefault();
        }

        public ICollection<Students> GetStudents(int specialtiesId)
        {
            return _universityDbContext.Students.Where(s => s.specialtie.Id == specialtiesId).ToList();
        }

        public ICollection<Teachers> GetTeachers(int specialtiesId)
        {
            return _universityDbContext.Teachers.Where(t => t.Specialties.Id == specialtiesId).ToList();
        }

        public bool Save()
        {
            var save = _universityDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateSpecialties(Specialties specialties)
        {
            _universityDbContext.Update(specialties);
            return Save();
        }
    }
}
