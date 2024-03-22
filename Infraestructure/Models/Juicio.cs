using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class Juicio
    {
        public int IdJuicio { get; set; }
        public string Expediente { get; set; }
        public string Descrpcion { get; set; }
        public string Caso { get; set; }
        public string Asunto { get; set; }
        public Nullable<decimal> Cuantia { get; set; }
        public string MonedaCuantia { get; set; }
        public string Oficina { get; set; }
        public string Circuito { get; set; }
        public string Sentencia { get; set; }
        public string Estado { get; set; }
        public Nullable<System.DateTime> FechaUltimaAct { get; set; }

        public Nullable<System.DateTime> FechaCreacion { get; set; }

        public Nullable<System.DateTime> FechaEstado { get; set; }
    }
}
