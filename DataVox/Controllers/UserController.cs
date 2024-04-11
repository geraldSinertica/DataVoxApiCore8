using AplicationCore.Services;
using DataVox.Models;
using Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.Models;
using System.IdentityModel.Tokens.Jwt;
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


        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public dynamic Login([FromBody] Object reqDATA)
        {
            try
            {
                IServiceUsuario service = new ServiceUsuario(Configuration);
                ResponseModel response = new ResponseModel();

                var data = JsonConvert.DeserializeObject<dynamic>(reqDATA.ToString());

                string user = data.user.ToString();
                string pass = data.pass.ToString();

                LoginDaTa loginData = service.login(user, pass);

                Usuario usuario = new Usuario();
                usuario = loginData.user;

                if (loginData.Code==401)
                {
                    response.StatusCode = 401;
                    response.Message = "No autorizado verifique las credenciales";
                    response.Data = null;

                    return Unauthorized(response);
                }
                else
                {
                    #region JWT
                    var jwt = Configuration.GetSection("Jwt").Get<Jwt>();
                    string userJson = System.Text.Json.JsonSerializer.Serialize(usuario);

                    var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                    new Claim("Email", usuario.Email),
                    new Claim("IdCliente", usuario.IdCliente.ToString()),
                    new Claim("Id", usuario.IdUsuario.ToString()),
                    new Claim("User", userJson),
                };

                    var KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

                    var signIn = new SigningCredentials(KEY, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(

                        jwt.Isssuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signIn

                    );
                    #endregion
                    response.StatusCode = 200;
                    response.Message = "Usuario verificado correctamente";
                    response.Data = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(response);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
