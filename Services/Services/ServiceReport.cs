using Microsoft.Extensions.Configuration;
using Repository.Models;
using Repository.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Servicess
{
   public class ServiceReport: IServiceReporte
    {
        private IRepositoryReport repository;
        public ServiceReport(IConfiguration configuration)
        {
            repository = new RepositoryReport(configuration);
        }

        public async Task<Report> PersonReport(string identification)
        {
            return repository.PersonReport(identification);
        }
    }
}
