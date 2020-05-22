using System.ComponentModel.DataAnnotations;

namespace dotnetapi.Models.Requests
{
    public class BrowserRequestSwapModel 
    {
        [Required]
        public string FieldId { get; set; }    
        [Required]
        public string Domain { get; set; }
        [Required]
        public string RandToken { get; set; }
        public int? Type { get; set; }
    }
    public class RequestSwapModel
    {
        [Required]
        public string FieldId { get; set; }
        [Required]
        public string Domain { get; set; }
        public int? Type {get; set; }
    }
    public class SubmitSwapModel
    {
        [Required]
        public int? SwapId { get; set; }
        [Required]
        public int? CredentialId { get; set; }
    }
    public class UserAuthenticatModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }


}