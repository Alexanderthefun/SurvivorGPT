using SurvivorGPT.Models;

namespace SurvivorGPT.Repositories
{
	public interface IShelterRepository
	{
		void Add(Shelter shelter);
		void Delete(int id);
		Shelter GetCurrentShelter(int shelterId);
		void Update(Shelter shelter);
	}
}