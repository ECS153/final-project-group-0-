using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using dotnetapi.Entities;
using dotnetapi.Helpers;
using dotnetapi.Models.Requests;

namespace dotnetapi.Services
{
    public interface ILogService
    {
        List<EventLog> getLogs(int userId);
    }
    public class LogService : ILogService
    {
        private DatabaseContext _context;

        public LogService(DatabaseContext context) 
        {
            _context = context;
        }

        public List<EventLog> getLogs(int userId)
        {
            var user = _context.Users.Include(u => u.Logs).FirstOrDefault(u => u.Id == userId);
            if (user == null) {
                throw new AppException("No Users with this ID have been found");
            }

            return user.Logs.ToList();

        }
        
    
    }
}