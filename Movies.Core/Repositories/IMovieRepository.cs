using Movies.Repository.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Repositories
{
	public interface IMovieRepository
	{
		Task<Movie?> GetMovieById(int id);
		Task<IEnumerable<Movie?>> GetMovieByGenreId(byte id);

	}
}
