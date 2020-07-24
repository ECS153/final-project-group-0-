using System.ComponentModel.DataAnnotations;

namespace dotnetapi.Models.Credentials
{
    public class CredentialCreateModel
    {
        [Required]
        [Range(0,4)]
        public int? Type {get; set;}
        [Required]
        public string Hint { get; set; }
        [Required]
        public string Value { get; set; }
        public string Domain { get; set; } = "";
    }
    public class CredentialReadModel {
        public int? Id { get; set; }
        [Range(0,4)]
        public int? Type { get; set; }
        public string Hint { get; set; }
        public string Domain { get; set; }
    }
    public class CredentialUpdateModel {
        [Required]
        public int? Id { get; set; }
        [Range(0,4)]
        public int? Type { get; set; }
        public string Hint { get; set; }
        public string Domain { get; set; }
    }
    public class CredentialDeleteModel {
        [Required]
        public int? Id { get; set;}
    }
}
