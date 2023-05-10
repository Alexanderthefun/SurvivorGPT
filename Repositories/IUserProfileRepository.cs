using SurvivorGPT.Models;
using System.Collections.Generic;

namespace SurvivorGPT.Repositories
{
	public interface IUserProfileRepository
	{
		public int Add(UserProfile userProfile);
		public void Delete(int id);
		public void Update(UserProfile userProfile);
		public List<UserProfile> GetAllUsers();
		public UserProfile GetByFirebaseUserId(string firebaseUserId);
		public UserProfile GetById(int Id);
	}
}