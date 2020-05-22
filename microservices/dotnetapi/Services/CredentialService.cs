using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using dotnetapi.Entities;
using dotnetapi.Helpers;
using dotnetapi.Models.Credentials;

using dotnetapi.Models.Users;


namespace dotnetapi.Services
{
    public interface ICredentialService
    {
        CredentialCreateModel Create(CredentialCreateModel model, int userId);
        List<Credential> Read(CredentialReadModel model, int userId);
        void Update(CredentialUpdateModel model, int userId);
        void Delete(CredentialDeleteModel model, int userId);
    }

    public class CredentialService: ICredentialService
    {
        private UserContext _UserContext;
        private IMapper _mapper;

        public CredentialService(UserContext context, IMapper mapper) 
        {
            _mapper = mapper;
            _UserContext = context;
        }
        public CredentialCreateModel Create(CredentialCreateModel model, int userId) 
        {
            var curUser = _UserContext.Users.Include(x => x.Credentials).First(u => u.Id == userId);
            
            if (curUser.Credentials.Any(x => x.Hint == model.Hint)) {
                throw new AppException("This credential hint is already used by another of your credentials");
            }
            curUser.Credentials.Add(_mapper.Map<Credential>(model));
            _UserContext.SaveChanges();

            return model;
        }
        public List<Credential> Read(CredentialReadModel model, int userId)
        {
            var curUser = _UserContext.Users.Include(x => x.Credentials).First(u => u.Id == userId);
          
            var creds = curUser.Credentials.Where(x => ( (model.Domain == null || x.Domain == model.Domain)
                                                      && (model.Hint == null   || x.Hint == model.Hint)
                                                      && (model.Id == null   || x.Id == model.Id)
                                                      && (model.Type == null   || x.Type == model.Type)
                                                      )).AsEnumerable();
            return creds.ToList();
        }
        public void Update(CredentialUpdateModel model, int userId)
        {
            var user = _UserContext.Users.Include(x => x.Credentials).First(u => u.Id == userId);
            try {

                var credential = user.Credentials.First(x => x.Id == model.Id);

                if (model.Domain != null) {
                    credential.Domain = model.Domain;
                }   
                if (model.Hint != null) {
                    credential.Hint = model.Hint;
                }
                if (model.Type != null) {
                    credential.Type = model.Type;
                }
                _UserContext.Users.Update(user);
                _UserContext.SaveChanges();

            } catch (InvalidOperationException){
                throw new AppException("None of your credentials have this Id");
            }
        }
        public void Delete(CredentialDeleteModel model, int userId)
        {
            var user = _UserContext.Users.Include(x => x.Credentials).First(u => u.Id == userId);
            try {
                user.Credentials.Remove(user.Credentials.First(x => x.Id == model.Id));
                _UserContext.Users.Update(user);
                _UserContext.SaveChanges();

            } catch (InvalidOperationException) {
                    throw new AppException("None of your credentials have this Id");
            }
        }
    }
}