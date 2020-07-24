using System.Collections.Generic;

namespace dotnetapi.Entities
{
    public class User
    {  
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PublicCredKey {get; set; }
        public ICollection<EventLog> Logs {get; set; }
    }
}