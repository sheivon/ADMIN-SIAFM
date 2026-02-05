using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{
    public class SolicitudPresupuestaria
    {
        public int id { get; set; }
        public string year { get; set; }
        public string solicitado { get; set; }
        public string concepto { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
    }
}