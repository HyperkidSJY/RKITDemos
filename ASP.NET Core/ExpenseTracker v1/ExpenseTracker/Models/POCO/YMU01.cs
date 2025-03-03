using ServiceStack.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class YMU01
    {
        [AutoIncrement]
        [PrimaryKey]
        public int U01F01 { get; set; } //id

        [Unique]
        public string U01F02 { get; set; } //email

        public string U01F03 { get; set; } //password
    }
}
