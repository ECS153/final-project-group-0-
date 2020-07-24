using AutoMapper;

using dotnetapi.Entities;
using dotnetapi.Models.Users;
using dotnetapi.Models.Credentials;
using dotnetapi.Models.Requests;

/* Allows Automatic Mapping From One Class to Another */
namespace dotnetapi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User Model Maps
            CreateMap<UserCreateModel, User>();
            CreateMap<UserUpdateModel, User>();
            CreateMap<User, UserReadModel>();

            // Credential Model Maps
            CreateMap<CredentialCreateModel, Credential>();
            CreateMap<CredentialReadModel, Credential>();
            CreateMap<CredentialUpdateModel, Credential>();
            CreateMap<Credential, CredentialReadModel>();

            // Swap Maps
            CreateMap<SubmitRequestModel, RequestSwap>();
            CreateMap<RequestSwap, ReadSwapModel>();
        }
    }
}