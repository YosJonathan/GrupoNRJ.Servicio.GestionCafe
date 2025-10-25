// <copyright file="AuthController.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Controlador de autenticacion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginSolicitud model)
        {
            if (model.Username == "root" && model.Password == "root")
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EstaEsUnaClaveSuperSeguraDeJWT_2025!"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim("role", "Admin")
            };

                var token = new JwtSecurityToken(
                    issuer: "GrupoNRJ.Servicio.GestionCafe",
                    audience: "GrupoNRJ.Cliente.GestionCafe",
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);

                return this.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return this.Unauthorized();
        }
    }
}
