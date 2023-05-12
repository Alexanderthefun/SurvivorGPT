using SurvivorGPT.Models;
using System.Collections.Generic;

namespace SurvivorGPT.Repositories
{
	public interface ICategoryRepository
	{
		void AddCategory(Category category);
		void DeleteCategory(int id);
		List<Category> GetAllCategories();
		Category GetCategoryById(int id);
		void UpdateCategory(Category category);
	}
}