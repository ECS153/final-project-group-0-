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
        Credential Create(Credential model, int userId);
        List<Credential> Read(Credential model, int userId);
        void Update(Credential model, int userId);
        void Delete(Credential model, int userId);
    }

    public class CredentialService: ICredentialService
    {
        private DatabaseContext _context;


        public CredentialService(DatabaseContext context)
        {
            _context = context;
        }

        public Credential Create(Credential cred, int userId) 
        {
            var curUser = _context.Users.Include(x => x.Credentials).First(u => u.Id == userId);
            
            if (curUser.Credentials.Any(x => x.Hint == cred.Hint)) {
                throw new AppException("This credential hint is already used by another of your credentials");
            }
          
            cred.Domain = cred.Domain.ToLower();
            curUser.Credentials.Add(cred);
            _context.SaveChanges();

            return cred;
        }

        public List<Credential> Read(Credential cred, int userId)
        {
            var curUser = _context.Users.Include(c => c.Credentials).First(u => u.Id == userId);
          
            var creds = curUser.Credentials.Where(c => ( (cred.Domain == null || (c.Domain == cred.Domain || c.Domain == ""))
                                                      && (cred.Hint == null   || c.Hint == cred.Hint)
                                                      && (cred.Id == 0        || c.Id == cred.Id)
                                                      && (cred.Type == null   || c.Type == cred.Type)
                                                      )).AsEnumerable();
            return creds.ToList();
        }

        public void Update(Credential cred, int userId)
        {
            var user = _context.Users.Include(x => x.Credentials).First(u => u.Id == userId);
            try {
                var credential = user.Credentials.First(x => x.Id == cred.Id);

                if (cred.Domain != null) {
                    credential.Domain = cred.Domain.ToLower();
                }   
                if (cred.Hint != null) {
                    credential.Hint = cred.Hint;
                }
                if (cred.Type != null) {
                    credential.Type = cred.Type;
                }
                _context.Users.Update(user);
                _context.SaveChanges();
            } catch (InvalidOperationException){
                throw new AppException("None of your credentials have this Id");
            }
        }

        public void Delete(Credential cred, int userId)
        {
            var user = _context.Users.Include(x => x.Credentials).First(u => u.Id == userId);
            try {
                user.Credentials.Remove(user.Credentials.First(x => x.Id == cred.Id));
                _context.Users.Update(user);
                _context.SaveChanges();

            } catch (InvalidOperationException) {
                    throw new AppException("None of your credentials have this Id");
            }
        }
    }
}