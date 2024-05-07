using Infraestructure.Models;
using Microsoft.Extensions.Configuration;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorys
{
    public class RepositoryPrecalificado : IRepositoryPrecalificado
    {
        IConfiguration Configuration { get; set; }

        public RepositoryPrecalificado(IConfiguration configuration) { 
            Configuration= configuration;
        }
        
        private List<Reglas> getRules(string identification)
        {
            try
            {
              List<Reglas> rules = null;

                string cadena = Configuration.GetConnectionString("DataVoxConnection");


                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                     var command = new SqlCommand("[dbo].[dbo.PreCalificado-MExpress]", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Identificacion", identification));

                     var reader = command.ExecuteReader();
                    rules = new List<Reglas>();
                    while (reader.Read()) 
                    {
                      
                        var rule = new Reglas()
                        {
                            Codigo = reader["CodigoRegla"] != DBNull.Value ? reader["CodigoRegla"].ToString() : "",
                            Resultado = reader["Resultado"] != DBNull.Value ? reader["Resultado"].ToString() : "",
                            Detalle = reader["Detalle"] != DBNull.Value ? reader["Detalle"].ToString() : ""
                        };
                        rules.Add(rule); 
                    }
                }


                return rules;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public Precalificado getPrecalificacionbyId(string identification)
        {
            try
            {
                List<Reglas> rules = getRules(identification);

                Precalificado precalificado = new Precalificado();

                precalificado.Reglas= rules;
              


                return precalificado;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
