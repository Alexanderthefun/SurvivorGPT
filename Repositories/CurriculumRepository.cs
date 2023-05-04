using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SurvivorGPT.Models;
using SurvivorGPT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurvivorGPT.Repositories
{
	public class CurriculumRepository : BaseRepository, ICurriculumRepository
	{
		public CurriculumRepository(IConfiguration configuration) : base(configuration) { }

		public Curriculum GetCurrentCurriculum(int curriculumId)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
						SELECT *
						FROM Curriculum
						WHERE Id = @curriculumId";

					DbUtils.AddParameter(cmd, "@curriculumId", curriculumId);

					Curriculum curriculum = null;

					var reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						curriculum = new Curriculum()
						{
							Id = DbUtils.GetInt(reader, "Id"),
							DailySchedule = DbUtils.GetString(reader, "DailySchedule"),
							DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
						};
					}
					reader.Close();

					return curriculum;
				}
			}
		}


		public void Add(Curriculum curriculum)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"INSERT INTO Curriculum
								(DailySchedule, DateCreated)
						OUTPUT INSERTED.ID
						VALUES (@DailySchedule, @DateCreated)";
					DbUtils.AddParameter(cmd, "DailySchedule", curriculum.DailySchedule);
					DbUtils.AddParameter(cmd, "DateCreated", curriculum.DateCreated);

					curriculum.Id = (int)cmd.ExecuteScalar();
				}
			}
		}

		public void Update(Curriculum curriculum)
		{
			using (var conn = Connection)
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"Update Curriculum
								SET DailySchedule = @DailySchedule,
									DateCreated = @DateCreated
								WHERE Id = @Id";

					DbUtils.AddParameter(cmd, "@Id", curriculum.Id);
					DbUtils.AddParameter(cmd, "@DailySchedule", curriculum.DailySchedule);
					DbUtils.AddParameter(cmd, "@DateCreated", curriculum.DateCreated);

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
					cmd.CommandText = "DELETE FROM Curriculum WHERE Id = @Id";
					DbUtils.AddParameter(cmd, "@Id", id);
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
