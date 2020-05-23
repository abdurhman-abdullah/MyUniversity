using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Dto;
using university.Model;
using university.Services.IRepository;

namespace university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private IBookRepository _bookRepository;
        private IStudentRepository _studentRepository;
        private ITeachersRepository _teachersRepository;
        private ISpecialtiesRepository _specialtiesRepository;
        public BookController(IBookRepository bookRepository,
                              IStudentRepository studentRepository,
                              ITeachersRepository teachersRepository,
                              ISpecialtiesRepository specialtiesRepository)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _teachersRepository = teachersRepository;
            _specialtiesRepository = specialtiesRepository;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _bookRepository.GetBooks();

            if (!ModelState.IsValid)
                return BadRequest();

            var bookDto = new List<BookDto>();

            foreach (var book in books)
            {
                bookDto.Add(new BookDto
                {
                    Id = book.Id,
                    Name = book.Name,
                    Number = book.Number,
                    DatePublished = book.DatePublished
                });
            }
            return Ok(bookDto);
        }

        [HttpGet("{bookId}" , Name = "GetBook")]
        public IActionResult GetBook(int bookId)
        {
            if (!_bookRepository.Exists(bookId))
                return NotFound();

            var book = _bookRepository.GetBook(bookId);

            if (!ModelState.IsValid)
                return BadRequest();

            var bookDto = new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Number = book.Number,
                DatePublished = book.DatePublished
            };
            return Ok(bookDto);
        }

        [HttpGet("{bookId}/student")]
        public IActionResult GetBooksByStudent(int bookId)
        {
            if (!_bookRepository.Exists(bookId))
                return NotFound();

            var students = _bookRepository.GetStudentsByBook(bookId);

            if (!ModelState.IsValid)
                return BadRequest();

            var studentDto = new List<StudentDto>();

            foreach (var student in students)
            {
                studentDto.Add(new StudentDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DatePublished = student.DatePublished
                });
            }
            return Ok(studentDto);
        }

        [HttpGet("{bookId}/teacher")]
        public IActionResult GetBooksByTeacher(int bookId)
        {
            if (!_bookRepository.Exists(bookId))
                return NotFound();

            var teachers = _bookRepository.GetTeachersByBook(bookId);

            if (!ModelState.IsValid)
                return BadRequest();

            var teacherDto = new List<TeacherDto>();

            foreach (var teacher in teachers)
            {
                teacherDto.Add(new TeacherDto
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    DatePublished = teacher.DatePublished
                });
            }
            return Ok(teacherDto);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody]Books createTobooks, [FromQuery] int teachers, [FromQuery]List<int> students)
        {
           var statusCode = validation(createTobooks, teachers, students);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            if (createTobooks.specialtie == null)
                return BadRequest("Please entry Specialtie");

            if (!_specialtiesRepository.Exists(createTobooks.specialtie.Id))
                return NotFound();

            createTobooks.specialtie = _specialtiesRepository.GetSpecialty(createTobooks.specialtie.Id);

            if (!_bookRepository.CreateBook(createTobooks , teachers, students))
            {
                ModelState.AddModelError("", $"Something went wrong saving the Book {createTobooks.Number}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetBook", new { bookId = createTobooks.Id }, createTobooks);
        }

        public StatusCodeResult validation(Books books, int teachers, List<int> students)
        {
            if (books == null || teachers <= 0 || students.Count <= 0)
            {
                ModelState.AddModelError("", "Missing book , teacher or student");
                return BadRequest();
            }
            
            if(_bookRepository.DepulicateBook(books.Number , books.Id))
            {
                ModelState.AddModelError("", $"DepulicateBook");
                return BadRequest();
            }

            if (!_teachersRepository.Exists(teachers))
            {
                ModelState.AddModelError("", "Teacher Not Found");
                return StatusCode(422);
            }

            foreach (var student in students)
            {
                if (!_studentRepository.Exists(student))
                {
                    ModelState.AddModelError("", "Student Not Found");
                    return StatusCode(422);
                }
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Critical Error");
                return BadRequest();
            }

            return NoContent();
        }
    }
}
