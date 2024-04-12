using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Models
{
    public class Auth
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

    }
}
