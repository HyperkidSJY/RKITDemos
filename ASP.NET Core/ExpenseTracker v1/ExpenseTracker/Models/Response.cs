namespace ExpenseTracker.Models
{
    public class Response
    {
        public dynamic Data { get; set; }

        public string Message { get; set; }
        
        public bool IsError { get; set; }
    }
}
