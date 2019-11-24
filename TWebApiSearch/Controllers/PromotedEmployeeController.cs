using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers
{
    public class PromotedEmployeeController : Controller
    {
        // GET: PromotedEmployee
        public ActionResult PromotedEmployee()
        {
            return View();
        }

       
        public JsonResult GetFinalCialMonth(int FinancialYear)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@FinancialYearId", FinancialYear);
            var FinalCialMonth = Models.DAL.GetDataTable(HRStoredProcedureName.GetMonthNameFinancialYearWise, dictionary,DBName.MascoSearch);
            var arr = new ArrayList();
            foreach (DataRow item in FinalCialMonth.Rows)
            {
                var obj = new
                {
                    ID = item["MonthVal"].ToString(),
                    Text = item["FinancialMonth"].ToString()
                };
                arr.Add(obj);
            }


            var obj1 = new { Data = arr};

            return new JsonResult { Data = obj1 };
        }

        public JsonResult GetAllUnit()
        {
           
            var AllUnit = Models.DAL.GetDataTable(HRStoredProcedureName.GetAllUnit, DBName.MascoSearch);
            var arr = new ArrayList();
            foreach (DataRow item in AllUnit.Rows)
            {
                var obj = new
                {
                    ID = item["UnitNo"].ToString(),
                    Text = item["UnitEName"].ToString()
                };
                arr.Add(obj);
            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }

        public JsonResult GetPromotedData(int year, int month, int category, int unit,string Tag )
        {
           Dictionary <string, dynamic> dictionary = new Dictionary<string, dynamic>();
          
            dictionary.Add("@yearId", year);
            dictionary.Add("@MonthId", month);
            dictionary.Add("@CategoryId", category);
            dictionary.Add("@unitId", unit);
            dictionary.Add("@Tag", Tag);
            var promotedData = Models.DAL.GetDataTable(HRStoredProcedureName.GetAllPromotedEmployee, dictionary, DBName.MascoSearch);
            //var arr = new ArrayList();
            var PromotedEmpList = new List<PromotedEmpList>();
            //PromotedEmpList = promotedData.DataTableToList<PromotedEmpList>();
            foreach (DataRow item in promotedData.Rows)
            {
               byte[] image = (byte[])item["Photo"];
                PromotedEmpList obj = new PromotedEmpList
                {

                    UnitName = item["UnitName"].ToString(),
                    EmpName = item["EmpName"].ToString(),
                    Department = item["Department"].ToString(),
                    Section = item["Section"].ToString(),
                    PreviousDesignation = item["PreviousDesignation"].ToString(),
                    PromotedDesignation = item["PromotedDesignation"].ToString(),
                    img = Convert.ToBase64String(image)
                };
                PromotedEmpList.Add(obj);
            }
            return Json(new { Data = PromotedEmpList }, JsonRequestBehavior.AllowGet);
           // var obj1 = new { Data = PromotedEmpList };
    
            //return new JsonResult()
            //{
            //    Data = obj1,
            //    //ContentType = contentType,
            //    //ContentEncoding = contentEncoding,
            //    //JsonRequestBehavior = behavior,
            //    MaxJsonLength = Int32.MaxValue
            //};
           // return new JsonResult { Data = obj1 };
        }
    }
   
}