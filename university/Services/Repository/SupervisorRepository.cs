using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;
using university.Services.IRepository;

namespace university.Services.Repository
{
    public class SupervisorRepository : ISupervisorRepository
    {
        private UniversityDbContext _universityDbContext;
        public SupervisorRepository(UniversityDbContext universityDbContext)
        {
            _universityDbContext = universityDbContext;
        }
        public bool CreateSupervisor(Supervisor supervisor)
        {
            _universityDbContext.Add(supervisor);
            return Save();
        }

        public bool DeleteSupervisor(Supervisor supervisor)
        {
            _universityDbContext.Remove(supervisor);
            return Save();
        }

        public bool DuplicateSupervisor(string FirstName, string LastName, string NameFamily, int Id)
        {
            var duplicate = _universityDbContext.Supervisors.Where(s => s.FirstName.Trim().ToUpper() == FirstName.Trim().ToUpper() &&
                                                                          s.LastName.Trim().ToUpper() == LastName.Trim().ToUpper() &&
                                                                           s.NameFamily.Trim().ToUpper() == NameFamily.Trim().ToUpper() &&
                                                                           s.Id != Id).FirstOrDefault();
            return duplicate == null ? false : true;
        }

        public bool Exists(int supervisorId)
        {
            return _universityDbContext.Supervisors.Any(s => s.Id == supervisorId);
        }

        public ICollection<Students> GetStudentsBySupervisor(int supervisorId)
        {
            return _universityDbContext.Students.Where(s => s.supervisor.Id == supervisorId).ToList();
        }

        public Supervisor GetSupervisor(int supervisorId)
        {
            return _universityDbContext.Supervisors.Where(s => s.Id == supervisorId).FirstOrDefault();
        }

        public Supervisor GetSupervisorByStudent(int studentId)
        {
            var supervoiser = _universityDbContext.Students.Where(s => s.Id == studentId).Select(s => s.supervisor.Id).FirstOrDefault();
            return _universityDbContext.Supervisors.Where(s => s.Id == supervoiser).FirstOrDefault();
        }

        public ICollection<Supervisor> GetSupervisors()
        {
            return _universityDbContext.Supervisors.OrderBy(s => s.FirstName).ToList();
        }

        public bool Save()
        {
            var save = _universityDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateSupervisor(Supervisor supervisor)
        {
            _universityDbContext.Update(supervisor);
            return Save();
        }
    }
}
