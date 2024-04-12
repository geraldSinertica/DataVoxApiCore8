using Infraestructure.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorys.Auth
{
    public interface IRepositoryAuth
    {
        HistorialRefreshToken GetHistorialRefreshToken(string token, string RefreshToken,int IdUsuario);
        HistorialRefreshToken SavehistorialRefreshToken(HistorialRefreshToken historialRefreshToken);
    }
}
