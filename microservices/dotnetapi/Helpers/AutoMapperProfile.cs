using AutoMapper;

using dotnetapi.Entities;
using dotnetapi.Models.Users;

namespace dotnetapi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            CreateMap<User, ViewModel>();
        }
    }
}