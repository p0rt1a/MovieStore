using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Queries.GetOrders;
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

            #region Customer Mappings
            CreateMap<CreateCustomerModel, Customer>();
            #endregion

            #region Order Mappings
            CreateMap<CreateOrderModel, Order>();
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.Name} {src.Customer.Surname}"))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate.ToString("dd/MM/yyyy")));
            #endregion
        }
    }
}
