﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurvivorGPT.Models
{
	public class Weapon
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}