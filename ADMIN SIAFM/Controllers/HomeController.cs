using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

using ADMIN_SIAFM.Models;

namespace ADMIN_SIAFM.Controllers
{
    public class HomeController : Controller
    {
        #region Login
        public ActionResult Login() { return View(); }
        [HttpPost]
        public JsonResult Login(string username,string password) 
        { int uid = 0;
            int rol = 0;
            string un = string.Empty; //retunring username from db
            if( DBControl.Login(username, password, out uid, out rol, out un) == false)
            {
                return null;
            }
            return Json(new { username,password }); 
        }
        public ActionResult Logout() { return View(); }
        #endregion


        public ActionResult Index()
        {
            return View();
        } 

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Usuarios
        public ActionResult Usuarios()
        {
            return View();
        }

        public JsonResult GetUsuarios(int year)
        {
            var lusr =  DBControl.GetUsuarios(year);

            return Json( new { data = lusr }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Roles
        public ActionResult Roles()
        {
            return View();
        }
        public JsonResult GetRoles(int year) { 
            var lrl = DBControl.GetRoles(year);
            return Json( new { data = lrl }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveRole( Roles rl)
        {
            var ret = DBControl.SaveRole(rl);
            return Json( new { data = ret }, JsonRequestBehavior.AllowGet );
        }
        #endregion

        #region Checkes
        public ActionResult Checkes(){return View();}
        public JsonResult CKce(int year)
        {
            var lck = DBControl.GetCK(year);
            return Json(new { data = lck}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteCK(int CK)
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SolicitudPresupuestaria() { return View(); }
        public JsonResult DisableSPres(int id)
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EnableSPres(int id)
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetARCKCE(int CK)
        {
            var CE = await DBControl.GetAllArckce();
            var filteredCE = CE.Where(item => item.CHEQUE == CK).ToList();
            return Json(new { data = filteredCE },JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetARCKCL(int CK)
        {
            var Cl = await DBControl.GetAllArckcl();
            var filteredCl = Cl.Where(item => item.CHEQUE == CK).ToList();
            return Json(new { data = filteredCl },JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetARCKMM(int CK)
        {
            var mm = await DBControl.GetAllArckmm();
            var filteredmm = mm.Where(item => item.NO_DOCU == CK).ToList();
            return Json(new { data = filteredmm },JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetARCKDIGV(int CK)
        {
            var digv = await DBControl.GetAllArckdigv();
            var filtereddigv = digv.Where(item => item.NO_SECUENCIA == CK).ToList();
            return Json(new { data = filtereddigv },JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> Soli_Pres(int yr, string sp)
        {
            var list = await DBControl.GetAllFPEppToAsync(); // Use your existing method
            var filtered = list.Where(x =>
                x.ANO == yr &&
                (string.IsNullOrEmpty(sp) || x.EPPTO_ID.ToString().Contains(sp))
            ).Select((x, idx) => new {
                id = idx + 1,
                year = x.ANO,
                solicitadoPor = x.SOLICITADO_POR,
                concepto = x.CONCEPTO,
                fecha = x.FECHA?.ToString("o"), // ISO format
                estado = x.ESTADO
            }).ToList();

            return Json(new { data = filtered }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Eliminar() {  return View(); } // Eliminar CK View
        #endregion

        #region CAJA
        public ActionResult Recibos() { return View(); }
      
        public JsonResult GetIngresos(int year)
        {
            var ing = DBControl.Ingresos(year);
            return Json(new {data = ing}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetIngresoXf(DateTime d1,DateTime d2,int year)
        {
            var ingxf = DBControl.Ingresosxf(d1,d2,year);
            return Json(new { data = ingxf }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Presupuesto
        public ActionResult RevivirSFP() {  return View(); }
        public JsonResult Soli_Pres(int yr)
        {
            var lp = DBControl.Soli_Pres(yr);
            return Json(new { data = lp }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}