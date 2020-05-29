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
    public class StudentController : Controller
    {
        private IStudentRepository _studentRepository;
        private ISupervisorRepository _supervisorRepository;
        private ISpecialtiesRepository _specialtiesRepository;
        public StudentController(IStudentRepository studentRepository,
                                 ISupervisorRepository supervisorRepository,
                                 ISpecialtiesRepository specialtiesRepository)
        {
            _studentRepository = studentRepository;
            _supervisorRepository = supervisorRepository;
            _specialtiesRepository = specialtiesRepository;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentRepository.GetStudents();

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

        [HttpGet("{studentId}", Name = "GetStudent")]
        public IActionResult GetStudentById(int studentId)
        {
            if (!_studentRepository.Exists(studentId))
                return NotFound();

            var student = _studentRepository.GetStudentById(studentId);

            if (!ModelState.IsValid)
                return BadRequest();

            var studentDto = new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                NameFamily = student.NameFamily,
                DatePublished = student.DatePublished
            };
            return Ok(studentDto);
        }

        [HttpGet("{studentId}/Specialties")]
        public IActionResult GetSpecialtiesByStudent(int studentId)
        {
            if (!_studentRepository.Exists(studentId))
                return NotFound();

            var specialties = _studentRepository.GetSpecialtiesByStudent(studentId);

            if (!ModelState.IsValid)
                return BadRequest();

            var specialtiesDto = new SpecialtiesDto
            {
                Id = specialties.Id,
                Name = specialties.Name,
                DatePublished = specialties.DatePublished
            };
            return Ok(specialtiesDto);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody]Students createToStudents)
        {
            if (createToStudents == null)
                return BadRequest(ModelState);

            if (createToStudents.supervisor == null)
                return BadRequest();

            if (!_supervisorRepository.Exists(createToStudents.supervisor.Id))
                return NotFound();

            createToStudents.supervisor = _supervisorRepository.GetSupervisor(createToStudents.supervisor.Id);

            if (createToStudents.specialtie == null)
                return BadRequest();

            if (!_specialtiesRepository.Exists(createToStudents.specialtie.Id))
                return NotFound();

            createToStudents.specialtie = _specialtiesRepository.GetSpecialty(createToStudents.specialtie.Id);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_studentRepository.CreateStudent(createToStudents))
            {
                ModelState.AddModelError("", "Something went wrong Saving");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetStudent", new { studentId = createToStudents.Id } , createToStudents) ;
        }

        [HttpPut("{studentId}")]
        public IActionResult UpdateStudents(int studentId ,[FromBody] Students updateToStudents)
        {
            if (updateToStudents == null)
                return BadRequest(ModelState);

            if (updateToStudents.Id != studentId)
                return BadRequest(ModelState);

            if (!_studentRepository.Exists(studentId))
                return NotFound();

            if (!_supervisorRepository.Exists(updateToStudents.supervisor.Id))
                return NotFound();

            updateToStudents.supervisor = _supervisorRepository.GetSupervisor(updateToStudents.supervisor.Id);

            if (!_specialtiesRepository.Exists(updateToStudents.specialtie.Id))
                return NotFound();

            updateToStudents.specialtie = _specialtiesRepository.GetSpecialty(updateToStudents.specialtie.Id);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_studentRepository.UpdateStudents(updateToStudents))
            {
                ModelState.AddModelError("", " Something went wrong Updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudents(int studentId)
        {
            if (!_studentRepository.Exists(studentId))
                return NotFound();

            var deleteToStudent = _studentRepository.GetStudentById(studentId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_studentRepository.DeleteStudents(deleteToStudent))
            {
                ModelState.AddModelError("", "Something went wrong Deleting");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
