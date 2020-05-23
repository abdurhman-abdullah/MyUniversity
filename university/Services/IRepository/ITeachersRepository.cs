using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;

namespace university.Services.IRepository
{
    public interface ITeachersRepository
    {
        bool Exists(int teacherId);
        bool DublicateTeacher(string FirstName , string LastName, string NameFamily, int teacherId);
        ICollection<Teachers> GetTeachers();
        Teachers GetTeacher(int teacherId);
        ICollection<Students> GetStudents(int teacherId);
        ICollection<Teachers> GetTeachersByBook(int bookId);
        Specialties specialtiesByTeacher(int teacherId);
        bool CreateTeacher(Teachers teachers);
        bool UdateTeacher(Teachers teachers);
        bool DeleteTeacher(Teachers teachers);
        bool Save();
        

    }
}
