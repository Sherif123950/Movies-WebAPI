using Movies.Repository.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
	public class MovieToReturnDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
		public double Rate { get; set; }
		public string StoryLine { get; set; }
		public byte[] Poster { get; set; }
		public byte GenreId { get; set; }
		public string GenreName { get; set; }
	}
}
