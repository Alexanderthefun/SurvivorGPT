using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurvivorGPT.Models
{
	public class UserProfile
	{
		public int Id { get; set; }
		public string FirebaseUserId { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string DisplayName { get; set; }
		[Required]
		public DateTime DateOfBirth { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string State { get; set; }
		[Required]
		public string Country { get; set; }
		public int? ShelterId { get; set; }
		public int? CurriculumId { get; set; }
		public bool? IsActive { get; set; }

	}
}
