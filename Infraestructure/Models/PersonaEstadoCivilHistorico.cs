using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class PersonaEstadoCivilHistorico
    {
        public int IdEvento { get; set; }

        [JsonIgnore]
        public Nullable<int> IdPersona { get; set; }
        [JsonIgnore]

        public Nullable<int> IdPersonaConyuge { get; set; }
        public string NombreCoyugue { get; set; }
        public string CitaMatrimonio { get; set; }
        public Nullable<short> TipoSuceso { get; set; }
        public Nullable<short> TipoMatrimonio { get; set; }
        public Nullable<System.DateTime> FechaSuceso { get; set; }
        [JsonIgnore]
        public Nullable<System.DateTime> FechaEntrada { get; set; }
        public Nullable<short> ProvinciaSuceso { get; set; }
        public Nullable<short> CantonSuceso { get; set; }
        public Nullable<short> DistritoSuceso { get; set; }
        public string LugarSuceso { get; set; }
        [JsonIgnore]
        public Nullable<bool> EsOculto { get; set; }
        [JsonIgnore]
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        [JsonIgnore]
        public Nullable<System.DateTime> FechaOculto { get; set; }
        [JsonIgnore]
        public Nullable<int> IdFuente { get; set; }
    }
}
