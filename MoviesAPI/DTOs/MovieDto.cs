using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
	public class MovieDto:BaseMovieDto
	{
		
		public IFormFile Poster { get; set; }
	}
}
