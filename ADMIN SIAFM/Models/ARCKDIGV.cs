using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{
    public class ARCKDIGV
    { 
        public string NO_CIA { get; set; }           // VARCHAR2(2)
        public string NO_CTA { get; set; }           // VARCHAR2(20)
        public string TIPO_DOCU { get; set; }        // VARCHAR2(2)
        public int NO_SECUENCIA { get; set; }        // NUMBER(8)
        public string TIPO_REFE { get; set; }        // VARCHAR2(2)
        public string NO_REFE { get; set; }          // VARCHAR2(8)
        public string ACREEDOR { get; set; }         // VARCHAR2(100), NOT NULL (via constraint)
        public string COM_1 { get; set; }            // VARCHAR2(160), NOT NULL (via constraint)
        public decimal MONTO { get; set; }           // NUMBER(12,4), NOT NULL (via constraint)
        public string NO_RUC { get; set; }           // VARCHAR2(15)
        public decimal? TASA_IGV { get; set; }       // NUMBER(6,2), Nullable
        public string CONSIGNANTE { get; set; }      // VARCHAR2(6)
        public string POLIZA { get; set; }           // VARCHAR2(6)
    }

}