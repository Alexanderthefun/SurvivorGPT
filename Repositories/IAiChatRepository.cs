using SurvivorGPT.Models;
using System.Collections.Generic;

namespace SurvivorGPT.Repositories
{
	public interface IAiChatRepository
	{
		void AddAiChat(AiChat aiChat);
		void DeleteAiChat(int id);
		AiChat GetAiChatById(int id);
		List<AiChat> GetAllAiChats();
		List<AiChat> GetAllChatsByCategoryId(int categoryId);
		List<AiChat> GetAllChatsByUserId(int userId);
		void UpdateAiChat(AiChat aiChat);
	}
}