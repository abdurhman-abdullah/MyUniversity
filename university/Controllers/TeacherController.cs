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
    public class TeacherController : Controller
    {
        private ITeachersRepository _teachersRepository;
        private IBookRepository _bookRepository;
        private IDepartmentDirectorsRepository _departmentDirectorsRepository;
        private ISpecialtiesRepository _specialtiesRepository;
        public TeacherController(ITeachersRepository teachersRepository,
                                 IBookRepository bookRepository,
                                 IDepartmentDirectorsRepository departmentDirectorsRepository,
                                 ISpecialtiesRepository specialtiesRepository)
        {
            _teachersRepository = teachersRepository;
            _bookRepository = bookRepository;
            _departmentDirectorsRepository = departmentDirectorsRepository;
            _specialtiesRepository = specialtiesRepository;
        }

        [HttpGet]
        public IActionResult GetTeachers()
        {

            var teachers = _teachersRepository.GetTeachers();

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
                    NameFamily = teacher.NameFamily,
                    DatePublished = teacher.DatePublished
                });
            }
            return Ok(teacherDto);
        }

        [HttpGet("{teacherId}", Name = "GetTeacher")]
        public IActionResult GetTeacher(int teacherId)
        {
            if (!_teachersRepository.Exists(teacherId))
                return NotFound();

            var teacher = _teachersRepository.GetTeacher(teacherId);

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

        [HttpGet("{teacherId}/divisions")]
        public IActionResult GetDivisions(int teacherId)
        {
            if (!_teachersRepository.Exists(teacherId))
                return NotFound();

            var divisions = _teachersRepository.GetDivisionsByTeacher(teacherId);

            if (!ModelState.IsValid)
                return BadRequest();

            var divisionsDto = new List<DivisionsDto>();

            foreach (var division in divisions)
            {
                divisionsDto.Add(new DivisionsDto
                {
                    Id = division.Id,
                    DivisionNo = division.DivisionNo
                });
            }
            return Ok(divisionsDto);
        }

        [HttpGet("teachers/{bookId}")]
        public IActionResult GetTeachersByBook(int bookId)
        {
            if (!_bookRepository.Exists(bookId))
                return NotFound();

            var teachers = _teachersRepository.GetTeachersByBook(bookId);

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
                    NameFamily = teacher.NameFamily,
                    DatePublished = teacher.DatePublished
                });
            }
            return Ok(teacherDto);
        }

        [HttpGet("{teacherId}/specialties")]
        public IActionResult specialtiesByTeacher(int teacherId)
        {
            if (!_teachersRepository.Exists(teacherId))
                return NotFound();

            var specialties = _teachersRepository.specialtiesByTeacher(teacherId);

            if (!ModelState.IsValid)
                return BadRequest();

            var specialtiesDto = new SpecialtiesDto
            {
                Id = specialties.Id,
                Name = specialties.Name
            };
            return Ok(specialtiesDto);
        }

        [HttpPost]
        public IActionResult CreateTeacher(Teachers createToteacher)
        {
            if (createToteacher == null)
                return BadRequest(ModelState);

            if(_teachersRepository.DublicateTeacher(createToteacher.FirstName , 
                                                    createToteacher.LastName,
                                                    createToteacher.NameFamily ,
                                                    createToteacher.Id))
            {
                ModelState.AddModelError("", $"This teacher {createToteacher.FirstName}" +
                                              $" {createToteacher.LastName}" +
                                              $" {createToteacher.NameFamily} is already exists");
                return StatusCode(422, ModelState);
            }

            if (createToteacher.departmentDirector == null)
                return BadRequest("Please entry DepartmentDirector");

            if (!_departmentDirectorsRepository.Exists(createToteacher.departmentDirector.Id))
                return NotFound();

            createToteacher.departmentDirector = _departmentDirectorsRepository.GetdepartmentDirector(createToteacher.departmentDirector.Id);

            if (createToteacher.Specialties == null)
                return BadRequest("Please entry Specialties");

            if (!_specialtiesRepository.Exists(createToteacher.Specialties.Id))
                return NotFound();

            createToteacher.Specialties = _specialtiesRepository.GetSpecialty(createToteacher.Specialties.Id);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_teachersRepository.CreateTeacher(createToteacher))
            {
                ModelState.AddModelError("" , $"Something wnt wrong saving {createToteacher.FirstName}" +
                                              $"{createToteacher.LastName}" +
                                              $"{createToteacher.NameFamily} ");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTeacher", new { teacherId = createToteacher.Id }, createToteacher);
        }

        [HttpPut("{teacherId}")]
        public IActionResult UdateTeacher(int teacherId , Teachers updateToteacher)
        {
            if (updateToteacher == null)
                return BadRequest(ModelState);

            if (updateToteacher.Id != teacherId)
                return BadRequest();

            if (!_teachersRepository.Exists(teacherId))
                return NotFound();

            if (_teachersRepository.DublicateTeacher(updateToteacher.FirstName,
                                                    updateToteacher.LastName,
                                                    updateToteacher.NameFamily,
                                                    updateToteacher.Id))
            {
                ModelState.AddModelError("", $"This teacher {updateToteacher.FirstName}" +
                                              $" {updateToteacher.LastName}" +
                                              $" {updateToteacher.NameFamily} is already exists");
                return StatusCode(422, ModelState);
            }

            if (updateToteacher.departmentDirector == null)
                return BadRequest("Please entry DepartmentDirector");

            if (!_departmentDirectorsRepository.Exists(updateToteacher.departmentDirector.Id))
                return NotFound();

            updateToteacher.departmentDirector = _departmentDirectorsRepository.GetdepartmentDirector(updateToteacher.departmentDirector.Id);

            if (updateToteacher.Specialties == null)
                return BadRequest("Please entry Specialties");

            if (!_specialtiesRepository.Exists(updateToteacher.Specialties.Id))
                return NotFound();

            updateToteacher.Specialties = _specialtiesRepository.GetSpecialty(updateToteacher.Specialties.Id);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_teachersRepository.UdateTeacher(updateToteacher))
            {
                ModelState.AddModelError("", $"Something went wrong updating {updateToteacher.FirstName}" +
                                              $" {updateToteacher.LastName}" +
                                              $" {updateToteacher.NameFamily}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{teacherId}")]
        public IActionResult DeleteTeacher(int teacherId)
        {
            if (!_teachersRepository.Exists(teacherId))
                return NotFound();

            var deleteToteacher = _teachersRepository.GetTeacher(teacherId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_teachersRepository.DeleteTeacher(deleteToteacher))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {deleteToteacher.FirstName}" +
                                             $"{deleteToteacher.LastName}" +
                                             $"{deleteToteacher.NameFamily}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
