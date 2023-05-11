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
	public class InventoryController : ControllerBase
	{
		private readonly IInventoryRepository _inventoryRepository;

		public InventoryController(IInventoryRepository inventoryRepository)
		{
			_inventoryRepository = inventoryRepository;
		}

		// ...::: GETS :::...
		[HttpGet("{id}")]
		public IActionResult GetInventory(int id)
		{
			var inventoryResult = _inventoryRepository.GetCurrentUserInventory(id);

			if (inventoryResult != null)
			{
				return Ok(inventoryResult);
			}
			else
			{
				 return Ok(new { data = (object)null });
			}
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
			return Ok();
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
			return Ok();
		}

		[HttpPost("energy")]
		public IActionResult AddEnergy(InventoryEnergy inventoryEnergy)
		{
			_inventoryRepository.AddEnergy(inventoryEnergy);
			return Ok();
		}

		[HttpPost("miscellaneous")]
		public IActionResult AddMiscellaneous(InventoryMiscellaneous inventoryMiscellaneous)
		{
			_inventoryRepository.AddMiscellaneous(inventoryMiscellaneous);
			return Ok();
		}

		[HttpPost("foodType")]
		public IActionResult Post(Food food)
		{
			_inventoryRepository.AddFoodType(food);
			return Ok(food);
		}


		// ...::: PUTS :::...
		[HttpPut("food/{id}")]
		public IActionResult UpdateFood(Food food, int id)
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
		[HttpDelete("deleteFood/{invId}/{foodId}")]
		public IActionResult DeleteFood(int InvId, int foodId)
		{
			_inventoryRepository.DeleteFood(InvId, foodId);
			return NoContent();
		}

		[HttpDelete("deleteFoodType/{id}")]
		public IActionResult DeleteFoodType(int id)
		{
			_inventoryRepository.DeleteFoodType(id);
			return NoContent();
		}

		[HttpDelete("deleteTool/{toolId}/{invId}")]
		public IActionResult DeleteTool(int toolId, int invId)
		{
			_inventoryRepository.DeleteTool(toolId, invId);
			return NoContent();
		}

		[HttpDelete("deleteWeapon/{weaponId}/{invId}")]
		public IActionResult DeleteWeapon(int weaponId, int invId)
		{
			_inventoryRepository.DeleteWeapon(weaponId, invId);
			return NoContent();
		}

		[HttpDelete("deleteEnergy/{energyId}/{invId}")]
		public IActionResult DeleteEnergy(int energyId, int invId)
		{
			_inventoryRepository.DeleteEnergy(energyId, invId);
			return NoContent();
		}

		[HttpDelete("deleteMiscellaneous/{miscellaneousId}/{invId}")]
		public IActionResult DeleteMiscellaneous(int miscellaneousId, int invId)
		{
			_inventoryRepository.DeleteMiscellaneous(miscellaneousId, invId);
			return NoContent();
		}
	}
}
