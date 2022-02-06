using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Models.Dto
{
    public class UsuarioDTO
    {
        public int idUsuario { get; set; }
        public string username { get; set; }
        public string nombre { get; set; }
        public string identificacion { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
    }
}