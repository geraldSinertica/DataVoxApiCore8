using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Appointment
    {
        public Appointment()
        {
            Nombramientos = new List<Company>();
        }
        public List<Company> Nombramientos { get; set; }
    }
}
