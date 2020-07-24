using System;
namespace dotnetapi.Entities
{
    public class ProxySwap
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Domain { get; set; }
        public string RandToken { get; set; }
        public string Credential {get; set; }
        public string ExpiresAt {get; set;}
        public int? UserId {get; set;}
    }
}

