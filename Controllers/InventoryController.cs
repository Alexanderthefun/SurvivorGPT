using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurvivorGPT.Models;
using SurvivorGPT.Repositories;
using System.Security.Claims;

namespace SurvivorGPT.Controllers
{
	[Route("api/[controller]")]
	//[Authorize]
	[ApiController]
	public class InventoryController : ControllerBase
	{
		private readonly IInventoryRepository _inventoryRepository;

		public InventoryController(IInventoryRepository inventoryRepository)
		{
			_inventoryRepository = inventoryRepository;
		}

		// ...::: GETS :::...
		[HttpGet("{id}")]
		public IActionResult GetInventory(int userId)
		{
			return Ok(_inventoryRepository.GetCurrentUserInventory(userId));
		}

		[HttpGet("getAllInvUserIds")]
		public IActionResult GetInventoryUserIds()
		{
			return Ok(_inventoryRepository.GetAllInventoryUserIds());
		}

		[HttpGet("tools")]
		public IActionResult GetTools()
		{
			return Ok(_inventoryRepository.GetTools());
		}

		[HttpGet("weapons")]
		public IActionResult GetWeapons()
		{
			return Ok(_inventoryRepository.GetWeapons());
		}

		[HttpGet("energy")]
		public IActionResult GetEnergies()
		{
			return Ok(_inventoryRepository.GetEnergies());
		}

		[HttpGet("miscellaneous")]
		public IActionResult GetMiscellaneousItems()
		{
			return Ok(_inventoryRepository.GetMiscellaneousItems());
		}

		// ...::: POSTS :::...
		[HttpPost("addInv")]
		public IActionResult AddInventory(Inventory inventory)
		{
			_inventoryRepository.AddInventory(inventory);
			return Ok(inventory);
		}

		[HttpPost("food")]
		public IActionResult AddFood(InventoryFood inventoryFood)
		{
			_inventoryRepository.AddFood(inventoryFood);
			return CreatedAtAction("Get", new { id =  inventoryFood.Id }, inventoryFood);
		}

		[HttpPost("tool")]
		public IActionResult AddTool(InventoryTool inventoryTool)
		{
			_inventoryRepository.AddTool(inventoryTool);
			return Ok();
		}

		[HttpPost("weapon")]
		public IActionResult AddWeapon(InventoryWeapon inventoryWeapon)
		{
			_inventoryRepository.AddWeapon(inventoryWeapon);
			return CreatedAtAction("Get", new { id = inventoryWeapon.Id }, inventoryWeapon);
		}

		[HttpPost("energy")]
		public IActionResult AddEnergy(InventoryEnergy inventoryEnergy)
		{
			_inventoryRepository.AddEnergy(inventoryEnergy);
			return CreatedAtAction("Get", new { id = inventoryEnergy.Id }, inventoryEnergy);
		}

		[HttpPost("miscellaneous")]
		public IActionResult AddMiscellaneous(InventoryMiscellaneous inventoryMiscellaneous)
		{
			_inventoryRepository.AddMiscellaneous(inventoryMiscellaneous);
			return CreatedAtAction("Get", new { id = inventoryMiscellaneous.Id }, inventoryMiscellaneous);
		}

		[HttpPost("{food}")]
		public IActionResult Post(Food food)
		{
			_inventoryRepository.AddFoodType(food);
			return CreatedAtAction("Get", new { id = food.Id }, food);
		}


		// ...::: PUTS :::...
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


		// ...::: DELETES :::...
		[HttpDelete("deleteFood/{id}")]
		public IActionResult DeleteFood(int id)
		{
			_inventoryRepository.DeleteFood(id);
			return NoContent();
		}

		[HttpDelete("deleteFoodType/{id}")]
		public IActionResult DeleteFoodType(int id)
		{
			_inventoryRepository.DeleteFoodType(id);
			return NoContent();
		}

		[HttpDelete("deleteTool/{id}")]
		public IActionResult DeleteTool(int id)
		{
			_inventoryRepository.DeleteTool(id);
			return NoContent();
		}

		[HttpDelete("deleteWeapon/{id}")]
		public IActionResult DeleteWeapon(int id)
		{
			_inventoryRepository.DeleteWeapon(id);
			return NoContent();
		}

		[HttpDelete("deleteEnergy/{id}")]
		public IActionResult DeleteEnergy(int id)
		{
			_inventoryRepository.DeleteEnergy(id);
			return NoContent();
		}

		[HttpDelete("deleteMiscellaneous/{id}")]
		public IActionResult DeleteMiscellaneous(int id)
		{
			_inventoryRepository.DeleteMiscellaneous(id);
			return NoContent();
		}
	}
}
