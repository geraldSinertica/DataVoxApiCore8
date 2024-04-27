using AplicationCore.Services;
using AplicationCore.Utils;
using Azure.Core;
using DataVox.Models;
using Infraestructure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Repository.Models;
using Services.Servicess;
using Services.Utils;
using System.IdentityModel.Tokens.Jwt;
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
        [Route("Completo")]
        public async Task<IActionResult> GetPersonReport(string identification,int tipoReporte)
        {
            ResponseModel response = new ResponseModel();
            FacturacionUtil facturacionUtil = new FacturacionUtil();
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;

                string accessToken =  HttpContext.Request.Headers["Authorization"];


                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenExpiradoSupuestamente = tokenHandler.ReadJwtToken(accessToken.Replace("Bearer ", ""));

                string idUsuario = tokenExpiradoSupuestamente.Claims.First(x =>x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();

                var idCliente = tokenExpiradoSupuestamente.Claims.First(x => x.Type == "IdCliente").Value;

                IServiceReporte service = new ServiceReport(Configuration);
                IServiceConsulta serviceConsulta = new ServiceConsulta(Configuration);


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
                        var consulta = facturacionUtil.CreateConsulta(ipAddress.ToString(),report.DatosGenerales.InformacionPersonal.PersonId);
                        var facture = facturacionUtil.createBill(Convert.ToInt32(idUsuario),Convert.ToInt32(idCliente),tipoReporte);
                        serviceConsulta.AddConsultaYFacturarion(facture,consulta);
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

        [Authorize]
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

        [Authorize]
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
