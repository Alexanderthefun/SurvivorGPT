using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SurvivorGPT.Models;
using SurvivorGPT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurvivorGPT.Repositories
{
	public class UserProfileRepository : BaseRepository, IUserProfileRepository
	{
		public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

		public UserProfile GetByFirebaseUserId(string firebaseUserId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
						SELECT *
						FROM UserProfile
						WHERE FirebaseUserId = @FirebaseUserId";

					DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

					UserProfile userProfile = null;

					var reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						userProfile = new UserProfile()
						{
							Id = DbUtils.GetInt(reader, "Id"),
							FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
							FirstName = DbUtils.GetString(reader, "FirstName"),
							LastName = DbUtils.GetString(reader, "LastName"),
							DisplayName = DbUtils.GetString(reader, "DisplayName"),
							Email = DbUtils.GetString(reader, "Email"),
							DateOfBirth = DbUtils.GetDateTime(reader, "DateOfBirth"),
							City = DbUtils.GetString(reader, "City"),
							State = DbUtils.GetString(reader, "State"),
							Country = DbUtils.GetString(reader, "Country"),
							ShelterId = DbUtils.GetNullableInt(reader, "ShelterId"),
							CurriculumId = DbUtils.GetNullableInt(reader, "CurriculumId"),
						};
					}
					reader.Close();

					return userProfile;
				}
			}
		}

		public UserProfile GetById(int Id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
						SELECT *
						FROM UserProfile
						WHERE Id = @Id";

					DbUtils.AddParameter(cmd, "@Id", Id);

					UserProfile userProfile = null;

					var reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						userProfile = new UserProfile()
						{
							Id = DbUtils.GetInt(reader, "Id"),
							FirstName = DbUtils.GetString(reader, "FirstName"),
							LastName = DbUtils.GetString(reader, "LastName"),
							DisplayName = DbUtils.GetString(reader, "DisplayName"),
							Email = DbUtils.GetString(reader, "Email"),
							DateOfBirth = DbUtils.GetDateTime(reader, "DateOfBirth"),
							City = DbUtils.GetString(reader, "City"),
							State = DbUtils.GetString(reader, "State"),
							Country = DbUtils.GetString(reader, "Country"),
							ShelterId = DbUtils.GetInt(reader, "ShelterId"),
							CurriculumId = DbUtils.GetInt(reader, "CurriculumId"),
						};
					}
					reader.Close();

					return userProfile;
				}
			}
		}

		public List<UserProfile> GetAllUsers()
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT Id, FirstName, LastName, DisplayName, 
											Email, DateOfBirth, City, State, Country,
											ShelterId, CurriculumId
										FROM UserProfile";

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var users = new List<UserProfile>();
						while (reader.Read())
						{
							var userId = DbUtils.GetInt(reader, "Id");

							var existingUser = users.FirstOrDefault(p => p.Id == userId);
							if (existingUser == null)
							{
								existingUser = new UserProfile()
								{
									Id = DbUtils.GetInt(reader, "Id"),
									FirstName = DbUtils.GetString(reader, "FirstName"),
									LastName = DbUtils.GetString(reader, "LastName"),
									DisplayName = DbUtils.GetString(reader, "DisplayName"),
									Email = DbUtils.GetString(reader, "Email"),
									DateOfBirth = DbUtils.GetDateTime(reader, "DateOfBirth"),
									City = DbUtils.GetString(reader, "City"),
									State = DbUtils.GetString(reader, "State"),
									Country = DbUtils.GetString(reader, "Country"),
									ShelterId = DbUtils.GetInt(reader, "ShelterId"),
									CurriculumId = DbUtils.GetInt(reader, "CurriculumId")
								};
								users.Add(existingUser);
							}
						}

						return users;
					}
				}
			}
		}

		public int Add(UserProfile userProfile)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"INSERT INTO UserProfile 
											(FirebaseUserId, FirstName, LastName, DisplayName,
											Email, DateOfBirth, City, State, Country, 
											ShelterId, CurriculumId)
										OUTPUT INSERTED.ID
										VALUES (@FirebaseUserId, @FirstName, @LastName, @DisplayName,
											@Email, @DateOfBirth, @City, @State, @Country, 
										    @ShelterId, @CurriculumId)";
					DbUtils.AddParameter(cmd, "@FirebaseUserId", userProfile.FirebaseUserId);
					DbUtils.AddParameter(cmd, "@FirstName", userProfile.FirstName);
					DbUtils.AddParameter(cmd, "@LastName", userProfile.LastName);
					DbUtils.AddParameter(cmd, "@DisplayName", userProfile.DisplayName);
					DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
					DbUtils.AddParameter(cmd, "@DateOfBirth", userProfile.DateOfBirth);
					DbUtils.AddParameter(cmd, "@City", userProfile.City);
					DbUtils.AddParameter(cmd, "@State", userProfile.State);
					DbUtils.AddParameter(cmd, "@Country", userProfile.Country);
					DbUtils.AddParameter(cmd, "@ShelterId", userProfile.ShelterId ?? (object)DBNull.Value);
					DbUtils.AddParameter(cmd, "@CurriculumId", userProfile.CurriculumId ?? (object)DBNull.Value);

					userProfile.Id = (int)cmd.ExecuteScalar();
				}
				return userProfile.Id;
			}
		}

		public void Update(UserProfile userProfile)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"Update UserProfile 
											SET FirstName = @FirstName,
												LastName = @LastName,
												DisplayName = @DisplayName,
												Email = @Email,
												DateOfBirth = @DateOfBirth,
												City = @City,
												State = @State,
												Country = @Country
											WHERE Id = @Id";

					DbUtils.AddParameter(cmd, "@Id", userProfile.Id);
					DbUtils.AddParameter(cmd, "@FirstName", userProfile.FirstName);
					DbUtils.AddParameter(cmd, "@LastName", userProfile.LastName);
					DbUtils.AddParameter(cmd, "@DisplayName", userProfile.DisplayName);
					DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
					DbUtils.AddParameter(cmd, "@DateOfBirth", userProfile.DateOfBirth);
					DbUtils.AddParameter(cmd, "@City", userProfile.City);
					DbUtils.AddParameter(cmd, "@State", userProfile.State);
					DbUtils.AddParameter(cmd, "@Country", userProfile.Country);

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
					cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Id", id);
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
