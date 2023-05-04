using System;
using System.Collections.Generic;
using System.Linq;
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
									});
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
	}
}
