using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurvivorGPT.Models
{
	public class Inventory
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public UserProfile UserProfile { get; set; }	
		public List<Food> foods { get; set; }
		public List<Tool> tools { get; set; }
		public List<Weapon> weapons { get; set; }
		public List<Energy> energySources { get; set; }
		public List<Miscellaneous> miscellaneousItems { get; set;}
	}
}
