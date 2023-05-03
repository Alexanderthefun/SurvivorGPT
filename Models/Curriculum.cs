using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurvivorGPT.Models
{
	public class Curriculum
	{
		public int Id { get; set; }
		[Required]
		public string DailyContent { get; set; }
		public int TribeMemberId { get; set; }
		public List<TribeMember> TribeMembers { get; set; }
	}
}
