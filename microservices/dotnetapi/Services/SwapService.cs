/* 
    This class is in charge of the ProxySwap and RequestSwap tables in the database.
    It is the only class that modifies or gets values from them 
    Each User has a list of RequestSwaps. This list acts as a queue, and you have
    operations for Enqueue, Dequeue, and Front. 
*/
using Microsoft.EntityFrameworkCore;
using System.Linq;

using dotnetapi.Entities;
using dotnetapi.Helpers;

namespace dotnetapi.Services
{
    public interface ISwapService
    {
        void Dequeue(int userId);
        void Enqueue(RequestSwap reqSwap);
        RequestSwap Front(int userId);
        void Swap(int? credId, int userId);
    }
    public class SwapService : ISwapService
    {
        private DatabaseContext _context;

        public SwapService(DatabaseContext context) 
        {
            _context = context;
        }

        public void Dequeue(int userId)
        {
            // Find user's first RequestSwap, order by RequestSwap.Id
            var topReq = _context.RequestSwaps.OrderBy(r => r.Id).FirstOrDefault(r => r.UserId == userId);
            if (topReq == null) {
                throw new AppException("User has no pending Request Swaps");
            }
            _context.RequestSwaps.Remove(topReq);
            _context.SaveChanges();            
        } 
 
        public void Enqueue(RequestSwap reqSwap)
        {
            _context.Add(reqSwap);
            _context.SaveChanges();
        }

        public RequestSwap Front(int userId)
        {
            RequestSwap topReq = _context.RequestSwaps.OrderBy(r => r.Id).FirstOrDefault(r => r.UserId == userId);
            return topReq;
        }

        public void Swap(int? credId, int userId)
        {
            // Verify that the credential is allowed to be used
            var userCred = _context.Credentials.FirstOrDefault(c => c.Id == credId && c.UserId == userId);
            if (userCred == null) {
                throw new AppException("User does not have a credential with this ID");
            }
            // Get user's top requestswap
            var reqSwap = Front(userId);
            if (reqSwap == null) {
                throw new AppException("User does not have any pending Request Swaps with this Id");
            }
            if (reqSwap.Domain != userCred.Domain && userCred.Domain  != "") {
                throw new AppException("User is not permitted to use this credential on this domain");
            }

            // Transform RequestSwap into ProxySwap
            var proxySwap = new ProxySwap();
            proxySwap.Ip = reqSwap.Ip;
            proxySwap.Domain = reqSwap.Domain;
            proxySwap.RandToken = reqSwap.RandToken;
            proxySwap.UserId = reqSwap.UserId;
            proxySwap.Credential = userCred.ValueHash;
 
            // Add to ProxySwap Database, remove from RequestSwap Database
            _context.ProxySwaps.Add(proxySwap);
            _context.RequestSwaps.Remove(reqSwap);
            _context.SaveChanges();
        }
    

    }
}