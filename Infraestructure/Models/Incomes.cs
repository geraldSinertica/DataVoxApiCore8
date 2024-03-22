using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
   public class Incomes
    {
        public decimal LastSalary { get; set; }
        public decimal  Avg2Months { get; set; }
        public decimal Avg3Months { get; set; }
        public decimal Avg6Months { get; set; }
        public decimal AvgYear { get; set; }
        public DateTime LastUpdate  { get; set; }

    }
}
