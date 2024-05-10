using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Company
    {
        public string RazonSocial { get; set; }
        public int IdTipoParte { get; set; }
        public int IdTipoRepresentacion { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string IndicadorJuntaDirectiva { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal PorcentajeCuotas { get; set; }
        public bool IndicadorPuestoVacante { get; set; }

    }
}
