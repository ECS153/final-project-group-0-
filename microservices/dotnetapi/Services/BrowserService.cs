using dotnetapi.Entities;

namespace dotnetapi.Services
{
    public interface IBrowserService
    {
        void Create(RequestSwap ReqSwap);
    }
    public class BrowserService : IBrowserService
    {
        private RequestSwapContext Dbcontext;

        public BrowserService(RequestSwapContext context) 
        {
            Dbcontext = context;
        }

        public void Create(RequestSwap ReqSwap)
        {
            Dbcontext.Add(ReqSwap);
            Dbcontext.SaveChanges();
        }
    
    }
}