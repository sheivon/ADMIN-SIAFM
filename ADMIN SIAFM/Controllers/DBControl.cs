using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

using Oracle.ManagedDataAccess.Client;
using ADMIN_SIAFM.Models;
using System.Web.Optimization;
using System.EnterpriseServices;
using System.EnterpriseServices.Internal;
using System.Data;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ADMIN_SIAFM.Controllers
{
    public class DBControl
    {
        private static string MagicWord = "olrfjRUM7356jkcza/;346h"; //enc key
        private static string key = "1N$f0_S#";
        private static string pwd = "1uc3$2";


        public static string Connexion()
        {
            return Connexion24(DateTime.Now.Year);
        }
        public static string Connexion24(int year)
        {
            if (year == 2022)
            {
                return ConfigurationManager.ConnectionStrings["OraSIAFM22"].ConnectionString; 
            }
            if (year == 2024)
            {
                return ConfigurationManager.ConnectionStrings["OraSIAFM24"].ConnectionString;
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["OraSIAFM25"].ConnectionString;
            }
        }
        public static string Connexionlogin(int year)
        {
            if(year == 2022)
            {

            }
            if(year == 2024) { }
            if(year == 2024) { }
            return "";
        }
        public static void ConnectToDatabase()
        { 

            using (OracleConnection conn = new OracleConnection(Connexion()))
            {
                conn.Open();
                // Perform database operations
            }
        }
        #region System Core
        public static string EncryptData(string Valor)
        {
            string str2;
            try
            {
                using (OracleConnection con = new OracleConnection(Connexion()))
                {
                    con.Open();

                    using (OracleCommand command = new OracleCommand())
                    {
                        command.Connection = con;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 600;
                        command.CommandText = "SELECT MS.PK_UTIL.F_ENCRIPTAR(:pr_valor,:pr_magic_key) FROM DUAL";
                        command.Parameters.Add("pr_valor", OracleDbType.Varchar2);
                        command.Parameters.Add("pr_magic_key", OracleDbType.Varchar2);
                        command.Parameters["pr_valor"].Value = Valor;
                        command.Parameters["pr_magic_key"].Value = key;
                        string str = command.ExecuteScalar().ToString();

                        str2 = str;
                        return str2;
                    } 
                }
            }
            catch { return string.Empty; }
           
          }
        public static string DecryptData(string Valor)
        {
            string str2;
            try
            {
                using (OracleConnection con = new OracleConnection(Connexion()))
                {
                    con.Open();

                    using (OracleCommand command = new OracleCommand())
                    {
                        command.Connection = con;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 600;
                        command.CommandText = "SELECT MS.PK_UTIL.F_DESENCRIPTAR(:pr_valor,:pr_magic_key) FROM DUAL";
                        command.Parameters.Add("pr_valor", OracleDbType.Varchar2);
                        command.Parameters.Add("pr_magic_key", OracleDbType.Varchar2);
                        command.Parameters["pr_valor"].Value = Valor;
                        command.Parameters["pr_magic_key"].Value = key;
                        string str = command.ExecuteScalar().ToString();

                        str2 = str;
                        return str2;
                    } 
                }
            }
            catch { return string.Empty; }
           
          }
        public static bool Insertar_Usuario(int IdRol, string NameUser, string Pass, string NombreCompleto, string Identificacion, string Telefono, string Correo, string Cargo,int year)
        {
            bool flag;
            try
            {
                using (OracleConnection connection = new OracleConnection(Connexion24(year)))
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandTimeout = 700,
                        CommandText = "begin insert into ms.ms_usuario(nombre, pass, activo, fecha_rg,  id_rol, name, identificacion, telefono, correo, cargo)  values(:pr_nombre, :pr_pass, :pr_activo, :pr_fecha_rg, :pr_id_rol, :pr_name,  :pr_identificacion, :pr_telefono, :pr_correo, :pr_cargo); commit; end;"
                    };
                    command.Parameters.Add("pr_nombre", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_pass", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_activo", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_fecha_rg", OracleDbType.Date);
                    command.Parameters.Add("pr_id_rol", OracleDbType.Int32);
                    command.Parameters.Add("pr_name", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_identificacion", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_telefono", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_correo", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_cargo", OracleDbType.Varchar2);
                    command.Parameters["pr_nombre"].Value = NombreCompleto;
                    command.Parameters["pr_pass"].Value = Pass;
                    command.Parameters["pr_activo"].Value = "A";
                    command.Parameters["pr_fecha_rg"].Value = DateTime.Now;
                    command.Parameters["pr_id_rol"].Value = IdRol;
                    command.Parameters["pr_name"].Value = NameUser;
                    command.Parameters["pr_identificacion"].Value = Identificacion;
                    command.Parameters["pr_telefono"].Value = Telefono;
                    command.Parameters["pr_correo"].Value = Correo;
                    command.Parameters["pr_cargo"].Value = Cargo;
                    command.ExecuteNonQuery();
                    connection.Close();
                    flag = true;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
        public static bool Login(string NameUser, string Pass, out int Id_Usuario, out int Id_Rol, out string Nombre_Usuario)
        {
            bool flag2;
            try
            {
                Id_Usuario = 0;
                Id_Rol = 0;
                Nombre_Usuario = "";
                using (OracleConnection connection = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.0.10.10)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=SIAFM24)));User Id=MS;Password=MS;")) // Connexion24(DateTime.Now.Year)))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand())
                    {
                        command.Connection = connection;

                        //command.CommandType = CommandType.StoredProcedure;
                        //command.CommandTimeout = 900;
                        ////command.CommandText = "PK_UTIL.P_VALIDAR_CREDENCIAL";
                        //command.CommandText = "PK_UTIL.P_VALIDAR_CREDENCIAL";

                        //command.Parameters.Add("P_USUARIO", OracleDbType.Varchar2);
                        //command.Parameters.Add("P_PASS", OracleDbType.Varchar2);
                        //command.Parameters.Add("P_ID_USUARIO", OracleDbType.Int32, (int)Id_Usuario, ParameterDirection.Output);
                        //command.Parameters.Add("P_ID_ROL", OracleDbType.Int32, (int)Id_Rol, ParameterDirection.Output);
                        //command.Parameters.Add("P_NOMBRE", OracleDbType.Varchar2, 0xff, Nombre_Usuario, ParameterDirection.Output);
                        //command.Parameters["P_USUARIO"].Value = NameUser;
                        //command.Parameters["P_PASS"].Value = Pass;
                        //command.ExecuteNonQuery();
                        //Id_Usuario = Convert.ToInt32(command.Parameters["P_ID_USUARIO"].Value.ToString());
                        //Id_Rol = Convert.ToInt32(command.Parameters["P_ID_ROL"].Value.ToString());
                        //Nombre_Usuario = command.Parameters["P_NOMBRE"].Value.ToString();
                        //connection.Close();
                        //flag2 = Id_Usuario > 0;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 900;
                        // Prepare the SELECT query
                        command.CommandText = "SELECT ID_USUARIO, NOMBRE, ACTIVO, ID_ROL, CARGO, IDENTIFICACION, TELEFONO, CORREO  FROM MS_USUARIO WHERE NAME=:P_USUARIO AND PASS=:P_PASS";

                        // Add parameters for the query
                        command.Parameters.Add("P_USUARIO", OracleDbType.Varchar2).Value = NameUser;
                        var securepass = Security.Encrypt(Pass);
                        command.Parameters.Add("P_PASS", OracleDbType.Varchar2).Value = securepass;
                           
                        // Execute the query using ExecuteReader for SELECT queries
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Check if the record is found
                            {
                                // Retrieve output values from the result set or the output parameters
                                Id_Usuario = Convert.ToInt32(reader["ID_USUARIO"]);
                                Id_Rol = Convert.ToInt32(reader["ID_ROL"]);
                                Nombre_Usuario = reader["NOMBRE"].ToString();

                                //Optionally, retrieve other fields if needed
                                // CARGO = reader["CARGO"].ToString();
                                //TELEFONO = reader["TELEFONO"].ToString();
                                //CORREO = reader["CORREO"].ToString();
                            }
                            else
                            {
                                // If no record is found, set default values or handle error.
                                Id_Usuario = 0;
                                Id_Rol = 0;
                                Nombre_Usuario = string.Empty;
                            } 
                            flag2 = Id_Usuario > 0;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Id_Usuario = 0;
                Id_Rol = 0;
                Nombre_Usuario = "";
                flag2 = false;
            }
            return flag2;
        }
        public bool Validar_Permiso(int Id_Rol, string Nombre_Acceso)
        {
            bool flag2;
            try
            {
                using (OracleConnection connection = new OracleConnection(Connexion24(2025)))
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandTimeout = 600,
                        CommandText = "SELECT MS.PK_UTIL.F_VALIDAR_PERMISO(:pr_id_rol,:pr_acceso,:pr_modulo) from dual"
                    };
                    command.Parameters.Add("pr_id_rol", OracleDbType.Int32);
                    command.Parameters.Add("pr_acceso", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_modulo", OracleDbType.Varchar2);
                    command.Parameters["pr_id_rol"].Value = Id_Rol;
                    command.Parameters["pr_acceso"].Value = Nombre_Acceso;
                    command.Parameters["pr_modulo"].Value = "SEGURIDAD";
                    string str = command.ExecuteScalar().ToString();
                    connection.Close();
                    flag2 = str == "S";
                }
            }
            catch (Exception)
            {
                flag2 = false;
            }
            return flag2;
        }
        public static bool deactivate_Usuario(int Id_Usuario,int year)
        {
            bool flag;
            try
            {
                using (OracleConnection connection = new OracleConnection(Connexion24(year)))
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandTimeout = 700,
                        CommandText = "begin update ms.ms_usuario set activo = :pr_activo,  fecha_bj = :pr_fecha_bj where id_usuario = :pr_id_usuario; commit; end;"
                    };
                    command.Parameters.Add("pr_activo", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_fecha_bj", OracleDbType.Date);
                    command.Parameters.Add("pr_id_usuario", OracleDbType.Int32);
                    command.Parameters["pr_activo"].Value = "I";
                    command.Parameters["pr_fecha_bj"].Value = DateTime.Now;
                    command.Parameters["pr_id_usuario"].Value = Id_Usuario;
                    command.ExecuteNonQuery();
                    connection.Close();
                    flag = true;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
        public static bool Inactivar_Rol(int Id_Rol,int year)
        {
            bool flag;
            try
            {
                using (OracleConnection connection = new OracleConnection(Connexion24(year)))
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandTimeout = 700,
                        CommandText = "begin update ms.ms_rol set activo = :pr_activo  where id_rol = :pr_id_rol; commit; end;"
                    };
                    command.Parameters.Add("pr_activo", OracleDbType.Varchar2);
                    command.Parameters.Add("pr_id_rol", OracleDbType.Int32);
                    command.Parameters["pr_activo"].Value = "I";
                    command.Parameters["pr_id_rol"].Value = Id_Rol;
                    command.ExecuteNonQuery();
                    connection.Close();
                    flag = true;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        #endregion
        #region Roles
        public static List<Roles> GetRoles(int yr)
        {
            var tmplist = new List<Roles>();
            try
            { 
                using (var conn = new OracleConnection(Connexion24(yr)))
                {
                    conn.Open();
                    var cmd = new OracleCommand("SELECT * FROM MS.MS_ROL", conn); 
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tmplist.Add(new Roles() {  ID_ROL = reader.GetInt32(0), Nombre = reader.GetString(1), Activo = reader.GetString(2) });
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return new List<Roles>(); }
            return tmplist;
        }
        public static Roles SaveRole( Roles rol)
        {
            var tmprol = new Roles();
            try
            {
                using (var conn = new OracleConnection(Connexion()))
                {
                    conn.Open();
                    var cmd = new OracleCommand();

                    cmd.Connection = conn; 
                    cmd.CommandText = "PK_UTIL.P_INSERT_ROL";
                    cmd.CommandType = CommandType.StoredProcedure;
                     
                    // Input parameters
                    cmd.Parameters.Add(new OracleParameter("P_NOMBRE", OracleDbType.Varchar2)).Value = rol.Nombre;
                    cmd.Parameters.Add(new OracleParameter("P_ACTIVO", OracleDbType.Varchar2)).Value = rol.Activo;

                    // Output parameter
                    OracleParameter outputId = new OracleParameter("P_ID", OracleDbType.Int32);
                    outputId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputId);
                    cmd.ExecuteNonQuery();

                    int tmpid = Convert.ToInt32(outputId.Value);

                    tmprol = new Roles() { ID_ROL = tmpid, Nombre = rol.Nombre, Activo = rol.Activo }; 
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return new Roles() { ID_ROL = -1, Nombre = "ERROR! ", Activo = "I" }; }
            return  tmprol;
        }
        #endregion
        
        #region Usuarios
        public static List<Usuarios> GetUsuarios(int year)
        {
            var tmplist = new List<Usuarios>();
            try
            { 
                using (var conn = new OracleConnection(Connexion24(year)))
                {
                    conn.Open();
                    var cmd = new OracleCommand("SELECT * FROM MS.MS_USUARIO", conn); 
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tmplist.Add(new Usuarios
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Pass = reader.GetString(2),
                                Activo = reader.GetString(3),
                                FechaRg = reader.GetDateTime(4),
                                FechaBj = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                IdRol = reader.GetInt32(6),
                                Name = reader.GetString(7),
                                Identificacion = reader.IsDBNull(8) ? null : reader.GetString(8),
                                Telefono = reader.IsDBNull(9) ? null : reader.GetString(9),
                                Correo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Cargo = reader.IsDBNull(11) ? null : reader.GetString(11)
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return new List<Usuarios>(); }
            return tmplist;
        }

        public static Usuarios ValidarUsuario(Usuarios usr)
        {
            try
            {
                if (usr == null) {
                
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message, ex); return new Usuarios(); }  
            return new Usuarios(); // tmpr
        }

        private static string GetPassword(string pass)
        {
            return "";
        }

        #endregion
       
        #region Checks
        public static List<CKce> GetCK(int yr)
        {
            List<CKce> cks = new List<CKce>();  
            try
            {
                using (OracleConnection conn = new OracleConnection(Connexion24(yr)))
                {
                    conn.Open();
                    using(OracleCommand cm = new OracleCommand())
                    { 
                        cm.Connection = conn; 
                        cm.CommandType = CommandType.Text;
                        //cm.CommandTimeout = 600;

                        string qry = "select NO_SECUENCIA,BENEFICIARIO,MONTO,NO_CTA,CHEQUE,EPPTO_ID,FECHA,ESTADO from arckce";

                        cm.CommandText = qry; 
                        var r = cm.ExecuteReader();
                        while (r.Read())
                        {
                            cks.Add(new CKce() { id =r.GetInt32(0), Benficiario = r.GetString(1), monto = r.GetDecimal(2), Cuenta = r.GetString(3), cheque = r.GetInt32(4)/*, Fecha = r.GetDateTime(5)*/ });
                        }
                        return cks;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return new List<CKce>(); }
        }

        public static async Task<List<ARCKCE>> GetAllArckce()
        {
            var result = new List<ARCKCE>();
            try
            {
                using (OracleConnection conn = new OracleConnection(Connexion24(2025)))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cm = new OracleCommand())
                    {
                        cm.Connection = conn;
                        cm.CommandType = CommandType.Text;

                        string qry = @"
                    SELECT
                        TIPO_DOCU,
                        NO_CTA,
                        NO_SECUENCIA,
                        NO_CIA,
                        CHEQUE,
                        MONEDA_CTA,
                        MONEDA_PAGO,
                        NO_PROVE,
                        FECHA,
                        MONTO,
                        BENEFICIARIO,
                        IND_ACT,
                        COM,
                        IND_CON,
                        ANULADO,
                        EMITIDO,
                        TOT_REF,
                        TOT_DIFE_CAM,
                        TOT_DB,
                        TOT_CR,
                        SALDO,
                        TIPO_CAMBIO,
                        MONTO_NOMINAL,
                        SALDO_NOMINAL,
                        AUTORIZA,
                        ORIGEN,
                        T_CAMB_C_V,
                        NO_ASIENTO,
                        MONTO_PROVE,
                        RETENCION,
                        RUC,
                        SUBTOTAL,
                        COD_RET,
                        IGV,
                        EN_CAJA,
                        TASA_IGV,
                        CONSIGNANTE,
                        POLIZA,
                        CENTRO,
                        GRUPO,
                        NO_CLIENTE,
                        NO_NOTA,
                        MTO_BASE_RET,
                        CXC_MTO_DB,
                        CXC_MTO_CR,
                        CENTRO2,
                        GRUPO2,
                        NO_CLIENTE2,
                        NO_NOTA2,
                        NOMBRE_COMERCIAL,
                        TIPO_PERSONA,
                        MTO_DEDUCCION,
                        DEPA,
                        AREA,
                        OBSERVACIONES,
                        COD_IM,
                        RETENCION_IM,
                        SEQ_IR,
                        SEQ_IM,
                        FECHA_SOL,
                        MTO_BASE_RET_IMI,
                        EPPTO_ID,
                        ESTADO,
                        MOTIVO
                    FROM ARCKCE";

                        cm.CommandText = qry;

                        using (var r = await cm.ExecuteReaderAsync())
                        {
                            while (await r.ReadAsync())
                            {
                                var item = new ARCKCE
                                {
                                    TIPO_DOCU = r.IsDBNull(r.GetOrdinal("TIPO_DOCU")) ? "N/A" : r.GetString(r.GetOrdinal("TIPO_DOCU")),
                                    NO_CTA = r.IsDBNull(r.GetOrdinal("NO_CTA")) ? "N/A" : r.GetString(r.GetOrdinal("NO_CTA")),
                                    NO_SECUENCIA = r.IsDBNull(r.GetOrdinal("NO_SECUENCIA")) ? 0 : r.GetInt32(r.GetOrdinal("NO_SECUENCIA")),
                                    NO_CIA = r.IsDBNull(r.GetOrdinal("NO_CIA")) ? "N/A" : r.GetString(r.GetOrdinal("NO_CIA")),
                                    CHEQUE = r.IsDBNull(r.GetOrdinal("CHEQUE")) ? 0 : r.GetInt32(r.GetOrdinal("CHEQUE")),
                                    MONEDA_CTA = r.IsDBNull(r.GetOrdinal("MONEDA_CTA")) ? "N/A" : r.GetString(r.GetOrdinal("MONEDA_CTA")),
                                    MONEDA_PAGO = r.IsDBNull(r.GetOrdinal("MONEDA_PAGO")) ? "N/A" : r.GetString(r.GetOrdinal("MONEDA_PAGO")),
                                    NO_PROVE = r.IsDBNull(r.GetOrdinal("NO_PROVE")) ? "N/A" : r.GetString(r.GetOrdinal("NO_PROVE")),
                                    FECHA = r.IsDBNull(r.GetOrdinal("FECHA")) ? DateTime.MinValue : r.GetDateTime(r.GetOrdinal("FECHA")),
                                    MONTO = r.IsDBNull(r.GetOrdinal("MONTO")) ? 0 : r.GetDecimal(r.GetOrdinal("MONTO")),
                                    BENEFICIARIO = r.IsDBNull(r.GetOrdinal("BENEFICIARIO")) ? "N/A" : r.GetString(r.GetOrdinal("BENEFICIARIO")),
                                    IND_ACT = r.IsDBNull(r.GetOrdinal("IND_ACT")) ? "N/A" : r.GetString(r.GetOrdinal("IND_ACT")),
                                    COM = r.IsDBNull(r.GetOrdinal("COM")) ? "N/A" : r.GetString(r.GetOrdinal("COM")),
                                    IND_CON = r.IsDBNull(r.GetOrdinal("IND_CON")) ? "N/A" : r.GetString(r.GetOrdinal("IND_CON")),
                                    ANULADO = r.IsDBNull(r.GetOrdinal("ANULADO")) ? "N/A" : r.GetString(r.GetOrdinal("ANULADO")),
                                    EMITIDO = r.IsDBNull(r.GetOrdinal("EMITIDO")) ? "N/A" : r.GetString(r.GetOrdinal("EMITIDO")),
                                    TOT_REF = r.IsDBNull(r.GetOrdinal("TOT_REF")) ? 0 : r.GetDecimal(r.GetOrdinal("TOT_REF")),
                                    TOT_DIFE_CAM = r.IsDBNull(r.GetOrdinal("TOT_DIFE_CAM")) ? 0 : r.GetDecimal(r.GetOrdinal("TOT_DIFE_CAM")),
                                    TOT_DB = r.IsDBNull(r.GetOrdinal("TOT_DB")) ? 0 : r.GetDecimal(r.GetOrdinal("TOT_DB")),
                                    TOT_CR = r.IsDBNull(r.GetOrdinal("TOT_CR")) ? 0 : r.GetDecimal(r.GetOrdinal("TOT_CR")),
                                    SALDO = r.IsDBNull(r.GetOrdinal("SALDO")) ? 0 : r.GetDecimal(r.GetOrdinal("SALDO")),
                                    TIPO_CAMBIO = r.IsDBNull(r.GetOrdinal("TIPO_CAMBIO")) ? 0 : r.GetDecimal(r.GetOrdinal("TIPO_CAMBIO")),
                                    MONTO_NOMINAL = r.IsDBNull(r.GetOrdinal("MONTO_NOMINAL")) ? 0 : r.GetDecimal(r.GetOrdinal("MONTO_NOMINAL")),
                                    SALDO_NOMINAL = r.IsDBNull(r.GetOrdinal("SALDO_NOMINAL")) ? 0 : r.GetDecimal(r.GetOrdinal("SALDO_NOMINAL")),
                                    AUTORIZA = r.IsDBNull(r.GetOrdinal("AUTORIZA")) ? "N/A" : r.GetString(r.GetOrdinal("AUTORIZA")),
                                    ORIGEN = r.IsDBNull(r.GetOrdinal("ORIGEN")) ? "N/A" : r.GetString(r.GetOrdinal("ORIGEN")),
                                    T_CAMB_C_V = r.IsDBNull(r.GetOrdinal("T_CAMB_C_V")) ? "N/A" : r.GetString(r.GetOrdinal("T_CAMB_C_V")),
                                    NO_ASIENTO = r.IsDBNull(r.GetOrdinal("NO_ASIENTO")) ? "N/A" : r.GetString(r.GetOrdinal("NO_ASIENTO")),
                                    MONTO_PROVE = r.IsDBNull(r.GetOrdinal("MONTO_PROVE")) ? 0 : r.GetDecimal(r.GetOrdinal("MONTO_PROVE")),
                                    RETENCION = r.IsDBNull(r.GetOrdinal("RETENCION")) ? 0 : r.GetDecimal(r.GetOrdinal("RETENCION")),
                                    RUC = r.IsDBNull(r.GetOrdinal("RUC")) ? "N/A" : r.GetString(r.GetOrdinal("RUC")),
                                    SUBTOTAL = r.IsDBNull(r.GetOrdinal("SUBTOTAL")) ? 0 : r.GetDecimal(r.GetOrdinal("SUBTOTAL")),
                                    COD_RET = r.IsDBNull(r.GetOrdinal("COD_RET")) ? "N/A" : r.GetString(r.GetOrdinal("COD_RET")),
                                    IGV = r.IsDBNull(r.GetOrdinal("IGV")) ? 0 : r.GetDecimal(r.GetOrdinal("IGV")),
                                    EN_CAJA = r.IsDBNull(r.GetOrdinal("EN_CAJA")) ? "N/A" : r.GetString(r.GetOrdinal("EN_CAJA")),
                                    TASA_IGV = r.IsDBNull(r.GetOrdinal("TASA_IGV")) ? 0 : r.GetDecimal(r.GetOrdinal("TASA_IGV")),
                                    CONSIGNANTE = r.IsDBNull(r.GetOrdinal("CONSIGNANTE")) ? "N/A" : r.GetString(r.GetOrdinal("CONSIGNANTE")),
                                    POLIZA = r.IsDBNull(r.GetOrdinal("POLIZA")) ? "N/A" : r.GetString(r.GetOrdinal("POLIZA")),
                                    CENTRO = r.IsDBNull(r.GetOrdinal("CENTRO")) ? "N/A" : r.GetString(r.GetOrdinal("CENTRO")),
                                    GRUPO = r.IsDBNull(r.GetOrdinal("GRUPO")) ? "N/A" : r.GetString(r.GetOrdinal("GRUPO")),
                                    NO_CLIENTE = r.IsDBNull(r.GetOrdinal("NO_CLIENTE")) ? 0 : r.GetInt32(r.GetOrdinal("NO_CLIENTE")),
                                    NO_NOTA = r.IsDBNull(r.GetOrdinal("NO_NOTA")) ? "N/A" : r.GetString(r.GetOrdinal("NO_NOTA")),
                                    MTO_BASE_RET = r.IsDBNull(r.GetOrdinal("MTO_BASE_RET")) ? 0 : r.GetDecimal(r.GetOrdinal("MTO_BASE_RET")),
                                    CXC_MTO_DB = r.IsDBNull(r.GetOrdinal("CXC_MTO_DB")) ? 0 : r.GetDecimal(r.GetOrdinal("CXC_MTO_DB")),
                                    CXC_MTO_CR = r.IsDBNull(r.GetOrdinal("CXC_MTO_CR")) ? 0 : r.GetDecimal(r.GetOrdinal("CXC_MTO_CR")),
                                    CENTRO2 = r.IsDBNull(r.GetOrdinal("CENTRO2")) ? "N/A" : r.GetString(r.GetOrdinal("CENTRO2")),
                                    GRUPO2 = r.IsDBNull(r.GetOrdinal("GRUPO2")) ? "N/A" : r.GetString(r.GetOrdinal("GRUPO2")),
                                    NO_CLIENTE2 = r.IsDBNull(r.GetOrdinal("NO_CLIENTE2")) ? 0 : r.GetInt32(r.GetOrdinal("NO_CLIENTE2")),
                                    NO_NOTA2 = r.IsDBNull(r.GetOrdinal("NO_NOTA2")) ? "N/A" : r.GetString(r.GetOrdinal("NO_NOTA2")),
                                    NOMBRE_COMERCIAL = r.IsDBNull(r.GetOrdinal("NOMBRE_COMERCIAL")) ? "N/A" : r.GetString(r.GetOrdinal("NOMBRE_COMERCIAL")),
                                    TIPO_PERSONA = r.IsDBNull(r.GetOrdinal("TIPO_PERSONA")) ? "N/A" : r.GetString(r.GetOrdinal("TIPO_PERSONA")),
                                    MTO_DEDUCCION = r.IsDBNull(r.GetOrdinal("MTO_DEDUCCION")) ? 0 : r.GetDecimal(r.GetOrdinal("MTO_DEDUCCION")),
                                    DEPA = r.IsDBNull(r.GetOrdinal("DEPA")) ? "N/A" : r.GetString(r.GetOrdinal("DEPA")),
                                    AREA = r.IsDBNull(r.GetOrdinal("AREA")) ? "N/A" : r.GetString(r.GetOrdinal("AREA")),
                                    OBSERVACIONES = r.IsDBNull(r.GetOrdinal("OBSERVACIONES")) ? "N/A" : r.GetString(r.GetOrdinal("OBSERVACIONES")),
                                    COD_IM = r.IsDBNull(r.GetOrdinal("COD_IM")) ? "N/A" : r.GetString(r.GetOrdinal("COD_IM")),
                                    RETENCION_IM = r.IsDBNull(r.GetOrdinal("RETENCION_IM")) ? 0 : r.GetDecimal(r.GetOrdinal("RETENCION_IM")),
                                    SEQ_IR = r.IsDBNull(r.GetOrdinal("SEQ_IR")) ? "N/A" : r.GetString(r.GetOrdinal("SEQ_IR")),
                                    SEQ_IM = r.IsDBNull(r.GetOrdinal("SEQ_IM")) ? "N/A" : r.GetString(r.GetOrdinal("SEQ_IM")),
                                    FECHA_SOL = r.IsDBNull(r.GetOrdinal("FECHA_SOL")) ? DateTime.MinValue : r.GetDateTime(r.GetOrdinal("FECHA_SOL")),
                                    MTO_BASE_RET_IMI = r.IsDBNull(r.GetOrdinal("MTO_BASE_RET_IMI")) ? 0 : r.GetDecimal(r.GetOrdinal("MTO_BASE_RET_IMI")),
                                    EPPTO_ID = r.IsDBNull(r.GetOrdinal("EPPTO_ID")) ? 0 : r.GetInt32(r.GetOrdinal("EPPTO_ID")),
                                    ESTADO = r.IsDBNull(r.GetOrdinal("ESTADO")) ? "N/A" : r.GetString(r.GetOrdinal("ESTADO")),
                                    MOTIVO = r.IsDBNull(r.GetOrdinal("MOTIVO")) ? "N/A" : r.GetString(r.GetOrdinal("MOTIVO"))
                                };

                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ARCKCE>(); // Return empty list on error
            }

            return result;
        }

        public static async Task<List<ARCKMM>> GetAllArckmm()
        {
            var result = new List<ARCKMM>();
            try
            {
                using (OracleConnection conn = new OracleConnection(Connexion24(2025)))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;

                        string query = @"
                    SELECT
                        NO_CIA,
                        NO_CTA,
                        PROCEDENCIA,
                        TIPO_DOC,
                        NO_DOCU,
                        FECHA,
                        BENEFICIARIO,
                        COMENTARIO,
                        MONTO,
                        ESTADO,
                        CONCILIADO,
                        MES,
                        ANO,
                        IND_CON,
                        FECHA_CONC,
                        FECHA_ANULADO,
                        IND_BORRADO,
                        IND_OTROMOV,
                        MONEDA_CTA,
                        TIPO_CAMBIO,
                        TIPO_AJUSTE,
                        IND_DIST,
                        T_CAMB_C_V,
                        IND_OTROS_MESES,
                        DEP_CAJA,
                        NO_PROVE,
                        EPPTO_ID,
                        COD_IR,
                        RETENCION_IR,
                        SEQ_IR,
                        COD_IM,
                        RETENCION_IM,
                        SEQ_IM,
                        SUBTOTAL,
                        MTO_BASE_RET_IR,
                        MTO_BASE_RET_IM
                    FROM ARCKMM";

                        cmd.CommandText = query;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new ARCKMM
                                {
                                    NO_CIA = reader.IsDBNull(0) ? null : reader.GetString(0),
                                    NO_CTA = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    PROCEDENCIA = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    TIPO_DOC = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    NO_DOCU = reader.IsDBNull(4) ? 0 : Convert.ToInt32(reader.GetDecimal(4)), // NUMBER(12) in Oracle often maps to Decimal in .NET
                                    FECHA = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                    BENEFICIARIO = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    COMENTARIO = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    MONTO = reader.IsDBNull(8) ? (decimal?)null : reader.GetDecimal(8),
                                    ESTADO = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    CONCILIADO = reader.IsDBNull(10) ? null : reader.GetString(10),
                                    MES = reader.IsDBNull(11) ? (int?)null : Convert.ToInt32(reader.GetDecimal(11)),
                                    ANO = reader.IsDBNull(12) ? (int?)null : Convert.ToInt32(reader.GetDecimal(12)),
                                    IND_CON = reader.IsDBNull(13) ? null : reader.GetString(13),
                                    FECHA_CONC = reader.IsDBNull(14) ? (DateTime?)null : reader.GetDateTime(14),
                                    FECHA_ANULADO = reader.IsDBNull(15) ? (DateTime?)null : reader.GetDateTime(15),
                                    IND_BORRADO = reader.IsDBNull(16) ? null : reader.GetString(16),
                                    IND_OTROMOV = reader.IsDBNull(17) ? null : reader.GetString(17),
                                    MONEDA_CTA = reader.IsDBNull(18) ? null : reader.GetString(18),
                                    TIPO_CAMBIO = reader.IsDBNull(19) ? (decimal?)null : reader.GetDecimal(19),
                                    TIPO_AJUSTE = reader.IsDBNull(20) ? null : reader.GetString(20),
                                    IND_DIST = reader.IsDBNull(21) ? null : reader.GetString(21),
                                    T_CAMB_C_V = reader.IsDBNull(22) ? null : reader.GetString(22),
                                    IND_OTROS_MESES = reader.IsDBNull(23) ? null : reader.GetString(23),
                                    DEP_CAJA = reader.IsDBNull(24) ? null : reader.GetString(24),
                                    NO_PROVE = reader.IsDBNull(25) ? null : reader.GetString(25),
                                    EPPTO_ID = reader.IsDBNull(26) ? (long?)null : reader.GetInt64(26),
                                    COD_IR = reader.IsDBNull(27) ? null : reader.GetString(27),
                                    RETENCION_IR = reader.IsDBNull(28) ? (decimal?)null : reader.GetDecimal(28),
                                    SEQ_IR = reader.IsDBNull(29) ? null : reader.GetString(29),
                                    COD_IM = reader.IsDBNull(30) ? null : reader.GetString(30),
                                    RETENCION_IM = reader.IsDBNull(31) ? (decimal?)null : reader.GetDecimal(31),
                                    SEQ_IM = reader.IsDBNull(32) ? null : reader.GetString(32),
                                    SUBTOTAL = reader.IsDBNull(33) ? (decimal?)null : reader.GetDecimal(33),
                                    MTO_BASE_RET_IR = reader.IsDBNull(34) ? (decimal?)null : reader.GetDecimal(34),
                                    MTO_BASE_RET_IM = reader.IsDBNull(35) ? (decimal?)null : reader.GetDecimal(35)

                                    //NO_CIA = reader["NO_CIA"]?.ToString(),
                                    //NO_CTA = reader["NO_CTA"]?.ToString(),
                                    //PROCEDENCIA = reader["PROCEDENCIA"]?.ToString(),
                                    //TIPO_DOC = reader["TIPO_DOC"]?.ToString(),
                                    //NO_DOCU = reader["NO_DOCU"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NO_DOCU"]),
                                    //FECHA = reader["FECHA"] == DBNull.Value ? null : (DateTime?)reader["FECHA"],
                                    //BENEFICIARIO = reader["BENEFICIARIO"]?.ToString(),
                                    //COMENTARIO = reader["COMENTARIO"]?.ToString(),
                                    //MONTO = reader["MONTO"] == DBNull.Value ? null : (decimal?)reader["MONTO"],
                                    //ESTADO = reader["ESTADO"]?.ToString(),
                                    //CONCILIADO = reader["CONCILIADO"]?.ToString(),
                                    //MES = reader["MES"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["MES"]),
                                    //ANO = reader["ANO"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["ANO"]),
                                    //IND_CON = reader["IND_CON"]?.ToString(),
                                    //FECHA_CONC = reader["FECHA_CONC"] == DBNull.Value ? null : (DateTime?)reader["FECHA_CONC"],
                                    //FECHA_ANULADO = reader["FECHA_ANULADO"] == DBNull.Value ? null : (DateTime?)reader["FECHA_ANULADO"],
                                    //IND_BORRADO = reader["IND_BORRADO"]?.ToString(),
                                    //IND_OTROMOV = reader["IND_OTROMOV"]?.ToString(),
                                    //MONEDA_CTA = reader["MONEDA_CTA"]?.ToString(),
                                    //TIPO_CAMBIO = reader["TIPO_CAMBIO"] == DBNull.Value ? null : (decimal?)reader["TIPO_CAMBIO"],
                                    //TIPO_AJUSTE = reader["TIPO_AJUSTE"]?.ToString(),
                                    //IND_DIST = reader["IND_DIST"]?.ToString(),
                                    //T_CAMB_C_V = reader["T_CAMB_C_V"]?.ToString(),
                                    //IND_OTROS_MESES = reader["IND_OTROS_MESES"]?.ToString(),
                                    //DEP_CAJA = reader["DEP_CAJA"]?.ToString(),
                                    //NO_PROVE = reader["NO_PROVE"]?.ToString(),
                                    //EPPTO_ID = reader["EPPTO_ID"] == DBNull.Value ? null : (long?)reader["EPPTO_ID"],
                                    //COD_IR = reader["COD_IR"]?.ToString(),
                                    //RETENCION_IR = reader["RETENCION_IR"] == DBNull.Value ? null : (decimal?)reader["RETENCION_IR"],
                                    //SEQ_IR = reader["SEQ_IR"]?.ToString(),
                                    //COD_IM = reader["COD_IM"]?.ToString(),
                                    //RETENCION_IM = reader["RETENCION_IM"] == DBNull.Value ? null : (decimal?)reader["RETENCION_IM"],
                                    //SEQ_IM = reader["SEQ_IM"]?.ToString(),
                                    //SUBTOTAL = reader["SUBTOTAL"] == DBNull.Value ? null : (decimal?)reader["SUBTOTAL"],
                                    //MTO_BASE_RET_IR = reader["MTO_BASE_RET_IR"] == DBNull.Value ? null : (decimal?)reader["MTO_BASE_RET_IR"],
                                    //MTO_BASE_RET_IM = reader["MTO_BASE_RET_IM"] == DBNull.Value ? null : (decimal?)reader["MTO_BASE_RET_IM"]
                                };

                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetAllArckmmAsync: " + ex.Message);
                return new List<ARCKMM>(); // Return empty on failure
            }

            return result;
        }

        public static async Task<List<ARCKCL>> GetAllArckcl()
        {
            var result = new List<ARCKCL>();

            try
            {
                using (OracleConnection conn = new OracleConnection(Connexion24(2025)))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;

                        string query = @"
                    SELECT
                        NO_CIA,
                        NO_CTA,
                        CC_1,
                        CC_2,
                        CC_3,
                        TIPO_DOCU,
                        CHEQUE,
                        COD_CONT,
                        TIPO_MOV,
                        MONTO,
                        MONTO_DOL,
                        MONEDA,
                        NO_SECUENCIA,
                        NO_ASIENTO,
                        TIPO_CAMBIO,
                        PRG_COD,
                        PRY_COD,
                        OBRACT_COD,
                        TAR_COD,
                        FF_COD,
                        OF_COD,
                        NO_LINEA,
                        EGR_COD
                    FROM ARCKCL";

                        cmd.CommandText = query;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new ARCKCL
                                {
                                    NO_CIA = reader.IsDBNull(0) ? "N/A" : reader.GetString(0),
                                    NO_CTA = reader.IsDBNull(1) ? "N/A" : reader.GetString(1),
                                    CC_1 = reader.IsDBNull(2) ? "N/A" : reader.GetString(2),
                                    CC_2 = reader.IsDBNull(3) ? "N/A" : reader.GetString(3),
                                    CC_3 = reader.IsDBNull(4) ? "N/A" : reader.GetString(4),
                                    TIPO_DOCU = reader.IsDBNull(5) ? "N/A" : reader.GetString(5),
                                    CHEQUE = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                                    COD_CONT = reader.IsDBNull(7) ? "N/A" : reader.GetString(7),
                                    TIPO_MOV = reader.IsDBNull(8) ? "N/A" : reader.GetString(8),
                                    MONTO = reader.IsDBNull(9) ? (decimal?)null : reader.GetDecimal(9),
                                    MONTO_DOL = reader.IsDBNull(10) ? (decimal?)null : reader.GetDecimal(10),
                                    MONEDA = reader.IsDBNull(11) ? "N/A" : reader.GetString(11),
                                    NO_SECUENCIA = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                                    NO_ASIENTO = reader.IsDBNull(13) ? "N/A" : reader.GetString(13),
                                    TIPO_CAMBIO = reader.IsDBNull(14) ? (decimal?)null : reader.GetDecimal(14),
                                    PRG_COD = reader.IsDBNull(15) ? "N/A" : reader.GetString(15),
                                    PRY_COD = reader.IsDBNull(16) ? "N/A" : reader.GetString(16),
                                    OBRACT_COD = reader.IsDBNull(17) ? "N/A" : reader.GetString(17),
                                    TAR_COD = reader.IsDBNull(18) ? "N/A" : reader.GetString(18),
                                    FF_COD = reader.IsDBNull(19) ? "N/A" : reader.GetString(19),
                                    OF_COD = reader.IsDBNull(20) ? "N/A" : reader.GetString(20),
                                    NO_LINEA = reader.IsDBNull(21) ? 0 : reader.GetInt32(21),
                                    EGR_COD = reader.IsDBNull(22) ? "N/A" : reader.GetString(22)
                                    //NO_CIA = reader.IsDBNull("NO_CIA") ? "N/A" : reader.GetString("NO_CIA"),
                                    //NO_CTA = reader.IsDBNull("NO_CTA") ? "N/A" : reader.GetString("NO_CTA"),
                                    //CC_1 = reader.IsDBNull("CC_1") ? "N/A" : reader.GetString("CC_1"),
                                    //CC_2 = reader.IsDBNull("CC_2") ? "N/A" : reader.GetString("CC_2"),
                                    //CC_3 = reader.IsDBNull("CC_3") ? "N/A" : reader.GetString("CC_3"),
                                    //TIPO_DOCU = reader.IsDBNull("TIPO_DOCU") ? "N/A" : reader.GetString("TIPO_DOCU"),
                                    //CHEQUE = reader.IsDBNull("CHEQUE") ? 0 : reader.GetInt32("CHEQUE"),
                                    //COD_CONT = reader.IsDBNull("COD_CONT") ? "N/A" : reader.GetString("COD_CONT"),
                                    //TIPO_MOV = reader.IsDBNull("TIPO_MOV") ? "N/A" : reader.GetString("TIPO_MOV"),
                                    //MONTO = reader.IsDBNull("MONTO") ? (decimal?)null : reader.GetDecimal("MONTO"),
                                    //MONTO_DOL = reader.IsDBNull("MONTO_DOL") ? (decimal?)null : reader.GetDecimal("MONTO_DOL"),
                                    //MONEDA = reader.IsDBNull("MONEDA") ? "N/A" : reader.GetString("MONEDA"),
                                    //NO_SECUENCIA = reader.IsDBNull("NO_SECUENCIA") ? 0 : reader.GetInt32("NO_SECUENCIA"),
                                    //NO_ASIENTO = reader.IsDBNull("NO_ASIENTO") ? "N/A" : reader.GetString("NO_ASIENTO"),
                                    //TIPO_CAMBIO = reader.IsDBNull("TIPO_CAMBIO") ? (decimal?)null : reader.GetDecimal("TIPO_CAMBIO"),
                                    //PRG_COD = reader.IsDBNull("PRG_COD") ? "N/A" : reader.GetString("PRG_COD"),
                                    //PRY_COD = reader.IsDBNull("PRY_COD") ? "N/A" : reader.GetString("PRY_COD"),
                                    //OBRACT_COD = reader.IsDBNull("OBRACT_COD") ? "N/A" : reader.GetString("OBRACT_COD"),
                                    //TAR_COD = reader.IsDBNull("TAR_COD") ? "N/A" : reader.GetString("TAR_COD"),
                                    //FF_COD = reader.IsDBNull("FF_COD") ? "N/A" : reader.GetString("FF_COD"),
                                    //OF_COD = reader.IsDBNull("OF_COD") ? "N/A" : reader.GetString("OF_COD"),
                                    //NO_LINEA = reader.IsDBNull("NO_LINEA") ? 0 : reader.GetInt32("NO_LINEA"),
                                    //EGR_COD = reader.IsDBNull("EGR_COD") ? "N/A" : reader.GetString("EGR_COD")
                                };

                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                return new List<ARCKCL>();
            }

            return result;
        }
        public static async Task<List<ARCKDIGV>> GetAllArckdigv()
        {
            var result = new List<ARCKDIGV>();

            try
            {
                using (OracleConnection conn = new OracleConnection(Connexion24(2025)))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;

                        string query = @"
                    SELECT
                        NO_CIA,
                        NO_CTA,
                        TIPO_DOCU,
                        NO_SECUENCIA,
                        TIPO_REFE,
                        NO_REFE,
                        ACREEDOR,
                        COM_1,
                        MONTO,
                        NO_RUC,
                        TASA_IGV,
                        CONSIGNANTE,
                        POLIZA
                    FROM ARCKDIGV";

                        cmd.CommandText = query;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new ARCKDIGV
                                {
                                    NO_CIA = reader.IsDBNull(0) ? "N/A" : reader.GetString(0),
                                    NO_CTA = reader.IsDBNull(1) ? "N/A" : reader.GetString(1),
                                    TIPO_DOCU = reader.IsDBNull(2) ? "N/A" : reader.GetString(2),
                                    NO_SECUENCIA = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    TIPO_REFE = reader.IsDBNull(4) ? "N/A" : reader.GetString(4),
                                    NO_REFE = reader.IsDBNull(5) ? "N/A" : reader.GetString(5),
                                    ACREEDOR = reader.IsDBNull(6) ? "N/A" : reader.GetString(6),
                                    COM_1 = reader.IsDBNull(7) ? "N/A" : reader.GetString(7),
                                    MONTO = reader.IsDBNull(8) ? 0m : reader.GetDecimal(8),
                                    NO_RUC = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    TASA_IGV = reader.IsDBNull(10) ? (decimal?)null : reader.GetDecimal(10),
                                    CONSIGNANTE = reader.IsDBNull(11) ? null : reader.GetString(11),
                                    POLIZA = reader.IsDBNull(12) ? null : reader.GetString(12)
                                };

                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                return new List<ARCKDIGV>();
            }

            return result;
        }

        //
        // SOLICITUD PRESUPUESTARIA
        //
        public static async Task<List<FP_EPPTO>> GetAllFPEppToAsync()
        {
            var result = new List<FP_EPPTO>();

            try
            {
                using (OracleConnection conn = new OracleConnection(Connexion24(2025))) // Replace with your actual connection function
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                    SELECT
                        MC_COD,
                        ANO,
                        EPPTO_ID,
                        FECHA,
                        ESTADO,
                        CONCEPTO,
                        MONTO,
                        SOLICITADO_POR,
                        NO_CTA,
                        CHEQUE,
                        CJA_CHICA
                    FROM FP_EPPTO";

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new FP_EPPTO
                                {
                                    MC_COD = reader.IsDBNull(0) ? "N/A" : reader.GetString(0),
                                    ANO = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                    EPPTO_ID = reader.IsDBNull(2) ? 0 : reader.GetInt64(2),
                                    FECHA = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                                    ESTADO = reader.IsDBNull(4) ? "N/A" : reader.GetString(4),
                                    CONCEPTO = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    MONTO = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
                                    SOLICITADO_POR = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    NO_CTA = reader.IsDBNull(8) ? null : reader.GetString(8),
                                    CHEQUE = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9),
                                    CJA_CHICA = reader.IsDBNull(10) ? "N" : reader.GetString(10)
                                };

                                result.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                // Optionally log more detailed error or rethrow
            }

            return result;
        }

        public static async Task<bool> UpdateEstadoToProcesoAsync(string mcCod, int ano, long epptoId)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(Connexion24(2025)))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                                            UPDATE FP_EPPTO
                                            SET ESTADO = 'P'
                                            WHERE ANO = :ano AND EPPTO_ID = :epptoId"; 
                        cmd.Parameters.Add(new OracleParameter("ano", ano));
                        cmd.Parameters.Add(new OracleParameter("epptoId", epptoId));

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                return false;
            }
        }


        //
        // DELECTION
        //
        public static async Task<int> DeleteArckmmByNoDocuAsync(int noDocu)
        {
            try
            {
                using (var conn = new OracleConnection(Connexion24(2025))) {  
                await conn.OpenAsync();

                string sql = "DELETE FROM ARCKMM WHERE NO_DOCU = :noDocu";

                    using (var cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("noDocu", noDocu));

                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR DeleteArckmmByNoDocuAsync] {ex.Message}");
                return -1;
            }
        }

        public static async Task<int> DeleteArckclByChequeAsync(int cheque)
        {
            try
            {
                using (var conn = new OracleConnection(Connexion24(2025)))
                {
                    await conn.OpenAsync();

                    string sql = "DELETE FROM ARCKCL WHERE CHEQUE = :cheque";

                    using (var cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("cheque", cheque));
                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR DeleteArckclByChequeAsync] {ex.Message}");
                return -1;
            }
        }
         
        public static async Task<int> DeleteArckceByNoDocuAsync(int noDocu)
        {
            try
            {
                using (var conn = new OracleConnection(Connexion24(2025)))
                {
                    await conn.OpenAsync();

                    string sql = "DELETE FROM ARCKCE WHERE NO_DOCU = :noDocu";

                    using (var cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("noDocu", noDocu));

                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR DeleteArckceByNoDocuAsync] {ex.Message}");
                return -1;
            }
        }


        #endregion

        #region Ingresos
        public static List<Ingresos> Ingresos(int yr)
        {
            var lin =  new List<Ingresos>();
            try
            {
                using (OracleConnection conn = new OracleConnection(Connexion24(yr)))
                {
                    conn.Open();

                    using (OracleCommand cm = new OracleCommand())
                    {
                        cm.CommandText = @"SELECT 
                                        A.periodo, 
                                        A.no_docu, 
                                        A.fecharecibo as fr,
                                        A.nbr_cobrador, 
                                        A.concepto,    
                                        D.ing_cod,
                                        t.ing_dsc,A.total FROM arccca A
                                        INNER JOIN  arccdc D ON A.No_Docu = D.NO_DOCU
                                        RIGHT JOIN  fp_ing t ON t.ing_cod = D.ing_cod
                                        WHERE D.ing_cod IS NOT NULL AND d.fecha BETWEEN TO_DATE(:d1, 'dd-mm-yyyy') AND TO_DATE(:d2, 'dd-mm-yyyy')";
                        string d1 = "01-01-2024";
                        string d2 = "21-02-2024";
                        //cm.Parameters.Add(new OracleParameter("d1", OracleDbType.Date) { Value = d1 });
                        //cm.Parameters.Add(new OracleParameter("d2", OracleDbType.Date) { Value = d2 });

                        cm.Parameters.Add(new OracleParameter("d1", d1));
                        cm.Parameters.Add(new OracleParameter("d2", d2));

                        cm.CommandType = CommandType.Text;
                        //cm.CommandTimeout = 600;
                        cm.Connection = conn;
                        var r = cm.ExecuteReader();

                        while (r.Read())
                        {
                            DateTime? fechaRecibidoNullable = r.IsDBNull(r.GetOrdinal("fr")) ? (DateTime?)null : r.GetDateTime(r.GetOrdinal("fr"));

                            // If fechaRecibidoNullable is null, we set it to DateTime.MinValue, otherwise use the value.
                            DateTime fechaRecibido = fechaRecibidoNullable.GetValueOrDefault(DateTime.MinValue);

                            lin.Add(new Ingresos()
                            {
                                Periodo = r.GetString(0),
                                no_doc = r.GetString(1),
                                fecharecibido = fechaRecibido,  // Non-nullable DateTime
                                cobrador = r.GetString(3),
                                concepto = r.GetString(4),
                                cod_ing = r.GetString(5),
                                DescripcionCodigo = r.GetString(6),
                                monto = r.GetDecimal(7),
                            }); 
                        }
                        return lin;
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<Ingresos>() ;
            }
        }
        
        public static List<Ingresos> Ingresosxf(DateTime d1, DateTime d2,int yr)
        { var lin =  new List<Ingresos>();
            try
            {
                using (OracleConnection conn = new OracleConnection(Connexion24(yr)))
                {
                    conn.Open(); 
                    using (OracleCommand cm = new OracleCommand())
                    {
                        cm.CommandText = @"SELECT 
                                        A.periodo, 
                                        A.no_docu, 
                                        A.fecharecibo as fr,
                                        A.nbr_cobrador, 
                                        A.concepto,    
                                        D.ing_cod,
                                        t.ing_dsc,A.Total FROM arccca A
                                        INNER JOIN  arccdc D ON A.No_Docu = D.NO_DOCU
                                        RIGHT JOIN  fp_ing t ON t.ing_cod = D.ing_cod
                                        WHERE D.ing_cod IS NOT NULL AND d.fecha BETWEEN TO_DATE(:d1, 'dd-mm-yyyy') AND TO_DATE(:d2, 'dd-mm-yyyy')";
                        // Format d1 and d2 as 'YYYY-MM-DD' (no time)
                        cm.Parameters.Add(new OracleParameter("d1",d1.Date.ToString("dd-MM-yyyy") ));
                        cm.Parameters.Add(new OracleParameter("d2",d2.Date.ToString("dd-MM-yyyy") ));

                        cm.CommandType = CommandType.Text;
                        //cm.CommandTimeout = 600;
                        cm.Connection = conn;
                        var r = cm.ExecuteReader();

                        while (r.Read())
                        {
                            DateTime? fechaRecibidoNullable = r.IsDBNull(r.GetOrdinal("fr")) ? (DateTime?)null : r.GetDateTime(r.GetOrdinal("fr"));

                            // If fechaRecibidoNullable is null, we set it to DateTime.MinValue, otherwise use the value.
                            DateTime fechaRecibido = fechaRecibidoNullable.GetValueOrDefault(DateTime.MinValue);

                            lin.Add(new Ingresos()
                            {
                                Periodo = r.GetString(0),
                                no_doc = r.GetString(1),
                                fecharecibido = fechaRecibido,  // Non-nullable DateTime
                                cobrador = r.GetString(3),
                                concepto = r.GetString(4),
                                cod_ing = r.GetString(5),
                                DescripcionCodigo = r.GetString(6),
                                monto = r.GetDecimal(7),
                            });
                        }
                        return lin;
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<Ingresos>();
            }
        }
        
        #endregion

        #region SolicitudPres
        public static int EnableSPres(int id,int yr)
        {
            using (OracleConnection conn = new OracleConnection(Connexion24(yr)))
            {
                conn.Open();
                using(OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = conn;
                    cm.CommandType = CommandType.Text;
                    cm.CommandTimeout = 600;

                    string qry = $"UPDATE SET ESTADO=P WHERE EPTO={id}";  
                    if (cm.ExecuteNonQuery() == 1)
                        return id; 
                    else return -1;
                }
            }
        }
        public static int DisableSPres(int id, int yr)
        {
            using (OracleConnection conn = new OracleConnection(Connexion24(yr)))
            {
                conn.Open();
                using(OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = conn;
                    cm.CommandType = CommandType.Text;
                    cm.CommandTimeout = 600;

                    string qry = $"UPDATE SET ESTADO=A WHERE EPTO={id}";  
                    if (cm.ExecuteNonQuery() == 1)
                        return id; 
                    else return -1;
                }
            }
        }
        public static List<SolicitudPresupuestaria> Soli_Pres(int year)
        { 
            List<SolicitudPresupuestaria> pl = new List<SolicitudPresupuestaria> ();
            try
            {
                using (OracleConnection con = new OracleConnection(Connexion24(year)))
                {
                    con.Open();
                    using (OracleCommand cm = new OracleCommand())
                    {
                        cm.Connection = con;
                        cm.CommandType = CommandType.Text;
                        cm.CommandTimeout = 600;
                        cm.CommandText = "select EPPTO_ID,ano,fecha,solicitado_por,concepto,estado from fp_eppto";

                        var r = cm.ExecuteReader();
                        while (r.Read()) {
                            DateTime? fechaRecibidoNullable = r.IsDBNull(r.GetOrdinal("fecha")) ? (DateTime?)null : r.GetDateTime(r.GetOrdinal("fecha"));

                            // If fechaRecibidoNullable is null, we set it to DateTime.MinValue, otherwise use the value.
                            DateTime fechaRecibido = fechaRecibidoNullable.GetValueOrDefault(DateTime.MinValue);
                            pl.Add(new SolicitudPresupuestaria() { id = r.GetInt32(0), year = r.GetString(1) , solicitado = r.GetString(3), concepto = r.GetString(4), estado = r.GetString(5)});
                        
                        }
                        return pl;
                    }
                }
            }
            catch (Exception ex) { return new List<SolicitudPresupuestaria>(); } 
        }
        #endregion
    }
}