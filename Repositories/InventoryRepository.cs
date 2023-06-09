﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SurvivorGPT.Models;
using SurvivorGPT.Utils;


namespace SurvivorGPT.Repositories
{
	public class InventoryRepository : BaseRepository, IInventoryRepository
	{
		public InventoryRepository(IConfiguration configuration) : base(configuration) { }

		public Inventory GetCurrentUserInventory(int userId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
						SELECT  i.Id, i.UserId,
								u.Id as UserProfileId, u.FirstName, u.LastName, u.City, u.State, u.Country,
								inf.Id, inf.InventoryId, inf.FoodId,
								f.Id, f.Name as FoodName, f.Count, f.Protein, f.FruitVeggieFungi,
								it.Id, it.InventoryId, it.ToolId,
								t.Id, t.Name ToolName,
								iw.Id, iw.InventoryId, iw.WeaponId,
								w.Id, w.Name WeaponName,
								ie.Id, ie.InventoryId, ie.EnergyId,
								e.Id, e.Name EnergyName,
								im.Id, im.InventoryId, im.MiscellaneousId,
								m.id, m.Name as MiscellaneousName
						FROM Inventory i
						LEFT JOIN UserProfile u on i.UserId = u.Id
						LEFT JOIN InventoryFood inf on inf.InventoryId = i.id
						LEFT JOIN Food f on inf.FoodId = f.Id
						LEFT JOIN InventoryTool it on it.InventoryId = i.Id
						LEFT JOIN Tool t on it.ToolId = t.Id
						LEFT JOIN InventoryWeapon iw on iw.InventoryId = i.Id
						LEFT JOIN Weapon w on iw.WeaponId = w.Id
						LEFT JOIN InventoryEnergy ie on ie.InventoryId = i.Id
						LEFT JOIN Energy e on ie.EnergyId = e.Id
						LEFT JOIN InventoryMiscellaneous im on im.InventoryId = i.Id
						LEFT JOIN Miscellaneous m on im.MiscellaneousId = m.Id
						WHERE u.Id = @userId";
					DbUtils.AddParameter(cmd, "@userId", userId);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						Inventory inventory = null;
						while (reader.Read())
						{
							if (inventory == null)
							{
								inventory = new Inventory()
								{
									Id = DbUtils.GetInt(reader, "Id"),
									UserId = DbUtils.GetInt(reader, "userId"),
									UserProfile = new UserProfile()
									{
										Id = DbUtils.GetInt(reader, "UserProfileId"),
										FirstName = DbUtils.GetString(reader, "FirstName"),
										LastName = DbUtils.GetString(reader, "LastName"),
										City = DbUtils.GetString(reader, "City"),
										State = DbUtils.GetString(reader, "State"),
										Country = DbUtils.GetString(reader, "Country")
									},
									foods = new List<Food>(),
									tools = new List<Tool>(),
									weapons = new List<Weapon>(),
									energySources = new List<Energy>(),
									miscellaneousItems = new List<Miscellaneous>()
								};

							}

							if (DbUtils.IsNotDbNull(reader, "FoodId"))
							{
								var ExistingFood = inventory.foods.FirstOrDefault(f => f.Id == DbUtils.GetInt(reader, "FoodId"));
								if (ExistingFood == null)
								{
									inventory.foods.Add(new Food()
									{
										Id = DbUtils.GetInt(reader, "FoodId"),
										Name = DbUtils.GetString(reader, "FoodName"),
										Count = DbUtils.GetInt(reader, "Count"),
										Protein = reader.GetBoolean(reader.GetOrdinal("Protein")),
										FruitVeggieFungi = reader.GetBoolean(reader.GetOrdinal("FruitVeggieFungi"))
									});
								}
							}

							if (DbUtils.IsNotDbNull(reader, "ToolId"))
							{
								var ExistingTool = inventory.tools.FirstOrDefault(t => t.Id == DbUtils.GetInt(reader, "ToolId"));
								if (ExistingTool == null)
								{
									inventory.tools.Add(new Tool()
									{
										Id = DbUtils.GetInt(reader, "ToolId"),
										Name = DbUtils.GetString(reader, "ToolName")
									}); ;
								}
							}

							if (DbUtils.IsNotDbNull(reader, "WeaponId"))
							{
								var ExistingWeapon = inventory.weapons.FirstOrDefault(w => w.Id == DbUtils.GetInt(reader, "WeaponId"));
								if (ExistingWeapon == null)
								{
									inventory.weapons.Add(new Weapon()
									{
										Id = DbUtils.GetInt(reader, "WeaponId"),
										Name = DbUtils.GetString(reader, "WeaponName")
									});
								}
							}

							if (DbUtils.IsNotDbNull(reader, "EnergyId"))
							{
								var ExistingEnergy = inventory.energySources.FirstOrDefault(e => e.Id == DbUtils.GetInt(reader, "EnergyId"));
								if (ExistingEnergy == null)
								{
									inventory.energySources.Add(new Energy()
									{
										Id = DbUtils.GetInt(reader, "EnergyId"),
										Name = DbUtils.GetString(reader, "EnergyName")
									});
								}
							}
							if (DbUtils.IsNotDbNull(reader, "MiscellaneousId"))
							{
								var ExistingMiscellaneous = inventory.miscellaneousItems.FirstOrDefault(m => m.Id == DbUtils.GetInt(reader, "MiscellaneousId"));
								if (ExistingMiscellaneous == null)
								{
									inventory.miscellaneousItems.Add(new Miscellaneous()
									{
										Id = DbUtils.GetInt(reader, "MiscellaneousId"),
										Name = DbUtils.GetString(reader, "MiscellaneousName")
									});
								}
							}
						}
						return inventory;
					}
				}
			}
		}

		public List<Inventory> GetAllInventoryUserIds()
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT Id, UserId 
										FROM Inventory";
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var inventoryUserIds = new List<Inventory>();
						while (reader.Read())
						{
							inventoryUserIds.Add(new Inventory()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								UserId = DbUtils.GetInt(reader, "UserId")
							});
						}
						return inventoryUserIds;
					}
				}
			}
		}

		public void AddInventory(Inventory inventory)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					// Check if there's an existing Inventory object with the same UserId
					cmd.CommandText = @"SELECT COUNT(*)
                                FROM Inventory
                                WHERE UserId = @checkUserId";
					DbUtils.AddParameter(cmd, "@checkUserId", inventory.UserId);

					int existingRows = (int)cmd.ExecuteScalar();

					// If an Inventory object with the same UserId already exists, don't insert a new row
					if (existingRows > 0)
					{
						Console.WriteLine("There is already an inventory with that UserId!");
						return ;
					}

					// If there's no existing Inventory object with the same UserId, insert a new row
					cmd.CommandText = @"INSERT INTO Inventory
                                (UserId)
                                OUTPUT INSERTED.ID
                                VALUES (@UserId)";
					DbUtils.AddParameter(cmd, "@UserId", inventory.UserId);

					inventory.Id = (int)cmd.ExecuteScalar();
				}
			}
		}

		public Food AddFoodType(Food food)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"INSERT INTO Food
											(Name, Count, Protein, FruitVeggieFungi)
										OUTPUT INSERTED.ID
										VALUES (@Name, @Count, @Protein, @FruitVeggieFungi)";
					DbUtils.AddParameter(cmd, "@Name", food.Name);
					DbUtils.AddParameter(cmd, "@Count", food.Count);
					DbUtils.AddParameter(cmd, "@Protein", food.Protein);
					DbUtils.AddParameter(cmd, "@FruitVeggieFungi", food.FruitVeggieFungi);

					food.Id = (int)cmd.ExecuteScalar();
				}
				return food;
			}
		}

		public void DeleteFoodType(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "DELETE FROM Food WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Id", id);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public void AddFood(InventoryFood inventoryFood)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"INSERT INTO InventoryFood
											(InventoryId, FoodId)
										OUTPUT INSERTED.ID
										VALUES 
											(@InventoryId, @FoodId)";
					DbUtils.AddParameter(cmd, "@InventoryId", inventoryFood.InventoryId);
					DbUtils.AddParameter(cmd, "@FoodId", inventoryFood.FoodId);

					inventoryFood.Id = (int)cmd.ExecuteScalar();
				}
			}
		}
		public void UpdateFood(Food food)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"Update Food
								SET Name = @Name,
									Count = @Count,
									Protein = @Protein,
									FruitVeggieFungi = @FruitVeggieFungi
								WHERE Id = @Id";

					DbUtils.AddParameter(cmd, "@Id", food.Id);
					DbUtils.AddParameter(cmd, "@Name", food.Name);
					DbUtils.AddParameter(cmd, "@Count", food.Count);
					DbUtils.AddParameter(cmd, "@Protein", food.Protein);
					DbUtils.AddParameter(cmd, "@FruitVeggieFungi", food.FruitVeggieFungi);

					cmd.ExecuteNonQuery();

				}
			}
		}

		public void DeleteFood(int InvId, int foodId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "DELETE FROM InventoryFood WHERE InventoryId = @InvId AND FoodId = @foodId";
					DbUtils.AddParameter(cmd, "@InvId", InvId);
					DbUtils.AddParameter(cmd, "@foodId", foodId);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public void UpdateTool(Tool tool)
		{
			using (var conn = Connection)
			{
				conn.Open();

				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"Update Tool
										SET Name = @Name
										WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "Id", tool.Id);
					DbUtils.AddParameter(cmd, "Name", tool.Name);

					cmd.ExecuteNonQuery();
				}
			}
		}


		public void AddTool(InventoryTool inventoryTool)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					// Check if the InventoryTool with the same InventoryId and ToolId already exists
					cmd.CommandText = @"SELECT COUNT(*)
                                FROM InventoryTool
                                WHERE InventoryId = @checkInventoryId AND ToolId = @checkToolId";
					DbUtils.AddParameter(cmd, "@checkInventoryId", inventoryTool.InventoryId);
					DbUtils.AddParameter(cmd, "@checkToolId", inventoryTool.ToolId);

					int existingRows = (int)cmd.ExecuteScalar();

					// If the InventoryTool already exists, don't insert a new row
					if (existingRows > 0)
					{
						Console.WriteLine("There is already an InventoryTool with that InventoryId and UserId.");
						return;
					}

					// If the InventoryTool doesn't exist, insert a new row
					cmd.CommandText = @"INSERT INTO InventoryTool
                                (InventoryId, ToolId)
                                OUTPUT INSERTED.ID
                                VALUES 
                                (@InventoryId, @ToolId)";
					DbUtils.AddParameter(cmd, "@InventoryId", inventoryTool.InventoryId);
					DbUtils.AddParameter(cmd, "@ToolId", inventoryTool.ToolId);

					inventoryTool.Id = (int)cmd.ExecuteScalar();
				}
			}
		}

		public void DeleteTool(int ToolId, int InvId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "DELETE FROM InventoryTool WHERE ToolId = @ToolId AND InventoryId = @InvId";
					DbUtils.AddParameter(cmd, "@ToolId", ToolId);
					DbUtils.AddParameter(cmd, "@InvId", InvId);
					cmd.ExecuteNonQuery();
				}
			}
		}
		public List<Tool> GetTools()
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT *
										FROM Tool";

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var tools = new List<Tool>();
						while (reader.Read())
						{
							tools.Add(new Tool()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								Name = DbUtils.GetString(reader, "Name")
							});
						}
						return tools;
					}
				}
			}
		}

		public void UpdateWeapon(Weapon weapon)
		{
			using (var conn = Connection)
			{
				conn.Open();

				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"Update Weapon
										SET Name = @Name
										WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "Id", weapon.Id);
					DbUtils.AddParameter(cmd, "Name", weapon.Name);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public void AddWeapon(InventoryWeapon inventoryWeapon)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT COUNT(*)
                                FROM InventoryWeapon
                                WHERE InventoryId = @checkInventoryId AND WeaponId = @checkWeaponId";
					DbUtils.AddParameter(cmd, "@checkInventoryId", inventoryWeapon.InventoryId);
					DbUtils.AddParameter(cmd, "@checkWeaponId", inventoryWeapon.WeaponId);

					int existingRows = (int)cmd.ExecuteScalar();

					if (existingRows > 0)
					{
						Console.WriteLine("There is already an InventoryWeapon with that InventoryId and UserId.");
						return;
					}

					cmd.CommandText = @"INSERT INTO InventoryWeapon
                                (InventoryId, WeaponId)
                                OUTPUT INSERTED.ID
                                VALUES 
                                (@InventoryId, @WeaponId)";
					DbUtils.AddParameter(cmd, "@InventoryId", inventoryWeapon.InventoryId);
					DbUtils.AddParameter(cmd, "@WeaponId", inventoryWeapon.WeaponId);

					inventoryWeapon.Id = (int)cmd.ExecuteScalar();
				}
			}
		}

		public void DeleteWeapon(int WeaponId, int InvId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "DELETE FROM InventoryWeapon WHERE WeaponId = @WeaponId AND InventoryId = @InvId";
					DbUtils.AddParameter(cmd, "@WeaponId", WeaponId);
					DbUtils.AddParameter(cmd, "@InvId", InvId);
					cmd.ExecuteNonQuery();
				}
			}
		}

				public List<Weapon> GetWeapons()
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT *
										FROM Weapon";

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var weapons = new List<Weapon>();
						while (reader.Read())
						{
							weapons.Add(new Weapon()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								Name = DbUtils.GetString(reader, "Name")
							});
						}
						return weapons;
					}
				}
			}
		}

		public void UpdateEnergy(Energy energy)
		{
			using (var conn = Connection)
			{
				conn.Open();

				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"Update Energy
										SET Name = @Name
										WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "Id", energy.Id);
					DbUtils.AddParameter(cmd, "Name", energy.Name);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public void AddEnergy(InventoryEnergy inventoryEnergy)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT COUNT(*)
                                FROM InventoryEnergy
                                WHERE InventoryId = @checkInventoryId AND EnergyId = @checkEnergyId";
					DbUtils.AddParameter(cmd, "@checkInventoryId", inventoryEnergy.InventoryId);
					DbUtils.AddParameter(cmd, "@checkEnergyId", inventoryEnergy.EnergyId);

					int existingRows = (int)cmd.ExecuteScalar();

					if (existingRows > 0)
					{
						Console.WriteLine("There is already an InventoryEnergy with that InventoryId and UserId.");
						return;
					}

					cmd.CommandText = @"INSERT INTO InventoryEnergy
                                (InventoryId, EnergyId)
                                OUTPUT INSERTED.ID
                                VALUES 
                                (@InventoryId, @EnergyId)";
					DbUtils.AddParameter(cmd, "@InventoryId", inventoryEnergy.InventoryId);
					DbUtils.AddParameter(cmd, "@EnergyId", inventoryEnergy.EnergyId);

					inventoryEnergy.Id = (int)cmd.ExecuteScalar();
				}
			}
		}

		public void DeleteEnergy(int EnergyId, int InvId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "DELETE FROM InventoryEnergy WHERE EnergyId = @EnergyId AND InventoryId = @InvId";
					DbUtils.AddParameter(cmd, "@EnergyId", EnergyId);
					DbUtils.AddParameter(cmd, "@InvId", InvId);
					cmd.ExecuteNonQuery();
				}
			}
		}


		public List<Energy> GetEnergies()
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT *
										FROM Energy";

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var energies = new List<Energy>();
						while (reader.Read())
						{
							energies.Add(new Energy()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								Name = DbUtils.GetString(reader, "Name")
							});
						}
						return energies;
					}
				}
			}
		}

		public void UpdateMiscellaneous(Miscellaneous miscellaneous)
		{
			using (var conn = Connection)
			{
				conn.Open();

				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"Update Miscellaneous
										SET Name = @Name
										WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "Id", miscellaneous.Id);
					DbUtils.AddParameter(cmd, "Name", miscellaneous.Name);

					cmd.ExecuteNonQuery();
				}
			}
		}


		public void AddMiscellaneous(InventoryMiscellaneous inventoryMiscellaneous)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT COUNT(*)
                                FROM InventoryMiscellaneous
                                WHERE InventoryId = @checkInventoryId AND MiscellaneousId = @checkMiscellaneousId";
					DbUtils.AddParameter(cmd, "@checkInventoryId", inventoryMiscellaneous.InventoryId);
					DbUtils.AddParameter(cmd, "@checkMiscellaneousId", inventoryMiscellaneous.MiscellaneousId);

					int existingRows = (int)cmd.ExecuteScalar();

					if (existingRows > 0)
					{
						Console.WriteLine("There is already an InventoryMiscellaneous with that InventoryId and UserId.");
						return;
					}

					cmd.CommandText = @"INSERT INTO InventoryMiscellaneous
                                (InventoryId, MiscellaneousId)
                                OUTPUT INSERTED.ID
                                VALUES 
                                (@InventoryId, @MiscellaneousId)";
					DbUtils.AddParameter(cmd, "@InventoryId", inventoryMiscellaneous.InventoryId);
					DbUtils.AddParameter(cmd, "@MiscellaneousId", inventoryMiscellaneous.MiscellaneousId);

					inventoryMiscellaneous.Id = (int)cmd.ExecuteScalar();
				}
			}
		}


		public void DeleteMiscellaneous(int MiscellaneousId, int InvId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "DELETE FROM InventoryMiscellaneous WHERE MiscellaneousId = @MiscellaneousId AND InventoryId = @InvId";
					DbUtils.AddParameter(cmd, "@MiscellaneousId", MiscellaneousId);
					DbUtils.AddParameter(cmd, "@InvId", InvId);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public List<Miscellaneous> GetMiscellaneousItems()
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT *
										FROM Miscellaneous";

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var miscellaneousItems = new List<Miscellaneous>();
						while (reader.Read())
						{
							miscellaneousItems.Add(new Miscellaneous()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								Name = DbUtils.GetString(reader, "Name")
							});
						}
						return miscellaneousItems;
					}
				}
			}
		}
	}
}
