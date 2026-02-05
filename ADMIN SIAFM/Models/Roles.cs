using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{
    public class Roles
    {
        public int ID_ROL {  get; set; }
        public string Nombre { get; set; }
        public string Activo { get; set; }  // A = ACTIVO, I = INACTIVO 
    }
}