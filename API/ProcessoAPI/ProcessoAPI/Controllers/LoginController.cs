using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProcessoAPI.Domains;
using ProcessoAPI.Interfaces;
using ProcessoAPI.ViewModel;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProcessoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository repo)
        {
            _usuarioRepository = repo;
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.Login(login.Email, login.Senha);

                if (usuarioBuscado == null)
                {
                    return Unauthorized(new { msg = "Email ou Senha invalida!" });

                }

                var minhasClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim("role", usuarioBuscado.IdTipoUsuario.ToString())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("api-authenticator"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var meuToken = new JwtSecurityToken(
                        issuer: "ProcessoAPI",
                        audience: "ProcessoAPI",
                        claims: minhasClaims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: creds
                    ); 

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(meuToken) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
