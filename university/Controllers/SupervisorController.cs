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
    public class SupervisorController : Controller
    {
        private ISupervisorRepository _supervisorRepository;
        private IDepartmentDirectorsRepository _departmentDirectorsRepository;
        private IStudentRepository _studentRepository;
        public SupervisorController(ISupervisorRepository supervisorRepository , 
                                    IDepartmentDirectorsRepository departmentDirectorsRepository ,
                                    IStudentRepository studentRepository)
        {
            _supervisorRepository = supervisorRepository;
            _departmentDirectorsRepository = departmentDirectorsRepository;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult GetSupervisors()
        {
            var supervisors = _supervisorRepository.GetSupervisors();

            if (!ModelState.IsValid)
                return BadRequest();

            var supervisorDto = new List<SupervisorDto>();

            foreach(var supervisor in supervisors)
            {
                supervisorDto.Add(new SupervisorDto
                {
                    Id = supervisor.Id,
                    FirstName = supervisor.FirstName,
                    LastName = supervisor.LastName,
                    NameFamily = supervisor.NameFamily,
                    DatePublished = supervisor.DatePublished
                });
            }
            return Ok(supervisorDto);
        }

        [HttpGet("{supervisorId}" , Name = "GetSupervisor")]
        public IActionResult GetSupervisor(int supervisorId)
        {
            if (!_supervisorRepository.Exists(supervisorId))
                return NotFound();

            var supervisor = _supervisorRepository.GetSupervisor(supervisorId);

            if (!ModelState.IsValid)
                return BadRequest();

            var supervisorDto = new SupervisorDto
            {
                Id = supervisor.Id,
                FirstName = supervisor.FirstName,
                LastName = supervisor.LastName,
                NameFamily = supervisor.NameFamily,
                DatePublished = supervisor.DatePublished
            };
            return Ok(supervisorDto);
        }

        [HttpGet("{supervisorId}/student")]
        public IActionResult GetStudentsBySupervisor(int supervisorId)
        {
            if (!_supervisorRepository.Exists(supervisorId))
                return NotFound();

            var students = _supervisorRepository.GetStudentsBySupervisor(supervisorId);

            if (students.Count <= 0)
            {
                ModelState.AddModelError("", "The supervisor don't have students");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var studentDto = new List<StudentDto>();

            foreach(var student in students)
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

        [HttpGet("supervisor/{studentId}")]
        public IActionResult GetSupervisorByStudent(int studentId)
        {
            if (!_studentRepository.Exists(studentId))
                return NotFound();

            var supervisor = _supervisorRepository.GetSupervisorByStudent(studentId);

            if (!ModelState.IsValid)
                return BadRequest();

            var supervisorDto = new SupervisorDto
            {
                Id = supervisor.Id,
                FirstName = supervisor.FirstName,
                LastName = supervisor.LastName,
                NameFamily = supervisor.NameFamily,
                DatePublished = supervisor.DatePublished
            };

            return Ok(supervisorDto);
        }

        [HttpPost]
       public IActionResult CreateSupervisor([FromBody]Supervisor CreatTosupervisor)
        {
            if (CreatTosupervisor == null)
                return BadRequest();

            if(_supervisorRepository.DuplicateSupervisor(CreatTosupervisor.FirstName , 
                                                         CreatTosupervisor.LastName , 
                                                         CreatTosupervisor.NameFamily ,
                                                         CreatTosupervisor.Id))
            {
                ModelState.AddModelError("", $"Supervisor {CreatTosupervisor.FirstName}" +
                                                            $"{CreatTosupervisor.LastName}" +
                                                            $"{CreatTosupervisor.NameFamily} is already exists");
                return StatusCode(422, ModelState);
            }

            if (!_departmentDirectorsRepository.Exists(CreatTosupervisor.Department.Id))
                return NotFound();

            CreatTosupervisor.Department = _departmentDirectorsRepository.GetdepartmentDirector(CreatTosupervisor.Department.Id);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_supervisorRepository.CreateSupervisor(CreatTosupervisor))
            {
                ModelState.AddModelError("", $"Someting went wrong saveing {CreatTosupervisor.FirstName}" +
                                                                         $"{CreatTosupervisor.LastName}" +
                                                                         $"{CreatTosupervisor.NameFamily}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetSupervisor", new { supervisorId = CreatTosupervisor.Id }, CreatTosupervisor);
        }

        [HttpPut("{supervisorId}")]
        public IActionResult UpdateSupervisor(int supervisorId, [FromBody]Supervisor UpdateTosupervisor)
        {
            if (supervisorId != UpdateTosupervisor.Id)
                return BadRequest(ModelState);

            if (!_supervisorRepository.Exists(supervisorId))
                return NotFound();

            if (UpdateTosupervisor == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_supervisorRepository.DuplicateSupervisor(UpdateTosupervisor.FirstName,
                                                        UpdateTosupervisor.LastName,
                                                        UpdateTosupervisor.NameFamily,
                                                        UpdateTosupervisor.Id))
            {
                ModelState.AddModelError("", $"Supervisor {UpdateTosupervisor.FirstName}" +
                                                            $"{UpdateTosupervisor.LastName}" +
                                                            $"{UpdateTosupervisor.NameFamily} is already exists");
                return StatusCode(422, ModelState);
            }

            if (!_departmentDirectorsRepository.Exists(UpdateTosupervisor.Department.Id))
                return NotFound();

            UpdateTosupervisor.Department = _departmentDirectorsRepository.GetdepartmentDirector(UpdateTosupervisor.Department.Id);

            if (!_supervisorRepository.UpdateSupervisor(UpdateTosupervisor))
            {
                ModelState.AddModelError("", $"Something went wrong Updating {UpdateTosupervisor.FirstName}" +
                                                                            $"{UpdateTosupervisor.LastName}" +
                                                                            $"{UpdateTosupervisor.NameFamily}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{supervisorId}")]
        public IActionResult DeleteSupervisor(int supervisorId)
        {
            if (!_supervisorRepository.Exists(supervisorId))
                return NotFound();

            var supervisorid = _supervisorRepository.GetSupervisor(supervisorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_supervisorRepository.DeleteSupervisor(supervisorid))
            {
                ModelState.AddModelError("", $"Something went wrong Deleting {supervisorid.FirstName}" +
                                             $"{supervisorid.LastName}" +
                                             $"{supervisorid.NameFamily}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
