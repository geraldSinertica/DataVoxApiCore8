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
           
            Persona = new List<Vinculo>();
        }
       
        public TipoVinculo Tipo { get; set; }
        public List<Vinculo> Persona { get; set; }
       
    }
}
