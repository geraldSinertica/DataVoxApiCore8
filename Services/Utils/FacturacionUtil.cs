using Infraestructure.Models;
using Infraestructure.Models.Auth;
using Newtonsoft.Json.Linq;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Utils
{
    public class FacturacionUtil
    {
        public Facturacion createBill(int idUsuario, int idCliente, int tipoReporte)
        {
            try
            {


                return new Facturacion() { IdUsuario = idUsuario, IdCliente = idCliente, TipoReporte = tipoReporte };


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Consulta CreateConsulta(string ip,int idPersona)
        {
            try
            {


                return new Consulta() { FechaBusqueda = DateTime.Now, FechaConsulta = DateTime.Now, Motivo = 23
                    , Producto = "1", Tipo = "1",IP=ip,IdPersona=idPersona,IdCliente=1,Cobrable='S' };


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
