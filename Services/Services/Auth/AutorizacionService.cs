using Infraestructure.Models;
using Infraestructure.Models.Auth;
using Infraestructure.Repositorys;
using Infraestructure.Repositorys.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Services.Auth
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly IConfiguration Configuration;
        private readonly IRepositoryAuth repository;
        private readonly IRepositoryUsuario repositoryUsuario;
        private readonly string key;

        public AutorizacionService(IConfiguration configuration,string Key)
        { 
            Configuration = configuration;
            repository=new RepositoryAuth(Configuration);
            repositoryUsuario=new RepositoryUsuario(Configuration);
            key= Key;
        }

        private string GenerarToken(string idUsuario)
        {

            var keyBytes = Encoding.UTF8.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;


        }

        private string GenerarRefreshToken()
        {

            var byteArray = new byte[64];
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }

        private async Task<AutorizacionResponse> GuardarHistorialRefreshToken(int idUsuario,string token,string refreshToken)
        {
            try
            {

                var historialRefreshToken = new HistorialRefreshToken
                {
                    IdUsuario = idUsuario,
                    Token = token,
                    RefreshToken = refreshToken,
                    FechaCreacion = DateTime.UtcNow,
                    FechaExpiracion = DateTime.UtcNow.AddMinutes(20)
                };

                repository.SavehistorialRefreshToken(historialRefreshToken);

                return new AutorizacionResponse { Token = token, RefreshToken = refreshToken, Resultado = true, Msg = "Ok" };


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public async Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario)
        {
            try
            {
                HistorialRefreshToken historial = repository.GetHistorialRefreshToken(refreshTokenRequest.TokenExpirado, refreshTokenRequest.RefreshToken, idUsuario);

                if(historial == null)
                {
                    return new AutorizacionResponse { Resultado = false, Msg = "No existe refreshToken" };
                }

                var refreshTokenCreado = GenerarRefreshToken();
                var tokenCreado = GenerarToken(idUsuario.ToString());

                return await GuardarHistorialRefreshToken(idUsuario, tokenCreado, refreshTokenCreado);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            try
            {
                Usuario usuario =repositoryUsuario.getUserAuth(autorizacion.Email,autorizacion.Password);

                if (usuario == null)
                {
                    return await Task.FromResult<AutorizacionResponse>(null);
                }


                string tokenCreado = GenerarToken(usuario.IdUsuario.ToString());

                string refreshTokenCreado = GenerarRefreshToken();

                //return new AutorizacionResponse() { Token = tokenCreado, Resultado = true, Msg = "Ok" };

                return await GuardarHistorialRefreshToken(usuario.IdUsuario, tokenCreado, refreshTokenCreado);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
