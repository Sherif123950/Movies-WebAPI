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
			CreateMap<UpdateMovieDto,Movie>().ForMember(dst => dst.Poster, opt => opt.Ignore()).ReverseMap();
			CreateMap<MovieDto,Movie>().ForMember(dst=>dst.Poster,opt=>opt.Ignore()).ReverseMap();
			CreateMap<Movie,MovieToReturnDto>().ForMember(dst=>dst.GenreName,opt=>opt.MapFrom(src=>src.Genre.Name)).ReverseMap();
		}
	}
}
