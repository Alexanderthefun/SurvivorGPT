using SurvivorGPT.Models;

namespace SurvivorGPT.Repositories
{
	public interface IInventoryRepository
	{
		Inventory GetCurrentUserInventory(int userId);
	}
}