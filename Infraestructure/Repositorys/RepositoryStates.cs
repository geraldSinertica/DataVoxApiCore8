﻿using Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositorys
{
    public class RepositoryStates : IRepositoryStates
    {
        IConfiguration Configuration1 { get; }
        public RepositoryStates(IConfiguration configuration)
        {
            Configuration1 = configuration;
        }
        #region Bienes Inmuebles
        private List<Limit> GetLimitByState(int idState)
        {
            try
            {
                List<Limit> limits = null;

                string cadena = Configuration1.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerLinderosPorInmueble", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdInmueble", idState));

                     var reader = command.ExecuteReader();
                    limits = new List<Limit>();
                    while (reader.Read())
                    {
                        var limit = new Limit()
                        {
                            PuntoCardinal = reader["PuntoCardinal"] != DBNull.Value ? reader["PuntoCardinal"].ToString() :"",
                            Lindero = reader["Lindero"] != DBNull.Value ? reader["Lindero"].ToString() : ""
                        };

                        limits.Add(limit);
                    }
                }
                

                return limits;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private List<Assessment> GetAssessmentByState(long idState)
        {
            try
            {
                List<Assessment> assessments = null;

                string cadena = Configuration1.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerGravamenesPorInmueble", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdInmueble ", idState));

                     var reader = command.ExecuteReader();
                    assessments = new List<Assessment>();
                    while (reader.Read())
                    {

                        var assessment = new Assessment()
                        {
                            CitaGravamen = reader["idGravamen"].ToString(),
                            Moneda = reader["Monto"] != DBNull.Value ? reader["Moneda"].ToString() :"",
                            Monto = reader["Monto"] != DBNull.Value ? Convert.ToDecimal(reader["Monto"]):0,
                            FechaInicia = reader["FechaVence"] != DBNull.Value ? Convert.ToDateTime(reader["FechaInicia"]):DateTime.MinValue,
                            FechaVence = reader["FechaVence"] != DBNull.Value ? Convert.ToDateTime(reader["FechaVence"]) : DateTime.MinValue,
                            FechaInterrupcion = reader["FechaInterrupcion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaInterrupcion"]) : (DateTime?)null,
                            Interes = reader["Interes"] != DBNull.Value ? reader["Interes"].ToString():"",
                            FormaPago = reader["FormaPago"] != DBNull.Value ? reader["FormaPago"].ToString():"",
                            FechaUltActualizacion = reader["FechaUltActualizacion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaUltActualizacion"]) : DateTime.MinValue,
                            ClaseResponsabilidad = reader["ClaseResponsabilidad"] != DBNull.Value ? Convert.ToChar(reader["ClaseResponsabilidad"]) : (char?)null,
                            TomoCredito = reader["TomoCredito"] != DBNull.Value ? Convert.ToInt32(reader["TomoCredito"]) : (int?)null,
                            AsientoCredito = reader["AsientoCredito"] != DBNull.Value ? Convert.ToInt32(reader["AsientoCredito"]) : (int?)null,
                            ConsecutivoCredito = reader["ConsecutivoCredito"] != DBNull.Value ? Convert.ToInt32(reader["ConsecutivoCredito"]) : (int?)null,
                            SecuenciaCredito = reader["SecuenciaCredito"] != DBNull.Value ? Convert.ToInt32(reader["SecuenciaCredito"]) : (int?)null,
                            SubsecuenciaCredito = reader["SubsecuenciaCredito"] != DBNull.Value ? Convert.ToInt32(reader["SubsecuenciaCredito"]) : (int?)null,
                            Grado = reader["Grado"] != DBNull.Value ? Convert.ToInt32(reader["Grado"]) : (int?)null,
                            ReferenciaFinca = reader["Referencia_Finca"].ToString(),
                            ReferenciaGravamen = reader["Referencia_Gravamen"].ToString(),
                            BaseRemate = reader["Base_Remate"].ToString()
                        };

                        assessments.Add(assessment);
                    }
                }
                

                return assessments;
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

        private List<Annotation> GetAnnotationsByState(long idState)
        {
            try
            {
                List<Annotation> annotations = null;


                string cadena = Configuration1.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerAnotacionesPorInmueble", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdInmueble ", idState));

                     var reader = command.ExecuteReader();
                    annotations = new List<Annotation>();
                    while (reader.Read())
                    {

                        var annotation = new Annotation();


                        annotation.IdAnotacion = reader["idAnotacion"] != DBNull.Value ? long.Parse(reader["idAnotacion"].ToString()) : 0;
                        annotation.CitaAnotacion = reader["CitaAnotacion"].ToString();
                        annotation.TipoOperacion = reader["TipoOperacion"].ToString();
                        annotation.FechaAnotacion = reader["FechaAnotacion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaAnotacion"]) : DateTime.MinValue;
                        annotation.Derecho = reader["Derecho"] != DBNull.Value ? Convert.ToInt16(reader["Derecho"]) : (short?)null;
                        annotation.IdGravamen = reader["IdGravamen"] != DBNull.Value ? Convert.ToInt32(reader["IdGravamen"]) : (int?)null;
                        annotation.CreditoAsociado = reader["CreditoAsociado"].ToString();
                         annotation.SecuenciaAfectada = reader["SecuenciaAfectada"] != DBNull.Value ? Convert.ToInt16(reader["SecuenciaAfectada"]) : (short?)null;
                        

                        annotations.Add(annotation);
                    }
                }
                

                return annotations;
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

        private List<RealEstate> GetRealEstateByPerson(long PersonId)
        {
            try
            {
                List<RealEstate> states = null;

                string cadena = Configuration1.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerInmueblesPorPersona", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                     var reader = command.ExecuteReader();
                    states = new List<RealEstate>();
                    while (reader.Read())
                    {

                        RealEstate realEstate = new RealEstate();

                        realEstate.IdInmueble = Convert.ToInt32(reader["IdInmueble"].ToString());
                        realEstate.NumeroFinca = reader["NumeroFinca"].ToString();
                        realEstate.Canton = reader["Canton"].ToString();
                        realEstate.Distrito = reader["Distrito"].ToString();
                        realEstate.Medida = Convert.ToDecimal(reader["Medida"].ToString());
                        realEstate.IdOperacion = reader["IdOperacion"].ToString();
                        realEstate.Presentacion = reader["Presentacion"].ToString();
                        realEstate.FechaUltActualizacion = Convert.ToDateTime(reader["FechaUltActualizacion"].ToString());
                        realEstate.Naturaleza = reader["Naturaleza"].ToString();
                        realEstate.Plano = reader["Plano"].ToString();
                        realEstate.Avaluo = Convert.ToDecimal(reader["Avaluo"].ToString());
                        realEstate.ValorPorcentual = Convert.ToDecimal(reader["ValorPorcentual"].ToString());

                        // Aquí puedes llamar a los métodos GetLimitByState, GetAssessmentByState y GetAnnotationsByState
                        // para llenar las listas Limits, Assessments y Annotations, respectivamente.
                        realEstate.Limits = GetLimitByState(realEstate.IdInmueble);
                        realEstate.Assessments = GetAssessmentByState(realEstate.IdInmueble);
                        realEstate.Annotations = GetAnnotationsByState(realEstate.IdInmueble);
                        realEstate.Limits = GetLimitByState(realEstate.IdInmueble);
                        realEstate.Assessments = GetAssessmentByState(realEstate.IdInmueble);
                        realEstate.Annotations = GetAnnotationsByState(realEstate.IdInmueble);
                        states.Add(realEstate);
                    }
                }
                

                return states;
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
        #endregion

        #region Bienes Muebles
        private List<Vehicles> GetVehiclesbyPerson(int PersonId)
        {
            try
            {
               List<Vehicles> vehicles = null;

                string cadena = Configuration1.GetConnectionString("DataVoxConnection");

                using (var connection = new SqlConnection(cadena))
                {
                    connection.Open();

                     var command = new SqlCommand("ObtenerMueblesPorPersona", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                     var reader = command.ExecuteReader();
                    vehicles = new List<Vehicles>();
                    while (reader.Read())
                    {

                        var vehicle = new Vehicles()
                        {
                            IdMueble = Convert.ToInt32(reader["IdMueble"]),
                            Placa = reader["Placa"].ToString(),
                            TipoBien = reader["TipoBien"].ToString(),
                            NumeroVIN = reader["NumeroVIN"].ToString(),
                            NumeroChasis = reader["NumeroChasis"].ToString(),
                            Marca = reader["Marca"].ToString(),
                            Carroceria = reader["Carroceria"].ToString(),
                            AnoFabricacion = Convert.ToInt32(reader["AnoFabricacion"]),
                            CapacidadPasajeros = reader["CapacidadPasajeros"] != DBNull.Value ? Convert.ToInt16(reader["CapacidadPasajeros"]) : (short?)null,
                            NumeroMotor = reader["NumeroMotor"].ToString(),
                            NumeroSerieMotor = reader["NumeroSerieMotor"].ToString(),
                            FabricanteMotor = reader["FabricanteMotor"].ToString(),
                            ClaseBien = reader["ClaseBien"].ToString(),
                            CodigoBien = reader["CodigoBien"].ToString(),
                            NumeroRegistral = reader["NumeroRegistral"] != DBNull.Value ? Convert.ToDecimal(reader["NumeroRegistral"]) : (decimal?)null,
                            TomoInscripcion = reader["TomoInscripcion"].ToString(),
                            AsientoInscripcion = reader["AsientoInscripcion"].ToString(),
                            SecuenciaInscripcion = reader["SecuenciaInscripcion"].ToString(),
                            FechaInscripcion = reader["FechaInscripcion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaInscripcion"]) : (DateTime?)null,
                            NumIdAnterior = reader["NumIdAnterior"].ToString(),
                            CitasAnteriores = reader["CitasAnteriores"].ToString(),
                            ValorHaciendaInscripcion = reader["ValorHaciendaInscripcion"] != DBNull.Value ? Convert.ToDecimal(reader["ValorHaciendaInscripcion"]) : (decimal?)null,
                            ValorHaciendaOficial = reader["ValorHaciendaOficial"] != DBNull.Value ? Convert.ToDecimal(reader["ValorHaciendaOficial"]) : (decimal?)null,
                            ValorContrato = reader["ValorContrato"] != DBNull.Value ? Convert.ToDecimal(reader["ValorContrato"]) : (decimal?)null,
                            EstadoActual = reader["EstadoActual"].ToString(),
                            EstadoActualTributario = reader["EstadoActualTributario"].ToString(),
                            Uso = reader["Uso"].ToString(),
                            TomoUltMovimiento = reader["TomoUltMovimiento"].ToString(),
                            AsientoUltMovimiento = reader["AsientoUltMovimiento"].ToString(),
                            SecuenciaUltMovimiento = reader["SecuenciaUltMovimiento"].ToString(),
                            FechaUltMovimiento = reader["FechaUltMovimiento"] != DBNull.Value ? reader["FechaUltMovimiento"].ToString() : null,
                            IdMoneda = reader["IdMoneda"] != DBNull.Value ? Convert.ToInt32(reader["IdMoneda"]) : (int?)null
                        };

                        vehicles.Add(vehicle);
                    }
                }




                return vehicles;
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
    

        private List<Ships> GetShipsByPerson(int PersonId)
        {
            try
            {
                List<Ships> ships = null;



                string cadena = Configuration1.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerBuquesPorPersona", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                     var reader = command.ExecuteReader();
                    ships = new List<Ships>();
                    while (reader.Read())
                    {

                        var ship = new Ships()
                        {
                            IdMueble = Convert.ToInt32(reader["IdMueble"]),
                            Placa = reader["Placa"].ToString(),
                            TipoBien = reader["TipoBien"].ToString(),
                            NumeroSerie = reader["NumeroSerie"].ToString(),
                            NumeroChasis = reader["NumeroChasis"].ToString(),
                            NombreBuque = reader["NombreBuque"].ToString(),
                            IdMarca = reader["Marca"].ToString(),
                            AnoConstruccion = Convert.ToInt32(reader["AnoConstruccion"]),
                            NumeroCasco = reader["NumeroCasco"].ToString(),
                            NumeroManga = Convert.ToDecimal(reader["NumeroManga"]),
                            NumeroEslora = Convert.ToDecimal(reader["NumeroEslora"]),
                            NumeroPuntal = Convert.ToDecimal(reader["NumeroPuntal"]),
                            NombreConstructor = reader["NombreConstructor"].ToString(),
                            LugarConstruccion = reader["LugarConstruccion"].ToString(),
                            NumeroMotor = reader["NumeroMotor"].ToString(),
                            NumeroSerieMotor = reader["NumeroSerieMotor"].ToString(),
                            FabricanteMotor = reader["FabricanteMotor"].ToString(),
                            ClaseBien = reader["ClaseBien"].ToString(),
                            CodigoBien = reader["CodigoBien"].ToString(),
                            NumeroRegistral = reader["NumeroRegistral"] != DBNull.Value ? Convert.ToDecimal(reader["NumeroRegistral"]) : (decimal?)null,
                            TomoInscripcion = reader["TomoInscripcion"].ToString(),
                            AsientoInscripcion = reader["AsientoInscripcion"].ToString(),
                            SecuenciaInscripcion = reader["SecuenciaInscripcion"].ToString(),
                            FechaInscripcion = reader["FechaInscripcion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaInscripcion"]) : (DateTime?)null,
                            NumIdAnterior = reader["NumIdAnterior"].ToString(),
                            CitasAnteriores = reader["CitasAnteriores"].ToString(),
                            ValorHaciendaInscripcion = reader["ValorHaciendaInscripcion"] != DBNull.Value ? Convert.ToDecimal(reader["ValorHaciendaInscripcion"]) : (decimal?)null,
                            ValorHaciendaOficial = reader["ValorHaciendaOficial"] != DBNull.Value ? Convert.ToDecimal(reader["ValorHaciendaOficial"]) : (decimal?)null,
                            ValorContrato = reader["ValorContrato"] != DBNull.Value ? Convert.ToDecimal(reader["ValorContrato"]) : (decimal?)null,
                            EstadoActual = reader["EstadoActual"].ToString(),
                            EstadoActualTributario = reader["EstadoActualTributario"].ToString(),
                            Uso = reader["Uso"].ToString(),
                            TomoUltMovimiento = reader["TomoUltMovimiento"].ToString(),
                            AsientoUltMovimiento = reader["AsientoUltMovimiento"].ToString(),
                            SecuenciaUltMovimiento = reader["SecuenciaUltMovimiento"].ToString(),
                            FechaUltMovimiento = reader["FechaUltMovimiento"].ToString(),
                            IdMoneda = reader["IdMoneda"] != DBNull.Value ? Convert.ToInt32(reader["IdMoneda"]) : (int?)null
                        };

                        ships.Add(ship);
                    }
                }
                

                return ships;
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

        private List<Aircraft> GetAircraftByPerson(int PersonId)
        {
            try
            {
                List<Aircraft> aircrafts = null;


                string cadena = Configuration1.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                     var command = new SqlCommand("ObtenerAvionesPorPersona", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdPersona", PersonId));

                    var reader = command.ExecuteReader();
                    aircrafts = new List<Aircraft>();
                    while (reader.Read())
                    {

                        var aircraft = new Aircraft()
                        {
                            IdMueble = reader["IdMueble"] != DBNull.Value ? Convert.ToInt32(reader["IdMueble"]): 0,
                            Placa = reader["Placa"].ToString(),
                            TipoBien = reader["TipoBien"].ToString(),
                            NumeroSerie = reader["NumeroSerie"].ToString(),
                            IdMarca = reader["Marca"].ToString(),
                            AnoFabricacion = reader["AnoFabricacion"] != DBNull.Value ? Convert.ToDecimal(reader["AnoFabricacion"]) : (decimal?)null,
                            Estilo = reader["Estilo"].ToString(),
                            Modelo = reader["Modelo"].ToString(),
                            Fabricante = reader["Fabricante"].ToString(),
                            PesoMaximo = reader["PesoMaximo"] != DBNull.Value ? Convert.ToDecimal(reader["PesoMaximo"]) : (decimal?)null,
                            PesoVacio = reader["PesoVacio"] != DBNull.Value ? Convert.ToDecimal(reader["PesoVacio"]) : (decimal?)null,                         
                            ClaseBien = reader["ClaseBien"].ToString(),
                            CodigoBien = reader["CodigoBien"].ToString(),
                            NumeroRegistral = reader["NumeroRegistral"] != DBNull.Value ? Convert.ToDecimal(reader["NumeroRegistral"]) : (decimal?)null,
                            TomoInscripcion = reader["TomoInscripcion"].ToString(),
                            AsientoInscripcion = reader["AsientoInscripcion"].ToString(),
                            SecuenciaInscripcion = reader["SecuenciaInscripcion"].ToString(),
                            FechaInscripcion = reader["FechaInscripcion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaInscripcion"]) : (DateTime?)null,
                            NumIdAnterior = reader["NumIdAnterior"].ToString(),
                            CitasAnteriores = reader["CitasAnteriores"].ToString(),
                            ValorHaciendaInscripcion = reader["ValorHaciendaInscripcion"] != DBNull.Value ? Convert.ToDecimal(reader["ValorHaciendaInscripcion"]) : (decimal?)null,
                            ValorHaciendaOficial = reader["ValorHaciendaOficial"] != DBNull.Value ? Convert.ToDecimal(reader["ValorHaciendaOficial"]) : (decimal?)null,
                            ValorContrato = reader["ValorContrato"] != DBNull.Value ? Convert.ToDecimal(reader["ValorContrato"]) : (decimal?)null,
                            EstadoActual = reader["EstadoActual"].ToString(),
                            EstadoActualTributario = reader["EstadoActualTributario"].ToString(),
                            Uso = reader["Uso"].ToString(),
                            TomoUltMovimiento = reader["TomoUltMovimiento"].ToString(),
                            AsientoUltMovimiento = reader["AsientoUltMovimiento"].ToString(),
                            SecuenciaUltMovimiento = reader["SecuenciaUltMovimiento"].ToString(),
                            FechaUltMovimiento = reader["FechaUltMovimiento"].ToString(),
                            IdMoneda = reader["IdMoneda"] != DBNull.Value ? Convert.ToInt32(reader["IdMoneda"]) : (int?)null
                        };

                        aircrafts.Add(aircraft);
                    }
                }
                

                return aircrafts;
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

        private PersonalProperty GetPersonalPropertyByPerson(int PersonId)
        {
            try
            {
                PersonalProperty personalProperty = new PersonalProperty();

                List<Vehicles> vehicles = GetVehiclesbyPerson(PersonId);
                List<Ships> ships = GetShipsByPerson(PersonId);
                List<Aircraft> aircraft = GetAircraftByPerson(PersonId);

                if (vehicles != null && vehicles.Count > 0)
                {
                    personalProperty.Vehiculos = vehicles;
                }

                if (ships != null && ships.Count > 0)
                {
                    personalProperty.Buques = ships;
                }

                if (aircraft != null && aircraft.Count > 0)
                {
                    personalProperty.Aeronaves = aircraft;
                }


                return personalProperty;
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
        #endregion
        public States GetStatesByPerson(int PersonId)
        {
            try
            {
                States states = null;

                List<RealEstate> realEstates = GetRealEstateByPerson(PersonId);
                PersonalProperty personalProperty = GetPersonalPropertyByPerson(PersonId);

                if (realEstates != null)
                { 
                    states = new States();
                    states.BienesMuebles = realEstates;
                    states.BienesInmuebles =  personalProperty;
                }




                return states;
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
