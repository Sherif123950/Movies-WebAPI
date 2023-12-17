using AutoMapper;
using Movies.Repository.Data.Models;
using MoviesAPI.DTOs;

namespace MoviesAPI.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<CreateGenreDto, Genre>().ReverseMap();
		}
	}
}
