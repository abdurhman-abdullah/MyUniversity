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
    public class SpecialtiesController : Controller
    {
        private ISpecialtiesRepository _specialtiesRepository;
        private ISectionRepository _sectionRepository;
        public SpecialtiesController(ISpecialtiesRepository specialtiesRepository,
                                    ISectionRepository sectionRepository)
        {
            _specialtiesRepository = specialtiesRepository;
            _sectionRepository = sectionRepository;
        }

        [HttpGet]
        public IActionResult GetSpecialties()
        {
            var specialties = _specialtiesRepository.GetSpecialties();

            if (!ModelState.IsValid)
                return BadRequest();

            var specialtiesDto = new List<SpecialtiesDto>();

            foreach (var specialtie in specialties)
            {
                specialtiesDto.Add(new SpecialtiesDto
                {
                    Id = specialtie.Id,
                    Name = specialtie.Name,
                    DatePublished = specialtie.DatePublished
                });
            }
            return Ok(specialtiesDto);
        }

        [HttpGet("{specialtiesId}", Name = "GetSpecialty")]
        public IActionResult GetSpecialty(int specialtiesId)
        {
            if (!_specialtiesRepository.Exists(specialtiesId))
                return NotFound();

            var specialtie = _specialtiesRepository.GetSpecialty(specialtiesId);

            if (!ModelState.IsValid)
                return BadRequest();

            var specialtiesDto = new SpecialtiesDto
            {
                Id = specialtie.Id,
                Name = specialtie.Name,
                DatePublished = specialtie.DatePublished
            };
            return Ok(specialtiesDto);
        }

        [HttpGet("{specialtiesId}/student")]
        public IActionResult GetStudents(int specialtiesId)
        {
            if (!_specialtiesRepository.Exists(specialtiesId))
                return NotFound();

            var students = _specialtiesRepository.GetStudents(specialtiesId);

            if (students.Count <= 0)
                return BadRequest("Specialties Ddon't have Students");

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
                    NameFamily = student.NameFamily,
                    DatePublished = student.DatePublished
                });
            }
            return Ok(studentDto);
        }

        [HttpGet("{specialtiesId}/teacher")]
        public IActionResult GetTeachers(int specialtiesId)
        {
            if (!_specialtiesRepository.Exists(specialtiesId))
                return NotFound();

            var teachers = _specialtiesRepository.GetTeachers(specialtiesId);

            if (teachers.Count <= 0)
                return BadRequest("Specialties don't have Teachers");

            if (!ModelState.IsValid)
                return BadRequest();

            var teacherDto = new List<TeacherDto>();

            foreach(var teacher in teachers)
            {
                teacherDto.Add(new TeacherDto
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    NameFamily = teacher.NameFamily,
                    DatePublished = teacher.DatePublished
                });
            }
            return Ok(teacherDto);
        }

        [HttpGet("{specialtiesId}/book")]
        public IActionResult GetBooks(int specialtiesId)
        {
            if (!_specialtiesRepository.Exists(specialtiesId))
                return NotFound();

            var books = _specialtiesRepository.GetBooks(specialtiesId);

            if (books.Count <= 0)
                return BadRequest("Specialties don't have Books");

            if (!ModelState.IsValid)
                return BadRequest();

            var bookDto = new List<BookDto>();

            foreach(var book in books)
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
        

        [HttpPost]
        public IActionResult CreateSpecialties([FromBody]Specialties createToSpecialties)
        {
            if (createToSpecialties == null)
                return BadRequest(ModelState);

            if(_specialtiesRepository.DuplicateSpecialties(createToSpecialties.Name , createToSpecialties.Id))
            {
                ModelState.AddModelError("", $"Specialties {createToSpecialties.Name} is already exists");
                return StatusCode(422, ModelState);
            }

            if (createToSpecialties.TheSections == null)
                return BadRequest("Please enter Sections");

            if (!_sectionRepository.Exists(createToSpecialties.TheSections.Id))
                return NotFound();

            createToSpecialties.TheSections = _sectionRepository.GetById(createToSpecialties.TheSections.Id);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_specialtiesRepository.CreateSpecialties(createToSpecialties))
            {
                ModelState.AddModelError("", $"Someting went wrong saveing {createToSpecialties.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetSpecialty", new { specialtiesId = createToSpecialties.Id }, createToSpecialties);
        }
        
        [HttpPut("{specialtiesId}")]
        public IActionResult UpdateSpecialties(int specialtiesId , [FromBody]Specialties updateToSpecialties)
        {
            if (updateToSpecialties.Id != specialtiesId)
                return BadRequest();

            if (updateToSpecialties == null)
                return BadRequest(ModelState);

            if (!_specialtiesRepository.Exists(updateToSpecialties.Id))
                return NotFound();

            if(_specialtiesRepository.DuplicateSpecialties(updateToSpecialties.Name , updateToSpecialties.Id))
            {
                ModelState.AddModelError("", $"The Specialties {updateToSpecialties.Name} is already exists");
                return StatusCode(422, ModelState);
            }

            if (updateToSpecialties.TheSections == null)
                return BadRequest("Please enter Sections");

            if (!_sectionRepository.Exists(updateToSpecialties.TheSections.Id))
                return NotFound();

            updateToSpecialties.TheSections = _sectionRepository.GetById(updateToSpecialties.TheSections.Id);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_specialtiesRepository.UpdateSpecialties(updateToSpecialties))
            {
                ModelState.AddModelError("", $"Something went wrong Updating {updateToSpecialties.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{specialtiesId}")]
        public IActionResult DeleteSpecialties(int specialtiesId)
        {
            if (specialtiesId == 0)
                return BadRequest();

            if (!_specialtiesRepository.Exists(specialtiesId))
                return NotFound();

            var deleteToSpecialties = _specialtiesRepository.GetSpecialty(specialtiesId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_specialtiesRepository.DeleteSpecialties(deleteToSpecialties))
            {
                ModelState.AddModelError("", $"Something went wrong deleting");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
