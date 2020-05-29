using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;
using university.Services.IRepository;

namespace university.Services.Repository
{
    public class BookRepository : IBookRepository
    {
        private UniversityDbContext _universityDbContext;
        public BookRepository(UniversityDbContext universityDbContext)
        {
            _universityDbContext = universityDbContext;
        }
        public bool CreateBook(Books books, List<int> teacherId)
        {
            var teachers = _universityDbContext.Teachers.Where(t => teacherId.Contains(t.Id)).ToList();
            foreach (var teacher in teachers)
            {
                var bookTeacher = new BooksTeachers
                {
                    Books = books,
                    Teachers = teacher
                };
                _universityDbContext.Add(bookTeacher);
            }
            _universityDbContext.Add(books);
            return Save();
        }

        public bool DeleteBook(Books books)
        {
            _universityDbContext.Remove(books);
            return Save();
        }

        public bool DepulicateBook(int bookNumber, int Id)
        {
           var book =  _universityDbContext.Books.Where(b => b.Number == bookNumber && b.Id != Id).FirstOrDefault();
            return book == null ? false : true;
        }

        public bool Exists(int bookId)
        {
            return _universityDbContext.Books.Any(b => b.Id == bookId);
        }

        public Books GetBook(int bookId)
        {
            return _universityDbContext.Books.Where(b => b.Id == bookId).FirstOrDefault();
        }

        public ICollection<Books> GetBooks()
        {
            return _universityDbContext.Books.OrderBy(b => b.Name).ToList();
        }
        public ICollection<Teachers> GetTeachersByBook(int bookId)
        {
            var taechers = _universityDbContext.BooksTeachers.Where(b => b.BookId == bookId).Select(t => t.Teachers.Id).FirstOrDefault();
            return _universityDbContext.Teachers.Where(t => t.Id == taechers).ToList();
        }

        public bool Save()
        {
            var save = _universityDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }
        public bool UpdateBook(Books books, List<int> teacherId)
        {
            var teachers = _universityDbContext.Teachers.Where(t => teacherId.Contains(t.Id)).ToList();
            var deleteBookTeacher = _universityDbContext.BooksTeachers.Where(t => t.BookId == books.Id);

            _universityDbContext.RemoveRange(deleteBookTeacher);

            foreach (var teacher in teachers)
            {
                var bookTeacher = new BooksTeachers()
                {
                    Books = books,
                    Teachers = teacher
                };
                _universityDbContext.Add(bookTeacher);
            }
            _universityDbContext.Update(books);
            return Save();
        }
    }
}
