using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Vehicles
    {
        public int IdMueble { get; set; }
        public string Placa { get; set; }
        public string TipoBien { get; set; }
        public string NumeroVIN { get; set; }
        public string NumeroChasis { get; set; }
        public string Marca { get; set; }
        public string Carroceria { get; set; }
        public int AnoFabricacion { get; set; }
        public short? CapacidadPasajeros { get; set; }
        public string NumeroMotor { get; set; }
        public string NumeroSerieMotor { get; set; }
        public string FabricanteMotor { get; set; }
        public string ClaseBien { get; set; }
        public string CodigoBien { get; set; }
        public decimal? NumeroRegistral { get; set; }
        public string TomoInscripcion { get; set; }
        public string AsientoInscripcion { get; set; }
        public string SecuenciaInscripcion { get; set; }
        public DateTime? FechaInscripcion { get; set; }
        public string NumIdAnterior { get; set; }
        public string CitasAnteriores { get; set; }
        public decimal? ValorHaciendaInscripcion { get; set; }
        public decimal? ValorHaciendaOficial { get; set; }
        public decimal? ValorContrato { get; set; }
        public string EstadoActual { get; set; }
        public string EstadoActualTributario { get; set; }
        public string Uso { get; set; }
        public string TomoUltMovimiento { get; set; }
        public string AsientoUltMovimiento { get; set; }
        public string SecuenciaUltMovimiento { get; set; }
        public string FechaUltMovimiento { get; set; }
        public int? IdMoneda { get; set; }
    }
}
