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
using dotnetapi.Models.Requests;

namespace dotnetapi.Services
{
    public interface ISwapService
    {
        void Dequeue(int userId);
        void Enqueue(RequestSwap reqSwap);
        RequestSwap Front(int userId);
        void Swap(PiSubmitSwapModel model, int userId);
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
            // Find user by Id
            User user = findUser(userId);

            // Find user's first RequestSwap, order by RequestSwap.Id
            if (user.RequestSwaps.Any()) {
                var topReq = user.RequestSwaps.OrderBy(r => r.Id).FirstOrDefault();
                user.RequestSwaps.Remove(topReq);
                _context.SaveChanges();
            } else {
                throw new AppException("User has no pending Request Swaps");
            }
        } 
 
        public void Enqueue(RequestSwap reqSwap)
        {
            _context.Add(reqSwap);
            _context.SaveChanges();
        }

        public RequestSwap Front(int userId)
        {
            // Find user by Id
            User user = findUser(userId);

            // Find user's first RequestSwap, order by RequestSwap.Id, If User has no requests swaps, return null
            if (user.RequestSwaps.Any()) {
                return user.RequestSwaps.OrderBy(r => r.Id).FirstOrDefault();
            } else {
                return null;
            }
        }

        public void Swap(PiSubmitSwapModel model, int userId)
        {
            // Find user by Id
            User user = findUser(userId);

            // Verify that the credential is allowed to be used
            var userCred = user.Credentials.FirstOrDefault(c => c.Id == model.CredentialId);
            if (userCred == null) {
                throw new AppException("User does not have a credential with this ID");
            }
            var reqSwap = user.RequestSwaps.FirstOrDefault(r => r.Id == model.SwapId);
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
            user.RequestSwaps.Remove(reqSwap);
            _context.SaveChanges();
        }
    
        ////////////////////////////////////////////////////////////////////////////////
        //////////////////////// Private Helper Functions //////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        private User findUser(int userId)
        {
            // Find user by Id
            var user = _context.Users.Include(u => u.Credentials).Include(r => r.RequestSwaps).FirstOrDefault(u => u.Id == userId);
            if (user == null) {
                throw new AppException("No Users with this ID have been found");
            }
            return user;
        }
    }
}