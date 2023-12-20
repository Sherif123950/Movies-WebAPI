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
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationDbContext _dbContext;

		public GenericRepository(ApplicationDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public async Task<int> CreateAsync(T model)
		{
			await _dbContext.Set<T>().AddAsync(model);
			return await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{

			if (typeof(T) == typeof(Genre))
			{
				return (IEnumerable<T>)await _dbContext.Genres.OrderBy(g => g.Name).ToListAsync();
			}
			if (typeof(T) == typeof(Movie))
			{
				return (IEnumerable<T>)await _dbContext.Movies.OrderByDescending(f => f.Rate).Include(M => M.Genre).ToListAsync();
			}
			return await _dbContext.Set<T>().ToListAsync();
		}
		public async Task<T?> GetByIdAsync(byte id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public Task<int> UpdateAsync(T model)
		{
			if (typeof(T) == typeof(Movie))
			{
				var movie = model as Movie;
				_dbContext.Movies.Update(movie);
				return _dbContext.SaveChangesAsync();
			}
			return _dbContext.SaveChangesAsync();
		}
		public Task<int> DeleteAsync(T model)
		{
			_dbContext.Remove(model);
			return _dbContext.SaveChangesAsync();
		}
	}
}
