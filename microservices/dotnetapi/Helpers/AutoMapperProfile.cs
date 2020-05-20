using AutoMapper;

using dotnetapi.Entities;
using dotnetapi.Models.Users;


/* Allows Automatic Mapping From One Class to Another */
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