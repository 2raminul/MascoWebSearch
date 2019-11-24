using Newtonsoft.Json;
using System;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using TWebApiSearch.BLL.Document_Download_Section;
using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers.Document_Download_Section
{
    [Startup.NoCacheAttribute]
    public class ShortLeaveFormController : Controller
    {
        // GET: ShortLeaveForm
        private ShortLeaveFormBLL _objBLL = new ShortLeaveFormBLL();
        public ActionResult ShortLeaveForm()
        {
            if (Session["UserName"] != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "Leave";
                obj.Action = "LeaveApplication";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public JsonResult GetInitialData()
        {
            DocumentDownloadSection obj = new DocumentDownloadSection();
            if (Session["UserName"] != null)
            {
                obj = _objBLL.GetData(Session["EMP_ID"].ToString());
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public FileResult OpenFile(string Param, string ReportType)
        {
            try
            {
                DocumentDownloadSection data = JsonConvert.DeserializeObject<DocumentDownloadSection>(Param);
                DataSet ds = _objBLL.SetDataForReport(data);
                var rf = ReportFormat.getReport(ReportType, ds, data);
                return File(rf.stream, rf.mimeType);
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            return null;
        }
        public FileResult DownloadFile(string Param, string ReportType)
        {
            DocumentDownloadSection data = JsonConvert.DeserializeObject<DocumentDownloadSection>(Param);
            DataSet ds = _objBLL.SetDataForReport(data);
            var rf = ReportFormat.getReport(ReportType, ds, data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            return File(rf.stream, "application/pdf", data.OperationName+ DateTime.Now.ToString("dd-MM-yyyy hhmm")+ ".pdf");
        }

    }
}