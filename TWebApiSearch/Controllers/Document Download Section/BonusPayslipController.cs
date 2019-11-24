using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TWebApiSearch.BLL.Document_Download_Section;
using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers.Document_Download_Section
{
    [Startup.NoCacheAttribute]
    public class BonusPayslipController : Controller
    {
        // GET: BonusPayslip
        private BonusPayslipBLL _objBLL = new BonusPayslipBLL();
        private DocumentDownloadSection _model = new DocumentDownloadSection();
        private List<ComboData> lComboData = new List<ComboData>();
        public ActionResult BonusPayslip()
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
        public JsonResult GetFinalCialYear()
        {
            _objBLL = new BonusPayslipBLL();
            if (Session["UserName"] != null)
            {
                lComboData = _objBLL.LoadFinYearDropDown();
            }
            return Json(lComboData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMonth(string FinYear)
        {
            _objBLL = new BonusPayslipBLL();
            if (Session["UserName"] != null)
            {
                lComboData = _objBLL.LoadMonthDropDown(FinYear,Convert.ToString(Session["EMP_ID"]));
            }
            return Json(lComboData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendMail(DocumentDownloadSection data)
        {
            string error = string.Empty;
            try
            {
                error = _objBLL.GetDataForReport(data, Session["EMP_CODE"].ToString());
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return Json(error, JsonRequestBehavior.AllowGet);
        }
    }
}