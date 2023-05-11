using SurvivorGPT.Models;
using System.Collections.Generic;

namespace SurvivorGPT.Repositories
{
	public interface IInventoryRepository
	{
		public void AddInventory(Inventory inventory);
		public void AddFood(InventoryFood inventoryFood);
		public void AddTool(InventoryTool inventoryTool);
		public void AddWeapon(InventoryWeapon inventoryWeapon);
		public void AddEnergy(InventoryEnergy inventoryEnergy);
		public void AddMiscellaneous(InventoryMiscellaneous inventoryMiscellaneous);
		public Food AddFoodType(Food food);

		public void UpdateFood(Food food);
		public void UpdateTool(Tool tool);
		public void UpdateWeapon(Weapon weapon);
		public void UpdateEnergy(Energy energy);
		public void UpdateMiscellaneous(Miscellaneous miscellaneous);

		public void DeleteFood(int InvId, int foodId);
		public void DeleteFoodType(int id);
		public void DeleteTool(int ToolId, int InvId);
		public void DeleteWeapon(int WeaponId, int InvId);
		public void DeleteEnergy(int EnergyId, int InvId);
		public void DeleteMiscellaneous(int MiscellaneousId, int InvId);

		public Inventory GetCurrentUserInventory(int userId);
		public List<Inventory> GetAllInventoryUserIds();
		public List<Tool> GetTools();
		public List<Weapon> GetWeapons();
		public List<Energy> GetEnergies();
		public List<Miscellaneous> GetMiscellaneousItems();

	}
}