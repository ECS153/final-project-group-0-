using AutoMapper;

using dotnetapi.Entities;
using dotnetapi.Models.Users;
using dotnetapi.Models.Credentials;
using dotnetapi.Models.RequestModels;

/* Allows Automatic Mapping From One Class to Another */
namespace dotnetapi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserCreateModel, User>();
            CreateMap<UserUpdateModel, User>();
            CreateMap<User, UserReadModel>();

            CreateMap<CredentialCreateModel, Credential>();
            CreateMap<CredentialUpdateModel, Credential>();
            CreateMap<Credential, CredentialReadModel>();

            CreateMap<BrowserRequestSwapModel, RequestSwap>();
        }
    }
}