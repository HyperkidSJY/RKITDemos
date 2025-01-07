using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORMTools.Models
{
    public class DTOSTU01
    {
        [JsonProperty("P01101")]//id
        public int P01F01 { get; set; }

        [JsonProperty("P01102")] //name
        public string P01F02 { get; set; }

        [JsonProperty("P01103")] //dept
        public string P01F03 { get; set; }

        [JsonProperty("P01104")]//sem
        public int P01F04 { get; set; }

    }
}