namespace dotnetapi.Entities
{
    public class EventLog
    {
        public int Id { get; set; }
       
        public int Level {get; set; } = 0;
        public string Message { get; set; }
    }
}