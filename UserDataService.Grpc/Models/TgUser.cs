namespace UserDataService.Grpc.Models
{
    public class TgUser
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime AddDate { get; set; }
    }
}
