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
        private ITeachersRepository _teachersRepository;
        private ISpecialtiesRepository _specialtiesRepository;
        private IDivisionRepository _divisionRepository;
        public BookController(IBookRepository bookRepository,
                              ITeachersRepository teachersRepository,
                              ISpecialtiesRepository specialtiesRepository,
                              IDivisionRepository divisionRepository)
        {
            _bookRepository = bookRepository;
            _teachersRepository = teachersRepository;
            _specialtiesRepository = specialtiesRepository;
            _divisionRepository = divisionRepository;
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
        public IActionResult CreateBook([FromBody]Books createTobooks, [FromQuery] List<int> teachersId)
        {
           var statusCode = validation(createTobooks, teachersId);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            if (createTobooks.specialtie == null)
                return BadRequest("Please entry Specialtie");

            if (!_specialtiesRepository.Exists(createTobooks.specialtie.Id))
                return NotFound();

            createTobooks.specialtie = _specialtiesRepository.GetSpecialty(createTobooks.specialtie.Id);

            if (!_bookRepository.CreateBook(createTobooks , teachersId))
            {
                ModelState.AddModelError("", $"Something went wrong saving the Book {createTobooks.Number}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetBook", new { bookId = createTobooks.Id }, createTobooks);
        }

        [HttpPut("{bookId}")]
        public IActionResult UpdateBook(Books updateTobooks, int bookId ,List<int> teachersId)
        {
            var statusCode = validation(updateTobooks, teachersId);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            if (updateTobooks.Id != bookId)
                return BadRequest();

            if (!_bookRepository.Exists(bookId))
                return NotFound();

            if (updateTobooks.specialtie == null)
                return BadRequest("Please entry Specialtie");

            if (!_specialtiesRepository.Exists(updateTobooks.specialtie.Id))
                return NotFound();

            updateTobooks.specialtie = _specialtiesRepository.GetSpecialty(updateTobooks.specialtie.Id);


            if (!_bookRepository.UpdateBook(updateTobooks , teachersId))
            {
                ModelState.AddModelError("", $"Something went wrong updating {updateTobooks.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{bookId}")]
        public IActionResult DeleteBook(int bookId)
        {
            if (!_bookRepository.Exists(bookId))
                return NotFound();

            var deleteToBook = _bookRepository.GetBook(bookId);
            var deleteToDivision = _divisionRepository.GetDivisionsByBook(bookId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_divisionRepository.DeleteDivisions(deleteToDivision.ToList()))
            {
                ModelState.AddModelError("", $"Something went wrong deleting Division");
                return StatusCode(500, ModelState);
            }
            if (!_bookRepository.DeleteBook(deleteToBook))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {deleteToBook.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        public StatusCodeResult validation(Books books,List<int> teachersId)
        {
            if (books == null || teachersId.Count <= 0)
            {
                ModelState.AddModelError("", "Missing book or teacher");
                return BadRequest();
            }
            
            if(_bookRepository.DepulicateBook(books.Number , books.Id))
            {
                ModelState.AddModelError("", $"DepulicateBook");
                return BadRequest();
            }

            foreach (var teacher in teachersId)
            {
                if (!_teachersRepository.Exists(teacher))
                {
                    ModelState.AddModelError("", "Teacher Not Found");
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
