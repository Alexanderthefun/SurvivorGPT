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
	public class UserProfileController : ControllerBase
	{
		private readonly IUserProfileRepository _userProfileRepository;

		public UserProfileController(IUserProfileRepository userProfileRepository)
		{
			_userProfileRepository = userProfileRepository;
		}

		//[Authorize]
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_userProfileRepository.GetAllUsers());
		}

		//[Authorize]
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var user = _userProfileRepository.GetById(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		//[Authorize]
		[HttpGet("DoesUserExist/{firebaseUserId}")]
		public IActionResult GetByFirebaseUserId(string firebaseUserId)
		{
			var user = _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		[HttpGet("Me")]
		public IActionResult Me()
		{
			var userProfile = GetCurrentUserProfile();
			if (userProfile == null)
			{
				return NotFound();
			}

			return Ok(userProfile);
		}
		private UserProfile GetCurrentUserProfile()
		{
			var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
		}

		//[Authorize]
		[HttpPost]
		public IActionResult Post(UserProfile user)
		{
			_userProfileRepository.Add(user);
			return CreatedAtAction("Get", new { id = user.Id }, user);
		}

		//[Authorize]
		[HttpPut("{id}")]
		public IActionResult Put(int id, UserProfile user)
		{
			if (id != user.Id)
			{
				return BadRequest();
			}

			_userProfileRepository.Update(user);
			return NoContent();
		}

		//[Authorize]
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_userProfileRepository.Delete(id);
			return NoContent();
		}
	}
}
