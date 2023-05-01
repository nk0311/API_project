namespace MyFirstAPI.Models
{
    public class Response
    {
        //internal object cars;

        public int statusCode { get; set; }
        public string statusDescription { get; set; }

        public List<User> users { get; set; } = new();
        public List<Car> cars { get; set; } = new();
    }
}