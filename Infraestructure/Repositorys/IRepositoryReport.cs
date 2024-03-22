using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositorys
{
   public interface IRepositoryReport
    {
        Report PersonReport(string identification);
    }
}
