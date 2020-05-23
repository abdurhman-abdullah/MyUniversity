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
        public bool CreateBook(Books books, int teacher, List<int> student)
        {
            var teachers = _universityDbContext.Teachers.Where(t => t.Id == teacher).FirstOrDefault();
            var students = _universityDbContext.Students.Where(s => student.Contains(s.Id)).ToList();

                foreach (var studen in students)
                {
                    var bookTeacherStudents = new BookTeacherStudent()
                    {
                        teachers = teachers,
                        books = books,
                        student = studen
                    };
                    _universityDbContext.Add(bookTeacherStudents);
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

        public ICollection<Students> GetStudentsByBook(int bookId)
        {
            var students = _universityDbContext.BookTeacherStudents.Where(b => b.bookId == bookId).Select(s => s.student.Id).FirstOrDefault();
            return _universityDbContext.Students.Where(s => s.Id == students).ToList();
        }

        public ICollection<Teachers> GetTeachersByBook(int bookId)
        {
            var taechers = _universityDbContext.BookTeacherStudents.Where(b => b.bookId == bookId).Select(t => t.teachers.Id).FirstOrDefault();
            return _universityDbContext.Teachers.Where(t => t.Id == taechers).ToList();
        }

        public bool Save()
        {
            var save = _universityDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }
        public bool UpdateBook(Books books, List<int> teacher, List<int> student)
        {
            var teachers = _universityDbContext.Teachers.Where(t => teacher.Contains(t.Id)).ToList();
            var students = _universityDbContext.Students.Where(s => student.Contains(s.Id)).ToList();

            foreach (var teache in teachers)
            {
                var bookTeacherStudent = new BookTeacherStudent()
                {
                    books = books,
                    teachers = teache
                };
                _universityDbContext.Update(bookTeacherStudent);
            }

                foreach (var studen in students)
                {
                    var bookTeacherStudents = new BookTeacherStudent()
                    {
                        student = studen
                    };
                    _universityDbContext.Update(bookTeacherStudents);
                }
            _universityDbContext.Update(books);
            return Save();
        }
    }
}
