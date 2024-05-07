using Infraestructure;
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

namespace Repository.Repositorys
{
   

    public class RepositoryPersona : IRepositoryPersona
    {
        IConfiguration Configuration { get; }
        public RepositoryPersona(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        private PersonalData PersonalInformation(string identification)
        {
            try
            {
                PersonalData Persona = null;

                /*string cadena = Configuration.GetConnectionString("DataVoxConnection")*/;
                string cadena = Configuration.GetConnectionString("DataVoxConnection");
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                     var command = new SqlCommand("ObtenerDatosPersonaConIdentificacion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Identificacion", identification));

                     var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Persona = new PersonalData
                        {
                            PersonId = Convert.ToInt32(reader["IdPersona"]),
                            Identificacion = reader["Identificacion"].ToString(),
                            TipoIdentificacion = Convert.ToInt32(reader["IdIdentificacionTipo"]),
                            NombreCompleto = $"{reader["PrimerNombre"].ToString().Trim()} {reader["SegundoNombre"].ToString().Trim()} {reader["PrimerApellido"].ToString().Trim()} {reader["SegundoApellido"].ToString().Trim()}",
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                            Genero = reader["IdSexo"] != DBNull.Value ? Convert.ToChar(reader["IdSexo"]) : 'E',
                            EstadoDeVida = reader["EstadoVida"].ToString(),
                            LugarNacimiento = reader["LugarNacimiento"].ToString(),
                            EstadoCivil = Convert.ToInt32(reader["IdEstadoCivil"]),
                            Nacionalidad = Convert.ToInt32(reader["Nacionalidad"]),
                            NombrePadre = reader["NombrePadre"].ToString(),
                            NombreMadre = reader["NombreMadre"].ToString(),
                            IdentificationIssue = reader["FechaEmision"].ToString(),
                            IdentificacionVencimiento = reader["FechaVencimiento"].ToString()
                        };
                        Persona.CivilStatusHistoric = GetCivilStatusHistoric(Persona.PersonId);
                    }
                }
              
