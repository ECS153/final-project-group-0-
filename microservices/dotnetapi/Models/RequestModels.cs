using System.ComponentModel.DataAnnotations;

namespace dotnetapi.Models.Requests
{
    public class BrowserSubmitRequestModel 
    {
        [Required]
        public string FieldId { get; set; }    
        [Required]
        public string Domain { get; set; }
        [Required]
        public string RandToken { get; set; }
        public int? Type { get; set; }
    }
    public class PiRequestSwapModel
    {
        [Required]
        public string FieldId { get; set; }
        [Required]
        public string Domain { get; set; }
        public int? Type {get; set; }
    }
    public class PiSubmitSwapModel
    {
        [Required]
        public int? SwapId { get; set; }
        [Required]
        public int? CredentialId { get; set; }
    }
}