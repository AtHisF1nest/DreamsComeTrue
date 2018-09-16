using AutoMapper;
using DreamsComeTrueAPI.Dtos;
using DreamsComeTrueAPI.Models;

namespace DreamsComeTrueAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForPreviewDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photo.Url);
                });
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.CategoryType, opt => {
                    opt.MapFrom(src => src.CategoryType.GetDescription());
                });
            CreateMap<TodoItem, TodoItemDto>()
                .ForMember(dest => dest.Created, opt => {
                    opt.MapFrom(src => src.Created.ToShortDateString());
                });
        }
    }
}