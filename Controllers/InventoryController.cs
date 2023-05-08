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

		//[Authorize]
		[HttpGet("{id}")]
		public IActionResult GetInventory(int userId)
		{
			return Ok(_inventoryRepository.GetCurrentUserInventory(userId));
		}

		//[Authorize]
		[HttpPost]
		public IActionResult AddInventory(Inventory inventory)
		{
			_inventoryRepository.AddInventory(inventory);
			return CreatedAtAction("Get", new { id =  inventory.Id }, inventory);
		}

		//[Authorize]
		[HttpPost("{food}")]
		public IActionResult Post(Food food)
		{
			_inventoryRepository.AddFood(food);
			return CreatedAtAction("Get", new { id = food.Id }, food);
		}

		//[Authorize]
		[HttpPut("{food}/{id}")]
		public IActionResult UpdateFood(int id, Food food)
		{
			if (id != food.Id)
			{
				return BadRequest();
			}

			_inventoryRepository.UpdateFood(food);
			return NoContent();
		}

		//[Authorize]
		[HttpPut("{tool}/{id}")]
		public IActionResult UpdateTool(int id, Tool tool)
		{
			if (id != tool.Id)
			{
				return BadRequest();
			}

			_inventoryRepository.UpdateTool(tool);
			return NoContent();
		}

		[HttpPut("{weapon}/{id}")]
		public IActionResult UpdateWeapon(int id, Weapon weapon)
		{
			if (id != weapon.Id)
			{
				return BadRequest();
			}

			_inventoryRepository.UpdateWeapon(weapon);
			return NoContent();
		}

		[HttpPut("{energy}/{id}")]
		public IActionResult UpdateEnergy(int id, Energy energy)
		{
			if (id != energy.Id)
			{
				return BadRequest();
			}

			_inventoryRepository.UpdateEnergy(energy);
			return NoContent();
		}

		[HttpPut("{miscellaneous}/{id}")]
		public IActionResult UpdateMiscellaneous(int id, Miscellaneous miscellaneous)
		{
			if (id != miscellaneous.Id)
			{
				return BadRequest();
			}

			_inventoryRepository.UpdateMiscellaneous(miscellaneous);
			return NoContent();
		}


		//TEST YOUR PUTS WITH TOOL, WEAPON, ENERGY, MISCELLANEOUS!!!
	}
}
