using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurvivorGPT.Models
{
	public class Curriculum
	{
		public int Id { get; set; }
		[Required]
		public string DailySchedule { get; set; }
		public DateTime? DateCreated { get; set; }
	}
}
