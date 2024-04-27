using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class Facturacion
    {
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public int TipoReporte { get; set; }
    }
}
