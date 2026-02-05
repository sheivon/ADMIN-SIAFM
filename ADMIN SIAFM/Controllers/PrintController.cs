using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN_SIAFM.Controllers
{
    public class PrintController : Controller
    {
        private string GetTitle() { return "ALCALDIA MUNICIAPAL DE KUKRA HILL"; }
        private string GetSubTitle() { return "ALCALDIA MUNICIAPAL DE KUKRA HILL"; }
        // GET: Print
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrintCategorias()
        { // Create an instance of the LocalReport class and specify the report path
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports/rdlc"), "rptCategorias.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Error");
            }
            // Set the parameter for the report
            ReportParameter rp = new ReportParameter("rptTitle", GetTitle());
            ReportParameter srp = new ReportParameter("rptSubTitle", GetSubTitle());
            lr.SetParameters(new ReportParameter[] { rp, srp });

            // Supply data for the report
            DataTable dt =  new DataTable(); // return a DataTable with the required data
            ReportDataSource rd = new ReportDataSource("DSCategorias", dt); // DataSet1 is the name of the DataSet in your RDLC report
            lr.DataSources.Add(rd);

            // Render the report to a byte array
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.08in</MarginTop>" +
            "  <MarginLeft>0.08in</MarginLeft>" +
            "  <MarginRight>0.08in</MarginRight>" +
            "  <MarginBottom>0.08in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            // Return the rendered report as a FileResult
            return File(renderedBytes, mimeType);
        }
        [HttpPost]
        public ActionResult PrintCategoriastoExcel()
        {
            // Assuming you have a method to get data from your DataTable
            DataTable dt = /*Modules.GetrptCategorias()*/ new DataTable();

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Categorias");

                // Load DataTable into worksheet
                worksheet.Cells["B1"].LoadFromDataTable(dt, true);

                // Format the header for columns
                using (ExcelRange rng = worksheet.Cells["B1:F1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));
                    rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    rng.Style.ShrinkToFit = true;
                    // Set font size
                    rng.Style.Font.Size = 12; // Adjust the font size as needed

                    // Enable text wrapping
                    rng.Style.WrapText = true;

                    // Optionally, adjust the row height to fit the content
                    worksheet.Cells["B1:F1"].AutoFitColumns();
                    //worksheet.Cells["B1:J1"].AutoFitRows(); 
                }

                // Convert the ExcelPackage to a byte array
                byte[] fileContents = package.GetAsByteArray();

                // Return the Excel file as a FileResult
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Categorias.xlsx");
            }
        }


        #region Recibos
        public ActionResult PrintRecibos()
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports/rdlc"), "rptCategorias.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Error");
            }
            // Set the parameter for the report
            ReportParameter rp = new ReportParameter("rptTitle", GetTitle());
            ReportParameter srp = new ReportParameter("rptSubTitle", GetSubTitle());
            lr.SetParameters(new ReportParameter[] { rp, srp });

            // Supply data for the report
            DataTable dt = new DataTable(); // return a DataTable with the required data
            ReportDataSource rd = new ReportDataSource("DSCategorias", dt); // DataSet1 is the name of the DataSet in your RDLC report
            lr.DataSources.Add(rd);

            // Render the report to a byte array
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.08in</MarginTop>" +
            "  <MarginLeft>0.08in</MarginLeft>" +
            "  <MarginRight>0.08in</MarginRight>" +
            "  <MarginBottom>0.08in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            // Return the rendered report as a FileResult
            return File(renderedBytes, mimeType);
        }
        #endregion
    }
}