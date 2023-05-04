using SurvivorGPT.Models;
using System.Collections.Generic;

namespace SurvivorGPT.Repositories
{
	public interface IUserProfileRepository
	{
		void Add(UserProfile userProfile);
		void Delete(int id);
		void Update(UserProfile userProfile);
		List<UserProfile> GetAllUsers();
		UserProfile GetByFirebaseUserId(string firebaseUserId);
		UserProfile GetById(int Id);
	}
}