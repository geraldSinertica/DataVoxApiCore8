using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataVox.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Isssuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
