using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TWebApiSearch.App_Start;
using TWebApiSearch.BLL;
using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers
{
    [Startup.NoCacheAttribute]
    public class HomeController : Controller
    {
        List<SearchPanelInformation> _lstSearchPanelInformation = new List<SearchPanelInformation>();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
               return RedirectToAction("LogIn", "LogIn");
            }
            
        }
        public JsonResult GetLearnInformation()
        {
            DataTable dt = Models.DAL.GetDataTable(SearchStoreProcedureName.GetLearnInformation, DBName.SearchET);
            _lstSearchPanelInformation = new List<SearchPanelInformation>();
            if (dt.Rows.Count > 0)
            {
                _lstSearchPanelInformation = dt.DataTableToList<SearchPanelInformation>();
            }
            return new JsonResult { Data = _lstSearchPanelInformation };
        }
        

        public JsonResult GetLoginViewList()
        {
            DataTable dt = Models.DAL.GetDataTable(SearchStoreProcedureName.GetLoginViewList, DBName.SearchET);

            UserView userView = new UserView();
            if (dt.Rows.Count>0)
            {
                userView.NumberofListedUser = dt.Rows[0]["NumberofListedUser"].ToString();
                userView.NumberofView = dt.Rows[0]["NumberofView"].ToString();
                userView.CurrentlyViewing = dt.Rows[0]["CurrentlyViewing"].ToString();
                userView.TodaysView = dt.Rows[0]["TodaysView"].ToString();
            }
            return new JsonResult { Data = userView };
        }
        public JsonResult MascoSearch()
        {

            var aData = Models.DAL.GetDataTable(HRStoredProcedureName.MascoSearch_Sp_SearchText, DBName.ControlPanal);

            var arr = new ArrayList();

            foreach (DataRow item in aData.Rows)
            {
                var obj = new
                {
                    value = item["SearchText"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }

        public ActionResult LogOut()
        {
            WebSearchLogInBLL webSearchLogInBll=new WebSearchLogInBLL();
            int MULRID = 0;
            if (!string.IsNullOrEmpty(Session["MULRID"].ToString()))
            {
                MULRID = Convert.ToInt32(Session["MULRID"].ToString());
                webSearchLogInBll.ModuleUserLogout(MULRID);
            }

            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("LogIn", "LogIn");
        }

        
    }
}
