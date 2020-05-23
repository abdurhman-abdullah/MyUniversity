using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;

namespace university.Services.IRepository
{
    public interface ISectionRepository
    {
        bool Exists(int sectionId);
        bool DuplicateSection(string name, int sectionId);
        bool DuplicateDepartment(int departmentId , int Id);
        ICollection<TheSections> GetTheSections();
        TheSections GetById(int sectionId);
        DepartmentDirectors GetDepartmentBySections(int sectionId);
        TheSections GetSectionsOfADepartment(int departmentDirectorsId);
        bool CreattheSection(TheSections theSections);
        bool UpdatetheSection(TheSections theSections);
        bool DeletetheSection(TheSections theSections);
        bool Save();
    }
}
