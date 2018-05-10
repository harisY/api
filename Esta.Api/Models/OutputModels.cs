using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esta.Api.Models
{
    public class OutputModels
    {
        public string api_status { get; set; }
        public string api_message { get; set; }
        public object items { get; set; }
    }
}