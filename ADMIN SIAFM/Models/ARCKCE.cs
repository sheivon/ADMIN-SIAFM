using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ADMIN_SIAFM.Models
{
    public class ARCKCE
    {
        [Key]
        [Column(Order = 0)]
        public string TIPO_DOCU { get; set; }

        [Key]
        [Column(Order = 1)]
        public string NO_CTA { get; set; }

        [Key]
        [Column(Order = 2)]
        public int NO_SECUENCIA { get; set; }

        [Key]
        [Column(Order = 3)]
        public string NO_CIA { get; set; }

        public int CHEQUE { get; set; }

        public string MONEDA_CTA { get; set; }
        public string MONEDA_PAGO { get; set; }
        public string NO_PROVE { get; set; }
        public DateTime? FECHA { get; set; }
        public decimal? MONTO { get; set; }
        public string BENEFICIARIO { get; set; }
        public string IND_ACT { get; set; }
        public string COM { get; set; }
        public string IND_CON { get; set; }
        public string ANULADO { get; set; }
        public string EMITIDO { get; set; }
        public decimal? TOT_REF { get; set; }
        public decimal? TOT_DIFE_CAM { get; set; }
        public decimal? TOT_DB { get; set; }
        public decimal? TOT_CR { get; set; }
        public decimal? SALDO { get; set; }
        public decimal? TIPO_CAMBIO { get; set; }
        public decimal? MONTO_NOMINAL { get; set; }
        public decimal? SALDO_NOMINAL { get; set; }
        public string AUTORIZA { get; set; }
        public string ORIGEN { get; set; }
        public string T_CAMB_C_V { get; set; }
        public string NO_ASIENTO { get; set; }
        public decimal? MONTO_PROVE { get; set; }
        public decimal? RETENCION { get; set; }
        public string RUC { get; set; }
        public decimal? SUBTOTAL { get; set; }
        public string COD_RET { get; set; }
        public decimal? IGV { get; set; }
        public string EN_CAJA { get; set; }
        public decimal? TASA_IGV { get; set; }
        public string CONSIGNANTE { get; set; }
        public string POLIZA { get; set; }
        public string CENTRO { get; set; }
        public string GRUPO { get; set; }
        public int? NO_CLIENTE { get; set; }
        public string NO_NOTA { get; set; }
        public decimal? MTO_BASE_RET { get; set; }
        public decimal? CXC_MTO_DB { get; set; }
        public decimal? CXC_MTO_CR { get; set; }
        public string CENTRO2 { get; set; }
        public string GRUPO2 { get; set; }
        public int? NO_CLIENTE2 { get; set; }
        public string NO_NOTA2 { get; set; }
        public string NOMBRE_COMERCIAL { get; set; }
        public string TIPO_PERSONA { get; set; }
        public decimal? MTO_DEDUCCION { get; set; }
        public string DEPA { get; set; }
        public string AREA { get; set; }
        public string OBSERVACIONES { get; set; }
        public string COD_IM { get; set; }
        public decimal? RETENCION_IM { get; set; }
        public string SEQ_IR { get; set; }
        public string SEQ_IM { get; set; }
        public DateTime? FECHA_SOL { get; set; }
        public decimal? MTO_BASE_RET_IMI { get; set; }
        public int? EPPTO_ID { get; set; }
        public string ESTADO { get; set; }
        public string MOTIVO { get; set; }
    }
}