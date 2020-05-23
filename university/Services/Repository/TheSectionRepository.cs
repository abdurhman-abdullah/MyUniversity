using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;
using university.Services.IRepository;

namespace university.Services.Repository
{
    public class TheSectionRepository : ISectionRepository
    {
        private UniversityDbContext _universityDbContext;
        public TheSectionRepository(UniversityDbContext universityDbContext)
        {
            _universityDbContext = universityDbContext;
        }
        public bool CreattheSection(TheSections theSections)
        {
            _universityDbContext.Add(theSections);
            return Save();
        }

        public bool DeletetheSection(TheSections theSections)
        {
            _universityDbContext.Remove(theSections);
            return Save();
        }

        public bool DuplicateDepartment(int departmentId , int Id)
        {
           var duplicate = _universityDbContext.TheSections.Where(d => d.Id != Id &&
                                                                  d.DepartmentDirectors.Id == departmentId).FirstOrDefault();
            return duplicate == null ? false : true;
        }

        public bool DuplicateSection(string name, int sectionId)
        {
            var duplicate =  _universityDbContext.TheSections.Where(s => s.Name.Trim().ToUpper() == name.Trim().ToUpper() 
                                                                    && s.Id != sectionId).FirstOrDefault();
            return duplicate == null ? false : true;
        }

        public bool Exists(int sectionId)
        {
            return _universityDbContext.TheSections.Any(s => s.Id == sectionId);
        }

        public TheSections GetById(int sectionId)
        {
           return _universityDbContext.TheSections.Where(s => s.Id == sectionId).FirstOrDefault();
        }

        public DepartmentDirectors GetDepartmentBySections(int sectionId)
        {
            return _universityDbContext.TheSections.Where(s => s.Id == sectionId).Select(d => d.DepartmentDirectors).FirstOrDefault();
        }

        public TheSections GetSectionsOfADepartment(int departmentDirectorsId)
        {
            return _universityDbContext.TheSections.Where(s => s.DepartmentDirectors.Id == departmentDirectorsId).FirstOrDefault();
        }

        public ICollection<TheSections> GetTheSections()
        {
            return _universityDbContext.TheSections.OrderBy(s => s.Name).ToList();
        }

        public bool Save()
        {
            var save = _universityDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdatetheSection(TheSections theSections)
        {
            _universityDbContext.Update(theSections);
            return Save();
        }
    }
}
