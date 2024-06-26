﻿using Infraestructure;
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
    public class RepositoryOperations: IRepositoryOperations
    {
        IConfiguration Configuration { get; }
        public RepositoryOperations(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Operations GetOperationsbyPerson(int PersonId)
        {
            try
            {
                Operations Operatios = null;

                List<Credit> credits = GetAllOperationsbyPerson(PersonId);

                if (credits != null && credits.Count >0)
                {
                    Operatios = new Operations();
                    foreach (var item in credits)
                    {
                        if(item.SiglaEstado == "VIG")
                        {
                            Operatios.OpenOperations.Add(item);
                        }
                        else
                        {
                            Operatios.CloseOperations.Add(item);
                        }
                    }
                }

                    return Operatios;
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

        private List<Credit> GetAllOperationsbyPerson(int PersonId)
        {
            try
            {
                List<Credit> credits = null;

                string cadena = Configuration.GetConnectionString("DataVoxConnection");
                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerInformacionCreditoPorPersona", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                    var reader = command.ExecuteReader();
                    credits = new List<Credit>();
                    while (reader.Read()) // Itera sobre los resultados del procedimiento almacenado
                    {
                        // Crea una instancia de tb_Telefono para cada fila y asigna los valores correspondientes
                        var credit = new Credit()
                        {
                            NumeroCredito = reader["NumeroCredito"].ToString(),
                            Entidad = reader["Entidad"].ToString(),
                            FechaOtorgamiento = Convert.ToDateTime(reader["FechaOtorgamiento"]),
                            FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]),
                            FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"]),
                            SiglaEstado= reader["SiglaEstado"] != DBNull.Value ? reader["SiglaEstado"].ToString(): "",
                            MontoOtorgado = Convert.ToDecimal(reader["MontoOtorgado"] == DBNull.Value ? 0 : reader["MontoOtorgado"]),
                            SaldoActual = reader["SaldoActual"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SaldoActual"]),
                            SaldoMora = reader["SaldoMora"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SaldoMora"]),
                            IndiceMora = reader["IndiceMora"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["IndiceMora"]),
                            ValorCuota = reader["ValorCuota"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ValorCuota"]),
                            DiasMora = reader["DiasMora"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DiasMora"]),
                            Moneda = reader["Moneda"].ToString(),
                         
                            Estado = reader["Estado"].ToString(),
                        };

                        credits.Add(credit); // Agrega el teléfono a la lista
                    }
                }
                

                return credits;
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
