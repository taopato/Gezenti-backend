using AutoMapper;
using Gezenti.Application.Features.Auth.Dtos;
using Gezenti.Application.Features.Place.Commands;
using Gezenti.Application.Features.Place.Dtos;
using Gezenti.Domain.Entities;
using PlaceEntity = Gezenti.Domain.Entities.Place;

namespace Gezenti.Application.Features.Mapping
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ========== PLACE MAPPINGS ==========
            
   
            CreateMap<PlaceEntity, PlaceListDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => 
                    src.PlaceCategories.Select(pc => pc.Category.CategoryName).ToList()));

            CreateMap<PlaceEntity, GetPlaceDetailDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => 
                    src.PlaceCategories.Select(pc => pc.Category.CategoryName).ToList()))
                .ForMember(dest => dest.ImageUrls, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteCount, opt => opt.MapFrom(src => 
                    src.UserFavorites != null ? src.UserFavorites.Count : 0));


            CreateMap<CreatePlaceDto, PlaceEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PlaceCategories, opt => opt.Ignore())
                .ForMember(dest => dest.UserComments, opt => opt.Ignore())
                .ForMember(dest => dest.UserFavorites, opt => opt.Ignore())
                .ForMember(dest => dest.UserInteractions, opt => opt.Ignore())
                .ForMember(dest => dest.UserVisitHistory, opt => opt.Ignore())
                .ForMember(dest => dest.AverageRating, opt => opt.Ignore())
                .ForMember(dest => dest.TotalReviews, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<CreatePlaceCommand, PlaceEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PlaceCategories, opt => opt.Ignore())
                .ForMember(dest => dest.UserComments, opt => opt.Ignore())
                .ForMember(dest => dest.UserFavorites, opt => opt.Ignore())
                .ForMember(dest => dest.UserInteractions, opt => opt.Ignore())
                .ForMember(dest => dest.UserVisitHistory, opt => opt.Ignore())
                .ForMember(dest => dest.AverageRating, opt => opt.Ignore())
                .ForMember(dest => dest.TotalReviews, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<UpdatePlaceDetailDto, PlaceEntity>()
                .ForMember(dest => dest.PlaceCategories, opt => opt.Ignore())
                .ForMember(dest => dest.UserComments, opt => opt.Ignore())
                .ForMember(dest => dest.UserFavorites, opt => opt.Ignore())
                .ForMember(dest => dest.UserInteractions, opt => opt.Ignore())
                .ForMember(dest => dest.UserVisitHistory, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdatePlaceCommand, PlaceEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PlaceCategories, opt => opt.Ignore())
                .ForMember(dest => dest.UserComments, opt => opt.Ignore())
                .ForMember(dest => dest.UserFavorites, opt => opt.Ignore())
                .ForMember(dest => dest.UserInteractions, opt => opt.Ignore())
                .ForMember(dest => dest.UserVisitHistory, opt => opt.Ignore())
                .ForMember(dest => dest.AverageRating, opt => opt.Ignore())
                .ForMember(dest => dest.TotalReviews, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<PlaceEntity, UpdatePlaceDetailDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => 
                    src.PlaceCategories.Select(pc => pc.Category.CategoryName).ToList()))
                .ForMember(dest => dest.FavoriteCount, opt => opt.MapFrom(src => 
                    src.UserFavorites != null ? src.UserFavorites.Count : 0));

            // ========== USER MAPPINGS ==========
            

            CreateMap<User, LoggedInUserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserGmail))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => 
                    src.UserName != null && src.UserName.Contains(' ') 
                        ? src.UserName.Substring(0, src.UserName.IndexOf(' ')) 
                        : src.UserName ?? string.Empty))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => 
                    src.UserName != null && src.UserName.Contains(' ') 
                        ? src.UserName.Substring(src.UserName.IndexOf(' ') + 1) 
                        : string.Empty))
                .ForMember(dest => dest.AccessToken, opt => opt.Ignore());
        }
    }
}

