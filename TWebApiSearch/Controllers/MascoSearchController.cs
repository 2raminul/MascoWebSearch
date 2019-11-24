using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Web;
using System.Web.Mvc;
using TWebApiSearch.App_Start;
using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers
{
    [Startup.NoCacheAttribute]
    public class MascoSearchController : Controller
    {
        #region Views
        // GET: MascoSearch
        [AuthorizeActionFilter]
        public ActionResult Index()
        {

            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));
               
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult LeaveHistory()
        {

            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult AttendanceHistory()
        {
            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult BonusHistory()
        {
            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult SalaryHistory()
        {
            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult AdvanceHistory()
        {
            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult HolidayHistory()
        {
            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult EmployeesComPortal()
        {
            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult HourlyLeaveDetails()
        {
            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult OutStationDutyDetails()
        {
            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        

        #endregion

        #region Data Manipulation
        public JsonResult GetEmployeeInfo()
        {

            var arr = new ArrayList();
            var userInfo = (User)Session["UserName"];
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@empID", userInfo.EmpCode);

            var empInfo = Models.DAL.storedProc2(HRStoredProcedureName.GetEmployeeInfoByID, dictionary, DBName.HR);
            foreach (DataRow item in empInfo.Rows)
            {
                var obj = new
                {
                    EMP_CODE = item["EMP_CODE"].ToString(),
                    EMP_ENAME = item["EMP_ENAME"].ToString(),
                    DesigEName = item["DesigEName"].ToString(),
                    SectEName = item["SectEName"].ToString(),
                    DOJ = item["DOJ"].ToString(),
                    TotalServiceLength = item["TotalServiceLength"].ToString(),
                    WorkingUnit = item["WorkingUnit"].ToString(),
                    CostingUnit = item["CostingUnit"].ToString()
                };
                arr.Add(obj);
            };



            var EmployeeInfo = arr[0];
            return new JsonResult { Data = EmployeeInfo };
        }

        public JsonResult GetFinalCialYear()
        {

            var FinalCialYear = Models.DAL.GetDataTable(HRStoredProcedureName.sp_FinalYear_Select, DBName.HR);

            var arr = new ArrayList();

            var selectedVal = "";

            foreach (DataRow item in FinalCialYear.Rows)
            {
                var obj = new
                {
                    ID = item["FinalYearNo"].ToString(),
                    Text = item["FinalYearName"].ToString()
                };
                arr.Add(obj);

                var sss = item["IsActive"].ToString();

                if (sss == "True")
                {
                    selectedVal = item["FinalYearNo"].ToString();
                }


            }


            var obj1 = new { Data = arr, SelectedVal = selectedVal };

            return new JsonResult { Data = obj1 };
        }

        public JsonResult GetLeaveDetails(int finalcialYear, string employeCode)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            var userInfo = (User)Session["UserName"];

            dictionary.Add("@FinYear", finalcialYear);
            dictionary.Add("@EMPCODE", userInfo.EmpCode);

            var LeaveHistory = Models.DAL.storedProc2(HRStoredProcedureName.SPGetLeaveHistory, dictionary, DBName.HR);

            var DDDD = JsonConvert.SerializeObject(LeaveHistory);
            return new JsonResult { Data = DDDD };

        }

        public JsonResult GetLeaveAvailHistory(int finalcialYear)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            var userInfo = (User)Session["UserName"];

            dictionary.Add("@empNo", userInfo.EmpCode);
            dictionary.Add("@FinalYearNo", finalcialYear);

            var LeaveAvailHistory = Models.DAL.storedProc2(HRStoredProcedureName.GetLeaveAvailHistory, dictionary, DBName.HR);

            var results = JsonConvert.SerializeObject(LeaveAvailHistory);
            return new JsonResult { Data = results };

        }


        public JsonResult GetAllAttendenceByDate(string fromDate, string DateTo)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            var userInfo = (User)Session["UserName"];

            dictionary.Add("@empNo", userInfo.EmpCode);
            dictionary.Add("@fromDate", fromDate);
            dictionary.Add("@todate", DateTo);

            var AllAttendenceList = Models.DAL.storedProc2(SearchStoreProcedureName.GetAllLeaveCountByEmployeeCode_DateRange1, dictionary, DBName.SearchET);

            var results = JsonConvert.SerializeObject(AllAttendenceList);
            return new JsonResult { Data = results };
        }

        public JsonResult GetAttendanceHistory(string fromDate, string DateTo, string leaveType)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            //string fromdate = "";
            //string Tdate = "";
            //DateTime fromDate1 = DateTime.Parse(fromDate);
            //DateTime todate1 = DateTime.Parse(todate);
            var userInfo = (User)Session["UserName"];
            if (fromDate == "" || DateTo == "")
            {
                dictionary.Add("@fromDate", fromDate);
                dictionary.Add("@todate", DateTo);
                dictionary.Add("@empNo", userInfo.EmpCode);
                dictionary.Add("@FSts", leaveType);
            }
            else
            {
                var dd = fromDate.Split('-');
                var newDateFrom = dd[2] + "-" + dd[1] + "-" + dd[0];
                var dd1 = DateTo.Split('-');
                var newDateTo = dd1[2] + "-" + dd1[1] + "-" + dd1[0];

                dictionary.Add("@fromDate", newDateFrom);
                dictionary.Add("@todate", newDateTo);
                dictionary.Add("@empNo", userInfo.EmpCode);
                dictionary.Add("@FSts", leaveType);
            }

            ArrayList arr = new ArrayList();
            var LeaveAvailHistory = Models.DAL.storedProc2(HRStoredProcedureName.GetAttendanceHistory, dictionary, DBName.HR);
            //PunchDate,ShiftIn,ShiftOut,ShiftLate,FPunchIn,FPunchOut,FSts
            foreach (DataRow item in LeaveAvailHistory.Rows)
            {
                var PunchDate = Convert.ToDateTime(item["PunchDate"]).ToString("dd-MM-yyy");
                var ShiftIn = Convert.ToDateTime(item["ShiftIn"]).ToString("hh:mm:ss");
                var ShiftOut = Convert.ToDateTime(item["ShiftOut"]).ToString("hh:mm:ss");
                var ShiftLate = Convert.ToDateTime(item["ShiftLate"]).ToString("hh:mm:ss");
                var FPunchIn = item["FPunchIn"].ToString() == "" ? "" : Convert.ToDateTime(item["FPunchIn"]).ToString("hh:mm:ss");
                var FPunchOut = item["FPunchOut"].ToString() == "" ? "" : Convert.ToDateTime(item["FPunchOut"]).ToString("hh:mm:ss");
                TimeSpan PunchOut;
                TimeSpan ShiftO;
                TimeSpan additionTime;
                TimeSpan latetime;
                TimeSpan PunchIn;
                TimeSpan ShiftL;
                TimeSpan.TryParse("00:00:00", out additionTime);
                TimeSpan.TryParse("00:00:00", out latetime);
                var ShiftName = item["ShiftName"].ToString();
                string atime = string.Empty;
                string ltime = string.Empty;

                if (FPunchIn != "")
                {
                    TimeSpan.TryParse(FPunchIn, out PunchIn);

                    TimeSpan.TryParse(ShiftLate, out ShiftL);
                    if (PunchIn > ShiftL)
                    {
                        latetime = PunchIn.Subtract(ShiftL); // (-00:30:00)

                        var s = latetime.Seconds.ToString().Length == 1 ? "0" + latetime.Seconds.ToString() : latetime.Seconds.ToString();
                        var m = latetime.Minutes.ToString().Length == 1 ? "0" + latetime.Minutes.ToString() : latetime.Minutes.ToString();
                        var h = latetime.Hours.ToString().Length == 1 ? "0" + latetime.Hours.ToString() : latetime.Hours.ToString();
                        ltime = h + ":" + m + ":" + s;
                    }
                }

                if (FPunchOut != "")
                {

                    TimeSpan.TryParse(FPunchOut, out PunchOut);

                    TimeSpan.TryParse(ShiftOut, out ShiftO);
                    if (PunchOut > ShiftO)
                    {
                        additionTime = PunchOut.Subtract(ShiftO); // (-00:30:00)

                        var s = additionTime.Seconds.ToString().Length == 1 ? "0" + additionTime.Seconds.ToString() : additionTime.Seconds.ToString();
                        var m = additionTime.Minutes.ToString().Length == 1 ? "0" + additionTime.Minutes.ToString() : additionTime.Minutes.ToString();
                        var h = additionTime.Hours.ToString().Length == 1 ? "0" + additionTime.Hours.ToString() : additionTime.Hours.ToString();
                        atime = h + ":" + m + ":" + s;
                    }




                }


                var FSts = item["FSts"].ToString();

                var obj = new
                {
                    PunchDate = PunchDate,
                    ShiftName = ShiftName,
                    ShiftIn = ShiftIn,
                    ShiftOut = ShiftOut,
                    FPunchIn = FPunchIn,
                    FPunchOut = FPunchOut,
                    ShiftLate = ltime,
                    additionTime = atime,
                    FSts = FSts
                };
                arr.Add(obj);
            }

            //    var results = JsonConvert.SerializeObject(LeaveAvailHistory);
            return new JsonResult { Data = arr };


        }

        public JsonResult GetBonusHistory(int finalcialYear)
        {
            var userInfo = (User)Session["UserName"];
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();

            dictionary.Add("@finalCialYear", finalcialYear);
            dictionary.Add("@empNo", userInfo.EmpCode);

            var LeaveAvailHistory = Models.DAL.storedProc2(HRStoredProcedureName.GetBonusHistory, dictionary, DBName.HR);

            var results = JsonConvert.SerializeObject(LeaveAvailHistory);
            return new JsonResult { Data = results };


        }

        public JsonResult GetSalaryHistory(int finalcialYear)
        {
            var userInfo = (User)Session["UserName"];
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@finalCialYear", finalcialYear);

            dictionary.Add("@empNo", userInfo.EmpCode);


            var LeaveAvailHistory = Models.DAL.storedProc2(HRStoredProcedureName.GetSalaryHistory, dictionary, DBName.HR);

            var results = JsonConvert.SerializeObject(LeaveAvailHistory);
            return new JsonResult { Data = results };
        }

        public JsonResult GetAdvanceHistory(int finalcialYear)
        {

            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            var userInfo = (User)Session["UserName"];
            dictionary.Add("@finalCialYear", finalcialYear);
            dictionary.Add("@empNo", userInfo.EmpCode);


            var LeaveAvailHistory = Models.DAL.storedProc2(HRStoredProcedureName.GetAdvanceHistory, dictionary, DBName.HR);

            var results = JsonConvert.SerializeObject(LeaveAvailHistory);
            return new JsonResult { Data = results };


        }

        public JsonResult GetHolidayHistory(int finalcialYear,string Month)
        {
            var userInfo = (User)Session["UserName"];
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@finalCialYear", finalcialYear);
            dictionary.Add("@empNo", userInfo.EmpCode);
            dictionary.Add("@Month", string.IsNullOrEmpty(Month) ? "" : Month);


            var LeaveAvailHistory = Models.DAL.storedProc2(SearchStoreProcedureName.GetGetHolidayHistoryBy_FinYear_Month, dictionary, DBName.SearchET);

            var results = JsonConvert.SerializeObject(LeaveAvailHistory);
            return new JsonResult { Data = results };


        }
        public JsonResult GetHourlyLeaveHistory(int finalcialYear, string Month)
        {
            var userInfo = (User)Session["UserName"];
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@finalCialYear", finalcialYear);
            dictionary.Add("@empNo", userInfo.EmpId);
            dictionary.Add("@Month", string.IsNullOrEmpty(Month) ? "" : Month);


            var LeaveAvailHistory = Models.DAL.storedProc2(SearchStoreProcedureName.GetGetHourlyLeaveDetails_am, dictionary, DBName.SearchET);

            var results = JsonConvert.SerializeObject(LeaveAvailHistory);
            return new JsonResult { Data = results };


        }

        public JsonResult GetOutStationDuty(int finalcialYear, string Month)
        {
            var userInfo = (User)Session["UserName"];
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@finalCialYear", finalcialYear);
            dictionary.Add("@empNo", userInfo.EmpId);
            dictionary.Add("@Month", string.IsNullOrEmpty(Month) ? "" : Month);


            var outStationDuty = Models.DAL.storedProc2(SearchStoreProcedureName.GetGetOutStationDutyHistoryBy_FinYear_Month_am, dictionary, DBName.SearchET);

            var results = JsonConvert.SerializeObject(outStationDuty);
            return new JsonResult { Data = results };
        }

        public JsonResult GetSearchHeadBYSearchText(string SearchText)
        {

            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            var userInfo = (User)Session["UserName"];
            dictionary.Add("@SearchText", SearchText);
          // dictionary.Add("@empNo", userInfo.EmpCode);


            var SearchHead = Models.DAL.storedProc2(ControlPanelStoreProcedureName.GetSearchHeadBYSearchText, dictionary, DBName.ControlPanal);

            var arr = new ArrayList();
            foreach (DataRow item in SearchHead.Rows)
            {
                // ControllerName ViewName

                var obj = new
                {
                    Text = item["ViewName"].ToString(),
                    Title = item["ViewTitle"].ToString(),
                    ControllerName = item["ControllerName"].ToString(),

                };
                arr.Add(obj);
            }
            return new JsonResult { Data = arr };
        }
        #endregion

        #region"For Employee Communication Protal"

        public JsonResult GetUnitList()
        {

            var Unit = Models.DAL.GetDataTable(HRStoredProcedureName.sp_select_Unit, DBName.HR);

            var arr = new ArrayList();

            foreach (DataRow item in Unit.Rows)
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

        public JsonResult LoadAllDesignation()
        {

            var Unit = Models.DAL.GetDataTable(HRStoredProcedureName.sp_DesigInfo_Select, DBName.HR);

            var arr = new ArrayList();

            foreach (DataRow item in Unit.Rows)
            {
                var obj = new
                {
                    ID = item["DesigNo"].ToString(),
                    Text = item["DesigEName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }

        public JsonResult LoadAllSection()
        {

            var Unit = Models.DAL.GetDataTable(HRStoredProcedureName.sp_ddlSection_Select, DBName.HR);

            var arr = new ArrayList();

            foreach (DataRow item in Unit.Rows)
            {
                var obj = new
                {
                    ID = item["SectEName"].ToString(),
                    Text = item["SectEName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }



        public JsonResult loadUnitWiseEmployee(int unit)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@Unit", unit);
            var emp = Models.DAL.GetDataTable(HRStoredProcedureName.sp_SelectEmployeeByUnit, dictionary, DBName.HR);

            var arr = new ArrayList();

            foreach (DataRow item in emp.Rows)
            {
                var obj = new
                {
                    ID = item["EMP_ID"].ToString(),
                    Text = item["Name"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }

        public JsonResult DefultLoginEmployee(string Id)
        {
            //var User = (User)Session["UserName"];

            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            if (Id != "All")
            {
                dictionary.Add("@Code", Session["UserName"].ToString());
            }
            else
            { dictionary.Add("@Code", "0"); }
            var EmpData = Models.DAL.storedProc2(HRStoredProcedureName.MascoSearch_Sp_EmployeeInfoById, dictionary, DBName.HR);

            var results = JsonConvert.SerializeObject(EmpData);
            return new JsonResult { Data = results };


        }

        public JsonResult GetUnitWiseEployeeList(string unit,string desId, string section,string empCode)
        {

            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@unit", string.IsNullOrEmpty(unit) ? 0 : Convert.ToInt32(unit));
            dictionary.Add("@desId", string.IsNullOrEmpty(desId) ? "" : desId);
            dictionary.Add("@section", string.IsNullOrEmpty(section) ? "" : section);
            dictionary.Add("@empCode", string.IsNullOrEmpty(empCode) ? 0 : Convert.ToInt32(empCode));
            var aData = Models.DAL.storedProc2(HRStoredProcedureName.MascoSearch_Sp_EmployeeInfoByUnit, dictionary, DBName.HR);

            var results = JsonConvert.SerializeObject(aData);
            return new JsonResult { Data = results };
        }

        public JsonResult LoadAllDesignationUnitWise(string unit)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@unit", string.IsNullOrEmpty(unit) ? 0 : Convert.ToInt32(unit));

            var Unit = Models.DAL.GetDataTable(HRStoredProcedureName.MascoSearch_Sp_UnitWiseDesignation, dictionary, DBName.HR);

            var arr = new ArrayList();

            foreach (DataRow item in Unit.Rows)
            {
                var obj = new
                {
                    ID = item["DesigNo"].ToString(),
                    Text = item["DesigEName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }

        public JsonResult LoadAllSectionUnitWise(string unit)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("@unit", string.IsNullOrEmpty(unit) ? 0 : Convert.ToInt32(unit));

            var Unit = Models.DAL.GetDataTable(HRStoredProcedureName.MascoSearch_Sp_UnitWiseSection, dictionary, DBName.HR);

            var arr = new ArrayList();

            foreach (DataRow item in Unit.Rows)
            {
                var obj = new
                {
                    ID = item["SectEName"].ToString(),
                    Text = item["SectEName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }

        #endregion

        #region Annual Leave Details Region

        public ActionResult AnnualLeaveDetails()
        {
            if ((Session["UserName"]) != null)
            {
                VM_Routing obj = new VM_Routing();
                obj.Controller = "MascoSearch";
                obj.Action = "Index";
                // var obj = "@Url.Action('Index', 'MascoSearch')";
                ViewData.Model = HttpUtility.UrlEncode(JsonConvert.SerializeObject(obj));

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public JsonResult GetAnnualLeaveDataByFinYear(int finalcialYear)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            var userInfo = (User)Session["UserName"];

            dictionary.Add("@EMP_ID", userInfo.EmpId);
            dictionary.Add("@FinancialYear", finalcialYear);

            var Data = Models.DAL.storedProc2(HRStoredProcedureName.GetAnnualLeaveDataByFinYear, dictionary, DBName.MascoSearch);

            var results = JsonConvert.SerializeObject(Data);
            return new JsonResult { Data = results };

        }

        #endregion
    }
}