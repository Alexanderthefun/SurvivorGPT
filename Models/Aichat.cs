using System;
using System.ComponentModel.DataAnnotations;

namespace SurvivorGPT.Models
{
	public class AiChat
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Content { get; set; }
		public int CategoryId { get; set; }
		public DateTime DateCreated { get; set; }

	}
}
