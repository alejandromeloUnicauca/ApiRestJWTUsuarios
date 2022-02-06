using ApiRest.Models.Entities;
using ApiRest.Repositories;
using ApiRest.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiRest.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var usuario = unitOfWork.UsuarioRepository.Find(u => u.username == login.Username).FirstOrDefault();
            if (usuario == null)
                return NotFound();
            var password = Sha256.computeSha256(login.Password);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid = (usuario.password == password);
            if (isCredentialValid)
            {
                var token = TokenHelper.GenerateTokenJwt(login.Username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
