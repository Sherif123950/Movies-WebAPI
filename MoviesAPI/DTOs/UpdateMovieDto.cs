namespace MoviesAPI.DTOs
{
	public class UpdateMovieDto : BaseMovieDto
	{
		public IFormFile? Poster { get; set; }
	}
}
