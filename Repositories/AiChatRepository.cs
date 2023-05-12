using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SurvivorGPT.Models;
using SurvivorGPT.Utils;
using SurvivorGPT.Repositories;

namespace SurvivorGPT.Repositories
{
	public class AiChatRepository : BaseRepository, IAiChatRepository
	{
		public AiChatRepository(IConfiguration configuration) : base(configuration) { }

		public List<AiChat> GetAllAiChats()
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
                        SELECT Id, UserId, Content, CategoryId, DateCreated
                        FROM AiChat";

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var aiChats = new List<AiChat>();
						while (reader.Read())
						{
							aiChats.Add(new AiChat()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								UserId = DbUtils.GetInt(reader, "UserId"),
								Content = DbUtils.GetString(reader, "Content"),
								CategoryId = DbUtils.GetInt(reader, "CategoryId"),
								DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
							});
						}
						return aiChats;
					}
				}
			}
		}

		public List<AiChat> GetAllChatsByUserId(int userId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT Id, UserId, Content, CategoryId, DateCreated
                                FROM AiChat
                                WHERE UserId = @UserId";
					DbUtils.AddParameter(cmd, "@UserId", userId);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var chats = new List<AiChat>();
						while (reader.Read())
						{
							chats.Add(new AiChat()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								UserId = DbUtils.GetInt(reader, "UserId"),
								Content = DbUtils.GetString(reader, "Content"),
								CategoryId = DbUtils.GetInt(reader, "CategoryId"),
								DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
							});
						}
						return chats;
					}
				}
			}
		}

		public AiChat GetAiChatById(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
                        SELECT Id, UserId, Content, CategoryId, DateCreated
                        FROM AiChat
                        WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Id", id);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						AiChat aiChat = null;
						if (reader.Read())
						{
							aiChat = new AiChat()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								UserId = DbUtils.GetInt(reader, "UserId"),
								Content = DbUtils.GetString(reader, "Content"),
								CategoryId = DbUtils.GetInt(reader, "CategoryId"),
								DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
							};
						}
						return aiChat;
					}
				}
			}
		}

		public void AddAiChat(AiChat aiChat)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
                        INSERT INTO AiChat (UserId, Content, CategoryId, DateCreated)
                        OUTPUT INSERTED.ID
                        VALUES (@UserId, @Content, @CategoryId, @DateCreated)";
					DbUtils.AddParameter(cmd, "@UserId", aiChat.UserId);
					DbUtils.AddParameter(cmd, "@Content", aiChat.Content);
					DbUtils.AddParameter(cmd, "@CategoryId", aiChat.CategoryId);
					DbUtils.AddParameter(cmd, "@DateCreated", aiChat.DateCreated);

					aiChat.Id = (int)cmd.ExecuteScalar();
				}
			}
		}

		public void UpdateAiChat(AiChat aiChat)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
                        UPDATE AiChat
                        SET UserId = @UserId,
                            Content = @Content,
                            CategoryId = @CategoryId,
                            DateCreated = @DateCreated
                        WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Id", aiChat.Id);
					DbUtils.AddParameter(cmd, "@UserId", aiChat.UserId);
					DbUtils.AddParameter(cmd, "@Content", aiChat.Content);
					DbUtils.AddParameter(cmd, "@CategoryId", aiChat.CategoryId);
					DbUtils.AddParameter(cmd, "@DateCreated", aiChat.DateCreated);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public void DeleteAiChat(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = "DELETE FROM AiChat WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Id", id);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public List<AiChat> GetAllChatsByCategoryId(int categoryId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"SELECT Id, UserId, Content, CategoryId, DateCreated
                                FROM AiChat
                                WHERE CategoryId = @CategoryId";
					DbUtils.AddParameter(cmd, "@CategoryId", categoryId);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						var chats = new List<AiChat>();
						while (reader.Read())
						{
							chats.Add(new AiChat()
							{
								Id = DbUtils.GetInt(reader, "Id"),
								UserId = DbUtils.GetInt(reader, "UserId"),
								Content = DbUtils.GetString(reader, "Content"),
								CategoryId = DbUtils.GetInt(reader, "CategoryId"),
								DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
							});
						}
						return chats;
					}
				}
			}
		}
	}
}