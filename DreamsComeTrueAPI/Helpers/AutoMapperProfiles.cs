using System;
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
                })
                .ForMember(dest => dest.CategoryType, opt => {
                    opt.MapFrom(src => src.CategoryType.GetDescription());
                })
                .ForMember(dest => dest.LastDone, opt => {
                    opt.ResolveUsing(src => {
                        if(src.LastDone != null)
                            return ((DateTime)src.LastDone).ToString("yyyy-MM-dd");
                        else
                            return string.Empty;
                    });
                });
             CreateMap<History, HistoryDto>()
                .ForMember(dest => dest.Done, opt => {
                    opt.MapFrom(src => src.Done.ToString("yyyy-MM-dd HH:mm"));
                });
        }
    }
}