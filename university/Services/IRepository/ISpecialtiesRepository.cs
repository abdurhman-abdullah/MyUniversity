using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;

namespace university.Services.IRepository
{
    public interface ISpecialtiesRepository
    {
        bool Exists(int specialtiesId);
        bool DuplicateSpecialties(string name , int specialtiesId);
        ICollection<Specialties> GetSpecialties();
        Specialties GetSpecialty(int specialtiesId);
        ICollection<Books> GetBooks(int specialtiesId);
        ICollection<Teachers> GetTeachers(int specialtiesId);
        ICollection<Students> GetStudents(int specialtiesId);
        bool CreateSpecialties(Specialties specialties);
        bool UpdateSpecialties(Specialties specialties);
        bool DeleteSpecialties(Specialties specialties);
        bool Save();



    }
}
