using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Consulta
    {
        public long IdHuellaBusqueda { get; set; }
        public DateTime FechaConsulta { get; set; }
        public DateTime FechaBusqueda { get; set; }
        public string Producto { get; set; }
        public string Motivo { get; set; }
        public string Tipo { get; set; }
        public string IP { get; set; }
    }
}
