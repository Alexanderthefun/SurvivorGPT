using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SurvivorGPT.Models;
using SurvivorGPT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurvivorGPT.Repositories
{
	public class ShelterRepository : BaseRepository, IShelterRepository
	{
		public ShelterRepository(IConfiguration configuration) : base(configuration) { }

		public Shelter GetCurrentShelter(int shelterId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
						SELECT *
						FROM Shelter
						WHERE Id = @shelterId";

					DbUtils.AddParameter(cmd, "@shelterId", shelterId);

					Shelter shelter = null;

					var reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						shelter = new Shelter()
						{
							Id = DbUtils.GetInt(reader, "Id"),
							ShelterMaterials = DbUtils.GetString(reader, "ShelterMaterials"),
							ShelterPlan = DbUtils.GetString(reader, "ShelterPlan")
						};
					}
					reader.Close();

					return shelter;
				}
			}
		}


		public void Add(Shelter shelter)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"INSERT INTO Shelter
								(ShelterMaterials, ShelterPlan)
						OUTPUT INSERTED.ID
						VALUES (@ShelterMaterials, ShelterPlan)";
					DbUtils.AddParameter(cmd, "ShelterMaterials", shelter.ShelterMaterials);
					DbUtils.AddParameter(cmd, "ShelterPlan", shelter.ShelterPlan);

					shelter.Id = (int)cmd.ExecuteScalar();
				}
			}
		}

		public void Update(Shelter shelter)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"Update Shelter
								SET ShelterMaterials = @ShelterMaterials,
									ShelterPlan = @ShelterPlan
								WHERE Id = @Id";

					DbUtils.AddParameter(cmd, "@Id", shelter.Id);
					DbUtils.AddParameter(cmd, "@ShelterMaterials", shelter.ShelterMaterials);
					DbUtils.AddParameter(cmd, "@ShelterPlan", shelter.ShelterPlan);

					cmd.ExecuteNonQuery();

				}
			}
		}

		public void Delete(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "DELETE FROM Shelter WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Id", id);
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
