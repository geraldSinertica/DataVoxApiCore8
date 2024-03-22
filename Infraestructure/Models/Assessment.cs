using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Assessment
    {
        public string CitaGravamen { get; set; }
        public string Moneda { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaInicia { get; set; }
        public DateTime FechaVence { get; set; }
        public DateTime? FechaInterrupcion { get; set; }
        public string Interes { get; set; }
        public string FormaPago { get; set; }
        public DateTime? FechaUltActualizacion { get; set; }
        public char? ClaseResponsabilidad { get; set; }
        public int? TomoCredito { get; set; }
        public int? AsientoCredito { get; set; }
        public int? ConsecutivoCredito { get; set; }
        public int? SecuenciaCredito { get; set; }
        public int? SubsecuenciaCredito { get; set; }
        public int? Grado { get; set; }
        public string ReferenciaFinca { get; set; }
        public string ReferenciaGravamen { get; set; }
        public string BaseRemate { get; set; }
    }
}
