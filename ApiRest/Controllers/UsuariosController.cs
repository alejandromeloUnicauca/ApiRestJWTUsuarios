using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiRest.Models;
using ApiRest.Models.Context;
using ApiRest.Models.Dto;
using ApiRest.Repositories;
using ApiRest.Util;

namespace ApiRest.Controllers
{
    [Authorize]
    public class UsuariosController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Usuarios
        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UsuarioDTO> GetUsuarios()
        {
            //asignar datos de usuario y direccion a DTO
            var usuarios = from u in unitOfWork.UsuarioRepository.GetAll()
                           select new UsuarioDTO()
                           {
                               idUsuario = u.idUsuario,
                               identificacion = u.identificacion,
                               nombre = u.nombre,
                               telefono = u.telefono,
                               username = u.username,
                               //Concatenar la direccion si no es null
                               direccion = u.Direccion == null ? 
                                                            "" : 
                                                            u.Direccion.direccion + ", " 
                                                            + u.Direccion.ciudad + ", " 
                                                            + u.Direccion.departamento + " - " 
                                                            + u.Direccion.pais
                            };
            return usuarios;
        }

        // GET: api/Usuarios?identificacion=123
        /// <summary>
        /// Obtiene el usuario por Identificacion
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> GetUsuarioByIdentificacion(string identificacion)
        {
            Usuario usuario = unitOfWork.UsuarioRepository.Find(u => u.identificacion == identificacion).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }


            var usuariodto = new UsuarioDTO()
            {
                idUsuario = usuario.idUsuario,
                identificacion = usuario.identificacion,
                nombre = usuario.nombre,
                telefono = usuario.telefono,
                username = usuario.username,
                //Concatenar la direccion si no es null
                direccion = usuario.Direccion == null ?
                                            "" :
                                            usuario.Direccion.direccion + ", "
                                            + usuario.Direccion.ciudad + ", "
                                            + usuario.Direccion.departamento + " - "
                                            + usuario.Direccion.pais
            };
            return Ok(usuariodto);
        }

        // PUT: api/Usuarios/5
        /// <summary>
        /// Actualiza el usuario por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.idUsuario)
            {
                return BadRequest();
            }

            usuario.password = Sha256.computeSha256(usuario.password);

            try
            {
                unitOfWork.UsuarioRepository.Update(usuario);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return this.Content(HttpStatusCode.Conflict, new { response = ex.Message });
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Usuarios
        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            usuario.password = Sha256.computeSha256(usuario.password);

            try
            {

                unitOfWork.UsuarioRepository.Add(usuario);
                unitOfWork.Save();
            } catch (Exception ex)
            {
                return this.Content(HttpStatusCode.Conflict, new { response = ex.Message });
            }

            var usuariodto = new UsuarioDTO()
            {
                idUsuario = usuario.idUsuario,
                identificacion = usuario.identificacion,
                nombre = usuario.nombre,
                telefono = usuario.telefono,
                username = usuario.username,
                //Concatenar la direccion si no es null
                direccion = usuario.Direccion == null ?
                                            "" :
                                            usuario.Direccion.direccion + ", "
                                            + usuario.Direccion.ciudad + ", "
                                            + usuario.Direccion.departamento + " - "
                                            + usuario.Direccion.pais
            };

            return CreatedAtRoute("DefaultApi", new { id = usuariodto.idUsuario }, usuariodto);
        }

        // DELETE: api/Usuarios/5
        /// <summary>
        /// Elimina un usuario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> DeleteUsuario(int id)
        {
            Usuario usuario = unitOfWork.UsuarioRepository.GetById(id);
            if (usuario == null)
            {
                return NotFound();
            }

            try
            {
                unitOfWork.UsuarioRepository.Remove(usuario);
                unitOfWork.Save();
            } catch (Exception ex)
            {
                return this.Content(HttpStatusCode.Conflict, new { response = ex.Message });
            }

            return Ok(usuario);
        }

        private bool UsuarioExists(int id)
        {
            return unitOfWork.UsuarioRepository.GetById(id) != null;
        }
    }
}