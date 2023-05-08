using SurvivorGPT.Models;

namespace SurvivorGPT.Repositories
{
	public interface IInventoryRepository
	{
		Inventory GetCurrentUserInventory(int userId);
		public void AddInventory(Inventory inventory);
		public void AddFood(Food food);
		public void UpdateFood(Food food);
		public void UpdateTool(Tool tool);
		public void UpdateWeapon(Weapon weapon);
		public void UpdateEnergy(Energy energy);
		public void UpdateMiscellaneous(Miscellaneous miscellaneous);

	}
}