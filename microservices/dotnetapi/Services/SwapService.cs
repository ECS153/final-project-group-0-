using Microsoft.EntityFrameworkCore;
using System.Linq;

using dotnetapi.Entities;
using dotnetapi.Helpers;
using dotnetapi.Models.Requests;

namespace dotnetapi.Services
{
    public interface ISwapService
    {
        void Create(RequestSwap reqSwap);
        RequestSwap GetTop(int userId);
        void Swap(PiSubmitSwapModel model, int userId);
    }
    public class SwapService : ISwapService
    {
        private DatabaseContext _context;

        public SwapService(DatabaseContext context) 
        {
            _context = context;
        }

        public void Create(RequestSwap reqSwap)
        {
            _context.Add(reqSwap);
            _context.SaveChanges();
        }
 
        public RequestSwap GetTop(int userId)
        {
            // Find user by Id
            var user = _context.Users.Include(x => x.RequestSwaps).FirstOrDefault(u => u.Id == userId);
            if (user == null) {
                throw new AppException("No Users with this ID have been found");
            }

            // Find user's first RequestSwap, order by RequestSwap.Id
            if (user.RequestSwaps.Any()) {
                return user.RequestSwaps.OrderBy(r => r.Id).FirstOrDefault();
            } 
            //If User has no requests swaps, return null
            return null;
        } 
        public void Swap(PiSubmitSwapModel model, int userId)
        {
            // Find user by Id
            var user = _context.Users.Include(u => u.Credentials).Include(r => r.RequestSwaps).FirstOrDefault(u => u.Id == userId);
            if (user == null) {
                throw new AppException("No Users with this ID have been found");
            }

            // Verify that the credential is allowed to be used
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

            // Transform RequestSwap into ProxySwap
            var proxySwap = new ProxySwap();
            proxySwap.Ip = reqSwap.Ip;
            proxySwap.Domain = reqSwap.Domain;
            proxySwap.RandToken = reqSwap.RandToken;
            proxySwap.Credential = userCred.ValueHash;

            // Add to ProxySwap Database, remove from RequestSwap Database
            _context.ProxySwaps.Add(proxySwap);
            user.RequestSwaps.Remove(reqSwap); //TODO: ADD BACK LATER
            _context.SaveChanges();
        }
    
    }
}