                return Persona;

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
        private string GetPersonName(int PersonId)
        {
            try
            {
               string FullName = "";
                string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                     var command = new SqlCommand("ObtenerNombrePersonaById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                     var reader = command.ExecuteReader();
                    if (reader.Read())
                    {


                        FullName = $"{reader["PrimerNombre"].ToString().Trim()} {reader["SegundoNombre"].ToString().Trim()} {reader["PrimerApellido"].ToString().Trim()} {(reader["SegundoApellido"].ToString().Trim() != "NO INDICA" ? reader["SegundoApellido"].ToString().Trim() : "")}";


                    }
                }
                
               return FullName;

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
        private List<PersonaEstadoCivilHistorico> GetCivilStatusHistoric(int PersonId)
        {
            try
            {
                List<PersonaEstadoCivilHistorico> status = null;

                string cadena = Configuration.GetConnectionString("DataVoxConnection");


                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("sp_GetPersonaEstadoCivilHistorico", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                     var reader = command.ExecuteReader();
                    status = new List<PersonaEstadoCivilHistorico>();
                    while (reader.Read()) // Itera sobre los resultados del procedimiento almacenado
                    {
                        // Crea una instancia de tb_Telefono para cada fila y asigna los valores correspondientes
                        var statu = new PersonaEstadoCivilHistorico()
                        {
                            IdEvento = Convert.ToInt32(reader["IdEvento"]),
                            IdPersonaConyuge = reader["IdPersonaConyuge"] != DBNull.Value ? Convert.ToInt32(reader["IdPersonaConyuge"]) : (int?)null,
                            CitaMatrimonio = reader["CitaMatrimonio"].ToString(),
                            TipoSuceso = reader["TipoSuceso"] != DBNull.Value ? Convert.ToInt16(reader["TipoSuceso"]) : (short?)null,
                            TipoMatrimonio = reader["TipoMatrimonio"] != DBNull.Value ? Convert.ToInt16(reader["TipoMatrimonio"]) : (short?)null,
                            FechaSuceso = reader["FechaSuceso"] != DBNull.Value ? Convert.ToDateTime(reader["FechaSuceso"]) : (DateTime?)null,
                            ProvinciaSuceso = reader["ProvinciaSuceso"] != DBNull.Value ? Convert.ToInt16(reader["ProvinciaSuceso"]) : (short?)null,
                            CantonSuceso = reader["CantonSuceso"] != DBNull.Value ? Convert.ToInt16(reader["CantonSuceso"]) : (short?)null,
                            DistritoSuceso = reader["DistritoSuceso"] != DBNull.Value ? Convert.ToInt16(reader["DistritoSuceso"]) : (short?)null,
                            LugarSuceso = reader["LugarSuceso"] != DBNull.Value ? reader["LugarSuceso"].ToString() : ""
                        };
                        statu.NombreCoyugue = GetPersonName((int)statu.IdPersonaConyuge);
                        status.Add(statu); // Agrega el teléfono a la lista
                    }
                }
                

                return status;
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
        private List<Telefonos> getPhones(int PersonId)
        {
            try
            {
                List<Telefonos> phones = null;

                string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerTelefonosPorPersona", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                     var reader = command.ExecuteReader();
                    phones = new List<Telefonos>();
                    while (reader.Read()) // Itera sobre los resultados del procedimiento almacenado
                    {
                        // Crea una instancia de tb_Telefono para cada fila y asigna los valores correspondientes
                        var telefono = new Telefonos()
                        {
                            Telefono = reader["Telefono"].ToString(),
                            Tipo = reader["TipoTelefono"].ToString().Trim()
                        };

                        phones.Add(telefono); // Agrega el teléfono a la lista
                    }
                }
                

                return phones;
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
        private List<Direcciones> getDirections(int PersonId)
        {
            try
            {
                List<Direcciones> adress = null;

                string cadena = Configuration.GetConnectionString("DataVoxConnection");


                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerDireccionesPorIdPersona", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                     var reader = command.ExecuteReader();
                    adress = new List<Direcciones>();
                    while (reader.Read())
                    {

                        var adres = new Direcciones()
                        {
                            Direccion = reader["Direccion"].ToString(),

                        };

                        adress.Add(adres);
                    }
                }
                

                return adress;
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
        private Appointment GetAppointments(int PersonId)
        {
            try
            {
                Appointment appointment = null;

                string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerTop10EmpresasPorPersona", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                     var reader = command.ExecuteReader();
                    // adress = new List<tb_Direccion>();
                    while (reader.Read())
                    {

                        var company = new Company()
                        {
                            Cargo = reader["Cargo"] != DBNull.Value ? reader["Cargo"].ToString() : "",
                            NombreComercial = reader["NombreComercial"] != DBNull.Value ? reader["NombreComercial"].ToString() : "",
                            FechaInscripcion = reader["FechaInscripcion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaInscripcion"]) : DateTime.MinValue,
                            FechaInicio = reader["FechaInicio"] != DBNull.Value ? Convert.ToDateTime(reader["FechaInicio"]) : DateTime.MinValue,
                            FechaVencimiento = reader["FechaVencimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaVencimiento"]) : DateTime.MinValue,
                            FinesEmpresa = reader["FinesEmpresa"] != DBNull.Value ? reader["FinesEmpresa"].ToString() : "",
                            DescProrrogas = reader["DescProrrogas"] != DBNull.Value ? reader["DescProrrogas"].ToString() : "",
                            Representacion = reader["Representacion"] != DBNull.Value ? reader["Representacion"].ToString() : "",
                            MontoCapital = reader["MontoCapital"] != DBNull.Value ? Convert.ToDecimal(reader["MontoCapital"]) : 0,
                            CantidadAcciones = reader["CantidadAcciones"] != DBNull.Value ? Convert.ToInt32(reader["CantidadAcciones"]) : 0
                        };

                        appointment.Nombramientos.Add(company);
                    }
                }
                

                return appointment;
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
        private List<Vinculo> GetAllRelations(int PersonId)
        {
            try
            {
                List<Vinculo> relations = null;


               
                    using (SqlConnection connection = new SqlConnection(Configuration["ConnectionStrings:DataVoxConnection"]))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerDatosPersonaVinculo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                     var reader = command.ExecuteReader();
                    relations = new List<Vinculo>();
                    while (reader.Read())
                    {

                        var relation = new Vinculo();


                        relation.NombrePersona = reader["NombrePersona"] != DBNull.Value ? reader["NombrePersona"].ToString().Trim() : string.Empty;

                        relation.Tipo = reader["Tipo"] != DBNull.Value ? Convert.ToInt32(reader["Tipo"]) : 0;
                        relation.FechaVinculo = (DateTime)(reader["FechaVinculo"] != DBNull.Value ? Convert.ToDateTime(reader["FechaVinculo"]) : (DateTime?)null);


                        relations.Add(relation);
                    }
                }
                

                return relations;
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

        private Filiacion GetRelations(int PersonId)
        {
            try
            {
                Filiacion filiation = null;

                List<Vinculo> list = GetAllRelations(PersonId);

                if (list != null && list.Count >0)
                {
                    filiation = new Filiacion();
                    foreach (var item in list)
                    {
                        switch ((TipoVinculo)item.Tipo)
                        {
                            case TipoVinculo.MADRE:
                                filiation.Madre = item;
                                break;
                            case TipoVinculo.PADRE:
                                filiation.Padre = item;
                                break;
                            case TipoVinculo.HIJO:
                                filiation.Hijos.Add(item);
                                break;
                            case TipoVinculo.CONYUGE:
                                filiation.Conyugue = item;
                                break;
                            case TipoVinculo.HERMANO:
                                filiation.Hermanos.Add(item);
                                break;
                            case TipoVinculo.TIO:
                                filiation.Tios.Add(item);
                                break;
                                
                        }
                    }
                }
              

                return filiation;
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

        public ApplicantInformation GetPersonalInformation(string identification)
        {
            try
            {
                int PersonId = 0;
                ApplicantInformation report = null;

                PersonalData persona = PersonalInformation(identification);
                if(persona != null)
                {
                    PersonId = persona.PersonId;
                }
               
                 
                ContactData contactData = new ContactData();
                Appointment appointment = null;
                Filiacion filiacion = null;

                if(PersonId > 0)
                {
                    contactData.Telefonos = getPhones(PersonId);
                    contactData.Direcciones = getDirections(PersonId);
                    appointment = GetAppointments(PersonId);
                    filiacion = GetRelations(PersonId);

                }


                if (persona != null && contactData != null)
                {
                    report = new ApplicantInformation();
                    report.InformacionPersonal = persona;
                    report.TelefonosYDirecciones = contactData;
                    report.Nombramientos = appointment;
                    report.Filiaciones = filiacion;

                }

               

                return report;
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
