using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Filiacion
    {
        public Filiacion()
        {
            Hijos = new List<Vinculo>();
            Hermanos = new List<Vinculo>();
            Tios = new List<Vinculo>();
        }
        public Vinculo Padre { get; set; }
        public Vinculo Madre { get; set; }
        public Vinculo Conyugue { get; set; }
        public List<Vinculo> Hijos { get; set; }
        public List<Vinculo> Hermanos { get; set; }
        public List<Vinculo> Tios { get; set; }
    }
}
