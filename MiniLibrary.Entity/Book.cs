namespace MiniLibrary.Entity
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public double? Price { get; set; }
    }
}
