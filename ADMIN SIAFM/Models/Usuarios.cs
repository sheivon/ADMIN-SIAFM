using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{
    public class Usuarios
    {   public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Pass { get; set; }
        public string Activo { get; set; }
        public DateTime FechaRg { get; set; }
        public DateTime? FechaBj { get; set; }
        public int IdRol { get; set; }
        public string Name { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Cargo { get; set; }
    }
}