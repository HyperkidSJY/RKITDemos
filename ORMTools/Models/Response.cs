using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORMTools.Models
{
    public class Response
    {
        public dynamic Data { get; set; }
        public bool isError { get; set; }
        public string Message { get; set; }

    }
}