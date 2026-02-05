using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{
    public class ARCKCL
    { 
        public string NO_CIA { get; set; }
        public string NO_CTA { get; set; }
        public string CC_1 { get; set; }
        public string CC_2 { get; set; }
        public string CC_3 { get; set; }
        public string TIPO_DOCU { get; set; }
        public int CHEQUE { get; set; }
        public string COD_CONT { get; set; }
        public string TIPO_MOV { get; set; }
        public decimal? MONTO { get; set; }
        public decimal? MONTO_DOL { get; set; }
        public string MONEDA { get; set; }
        public int NO_SECUENCIA { get; set; }
        public string NO_ASIENTO { get; set; }
        public decimal? TIPO_CAMBIO { get; set; }
        public string PRG_COD { get; set; }
        public string PRY_COD { get; set; }
        public string OBRACT_COD { get; set; }
        public string TAR_COD { get; set; }
        public string FF_COD { get; set; }
        public string OF_COD { get; set; }
        public int NO_LINEA { get; set; }
        public string EGR_COD { get; set; }

        public string CC
        {
            get { return CC_1 + " - " + CC_2 + " - " + CC_3; }
        }
    }

}