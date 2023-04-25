namespace MyFirstAPI.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string statusDescription { get; set; }

        public List<User> users { get; set; } = new();
    }
}