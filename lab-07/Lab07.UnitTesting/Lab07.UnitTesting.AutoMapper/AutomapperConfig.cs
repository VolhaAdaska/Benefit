using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Lab07.UnitTesting.Models;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.DAL.Models.Identity;
using Lab07.UnitTesting.DAL.Models;
using Lab07.UnitTesting.Models;

namespace Lab07.UnitTesting.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, LoginModel>();
            CreateMap<UserDto, RegisterModel>();
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<LoginModel, UserDto>();
            CreateMap<RegisterModel, UserDto>();
            CreateMap<StoreDto, Store>();
            CreateMap<Store, StoreDto>();
            CreateMap<StoreViewModel, StoreDto>();
            CreateMap<StoreDto, StoreViewModel>();
            CreateMap<StoreTypeDto, StoreType>();
            CreateMap<StoreType, StoreTypeDto>();
            CreateMap<StoreTypeDto, StoreTypeViewModel>();
            CreateMap<StoreTypeViewModel, StoreTypeDto>();
        }
    }
}