using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;

namespace university.Services.IRepository
{
    public interface IStudentRepository
    {
        bool Exists(int studentId);
        ICollection<Students> GetStudents();
        Students GetStudentById(int studentId);
        Specialties GetSpecialtiesByStudent(int studentId);
        bool CreateStudent(Students students);
        bool UpdateStudents(Students students);
        bool DeleteStudents(Students students);
        bool Save();
    }
}
