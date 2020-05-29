using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;

namespace university.Services.IRepository
{
   public interface IBookRepository
    {
        bool Exists(int bookId);
        bool DepulicateBook(int bookNumber, int Id);
        ICollection<Books> GetBooks();
        Books GetBook(int bookId);
        ICollection<Teachers> GetTeachersByBook(int bookId);
        bool CreateBook(Books books , List<int> teacherId);
        bool UpdateBook(Books books , List<int> teacherId);
        bool DeleteBook(Books books);
        bool Save();
    }
}
