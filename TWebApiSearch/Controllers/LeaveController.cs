using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TWebApiSearch.App_Start;
using TWebApiSearch.BLL;
using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers
{
    [Startup.NoCacheAttribute]
    public class LeaveController : Controller
    {
        // GET: Leave
        [AuthorizeActionFilter]
        public ActionResult LeaveApplication()
        {           
            return View();
        }

        //public JsonResult LeaveApplication(string ID)
        //{


        //    var ddd = DAL.storedProc2(HRStoredProcedureName.usp_rpt_LeaveApplicationForm, param, DBName.HR);
        //    return new JsonResult
        //    {
        //        Data = ID
        //    };

        //}

        public ActionResult LeaveReport()
        {
            var User = (User)Session["UserName"];
            //return View();
            ReportClass rpt = new ReportClass();
            
            rpt.FileName = Server.MapPath("~/WebReport/Reports/rptLeaveApplicationFrom.rpt");
            rpt.Load();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", User.EmpCode);
            DataTable DT = Models.DAL.storedProc2(HRStoredProcedureName.usp_rpt_LeaveApplicationForm, param, DBName.HR);

            //string path = @"~\\WebReport\\DataSets\\dsLeaveApplicationFrom.xsd";
            //DT.WriteXmlSchema(Server.MapPath(path));
            rpt.SetDataSource(DT);
            Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //CLOSE REPORT OBJECT
            rpt.Close();
            rpt.Dispose();
            return File(stream, "application/pdf");
        }
        public ActionResult HourlyLeave()
        {
            var User = (User)Session["UserName"];
            //return View();
            ReportClass rpt = new ReportClass();

            rpt.FileName = Server.MapPath("~/WebReport/Reports/rptHourlyLeave.rpt");
            rpt.Load();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", User.EmpCode);
            DataTable DT = Models.DAL.storedProc2(HRStoredProcedureName.usp_rpt_LeaveApplicationForm, param, DBName.HR);

            //string path = @"~\\WebReport\\DataSets\\dsLeaveApplicationFrom.xsd";
            //DT.WriteXmlSchema(Server.MapPath(path));
            rpt.SetDataSource(DT);
            Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //CLOSE REPORT OBJECT
            rpt.Close();
            rpt.Dispose();
            return File(stream, "application/pdf");
        }
       
        public ActionResult OutStationDuty()
        {
            var User = (User)Session["UserName"];
            //return View();
            ReportClass rpt = new ReportClass();

            rpt.FileName = Server.MapPath("~/WebReport/Reports/rptOutStationDuty.rpt");
            rpt.Load();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", User.EmpCode);
            DataTable DT = Models.DAL.storedProc2(HRStoredProcedureName.usp_rpt_LeaveApplicationForm, param, DBName.HR);

            //string path = @"~\\WebReport\\DataSets\\dsLeaveApplicationFrom.xsd";
            //DT.WriteXmlSchema(Server.MapPath(path));
            rpt.SetDataSource(DT);
            Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //CLOSE REPORT OBJECT
            rpt.Close();
            rpt.Dispose();
            return File(stream, "application/pdf");
        }
        public ActionResult SpouseInfo(string ID)
        {
            var User = (User)Session["UserName"];
            //return View();
            ReportClass rpt = new ReportClass();

            rpt.FileName = Server.MapPath("~/WebReport/Reports/rptSpouseInformation.rpt");
            rpt.Load();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable DT = Models.DAL.storedProc2(HRStoredProcedureName.usp_rpt_SpouseInfo, param, DBName.HR);

            //string path = @"~\\WebReport\\DataSet0\\dsSpouseInformation.xsd";
            //DT.WriteXmlSchema(Server.MapPath(path));
            rpt.SetDataSource(DT);
            Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //CLOSE REPORT OBJECT
            rpt.Close();
            rpt.Dispose();
            return File(stream, "application/pdf");
        }



    }
}
