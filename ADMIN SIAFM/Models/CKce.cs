using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{
    public class CKce
    {
        public int id {  get; set; }
        public string Benficiario { get; set; }
        public string Cuenta { get; set; }
        public int cheque { get; set; }
        public decimal monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}