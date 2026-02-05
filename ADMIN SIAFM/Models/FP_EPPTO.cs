using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{
    public class FP_EPPTO
    {   public string MC_COD { get; set; }                   // VARCHAR2(2)
        public int ANO { get; set; }                         // NUMBER(4)
        public long EPPTO_ID { get; set; }                   // NUMBER(10)
        public DateTime? FECHA { get; set; }                 // DATE (nullable)
        public string ESTADO { get; set; }                   // VARCHAR2(1) E= emitido, A = Anulado ,P=Pendiente
        public string CONCEPTO { get; set; }                 // VARCHAR2(500)
        public decimal? MONTO { get; set; }                  // NUMBER(18,2) (nullable)
        public string SOLICITADO_POR { get; set; }           // VARCHAR2(100)
        public string NO_CTA { get; set; }                   // VARCHAR2(20)
        public int? CHEQUE { get; set; }                     // NUMBER(8) (nullable)
        public string CJA_CHICA { get; set; } = "N";         // VARCHAR2(1) default 'N'

        // Optional: Convenience property
        public bool EsCajaChica => CJA_CHICA == "S";
    }
}