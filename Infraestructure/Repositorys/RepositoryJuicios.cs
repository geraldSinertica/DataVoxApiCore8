using Infraestructure;
using Infraestructure.Models;
using Microsoft.Extensions.Configuration;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositorys
{
    public class RepositoryJuicios : IRepositoryJuicios
    {
        IConfiguration Configuration { get; }

        public RepositoryJuicios(IConfiguration configuration) { 
            Configuration = configuration;
        }
        public List<Juicio> GetJudgmentsbyPerson(int PersonId)
        {
            try
            {
                List<Juicio> judgments = null;

                string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                        using (var command = new SqlCommand("ObtenerJuiciosByPersonaId", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                            using (var reader = command.ExecuteReader())
                            {
                                judgments = new List<Juicio>();
                                while (reader.Read()) 
                                {

                                    var judgment = new Juicio()
                                    {
                                        IdJuicio = Convert.ToInt32(reader["IdJuicio"]),
                                        Expediente = reader["Expediente"].ToString(),
                                        Descrpcion = reader["Descrpcion"].ToString(),
                                        Caso = reader["Caso"].ToString(),
                                        Asunto = reader["Asunto"].ToString(),
                                        Cuantia = reader["Cuantia"] != DBNull.Value ? Convert.ToDecimal(reader["Cuantia"]) : 0,
                                        MonedaCuantia = reader["MonedaCuantia"] != DBNull.Value ? reader["MonedaCuantia"].ToString() : "",
                                        Oficina = reader["Oficina"].ToString(),
                                        Circuito = reader["Circuito"].ToString(),
                                        Sentencia = reader["Sentencia"].ToString(),
                                        Estado = reader["Estado"].ToString(),
                                        FechaUltimaAct = reader["FechaUltimaAct"] != DBNull.Value ? Convert.ToDateTime(reader["FechaUltimaAct"]) : (DateTime?)null,
                                        FechaCreacion = reader["FechaCreacion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaCreacion"]) : (DateTime?)null,
                                        FechaEstado = reader["FechaEstado"] != DBNull.Value ? Convert.ToDateTime(reader["FechaEstado"]) : (DateTime?)null
                                    };

                                    judgments.Add(judgment); 
                                }
                            }
                        }
                    }
                

                return judgments;
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
