using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class Precalificado
    {
        public Precalificado() {
            Reglas = new List<Reglas>();
        }
        public List<Reglas> Reglas { get; set; }
    }
}
