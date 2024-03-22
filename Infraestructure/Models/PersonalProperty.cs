using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class PersonalProperty
    {
        public PersonalProperty()
        {
            Vehiculos = new List<Vehicles>();
            Aeronaves = new List<Aircraft>();
            Buques = new List<Ships>();
        }
        public List<Vehicles> Vehiculos { get; set; }
        public List<Aircraft> Aeronaves { get; set; }
        public List<Ships> Buques { get; set; }
    }
}
