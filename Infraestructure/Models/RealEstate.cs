using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositorys
{
    public class RealEstate
    {
        public RealEstate()
        {
            Annotations = new List<Annotation>();
            Assessments = new List<Assessment>();
        }
        public int IdInmueble { get; set; }
        public string NumeroFinca { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public decimal? Medida { get; set; }
        public string IdOperacion { get; set; }
        public string Presentacion { get; set; }
        public DateTime? FechaUltActualizacion { get; set; }
        public string Naturaleza { get; set; }
        public string Plano { get; set; }
        public decimal? Avaluo { get; set; }
        public decimal? ValorPorcentual { get; set; }
        public List<Limit> Limits { get; set; }
       public List<Annotation> Annotations { get; set; }
       public List<Assessment> Assessments { get; set; }
    }
}
