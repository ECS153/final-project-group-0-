using System.ComponentModel.DataAnnotations;

namespace dotnetapi.Models.RequestModels
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
    public class PiRequestSwapModel
    {
        [Required]
        public string FieldId { get; set; }
        
    }


}