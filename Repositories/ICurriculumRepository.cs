using SurvivorGPT.Models;

namespace SurvivorGPT.Repositories
{
	public interface ICurriculumRepository
	{
		void Add(Curriculum curriculum);
		void Delete(int id);
		Curriculum GetCurrentCurriculum(int curriculumId);
		void Update(Curriculum curriculum);
	}
}