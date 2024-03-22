using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class ContactData
    {
        public ContactData()
        {
            Direcciones = new List<Direcciones>();
            Telefonos = new List<Telefonos>();
        }
        public List<Telefonos> Telefonos { get; set; }
        public List<Direcciones> Direcciones { get; set; }
    }
}
