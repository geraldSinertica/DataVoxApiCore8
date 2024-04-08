using Infraestructure.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Servicess
{
    public interface IServiceReporte
    {
       Task<Report> PersonReport(string identification);
       ReportJSON getPersonReport(string identificacion, int idType);
    }
}
