using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurvivorGPT.Models
{
	public class Food
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Count { get; set; }
		[Required]
		public bool Protein { get; set; }
		[Required]
		public bool FruitVeggieFungi { get; set; }
	}
}
