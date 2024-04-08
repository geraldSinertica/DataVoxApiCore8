using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Models;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Repositorys
{
    public class RepositoryReport : IRepositoryReport
    {
        private IRepositoryPersona repositoryPersona;
        private IRepositoryIncome repositoryIncome;
        private IRepositoryOperations repositoryOperations;
        private IRepositoryJuicios repositoryJuicios;
        private IRepositoryStates repositoryStates;
        private IRepositoryConsulta repositoryConsulta;
        private IConfiguration Configuration;

        public RepositoryReport(IConfiguration configuration)
        {
            repositoryPersona = new RepositoryPersona(configuration);
            repositoryIncome = new RepositoryIncomes(configuration);
            repositoryOperations = new RepositoryOperations(configuration);
            repositoryJuicios = new RepositoryJuicios(configuration);
            repositoryStates = new RepositoryStates(configuration);
            repositoryConsulta = new RepositoryConsulta(configuration);
            Configuration = configuration;
        }

        public ReportJSON getPersonReport(string identificacion, int idType)
        {
            try
            {
                ReportJSON Persona = null;


                string cadena = Configuration.GetConnectionString("DataVoxConnection");
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    using (var command = new SqlCommand("[dbo].[ConsultaPersonaFisica]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Identificacion", identificacion));
                        command.Parameters.Add(new SqlParameter("@TipoIdentificacion", idType));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Persona = new ReportJSON
                                {
                                    Reporte = reader.GetString(0)

                                };

                            }
                        }
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

        public Report PersonReport(string identification)
        {
            try
            {
                int PersonId = 0;

                Report report = null;

                ApplicantInformation personalData = null;
                Incomes Incomes = null;
                Operations operations = new Operations();
                States states = new States();
                PersonalProperty personalProperty = new PersonalProperty();

                List<Consulta> consultas = new List<Consulta>();
                List<Juicio> juicios = new List<Juicio>();


                personalData = repositoryPersona.GetPersonalInformation(identification);
               

                if (personalData != null)
                {
                    PersonId = personalData.InformacionPersonal.PersonId;
                    if(PersonId > 0)
                    {
                        Incomes = repositoryIncome.GetIncomesByPerson(PersonId);
                        operations = repositoryOperations.GetOperationsbyPerson(PersonId);
                        juicios = repositoryJuicios.GetJudgmentsbyPerson(PersonId);
                        states = repositoryStates.GetStatesByPerson(PersonId);
                        consultas = repositoryConsulta.GetAllByPerson(PersonId);
                       
                    }
                    
                }


                if (personalData != null)
                {
                    report = new Report();
                    report.DatosGenerales = personalData;
                    report.Ingresos = Incomes;
                    report.Obligaciones = operations;
                    report.Demandas = juicios;
                    report.Propiedades = states;
                    report.HistorialConsultas = consultas;

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
