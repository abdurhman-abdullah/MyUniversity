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
    public class DepartmentDirectorsController : Controller
    {
        private IDepartmentDirectorsRepository _departmentDirectorsRepository;
        private ISupervisorRepository _supervisorRepository;
        public DepartmentDirectorsController(IDepartmentDirectorsRepository departmentDirectorsRepository , ISupervisorRepository supervisorRepository)
        {
            _departmentDirectorsRepository = departmentDirectorsRepository;
            _supervisorRepository = supervisorRepository;
        }

        [HttpGet]
        public IActionResult GetDepartmentDirectors()
        {
            var departments = _departmentDirectorsRepository.GetDepartmentDirectors();

            if (!ModelState.IsValid)
                return BadRequest();

            var departmentDto = new List<DepartmentDirectorsDto>();

            foreach (var department in departments)
            {
                departmentDto.Add(new DepartmentDirectorsDto
                {
                    Id = department.Id,
                    FirstName = department.FirstName,
                    LastName = department.LastName,
                    NameFamily = department.NameFamily,
                    DatePublished = department.DatePublished
                });
            }
            return Ok(departmentDto);
        }

        [HttpGet("{departmentDirectorsId}" , Name = "GetDepartmentDirectors")]
        public IActionResult GetDepartmentDirectors(int departmentDirectorsId)
        {
            if (!_departmentDirectorsRepository.Exists(departmentDirectorsId))
                return NotFound();

            var department = _departmentDirectorsRepository.GetdepartmentDirector(departmentDirectorsId);
           
            if (!ModelState.IsValid)
                return BadRequest();

            var departmentDto = new DepartmentDirectorsDto
            {
                Id = department.Id,
                FirstName = department.FirstName,
                LastName = department.LastName,
                NameFamily = department.NameFamily,
                DatePublished = department.DatePublished
            };

            return Ok(departmentDto);
        }

        [HttpGet("{departmentdirectorId}/Supervisor")]
        public IActionResult GetsupervisorsBydepartmentDirector(int departmentdirectorId)
        {
            if (!_departmentDirectorsRepository.Exists(departmentdirectorId))
                return NotFound();

            var supervisors = _departmentDirectorsRepository.GetsupervisorsBydepartmentDirector(departmentdirectorId);

            if (!ModelState.IsValid)
                return BadRequest();

            var supervisorDto = new List<SupervisorDto>();

            foreach (var supervisor in supervisors)
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

        [HttpGet("departmentdirector/{supervisorId}")]
        public IActionResult GetdepartmentDirectorbysupervisors(int supervisorId)
        {
            if (!_supervisorRepository.Exists(supervisorId))
                return NotFound();

            var department = _departmentDirectorsRepository.GetdepartmentDirectorbysupervisors(supervisorId);

            if (!ModelState.IsValid)
                return BadRequest();

            var departmentdirectorDto = new DepartmentDirectorsDto
            {
                Id = department.Id,
                FirstName = department.FirstName,
                LastName = department.LastName,
                NameFamily = department.NameFamily,
                DatePublished = department.DatePublished
            };
            return Ok(departmentdirectorDto);
        }

        [HttpPost]
        public IActionResult CreateDepartmentDirectors([FromBody]DepartmentDirectors CreatedepartmentDirectors)
        {
            if (CreatedepartmentDirectors == null)
                return BadRequest(ModelState);
            
            if(_departmentDirectorsRepository.DuplicateDepartmentDirector(CreatedepartmentDirectors.FirstName , CreatedepartmentDirectors.LastName , CreatedepartmentDirectors.NameFamily , CreatedepartmentDirectors.Id))
            {
                ModelState.AddModelError("" , $"DepartmentDirector {CreatedepartmentDirectors.FirstName} " +
                                                                 $"{CreatedepartmentDirectors.LastName} " +
                                                                 $"{CreatedepartmentDirectors.NameFamily} is alreade exists");
                return StatusCode(422 , ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_departmentDirectorsRepository.CreateDepartmentDirector(CreatedepartmentDirectors))
            {
                ModelState.AddModelError("", $"Something went wrong saving {CreatedepartmentDirectors.FirstName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetDepartmentDirectors", new { departmentDirectorsId = CreatedepartmentDirectors.Id } , CreatedepartmentDirectors);
        }

        [HttpPut("{departmentDirectorsId}")]
        public IActionResult UpdateDepartmentDirectors([FromBody]DepartmentDirectors updatedepartmentDirectors ,int departmentDirectorsId)
        {
            if (updatedepartmentDirectors.Id != departmentDirectorsId)
                return BadRequest();

            if (!_departmentDirectorsRepository.Exists(departmentDirectorsId))
                return NotFound();

            if (updatedepartmentDirectors == null)
                return BadRequest();

            if(_departmentDirectorsRepository.DuplicateDepartmentDirector(updatedepartmentDirectors.FirstName , 
                                                                          updatedepartmentDirectors.LastName , 
                                                                          updatedepartmentDirectors.NameFamily ,
                                                                          departmentDirectorsId))
            {
                ModelState.AddModelError("", $"DepartmentDirector {updatedepartmentDirectors.FirstName} " +
                                                                $"{updatedepartmentDirectors.LastName} " +
                                                                $"{updatedepartmentDirectors.NameFamily} is alreade exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_departmentDirectorsRepository.UpdateDepartmentDirector(updatedepartmentDirectors))
            {
                ModelState.AddModelError("" , $"Something went wrong Updating {updatedepartmentDirectors.FirstName}" +
                                              $"{updatedepartmentDirectors.LastName}" + 
                                              $"{updatedepartmentDirectors.NameFamily}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{departmentDirectorsId}")]
        public IActionResult DeleteDepartmentDirectors(int departmentDirectorsId)
        {
            if (!_departmentDirectorsRepository.Exists(departmentDirectorsId))
                return NotFound();

            var departmentDirectors = _departmentDirectorsRepository.GetdepartmentDirector(departmentDirectorsId);
            
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_departmentDirectorsRepository.DeleteDepartmentDirector(departmentDirectors))
            {
                ModelState.AddModelError("", $"Something went wrong Deleting {departmentDirectors.FirstName}" +
                                             $"{departmentDirectors.LastName}" +
                                             $"{departmentDirectors.NameFamily}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
