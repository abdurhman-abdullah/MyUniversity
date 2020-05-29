using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;

namespace university.Services.IRepository
{
    public interface IDivisionRepository
    {
        bool Exists(int divisionId);
        bool DepulicatDivision(long divisionNo, int Id);
        ICollection<Division> GetDivisions();
        Division GetDivision(int divisionId);
        Books GetBookByDivision(int divisionId);
        ICollection<Division> GetDivisionsByBook(int bookId);
        Teachers GetTeacherByDivision(int divisionId);
        bool CreateDivision(Division division, List<int> studentId);
        bool UpdateDivision(Division division, List<int> studentId);
        bool DeleteDivision(Division division);
        bool DeleteDivisions(List<Division> divisions);
        bool Save();
    }
}
