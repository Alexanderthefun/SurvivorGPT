using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurvivorGPT.Models;
using SurvivorGPT.Repositories;
using System.Security.Claims;

namespace SurvivorGPT.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class ShelterController : ControllerBase
	{
		private readonly IShelterRepository _shelterRepository;

		public ShelterController(IShelterRepository shelterRepository)
		{
			_shelterRepository = shelterRepository;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var shelter = _shelterRepository.GetCurrentShelter(id);
			if (shelter == null)
			{
				return NotFound();
			}
			return Ok(shelter);
		}

		[HttpPost]
		public IActionResult Post(Shelter shelter)
		{
			_shelterRepository.Add(shelter);
			return CreatedAtAction("Get", new { id = shelter.Id }, shelter);
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, Shelter shelter)
		{
			if (id != shelter.Id)
			{
				return BadRequest();
			}

			_shelterRepository.Update(shelter);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_shelterRepository.Delete(id);
			return NoContent();
		}
	}
}
