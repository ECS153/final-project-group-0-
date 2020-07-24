namespace dotnetapi.Entities
{
    public class RequestSwap
    {
        public int Id { get; set; }
        public int? UserId { get; set; } //key references User Table
        public int? AuthId { get; set; }
        
        public string FieldId { get; set; }
        public string Ip { get; set; }
        public string Domain { get; set; }
        public string RandToken { get; set; }
        public int? Type { get; set; }
    }
}