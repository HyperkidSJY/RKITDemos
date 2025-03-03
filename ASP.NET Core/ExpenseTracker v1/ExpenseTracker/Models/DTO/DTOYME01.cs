using Newtonsoft.Json;

namespace ExpenseTracker.Models.DTO
{
    public class DTOYME01
    {
        [JsonProperty("E01101")]
        public int E01F01 { get; set; } //id

        [JsonProperty("E01102")]
        public string E01F02 { get; set; } //title

        [JsonProperty("E01103")]
        public double E01F03 { get; set; } //expense

        [JsonProperty("E01104")]
        public DateTime E01F04 { get; set; } //date

        [JsonProperty("E01105")]
        public int E01F05 { get; set; } //user id
    }
}
