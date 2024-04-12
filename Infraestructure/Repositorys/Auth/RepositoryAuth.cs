using Infraestructure.Models;
using Infraestructure.Models.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorys.Auth
{
    public class RepositoryAuth:IRepositoryAuth
    {
        private readonly IConfiguration Configuration;

        public RepositoryAuth(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private HistorialRefreshToken historialRefreshToken(string token, string RefreshToken, int IdUsuario)
        {
            try
            {
                HistorialRefreshToken historial = null;

                
                string cadena = Configuration.GetConnectionString("DataVoxConnection");
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    using var command = new SqlCommand("ObtenerDatosHistorialRefreshToken", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@token", token));
                    command.Parameters.Add(new SqlParameter("@RefreshToken", RefreshToken));
                    command.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));

                    using var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        historial = new HistorialRefreshToken
                        {
                            IdHistorialToken = reader["IdHistorialToken"] != DBNull.Value ? Convert.ToInt32(reader["IdHistorialToken"]) : 0,
                            IdUsuario = reader["IdUsuario"] != DBNull.Value ? Convert.ToInt32(reader["IdUsuario"]) : 0,
                            Token = reader["Token"]?.ToString(),
                            RefreshToken = reader["RefreshToken"]?.ToString(),
                            FechaCreacion = reader["FechaCreacion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaCreacion"]) : (DateTime?)null,
                            FechaExpiracion = reader["FechaExpiracion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaExpiracion"]) : (DateTime?)null,
                            EsActivo = reader["EsActivo"] != DBNull.Value && Convert.ToBoolean(reader["EsActivo"])
                        };

                    }
                }

                return historial;

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

        private HistorialRefreshToken SaveHistorialRefreshToken(HistorialRefreshToken history)
        {
            try
            {
                HistorialRefreshToken historial = null;


                string cadena = Configuration.GetConnectionString("DataVoxConnection");
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    using var command = new SqlCommand("InsertarHistorialRefreshToken", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdUsuario", history.IdUsuario);
                    command.Parameters.AddWithValue("@Token", history.Token);
                    command.Parameters.AddWithValue("@RefreshToken", history.RefreshToken);
                    command.Parameters.AddWithValue("@FechaCreacion", history.FechaCreacion);
                    command.Parameters.AddWithValue("@FechaExpiracion", history.FechaExpiracion);

                    // Ejecutar el comando y obtener el resultado
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            historial = new HistorialRefreshToken
                            {
                                IdHistorialToken = Convert.ToInt32(reader["IdHistorialToken"]),
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Token = reader["Token"].ToString(),
                                RefreshToken = reader["RefreshToken"].ToString(),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                FechaExpiracion = Convert.ToDateTime(reader["FechaExpiracion"]),
                                EsActivo = Convert.ToBoolean(reader["EsActivo"])
                            };
                        }
                    }
                }

                return historial;

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


        public HistorialRefreshToken GetHistorialRefreshToken(string token, string RefreshToken, int IdUsuario)
        {
            try
            {
                HistorialRefreshToken historial= historialRefreshToken(token, RefreshToken, IdUsuario);

                return historial;
               
            }           
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }

        public HistorialRefreshToken SavehistorialRefreshToken(HistorialRefreshToken historialRefreshToken)
        {
            try
            {
                HistorialRefreshToken historial = SaveHistorialRefreshToken(historialRefreshToken);

                return historial;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
