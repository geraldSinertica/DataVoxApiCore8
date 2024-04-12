﻿using AplicationCore.Utils;
using DataVox.Models;
using Infraestructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Services.Servicess;
using Services.Utils;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace DataVox.Controllers
{
    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public ReportController(IConfiguration configuration ) {
            Configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        [Route("full")]
        public async Task<IActionResult> GetPersonReport(string identification)
        {
            

            ResponseModel response = new ResponseModel();
            try
            {
               
                    IServiceReporte service = new ServiceReport(Configuration);

                    Report report = await service.PersonReport(identification);

                    if (report == null)
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Message = "Asset no encontrado, verifique el id";
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "Reporte encontrado";
                        response.Data = report;
                    }

                    return Ok(response);
                
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpGet]
        [Route("fullXML")]
        
        public async Task<IActionResult> GetPersonReportXML(string? identification)
        {
            ReportToXmlConverter converter = new ReportToXmlConverter();
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceReporte service = new ServiceReport(Configuration);

                Report report = await service.PersonReport(identification);

                if (report == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Asset no encontrado, verifique el id";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Reporte encontrado";
                    response.Data = converter.ConvertJsonToXml(JsonSerializer.Serialize(report)).InnerXml;
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpGet]
        [Route("Compacto")]
        public async Task<IActionResult> GetPersonReportCompacto(string identification,int idTyoe)
        {
            ReportToXmlConverter converter = new ReportToXmlConverter();
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceReporte service = new ServiceReport(Configuration);

                ReportJSON report =  service.getPersonReport(identification,idTyoe);

                if (report == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Asset no encontrado, verifique el id";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Reporte encontrado";
                    response.Data = report;
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return StatusCode(response.StatusCode, response);
            }
        }
    }
}
