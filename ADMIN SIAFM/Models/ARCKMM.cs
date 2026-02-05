using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{
    public class ARCKMM
    {
        public string NO_CIA { get; set; }
        public string NO_CTA { get; set; }
        public string PROCEDENCIA { get; set; }
        public string TIPO_DOC { get; set; }
        public int NO_DOCU { get; set; }

        public DateTime? FECHA { get; set; }
        public string BENEFICIARIO { get; set; }
        public string COMENTARIO { get; set; }
        public decimal? MONTO { get; set; }
        public string ESTADO { get; set; }
        public string CONCILIADO { get; set; }

        public int? MES { get; set; }
        public int? ANO { get; set; }
        public string IND_CON { get; set; }
        public DateTime? FECHA_CONC { get; set; }
        public DateTime? FECHA_ANULADO { get; set; }

        public string IND_BORRADO { get; set; }
        public string IND_OTROMOV { get; set; }

        public string MONEDA_CTA { get; set; }
        public decimal? TIPO_CAMBIO { get; set; }
        public string TIPO_AJUSTE { get; set; }

        public string IND_DIST { get; set; }
        public string T_CAMB_C_V { get; set; }

        public string IND_OTROS_MESES { get; set; }
        public string DEP_CAJA { get; set; }
        public string NO_PROVE { get; set; }

        public long? EPPTO_ID { get; set; }

        public string COD_IR { get; set; }
        public decimal? RETENCION_IR { get; set; }
        public string SEQ_IR { get; set; }

        public string COD_IM { get; set; }
        public decimal? RETENCION_IM { get; set; }
        public string SEQ_IM { get; set; }

        public decimal? SUBTOTAL { get; set; }

        public decimal? MTO_BASE_RET_IR { get; set; }
        public decimal? MTO_BASE_RET_IM { get; set; }
    }
}