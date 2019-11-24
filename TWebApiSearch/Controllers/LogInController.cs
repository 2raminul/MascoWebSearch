using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TWebApiSearch.BLL;
using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers
{
    [Startup.NoCacheAttribute]
    public class LogInController : Controller
    {
        WebSearchLogInBLL webSearchLogInBll = new WebSearchLogInBLL();
        // GET: LogIn
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }
        public JsonResult LoadUnitDropDown()
        {
            List<ComboData> lUnit = webSearchLogInBll.LoadUnitDropDown();
            return new JsonResult { Data = lUnit };

        }
        public JsonResult LoadEmpIdByUnit(string unitNo)
        {
            List<ComboData> lUnit = webSearchLogInBll.LoadEmpIdByUnit(unitNo);
            return new JsonResult { Data = lUnit };
        }
        public JsonResult LoadEmpInfoByUnit(string empId)
        {
            List<User> lEmpInfo = webSearchLogInBll.LoadEmpInfoByEmpId(empId);
            return new JsonResult { Data = lEmpInfo };
        }

        public JsonResult SaveUser(User user)
        {
            bool result = webSearchLogInBll.SaveUser(user);
            return Json(result);
        }

        public JsonResult GetUserImageById(string empCode)
        {
            string fileNameWeb = "/Content/Images/Masco.jpg";
            if (empCode != "admin")
            {

                Dictionary<string, object> paramList = new Dictionary<string, object>();
                paramList.Add("@id", empCode);
                DataTable dt = Models.DAL.GetDataTable("sp_EmpImageBYUserID", paramList, DBName.HR);
                if (dt.Rows.Count > 0)
                {
                    byte[] image = (byte[]) dt.Rows[0]["Attachment"];


                    string fileName = System.Web.HttpContext.Current.Request.MapPath("~/Content/EmpImages/") + "emp" +
                                      empCode + ".jpg";
                    fileNameWeb = "/Content/EmpImages/" + "emp" + empCode + ".jpg";
                    try
                    {
                        System.IO.File.WriteAllBytes(fileName, image);
                    }
                    catch (Exception)
                    {

                        //  throw;
                    }
                }
            }
            string Emp_Photo = fileNameWeb;
            return Json(Emp_Photo);
        }
        public ActionResult GetLoginAccess(string userName, string password)
        {
            User user = webSearchLogInBll.GetUserLogIn(userName, password);
            if (user != null)
            {
                Session["MainModuleId"] = "12";
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.EmpCode;
                Session["IsAdmin"] = user.IsAdmin;
                Session["EMP_CODE"] = user.EmpCode;
                Session["EMP_ID"] = user.EmpId;
                Session["DesigEName"] = user.DesigEName;
                Session["EMP_ENAME"] = user.EMP_ENAME;
                string mainModuleId = Session["MainModuleId"].ToString();
                Session["MULRID"] = webSearchLogInBll.ModuleUserLogin(Session["UserName"].ToString(), 12, getUserPC(),
                    getUserIP());
                Session["UserImage"] = "/Content/Images/Masco.jpg";
                Session["EmpName"] = "Admin";
                if (userName.ToUpper() != "ADMIN")
                {
                    Dictionary<string, object> paramList = new Dictionary<string, object>();
                    paramList.Add("@id", user.EmpCode);
                    DataTable dt = Models.DAL.GetDataTable("sp_EmpImageBYUserID", paramList, DBName.HR);
                    if (dt.Rows.Count > 0)
                    {

                        byte[] image = (byte[]) dt.Rows[0]["Attachment"];

                        string fileName = System.Web.HttpContext.Current.Request.MapPath("~/Content/EmpImages/") + "emp" +
                                          user.EmpCode + ".jpg";
                        Session["UserImage"] = "/Content/EmpImages/" + "emp" + user.EmpCode + ".jpg";
                        Session["EmpName"] = dt.Rows[0]["EMP_ENAME"].ToString();
                        try
                        {
                            System.IO.File.WriteAllBytes(fileName, image);
                        }
                        catch (Exception)
                        {

                            //  throw;
                        }
                    }
                }
                Session["UserName"] = user;
                var LoginHistory = new LoginHistory();
                LoginHistory.EMP_CODE = Session["EMP_CODE"].ToString();
                if (webSearchLogInBll.InsertLoginHistory(LoginHistory))
                {
                    return Json(user == null ? "false" : "true", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
                
            }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
        public JsonResult SendPassword(string empCode)
        {
            string message = webSearchLogInBll.SendPassword(empCode);
            return Json(message);
        }

        private string getUserPC()
        {
            try
            {
                HttpRequestBase Req = new HttpRequestWrapper(System.Web.HttpContext.Current.Request);
                return System.Net.Dns.GetHostByAddress(Req.UserHostAddress).HostName;

            }
            catch (System.Net.Sockets.SocketException ex)
            {
                return null;
            }
        }

        private string getUserIP()
        {
            try
            {
                var context = System.Web.HttpContext.Current;
                if (context != null)
                {
                    var request = context.Request;
                    if (request != null)
                    {
                        string xff = request.Headers["X-Forwarded-For"];
                        string clientIP = string.Empty;
                        if (!string.IsNullOrWhiteSpace(xff))
                        {
                            int idx = xff.IndexOf('\'', '\'');
                            if (idx > 0)
                            {
                                // multiple IP addresses, pick the first one
                                clientIP = xff.Substring(0, idx);
                            }
                            else
                            {
                                clientIP = xff;
                            }
                        }

                        return string.IsNullOrWhiteSpace(clientIP) ? request.UserHostAddress : clientIP;
                    }
                }

                return string.Empty;
            }
            catch (HttpException ex)
            {
                return null;
            }
        }
    }
}