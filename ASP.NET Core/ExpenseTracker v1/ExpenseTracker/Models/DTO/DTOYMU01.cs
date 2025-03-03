using Newtonsoft.Json;

namespace ExpenseTracker.Models.DTO
{
    public class DTOYMU01
    {
        [JsonProperty("U01101")]
        public int U01F01 { get; set; } //id

        [JsonProperty("U01102")]
        public string U01F02 { get; set; } //email

        [JsonProperty("U01103")]
        public string U01F03 { get; set; } //password
    }
}
