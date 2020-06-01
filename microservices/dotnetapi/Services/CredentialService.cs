using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using dotnetapi.Entities;
using dotnetapi.Helpers;
using dotnetapi.Models.Credentials;

namespace dotnetapi.Services
{
    public interface ICredentialService
    {
        CredentialCreateModel Create(CredentialCreateModel model, int userId);
        List<CredentialReadModel> Read(CredentialReadModel model, int userId);
        void Update(CredentialUpdateModel model, int userId);
        void Delete(CredentialDeleteModel model, int userId);
    }

    public class CredentialService: ICredentialService
    {
        private DatabaseContext _context;
        private IMapper _mapper;

        public CredentialService(DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public CredentialCreateModel Create(CredentialCreateModel model, int userId) 
        {
            var curUser = _context.Users.Include(x => x.Credentials).First(u => u.Id == userId);
            
            if (curUser.Credentials.Any(x => x.Hint == model.Hint)) {
                throw new AppException("This credential hint is already used by another of your credentials");
            }
            var cred = _mapper.Map<Credential>(model);
            cred.ValueHash = model.Value;
            cred.Domain = model.Domain.ToLower();
            curUser.Credentials.Add(cred);
            _context.SaveChanges();

            return model;
        }
        public List<CredentialReadModel> Read(CredentialReadModel model, int userId)
        {
            var curUser = _context.Users.Include(x => x.Credentials).First(u => u.Id == userId);
          
            var creds = curUser.Credentials.Where(x => ( (model.Domain == null || (x.Domain == model.Domain || x.Domain == ""))
                                                      && (model.Hint == null   || x.Hint == model.Hint)
                                                      && (model.Id == null     || x.Id == model.Id)
                                                      && (model.Type == null   || x.Type == model.Type)
                                                      )).AsEnumerable();

            var tmp = _mapper.Map<List<CredentialReadModel>>(creds);
            return tmp.ToList();
        }
        public void Update(CredentialUpdateModel model, int userId)
        {
            var user = _context.Users.Include(x => x.Credentials).First(u => u.Id == userId);
            try {
                var credential = user.Credentials.First(x => x.Id == model.Id);

                if (model.Domain != null) {
                    credential.Domain = model.Domain.ToLower();
                }   
                if (model.Hint != null) {
                    credential.Hint = model.Hint;
                }
                if (model.Type != null) {
                    credential.Type = model.Type;
                }
                _context.Users.Update(user);
                _context.SaveChanges();
            } catch (InvalidOperationException){
                throw new AppException("None of your credentials have this Id");
            }
        }
        public void Delete(CredentialDeleteModel model, int userId)
        {
            var user = _context.Users.Include(x => x.Credentials).First(u => u.Id == userId);
            try {
                user.Credentials.Remove(user.Credentials.First(x => x.Id == model.Id));
                _context.Users.Update(user);
                _context.SaveChanges();

            } catch (InvalidOperationException) {
                    throw new AppException("None of your credentials have this Id");
            }
        }
    }
}