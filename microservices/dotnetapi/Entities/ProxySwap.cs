using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System;
namespace dotnetapi.Entities
{
    public class ProxySwap
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Ip { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string RandToken { get; set; }
        [Required]
        public string Credential {get; set; }
    }
}

