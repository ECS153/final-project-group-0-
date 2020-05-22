using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

using dotnetapi.Entities;


namespace dotnetapi.Services
{
    public interface IRequestSwapService
    {
        void Create(RequestSwap ReqSwap);
       // List<RequestSwap> Read(int userId);
    }
    public class RequestSwapService : IRequestSwapService
    {
        private DatabaseContext _context;

        public RequestSwapService(DatabaseContext context) 
        {
            _context = context;
        }

        public void Create(RequestSwap ReqSwap)
        {
            _context.Add(ReqSwap);
            _context.SaveChanges();
        }

 

       /* public List<RequestSwap> Read(int userId) 
        {
            var reqSwaps = _context.RequestSwaps.Where(r => r.Id == userId);


            

        }*/
    
    }
}