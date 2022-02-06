using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    [Table("direccion")]
    public class Direccion
    {
        [Key,ForeignKey("usuario")]
        public int idDireccionUsuario { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Pais requerido")]
        public string pais { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Departamento requerido")]
        public string departamento { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ciudad requerida")]
        public string ciudad { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dirección requerida")]
        public string direccion{ get; set; }

        //propiedades de navegacion
        public virtual Usuario usuario { get; set; }
    }
}