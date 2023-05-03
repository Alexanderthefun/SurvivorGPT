using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SurvivorGPT.Repositories
{
	public class BaseRepository
	{
		private readonly string _connectionString;

		public BaseRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		//Protected makes it available to child classes, but inaccessible to any other code.
		protected SqlConnection Connection
		{
			get
			{
				return new SqlConnection(_connectionString);
			}
		}
	}
}
