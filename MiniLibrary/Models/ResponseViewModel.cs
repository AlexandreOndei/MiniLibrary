namespace MiniLibrary.Models
{
    public class ResponseViewModel
    {
        public ResponseViewModel()
        {
            Success = true;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public string Element { get; set; }
    }
}
