using AplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repositorys;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
       private  IConfiguration Configuration;
        private IRepositoryUsuario repository;

        public ServiceUsuario(IConfiguration configuration) {
            Configuration=configuration;
            repository=new RepositoryUsuario(Configuration);
        }
        public LoginDaTa login(string email, string password)
        {
            string crytpPasswd = Cryptography.EncrypthAES(password);

            return repository.login(email, crytpPasswd);
        }
    }
}
