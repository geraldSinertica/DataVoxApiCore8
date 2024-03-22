using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
   public class Operations
    {
        public Operations()
        {
            OpenOperations = new List<Credit>();
            CloseOperations = new List<Credit>();
        }
       public List<Credit> OpenOperations { get; set; }
       public List<Credit> CloseOperations { get; set; }
    }
}
