using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;

namespace university.Services.IRepository
{
    public interface IDepartmentDirectorsRepository
    {
        bool Exists(int departmentdirectorId);
        bool DuplicateDepartmentDirector(string FirstName , string LastName , string NameFamily, int departmentdirectorId);
        ICollection<DepartmentDirectors> GetDepartmentDirectors();
        DepartmentDirectors GetdepartmentDirector(int departmentdirectorId);
        ICollection<Supervisor> GetsupervisorsBydepartmentDirector(int departmentdirectorId);
        DepartmentDirectors GetdepartmentDirectorbysupervisors(int supervisorId);
        bool CreateDepartmentDirector(DepartmentDirectors departmentDirectors);
        bool UpdateDepartmentDirector(DepartmentDirectors departmentDirectors);
        bool DeleteDepartmentDirector(DepartmentDirectors departmentDirectors);
        bool Save();
    }
}
