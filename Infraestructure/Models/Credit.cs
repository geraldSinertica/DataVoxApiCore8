using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Credit
    {
        public string NumeroCredito { get; set; }
        public DateTime FechaOtorgamiento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Estado { get; set; }
        public string Moneda { get; set; }
        public decimal MontoOtorgado { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal SaldoMora { get; set; }
        public decimal IndiceMora { get; set; }
        public decimal ValorCuota { get; set; }
        public int DiasMora { get; set; }           
    }
}
