namespace UserDataService.Grpc.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
