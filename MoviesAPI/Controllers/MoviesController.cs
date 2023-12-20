using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.Repositories;
using Movies.Repository.Data.Models;
using MoviesAPI.DTOs;

namespace MoviesAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly IMovieRepository _movieRepository1;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Movie> _movieRepository;
		private List<string> _AllowedExtensionForPoster = new List<string> { ".jpg", ".png" };
		private long _MaximumSizeAllowedForPoster = 2 * 1024 * 1024;

		public MoviesController(IMovieRepository movieRepository1, IMapper mapper, IGenericRepository<Movie> movieRepository)
		{
			this._movieRepository1 = movieRepository1;
			this._mapper = mapper;
			this._movieRepository = movieRepository;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllMovieAsync()
		{
			var Movies = await _movieRepository.GetAllAsync();
			if (Movies is null)
				return NotFound("There is no movies");
			var MappedMovies = _mapper.Map<IEnumerable<Movie>, IEnumerable<MovieToReturnDto>>(Movies);
			return Ok(MappedMovies);

		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetMovieByIdAsync(int id) 
		{
			var movie = await _movieRepository1.GetMovieById(id);
			if (movie is null)
				return NotFound("There is no movie with this id");
			var MappedMovie = _mapper.Map<Movie, MovieToReturnDto>(movie);
			return Ok(MappedMovie);
        }
		[HttpGet("Genreid/{GenreId}")]
		public async Task<IActionResult> GetMovieByGenreIdAsync(byte GenreId)
		{
			var movies = await _movieRepository1.GetMovieByGenreId(GenreId);
			if (movies is null)
				return NotFound("There is no movie with this Genre id");
			var MappedMovies = _mapper.Map<IEnumerable<Movie?>,IEnumerable<MovieToReturnDto>>(movies);
			return Ok(MappedMovies);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
		{
			if (!_AllowedExtensionForPoster.Contains(Path.GetExtension(dto.Poster.FileName)))
				return BadRequest("Only .png or .jpg files allowed");
			if (_MaximumSizeAllowedForPoster < dto.Poster.Length)
				return BadRequest("Maximum allowed size is 2 MB");
			using var Stream = new MemoryStream();
			await dto.Poster.CopyToAsync(Stream);
			var MappedMovie = _mapper.Map<MovieDto, Movie>(dto);
			MappedMovie.Poster = Stream.ToArray();
			var Result = await _movieRepository.CreateAsync(MappedMovie);
			if (Result < 1)
				return BadRequest();
			return Ok(MappedMovie);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMovieAsync(int id,[FromForm]UpdateMovieDto model)
		{
			var movie = await _movieRepository1.GetMovieById(id);
			if (movie is null)
				return NotFound("No movie with this id");
			if (model.Poster is not null)
            {
				if (!_AllowedExtensionForPoster.Contains(Path.GetExtension(model.Poster.FileName)))
					return BadRequest("Only .png or .jpg files allowed");
				if (_MaximumSizeAllowedForPoster < model.Poster.Length)
					return BadRequest("Maximum allowed size is 2 MB");
				var stream = new MemoryStream();
				await model.Poster.CopyToAsync(stream);
				movie.Poster = stream.ToArray();
			}

			movie.Title = model.Title;
			movie.Year = model.Year;
			movie.Rate = model.Rate;
			movie.GenreId = model.GenreId;
			movie.StoryLine = model.StoryLine;


			var Result = await _movieRepository.UpdateAsync(movie);
			if (Result < 1)
				return BadRequest("There is error happened while updating this movie");
			return Ok(movie);
        }

		[HttpDelete("{id}")]
		public async Task<IActionResult>  DeleteMovieAsync(int id)
		{
			var movie = await _movieRepository1.GetMovieById(id);
			if (movie is null)
				return NotFound("There is no movie with this id");
			var Result = await _movieRepository.DeleteAsync(movie);
			if (Result < 1)
				return BadRequest("There is an error happened while deleting this movie");
			return Ok(movie);
        }
	}
}
