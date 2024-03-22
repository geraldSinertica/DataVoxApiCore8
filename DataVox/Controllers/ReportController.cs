using DataVox.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Services.Servicess;
using System.Net;

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
    }
}
