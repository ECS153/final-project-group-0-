using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using dotnetapi.Entities;
using dotnetapi.Helpers;
using dotnetapi.Models.Credentials;

using dotnetapi.Models.Requests;


namespace dotnetapi.Services
{
    public interface ISwapService
    {
        void Create(RequestSwap ReqSwap);
        RequestSwap GetTop(int UserId);
        void Swap(SubmitSwapModel model, int UserId);
    }
    public class SwapService : ISwapService
    {
        private DatabaseContext _context;

        public SwapService(DatabaseContext context) 
        {
            _context = context;
        }

        public void Create(RequestSwap ReqSwap)
        {
            _context.Add(ReqSwap);
            _context.SaveChanges();
        }

 
        public RequestSwap GetTop(int UserId)
        {
            var user = _context.Users.Include(x => x.RequestSwaps).FirstOrDefault(u => u.Id == UserId);
            if (user == null) {
                throw new AppException("No Users with this ID have been found");
            }
            if (user.RequestSwaps.Any()) {
                return user.RequestSwaps.OrderBy(r => r.Id).FirstOrDefault();
            } 
            return null;
        } 
        public void Swap(SubmitSwapModel model, int UserId)
        {
            var user = _context.Users.Include(u => u.Credentials).Include(r => r.RequestSwaps).FirstOrDefault(u => u.Id == UserId);
            if (user == null) {
                throw new AppException("No Users with this ID have been found");
            }
            //Verify that the credential is allowed to be used
           
            var userCred = user.Credentials.FirstOrDefault(c => c.Id == model.CredentialId);
            if (userCred == null) {
                throw new AppException("User does not have a credential with this ID");
            }

            var reqSwap = user.RequestSwaps.FirstOrDefault(r => r.Id == model.SwapId);
            if (reqSwap == null) {
                throw new AppException("User does not have any pending Request Swaps with this Id");
            }

            if (reqSwap.Domain != userCred.Domain) {
                throw new AppException("User is not permitted to use this credential on this domain");
            }

            var proxySwap = new ProxySwap();
            proxySwap.Ip = reqSwap.Ip;
            proxySwap.Domain = reqSwap.Domain;
            proxySwap.RandToken = reqSwap.RandToken;
            proxySwap.Credential = userCred.ValueHash;

            _context.ProxySwaps.Add(proxySwap);
            _context.SaveChanges();
           
        }
    
    }
}