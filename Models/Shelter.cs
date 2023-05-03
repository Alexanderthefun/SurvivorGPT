using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurvivorGPT.Models
{
	public class Shelter
	{
		public int Id { get; set; }
		[Required]
		public string BuildMaterials { get; set; }
		[Required]
		public string BuildPlan { get; set; }
	}
}