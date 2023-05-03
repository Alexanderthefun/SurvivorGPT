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
	public class InventoryController : ControllerBase
	{
		private readonly IInventoryRepository _inventoryRepository;

		public InventoryController(IInventoryRepository inventoryRepository)
		{
			_inventoryRepository = inventoryRepository;
		}

		[HttpGet]
		public IActionResult GetInventory(int userId)
		{
			return Ok(_inventoryRepository.GetCurrentUserInventory(userId));
		}
	}
}
