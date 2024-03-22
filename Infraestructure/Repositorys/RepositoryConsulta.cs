using Infraestructure;
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

namespace Repository.Repositorys
{
    public class RepositoryConsulta : IRepositoryConsulta
    {
        IConfiguration Configuration { get; }

        public RepositoryConsulta(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private List<Consulta> GetAll(int PersonID)
        {
            try
            {
                List<Consulta> consultas = null;

                string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                        using (var command = new SqlCommand("ConsultarDatosPersona", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@IdPersona", PersonID));

                            using (var reader = command.ExecuteReader())
                            {
                                consultas = new List<Consulta>();
                                while (reader.Read()) 
                                {
                                    
                                    var consulta = new Consulta()
                                    {
                                        IdHuellaBusqueda = reader["IdHuellaBusqueda"] != DBNull.Value ? Convert.ToInt64(reader["IdHuellaBusqueda"]) : 0,
                                        FechaConsulta = reader["FechaConsulta"] != DBNull.Value ? Convert.ToDateTime(reader["FechaConsulta"]) : default(DateTime),
                                        FechaBusqueda = reader["FechaBusqueda"] != DBNull.Value ? Convert.ToDateTime(reader["FechaBusqueda"]) : default(DateTime),
                                        Producto = reader["Producto"] != DBNull.Value ? reader["Producto"].ToString() : null,
                                        Motivo = reader["Motivo"] != DBNull.Value ? reader["Motivo"].ToString() : null,
                                        Tipo = reader["Tipo"] != DBNull.Value ? reader["Tipo"].ToString() : null,
                                        IP = reader["IP"] != DBNull.Value ? reader["IP"].ToString() : null
                                    };

                                    consultas.Add(consulta); 
                                }
                            }
                        }
                    }
                
                return consultas;
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
        public List<Consulta> GetAllByPerson(int PersonId)
        {
            try
            {
                List<Consulta> consultas = GetAll(PersonId);

                if(consultas != null && consultas.Count > 0)
                {
                    return consultas;
                }
               

                return consultas;
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
