using ServiceStack.DataAnnotations;

namespace ExpenseTracker.Models.POCO
{
    public class YME01
    {
        [AutoIncrement]
        [PrimaryKey]
        public int E01F01 { get; set; } //id

        public string E01F02 { get; set; } //title

        public double E01F03 { get; set; } //expense

        public DateTime E01F04 { get; set; } //date

        [ForeignKey(typeof(YMU01))]
        public int E01F05 { get; set; } //user id
    }
}
