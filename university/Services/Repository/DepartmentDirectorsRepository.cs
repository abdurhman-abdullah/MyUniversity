using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;
using university.Services.IRepository;

namespace university.Services.Repository
{
    public class DepartmentDirectorsRepository : IDepartmentDirectorsRepository
    {
        private UniversityDbContext _universityDbContext;
        public DepartmentDirectorsRepository(UniversityDbContext universityDbContext)
        {
            _universityDbContext = universityDbContext;
        }
        public bool CreateDepartmentDirector(DepartmentDirectors departmentDirectors)
        {
            _universityDbContext.Add(departmentDirectors);
            return Save();
        }

        public bool DeleteDepartmentDirector(DepartmentDirectors departmentDirectors)
        {
            _universityDbContext.Remove(departmentDirectors);
            return Save();
        }

        public bool DuplicateDepartmentDirector(string FirstName, string LastName, string NameFamily, int departmentdirectorId)
        {
            var duplicate = _universityDbContext.DepartmentDirectors.Where(d => d.FirstName.Trim().ToUpper() == FirstName.Trim().ToUpper() &&
                                                                           d.LastName.Trim().ToUpper() == LastName.Trim().ToUpper() &&
                                                                           d.NameFamily.Trim().ToUpper() == NameFamily.Trim().ToUpper() &&
                                                                           d.Id != departmentdirectorId).FirstOrDefault();
            return duplicate == null ? false : true;
        }

        public bool Exists(int departmentdirectorId)
        {
           return _universityDbContext.DepartmentDirectors.Any(d => d.Id == departmentdirectorId);
        }

        public DepartmentDirectors GetdepartmentDirector(int departmentdirectorId)
        {
           return _universityDbContext.DepartmentDirectors.Where(d => d.Id == departmentdirectorId).FirstOrDefault();
        }

        public DepartmentDirectors GetdepartmentDirectorbysupervisors(int supervisorId)
        {
            var superviserId = _universityDbContext.Supervisors.Where(s => s.Id == supervisorId).Select(dd => dd.Department.Id).FirstOrDefault();
            return _universityDbContext.DepartmentDirectors.Where(d => d.Id == superviserId).FirstOrDefault();
        }

        public ICollection<DepartmentDirectors> GetDepartmentDirectors()
        {
            return _universityDbContext.DepartmentDirectors.OrderBy(d => d.FirstName).ToList();
        }

        public ICollection<Supervisor> GetsupervisorsBydepartmentDirector(int departmentdirectorId)
        {
            return _universityDbContext.DepartmentDirectors.Where(d => d.Id == departmentdirectorId).
                                                                  Select(s => s.Supervisor.ToList()).FirstOrDefault();
        }

        public bool Save()
        {
            var save =  _universityDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateDepartmentDirector(DepartmentDirectors departmentDirectors)
        {
            _universityDbContext.Update(departmentDirectors);
            return Save();
        }
    }
}
