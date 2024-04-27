using Infraestructure.Models;
using Microsoft.Extensions.Configuration;
using Repository.Models;
using Repository.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Services
{
    public class ServiceConsulta : IServiceConsulta
    {
        private IRepositoryConsulta repository;
        public ServiceConsulta(IConfiguration configuration)
        {
            repository = new RepositoryConsulta(configuration);
        }

        public void AddConsultaYFacturarion(Facturacion facturacion, Consulta consulta)
        {
            repository.AddConsultaYFacturarion(facturacion, consulta);
        }
    }
}
