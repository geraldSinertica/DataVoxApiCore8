using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Consulta
    {
        [JsonIgnore]
        public long IdHuellaBusqueda { get; set; }

        public DateTime FechaConsulta { get; set; }
        public DateTime FechaBusqueda { get; set; }
        public string Cliente { get; set; }

        [JsonIgnore]
        public char Cobrable { get; set; }

        [JsonIgnore]
        public int IdCliente { get; set; }
        [JsonIgnore]
        public int IdPersona { get; set; }
        [JsonIgnore]
        public string Producto { get; set; }
        [JsonIgnore]
        public int Motivo { get; set; }
        [JsonIgnore]
        public string Tipo { get; set; }
        [JsonIgnore]
        public string IP { get; set; }
    }
}
