using AutoMapper;

using dotnetapi.Entities;
using dotnetapi.Models.Users;
using dotnetapi.Models.Credentials;

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

            CreateMap<CredentialCreateModel, Credential>();
            
            CreateMap<CredentialUpdateModel, Credential>();
        }
    }
}