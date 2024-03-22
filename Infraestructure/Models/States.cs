using Repository.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class States
    {
        public States()
        {
           BienesMuebles  = new List<RealEstate>();
        }
        public PersonalProperty BienesInmuebles { get; set; }
        public List<RealEstate> BienesMuebles { get; set; }
    }
}
