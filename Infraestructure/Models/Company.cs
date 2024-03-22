using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Company
    {
        public string Cargo { get; set; }
        public string NombreComercial { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string FinesEmpresa { get; set; }
        public string DescProrrogas { get; set; }
        public string Representacion { get; set; }
        public decimal MontoCapital { get; set; }
        public int CantidadAcciones { get; set; }
    }
}
