using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{ 
    public class Ingresos
    {
        public string Periodo { get; set; }
        public string no_doc { get; set; }
        public DateTime fecharecibido { get; set; }
        public string cobrador { get; set; }
        public string concepto { get; set; } 
        public string cod_ing {  get; set; }
        public string DescripcionCodigo { get; set; }
        public decimal monto { get; set; }
    }
}