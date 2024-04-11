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

namespace Infraestructure.Repositorys
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
       
        private readonly IConfiguration Configuration;
        public RepositoryUsuario(IConfiguration configuration ) {
            Configuration = configuration;
        }

        private Usuario getUserLogin(string email, string password)
        {
            try
            {
                Usuario usuario = null;

                /*string cadena = Configuration.GetConnectionString("DataVoxConnection")*/
                ;
                string cadena = Configuration.GetConnectionString("DataVoxConnection");
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    using var command = new SqlCommand("ObtenerUsuarioPorCredenciales", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Email", email));
                    command.Parameters.Add(new SqlParameter("@Password", password));

                    using var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            IdUsuario = reader["idUsuario"] != DBNull.Value ? Convert.ToInt32(reader["idUsuario"]) : 0,
                            NombreUsuario = reader["NombreUsuario"]?.ToString(),
                            Identificacion = reader["Identificacion"]?.ToString(),
                            IdCliente = reader["IdCliente"] != DBNull.Value ? Convert.ToInt32(reader["IdCliente"]) : 0,
                            AreaEmpresa = reader["AreaEmpresa"] != DBNull.Value ? Convert.ToByte(reader["AreaEmpresa"]) : (byte?)null,
                            Cargo = reader["Cargo"]?.ToString(),
                            Telefono = reader["Telefono"]?.ToString(),
                            Email = reader["Email"]?.ToString()
                        };
                        
                    }
                }

                return usuario;

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

        public LoginDaTa login(string email, string password)
        {
            try
            {
                LoginDaTa login = new LoginDaTa();
                Usuario usuario = getUserLogin(email,password);

                if (usuario != null)
                {
                    login.Code = 200;
                    login.user = usuario;
                    return login;
                }
                else
                {
                    login.Code = 401;
                    login.user = null;
                    return login;
                }

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
