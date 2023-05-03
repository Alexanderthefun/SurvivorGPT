using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurvivorGPT.Models
{
	public class Tool
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
