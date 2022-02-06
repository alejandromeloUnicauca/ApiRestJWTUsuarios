using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    [Table(name:"usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUsuario{ get; set; }
        [Required(AllowEmptyStrings=false, ErrorMessage="Identificación requerida")]
        [RegularExpression("^[0-9]*$", ErrorMessage ="Solo se admiten numeros")]
        [Index(IsUnique =true)]
        [StringLength(maximumLength:15)]
        public string identificacion { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre requerido")]
        public string nombre { get; set; }
        public string telefono { get; set; }
        [Index(IsUnique = true)]
        [StringLength(maximumLength:25)]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Usuario requerido")]
        public string username { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Password requerido")]
        public string password { get; set; }

        //Propiedades de navegacion
        public virtual Direccion Direccion { get; set; }
    }
}