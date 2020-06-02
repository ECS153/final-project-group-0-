using System.ComponentModel.DataAnnotations;

namespace dotnetapi.Models.Requests
{
    public class SubmitRequestModel 
    {
        [Required]
        public string FieldId { get; set; }  
        [Required]
        [Range(0,9999)]
        public int? AuthId { get; set; }  
        [Required]
        public string Domain { get; set; }
        [Required]
        public string RandToken { get; set; }
        [Required]
        public int? Type { get; set; }
    }
    public class RequestSwapModel
    {
        public int? Id { get; set; }
        [Required]
        [Range(0,9999)]
        public int? AuthId { get; set; } 
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
}