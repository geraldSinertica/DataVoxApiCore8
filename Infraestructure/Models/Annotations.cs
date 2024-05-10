using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
  public  class Annotation
    {
        public long IdAnotacion { get; set; }
        public string CitaAnotacion { get; set; }
        public string TipoOperacion { get; set; }
        public DateTime? FechaAnotacion { get; set; }
        public short? Derecho { get; set; }
        public int? IdGravamen { get; set; }
        public string CreditoAsociado { get; set; }
        public short? SecuenciaAfectada { get; set; }
    }
}
