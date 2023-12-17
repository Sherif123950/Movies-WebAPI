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
	public class GenresController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Genre> _genreRepository;

		public GenresController(IMapper mapper, IGenericRepository<Genre> genreRepository)
		{
			this._mapper = mapper;
			this._genreRepository = genreRepository;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllGenresAsync()
		{
			var Genres = await _genreRepository.GetAllAsync();
			if (Genres is null)
				return NotFound();
			return Ok(Genres);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetGenreByIdAsync([FromRoute] byte id)
		{
			var Genre = await _genreRepository.GetByIdAsync(id);
			if (Genre is null)
				return NotFound();
			return Ok(Genre);
		}
		[HttpPost]
		public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDto model)
		{
			var genre = _mapper.Map<CreateGenreDto,Genre>(model);
			var result = await _genreRepository.CreateAsync(genre);
			if (! (result > 0))
				return BadRequest();
			return Ok(genre);
        }
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateGenre(byte id, [FromBody] CreateGenreDto model)
		{
			var genre = await _genreRepository.GetByIdAsync(id);
			if (genre is null)
				return NotFound("There is no genre with this Id");
			genre.Name = model.Name;
			var Result = await _genreRepository.UpdateAsync();
			if (!(Result > 0))
				return BadRequest();
			return Ok(genre);
        }
	}
}
