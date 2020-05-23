using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;

namespace university.Services.IRepository
{
    public interface ISupervisorRepository
    {
        bool Exists(int supervisorId);
        bool DuplicateSupervisor(string FirstName , string LastName , string NameFamily, int Id);
        ICollection<Supervisor> GetSupervisors();
        Supervisor GetSupervisor(int supervisorId);
        ICollection<Students> GetStudentsBySupervisor(int supervisorId);
        Supervisor GetSupervisorByStudent(int studentId);
        bool CreateSupervisor(Supervisor supervisor);
        bool UpdateSupervisor(Supervisor supervisor);
        bool DeleteSupervisor(Supervisor supervisor);
        bool Save();

    }
}
