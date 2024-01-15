using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public int userId { get; set; }
        public DateTime Date { get; set; }
        public bool IsSuccess { get; set; }
        public JObject Result { get; set; }
    }
}
