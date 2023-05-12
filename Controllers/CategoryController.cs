using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurvivorGPT.Models;
using SurvivorGPT.Repositories;

namespace SurvivorGPT.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		// ...::: GETS :::...
		[HttpGet("{id}")]
		public IActionResult GetCategory(int id)
		{
			var categoryResult = _categoryRepository.GetCategoryById(id);

			if (categoryResult != null)
			{
				return Ok(categoryResult);
			}
			else
			{
				return Ok(new { data = (object)null });
			}
		}

		[HttpGet("getAll")]
		public IActionResult GetAllCategories()
		{
			return Ok(_categoryRepository.GetAllCategories());
		}

		// ...::: POSTS :::...
		[HttpPost("add")]
		public IActionResult AddCategory(Category category)
		{
			_categoryRepository.AddCategory(category);
			return Ok(category);
		}

		// ...::: PUTS :::...
		[HttpPut("{id}")]
		public IActionResult UpdateCategory(int id, Category category)
		{
			if (id != category.Id)
			{
				return BadRequest();
			}

			_categoryRepository.UpdateCategory(category);
			return NoContent();
		}

		// ...::: DELETES :::...
		[HttpDelete("{id}")]
		public IActionResult DeleteCategory(int id)
		{
			_categoryRepository.DeleteCategory(id);
			return NoContent();
		}
	}
}