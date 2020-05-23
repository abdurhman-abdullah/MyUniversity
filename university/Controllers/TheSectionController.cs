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
    public class TheSectionController : Controller
    {
        private ISectionRepository _sectionRepository;
        private IDepartmentDirectorsRepository _departmentDirectorsRepository;
        public TheSectionController(ISectionRepository sectionRepository, IDepartmentDirectorsRepository departmentDirectorsRepository)
        {
            _sectionRepository = sectionRepository;
            _departmentDirectorsRepository = departmentDirectorsRepository;
        }

        [HttpGet]
        public IActionResult GetSection()
        {

            var sections = _sectionRepository.GetTheSections();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sectionsDto = new List<SectionDto>();

            foreach (var section in sections)
            {
                sectionsDto.Add(new SectionDto
                {
                    Id = section.Id,
                    Name = section.Name,
                    DatePublished = section.DatePublished
                }
                );

            }
            return Ok(sectionsDto);
        }

        [HttpGet("{sectionId}" , Name = "GetSection")]
        public IActionResult GetSection(int sectionId)
        {
            if (!_sectionRepository.Exists(sectionId))
                return NotFound();

            var section = _sectionRepository.GetById(sectionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sectionDto = new SectionDto
            {
                Id = section.Id,
                Name = section.Name,
                DatePublished = section.DatePublished
            };

            return Ok(sectionDto);
        }

        [HttpGet("section/{departmentDirectorId}")]
        public IActionResult GetSectionsOfADepartment(int departmentDirectorId)
        {
            if (!_departmentDirectorsRepository.Exists(departmentDirectorId))
                return NotFound();

            var sections = _sectionRepository.GetSectionsOfADepartment(departmentDirectorId);

            if (!ModelState.IsValid)
                return BadRequest();

            var sectionDto = new SectionDto
            {
                Id = sections.Id,
                Name = sections.Name,
                DatePublished = sections.DatePublished
            };

            return Ok(sectionDto);
        }

        [HttpGet("{sectionId}/department")]
        public IActionResult GetDepartmentBySections(int sectionId)
        {
            if (!_sectionRepository.Exists(sectionId))
                return NotFound();

            var departments = _sectionRepository.GetDepartmentBySections(sectionId);

            if(departments == null)
            {
                ModelState.AddModelError("", $"This Section Not Have manager");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var departmentDto = new DepartmentDirectorsDto 
            {
                Id = departments.Id,
                FirstName = departments.FirstName,
                LastName = departments.LastName,
                NameFamily = departments.NameFamily,
                DatePublished = departments.DatePublished

            };
            return Ok(departmentDto);

        }

        [HttpPost]
        public IActionResult CreateSection([FromBody]TheSections sectionToCreate)
        {
            if (sectionToCreate == null)
                return BadRequest(ModelState);

            if (_sectionRepository.DuplicateSection(sectionToCreate.Name , sectionToCreate.Id))
            {
                ModelState.AddModelError("", $"Section {sectionToCreate.Name} is already exists");
                return StatusCode(422, ModelState);
            }

            if (!_departmentDirectorsRepository.Exists(sectionToCreate.DepartmentDirectors.Id))
                return NotFound();

            sectionToCreate.DepartmentDirectors =  _departmentDirectorsRepository.GetdepartmentDirector(sectionToCreate.DepartmentDirectors.Id);

            if (_sectionRepository.DuplicateDepartment(sectionToCreate.DepartmentDirectors.Id , sectionToCreate.Id))
            {
                ModelState.AddModelError("", $"The DepartmentDirectors {sectionToCreate.DepartmentDirectors.Id} has Section");
                return StatusCode(406, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_sectionRepository.CreattheSection(sectionToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {sectionToCreate.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetSection", new { sectionId = sectionToCreate.Id }, sectionToCreate);
        }

        [HttpPut("{sectionId}")]
        public IActionResult UdateSection([FromBody]TheSections sectionsToUpdate , int sectionId)
        {
            if (!_sectionRepository.Exists(sectionId))
                return NotFound();

            if (sectionId != sectionsToUpdate.Id)
                return BadRequest();

            if (sectionsToUpdate == null)
                return BadRequest(ModelState);

            if(_sectionRepository.DuplicateSection(sectionsToUpdate.Name , sectionsToUpdate.Id))
            {
                ModelState.AddModelError("", $"Section {sectionsToUpdate.Name} is already exists");
                return StatusCode(422, ModelState);
            }

            if (!_departmentDirectorsRepository.Exists(sectionsToUpdate.DepartmentDirectors.Id))
                return NotFound();

            sectionsToUpdate.DepartmentDirectors = _departmentDirectorsRepository.GetdepartmentDirector(sectionsToUpdate.DepartmentDirectors.Id);

             if (_sectionRepository.DuplicateDepartment(sectionsToUpdate.DepartmentDirectors.Id , sectionId))
            {
                ModelState.AddModelError("", $"The DepartmentDirectors {sectionsToUpdate.DepartmentDirectors.Id} has Section");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_sectionRepository.UpdatetheSection(sectionsToUpdate))
            {
                ModelState.AddModelError("", $"Something went wrong updating {sectionsToUpdate.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{sectionId}")]
        public IActionResult DeleteSection(int sectionId)
        {
            if (!_sectionRepository.Exists(sectionId))
                return NotFound();

            var section = _sectionRepository.GetById(sectionId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_sectionRepository.DeletetheSection(section))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {section.Name}");
                return StatusCode(500, ModelState);         
            }
            return NoContent();
        }
    }
}
