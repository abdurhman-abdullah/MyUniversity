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
    public class DivisionsController : Controller
    {
        private IDivisionRepository _divisionRepository;
        private IStudentRepository _studentRepository;
        private IBookRepository _bookRepository;
        private ITeachersRepository _teachersRepository;
        private ISpecialtiesRepository _specialtiesRepository;
        public DivisionsController(IDivisionRepository divisionRepository ,
                                   IStudentRepository studentRepository ,
                                   IBookRepository bookRepository ,
                                   ITeachersRepository teachersRepository ,
                                   ISpecialtiesRepository specialtiesRepository)
        {
            _divisionRepository = divisionRepository;
            _studentRepository = studentRepository;
            _bookRepository = bookRepository;
            _teachersRepository = teachersRepository;
            _specialtiesRepository = specialtiesRepository;
        }

        [HttpGet]
        public IActionResult GetDivisions()
        {
            var divisions = _divisionRepository.GetDivisions();

            if (!ModelState.IsValid)
                return BadRequest();

            var divisionsDto = new List<DivisionsDto>();

            foreach(var division in divisions)
            {
                divisionsDto.Add(new DivisionsDto
                {
                    Id = division.Id,
                    DivisionNo = division.DivisionNo
                });
            }
            return Ok(divisionsDto);
        }

        [HttpGet("{divisionId}" , Name = "GetDivision")]
        public IActionResult GetDivision(int divisionId)
        {
            if (!_divisionRepository.Exists(divisionId))
                return NotFound();

            var division = _divisionRepository.GetDivision(divisionId);

            if (!ModelState.IsValid)
                return BadRequest();

            var divisionDto = new DivisionsDto
            {
                Id = division.Id,
                DivisionNo = division.DivisionNo
            };

            return Ok(divisionDto);
        }

        [HttpGet("{divisionId}/book")]
        public IActionResult GetBookByDivision(int divisionId)
        {
            if (!_divisionRepository.Exists(divisionId))
                return NotFound();

            var book = _divisionRepository.GetBookByDivision(divisionId);

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

        [HttpGet("divisions/{bookId}")]
        public IActionResult GetDivisionsByBook(int bookId)
        {
            if (!_bookRepository.Exists(bookId))
                return NotFound();

            var divisions = _divisionRepository.GetDivisionsByBook(bookId);

            if (!ModelState.IsValid)
                return BadRequest();

            var divisionsDto = new List<DivisionsDto>();

            foreach(var division in divisions)
            {
                divisionsDto.Add(new DivisionsDto
                {
                    Id = division.Id,
                    DivisionNo = division.DivisionNo
                });
            }
            return Ok(divisionsDto);
        }

        [HttpGet("{divisionId}/teacher")]
        public IActionResult GetTeacherByDivision(int divisionId)
        {
            if (!_divisionRepository.Exists(divisionId))
                return NotFound();

            var teacher = _divisionRepository.GetTeacherByDivision(divisionId);

            if (!ModelState.IsValid)
                return BadRequest();

            var teacherDto = new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                NameFamily = teacher.NameFamily,
                DatePublished = teacher.DatePublished
            };
            return Ok(teacherDto);
        }

        [HttpPost]
        public IActionResult CreateDivision(Division createTodivision, List<int> studentId)
        {
            var statusCode = validation(createTodivision, studentId);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            if (createTodivision.Specialties == null)
                return BadRequest("Please entry Specialtie");

            if (!_specialtiesRepository.Exists(createTodivision.Specialties.Id))
                return NotFound();

            createTodivision.Specialties = _specialtiesRepository.GetSpecialty(createTodivision.Specialties.Id);

            if (createTodivision.Books == null)
                return BadRequest("Please Enter Book");

            if (!_bookRepository.Exists(createTodivision.Books.Id))
                return NotFound();

            createTodivision.Books = _bookRepository.GetBook(createTodivision.Books.Id);

            if (createTodivision.Teachers == null)
            {
                createTodivision.Teachers = null;
            }
            else
            {
                if (!_teachersRepository.Exists(createTodivision.Teachers.Id))
                    return NotFound();

                createTodivision.Teachers = _teachersRepository.GetTeacher(createTodivision.Teachers.Id);
            }
            if (!ModelState.IsValid)
                return BadRequest();

            if(!_divisionRepository.CreateDivision(createTodivision , studentId))
            {
                ModelState.AddModelError("GetDivision", $"Something went wrong saving the Division {createTodivision.DivisionNo}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetDivision", new { divisionId = createTodivision.Id }, createTodivision);
        }

        [HttpPut("{divisionId}")]
        public IActionResult UpdateDivision(Division updateTodivision, List<int> studentId , int divisionId)
        {
            var statusCode = validation(updateTodivision, studentId);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            if (updateTodivision.Id != divisionId)
                return BadRequest();

            if (!_divisionRepository.Exists(divisionId))
                return NotFound();

            if (updateTodivision.Specialties == null)
                return BadRequest("Please entry Specialtie");

            if (!_specialtiesRepository.Exists(updateTodivision.Specialties.Id))
                return NotFound();

            updateTodivision.Specialties = _specialtiesRepository.GetSpecialty(updateTodivision.Specialties.Id);

            if (updateTodivision.Books == null)
                return BadRequest("Please Enter Book");

            if (!_bookRepository.Exists(updateTodivision.Books.Id))
                return NotFound();

            updateTodivision.Books = _bookRepository.GetBook(updateTodivision.Books.Id);

            if (!_teachersRepository.Exists(updateTodivision.Teachers.Id))
                return NotFound();

            updateTodivision.Teachers = _teachersRepository.GetTeacher(updateTodivision.Teachers.Id);


            if (!_divisionRepository.UpdateDivision(updateTodivision, studentId))
            {
                ModelState.AddModelError("", $"Something went wrong updating {updateTodivision.DivisionNo}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{divisionId}")]
        public IActionResult DeleteDivision(int divisionId)
        {
            if (!_divisionRepository.Exists(divisionId))
                return NotFound();

            var deleteToDivision = _divisionRepository.GetDivision(divisionId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_divisionRepository.DeleteDivision(deleteToDivision))
            {
                ModelState.AddModelError("", $"Something went wrong deleting Division");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        public StatusCodeResult validation(Division division, List<int> studentId)
        {
            if(division == null || studentId.Count <= 0)
            {
                ModelState.AddModelError("", "Missing division or student");
                return BadRequest();
            }

            if(_divisionRepository.DepulicatDivision(division.DivisionNo , division.Id))
            {
                ModelState.AddModelError("", $"DepulicateDivision");
                return BadRequest();
            }

            foreach(var student in studentId)
            {
                if (!_studentRepository.Exists(student))
                    return NotFound();
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
