﻿using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
	public class CreateGenreDto
	{
		[MaxLength(100)]
        public string  Name { get; set; }
    }
}
