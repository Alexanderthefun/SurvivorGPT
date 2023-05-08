using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurvivorGPT.Models;
using SurvivorGPT.Repositories;
using System.Security.Claims;

namespace SurvivorGPT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CurriculumController : ControllerBase
	{
		private readonly ICurriculumRepository _curriculumRepository;

		public CurriculumController(ICurriculumRepository curriculumRepository)
		{
			_curriculumRepository = curriculumRepository;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int userId)
		{
			var curriculum = _curriculumRepository.GetCurrentCurriculum(userId);
			if (curriculum == null)
			{
				return NotFound();
			}
			return Ok(curriculum);
		}

		[HttpPost]
		public IActionResult Post(Curriculum curriculum)
		{
			_curriculumRepository.Add(curriculum);
			return CreatedAtAction("Get", new { id = curriculum.Id }, curriculum); ;
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, Curriculum curriculum)
		{
			if (id != curriculum.Id)
			{
				return BadRequest();
			}

			_curriculumRepository.Update(curriculum);
			return Ok(); 
		}

		//[Authorize]
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_curriculumRepository.Delete(id);
			return NoContent();
		}
	}
}
