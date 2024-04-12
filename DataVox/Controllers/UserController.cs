using AplicationCore.Services;
using AplicationCore.Services.Auth;
using AplicationCore.Utils;
using Azure;
using DataVox.Models;
using Infraestructure.Models;
using Infraestructure.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace DataVox.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IAutorizacionService AutorizacionService;

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
          
        }

        [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] AutorizacionRequest autorizacion)
        {
            autorizacion.Password = Cryptography.EncrypthAES(autorizacion.Password);
            var jwt = Configuration.GetSection("Jwt").Get<JWT>();

            IAutorizacionService autorizacionService = new AutorizacionService(Configuration, jwt.Key);
            var resultado_autorizacion = await autorizacionService.DevolverToken(autorizacion);
            if (resultado_autorizacion == null)
                return Unauthorized();

            return Ok(resultado_autorizacion);

        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> ObtenerRefreshToken([FromBody] RefreshTokenRequest request)
        {

            var jwt = Configuration.GetSection("Jwt").Get<JWT>();

            IAutorizacionService autorizacionService = new AutorizacionService(Configuration, jwt.Key);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenExpiradoSupuestamente = tokenHandler.ReadJwtToken(request.TokenExpirado);

            if (tokenExpiradoSupuestamente.ValidTo > DateTime.UtcNow)
                return BadRequest(new AutorizacionResponse { Resultado = false, Msg = "Token no ha expirado" });

            string idUsuario = tokenExpiradoSupuestamente.Claims.First(x =>
                x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();


            var autorizacionResponse = await autorizacionService.DevolverRefreshToken(request, int.Parse(idUsuario));

            if (autorizacionResponse.Resultado)
                return Ok(autorizacionResponse);
            else
                return BadRequest(autorizacionResponse);




        }



    }
}
