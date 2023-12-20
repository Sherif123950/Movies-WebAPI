using Microsoft.EntityFrameworkCore;
using Movies.Core.Repositories;
using Movies.Repository.Data;
using Movies.Repository.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Repository.Repositories
{
	public class MovieRepository : GenericRepository<Movie>, IMovieRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			this._dbContext = dbContext;
		}

		public async Task<IEnumerable<Movie?>> GetMovieByGenreId(byte id)
				=> await _dbContext.Movies.Where(M => M.Genre.Id == id)
			.OrderByDescending(Mov=>Mov.Rate)
			.Include(m => m.Genre)
			.ToListAsync();


		public async Task<Movie?> GetMovieById(int id)
		=> await _dbContext.Movies.Where(M => M.Id == id).Include(m => m.Genre).SingleOrDefaultAsync();

	}
}
