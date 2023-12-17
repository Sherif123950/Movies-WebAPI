using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(byte id);
		Task<int> CreateAsync(T model);
		Task<int> UpdateAsync();
	}
}
