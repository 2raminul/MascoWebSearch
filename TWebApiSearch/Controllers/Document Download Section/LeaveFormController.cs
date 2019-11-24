using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers.Document_Download_Section
{
    [Startup.NoCacheAttribute]
    public class LeaveFormController : Controller
    {
        // GET: LeaveForm
        public ActionResult LeaveForm()
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
    }
}