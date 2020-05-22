using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace dotnetapi.Entities
{
    public class RequestSwap
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FieldId { get; set; }
        public string Ip { get; set; }
        public string Domain { get; set; }
        public string RandToken { get; set; }
        public int? Type { get; set; }
    }
}