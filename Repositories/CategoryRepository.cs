using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SurvivorGPT.Models;
using SurvivorGPT.Utils;

namespace SurvivorGPT.Repositories
{
	public class CategoryRepository : BaseRepository, ICategoryRepository
	{
		public CategoryRepository(IConfiguration configuration) : base(configuration) { }

		public List<Category> GetAllCategories()
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT Id, Name
                                        FROM Category";
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var categories = new List<Category>();
						while (reader.Read())
						{
							categories.Add(new Category()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								Name = DbUtils.GetString(reader, "Name")
							});
						}
						return categories;
					}
				}
			}
		}

		public Category GetCategoryById(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT Id, Name
                                        FROM Category
                                        WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Id", id);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						Category category = null;
						if (reader.Read())
						{
							category = new Category()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								Name = DbUtils.GetString(reader, "Name")
							};
						}
						return category;
					}
				}
			}
		}

		public void AddCategory(Category category)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"INSERT INTO Category
                                        (Name)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name)";
					DbUtils.AddParameter(cmd, "@Name", category.Name);

					category.Id = (int)cmd.ExecuteScalar();
				}
			}
		}

		public void UpdateCategory(Category category)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"UPDATE Category
                                        SET Name = @Name
                                        WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Name", category.Name);
					DbUtils.AddParameter(cmd, "@Id", category.Id);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public void DeleteCategory(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "DELETE FROM Category WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Id", id);
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}