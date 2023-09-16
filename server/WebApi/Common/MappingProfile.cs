using AutoMapper;
using WebApi.Application.CategoryOperations.Command.CreateCategory;
using WebApi.Application.CategoryOperations.Queries.GetCategories;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.DirectorOperations.Queries.GetDirector;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovie;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.Queries.GetUser;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Movie Mappings
            CreateMap<Movie, MoviesViewModel>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Movie, MovieViewModel>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FullName));

            CreateMap<CreateMovieModel, Movie>();
            #endregion

            #region Category Mappings
            CreateMap<Category, CategoriesViewModel>();
            
            CreateMap<CreateCategoryModel, Category>();
            #endregion

            #region Director Mappings
            CreateMap<Director, DirectorViewModel>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd/MM/yyyy")));

            CreateMap<Movie, DirectorMovieViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateDirectorModel, Director>();
            #endregion
        
            #region User Mappings
            CreateMap<CreateUserModel, User>();

            CreateMap<User, UserViewModel>();

            CreateMap<Order, UserOrderViewModel>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.MovieImageUrl, opt => opt.MapFrom(src => src.Movie.ImageUrl))
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title));
            #endregion
        
            #region Order Mappings
            CreateMap<Order, OrdersViewModel>()
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => $"{src.User.Name} {src.User.Surname}"))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title));
            
            CreateMap<CreateOrderModel, Order>();
            #endregion 
        }
    }
}