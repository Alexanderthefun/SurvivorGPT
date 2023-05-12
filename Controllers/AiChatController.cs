using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurvivorGPT.Models;
using SurvivorGPT.Repositories;

namespace SurvivorGPT.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class AiChatController : ControllerBase
	{
		private readonly IAiChatRepository _aiChatRepository;

		public AiChatController(IAiChatRepository aiChatRepository)
		{
			_aiChatRepository = aiChatRepository;
		}

		// ...::: GETS :::...
		[HttpGet("{id}")]
		public IActionResult GetAiChat(int id)
		{
			var aiChatResult = _aiChatRepository.GetAiChatById(id);

			if (aiChatResult != null)
			{
				return Ok(aiChatResult);
			}
			else
			{
				return Ok(new { data = (object)null });
			}
		}

		[HttpGet("getAll")]
		public IActionResult GetAllAiChats()
		{
			return Ok(_aiChatRepository.GetAllAiChats());
		}

		[HttpGet("user/{userId}")]
		public IActionResult GetAllChatsByUserId(int userId)
		{
			return Ok(_aiChatRepository.GetAllChatsByUserId(userId));
		}

		[HttpGet("category/{categoryId}")]
		public IActionResult GetAllChatsByCategoryId(int categoryId)
		{
			return Ok(_aiChatRepository.GetAllChatsByCategoryId(categoryId));
		}

		// ...::: POSTS :::...
		[HttpPost("add")]
		public IActionResult AddAiChat(AiChat aiChat)
		{
			_aiChatRepository.AddAiChat(aiChat);
			return Ok(aiChat);
		}

		// ...::: PUTS :::...
		[HttpPut("{id}")]
		public IActionResult UpdateAiChat(int id, AiChat aiChat)
		{
			if (id != aiChat.Id)
			{
				return BadRequest();
			}

			_aiChatRepository.UpdateAiChat(aiChat);
			return NoContent();
		}

		// ...::: DELETES :::...
		[HttpDelete("{id}")]
		public IActionResult DeleteAiChat(int id)
		{
			_aiChatRepository.DeleteAiChat(id);
			return NoContent();
		}
	}
}