using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public readonly IMovieStoreDbContext _context;

        public MappingProfile()
        {
            #region Movie Mappings
            CreateMap<Movie, MovieViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Director.Name} {src.Director.Surname}"))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year.ToString("dd/MM/yyyy")));

            CreateMap<Movie, MovieDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Director.Name} {src.Director.Surname}"))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(x => $"{x.Actor.Name} {x.Actor.Surname}")));

            CreateMap<CreateMovieModel, Movie>();
            #endregion

            #region Genre Mappings
            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            #endregion
        }
    }
}
