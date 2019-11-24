using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TWebApiSearch.DAL;
using TWebApiSearch.Models;

namespace TWebApiSearch.BLL
{
    public class WebSearchLogInBLL
    {
        WebSearchLogInDAL webSearchLogInDal=new WebSearchLogInDAL();
        public List<ComboData> LoadUnitDropDown()
        {
            List<ComboData> lUnit = new List<ComboData>();
            DataTable dt = webSearchLogInDal.LoadUnitDropDown();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ComboData unit = new ComboData();
                    unit.Value = Int32.Parse(dt.Rows[i]["UnitNo"].ToString());
                    unit.DisplayName = dt.Rows[i]["UnitEName"].ToString();
                    lUnit.Add(unit);
                }

            }
            return lUnit;
        }

        public List<ComboData> LoadEmpIdByUnit(string unitNo)
        {
            List<ComboData> lEmpId=new List<ComboData>();
            DataTable dt = webSearchLogInDal.LoadEmpIdByUnit(unitNo);
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ComboData unit = new ComboData();
                    unit.Value = Int32.Parse(dt.Rows[i]["EMP_ID"].ToString());
                    unit.DisplayName = dt.Rows[i]["EMP_CODE"].ToString();
                    lEmpId.Add(unit);
                }
            }
            return lEmpId;
        }
        public List<User> LoadEmpInfoByEmpId(string empId)
        {
            List<User> lEmp = new List<User>();
            DataTable dt = webSearchLogInDal.LoadEmpInfoByUnit(empId);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    User emp = new User();
                    emp.EmpName = dt.Rows[i]["EMP_ENAME"].ToString();
                    emp.OfficeMail = dt.Rows[i]["Email"].ToString();
                    emp.OfficeCell = dt.Rows[i]["ContactMobile"].ToString();
                    lEmp.Add(emp);
                }
            }
            return lEmp;
        }

        public bool SaveUser(User user)
        {
            if (webSearchLogInDal.SaveUser(user) > 0)
            {
                return true;
            }
            return false;
        }

        public User GetUserLogIn(string userName, string password)
        {
            User user = null;
            DataTable dt = null;
            if (userName.ToUpper() == "ADMIN")
            {
                dt = webSearchLogInDal.GetAdminLogIn(userName, password);
            }
            else
            {
               dt=webSearchLogInDal.GetUserLogIn(userName, password);
            }
            if (dt.Rows.Count>0)
            {
                user=new User();
                user.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                user.EmpId = dt.Rows[0]["EMP_ID"].ToString();                
                user.EmpCode = dt.Rows[0]["EMP_CODE"].ToString();
                user.IsAdmin =dt.Rows[0]["IsAdmin"].ToString();
                user.EMP_ENAME = dt.Rows[0]["EMP_ENAME"].ToString();
                user.DesigEName = dt.Rows[0]["DesigEName"].ToString();
            }
            return user;
        }
        public int ModuleUserLogin(string userName, int _mainModuleId, string userPC, string userIP)
        {
            return webSearchLogInDal.ModuleUserLogin(userName, _mainModuleId, userPC, userIP);
        }
        public string SendPassword(string empCode)
        {
            if (!string.IsNullOrEmpty(empCode))
            {
                string passWord = EncryptDecrypt.DecryptText(webSearchLogInDal.GetPW(empCode));
                if (Convert.ToInt32(webSearchLogInDal.SendPW(empCode, passWord)) > 0)
                {
                    return "Mail Sent Successfully in your following mail!!";
                }
                else
                {
                    return "Mail Sending Failed!!";
                }

            }
            else
            {
                return "Please Provide Your User ID";
            }
        }

        public void ModuleUserLogout(int MULRID)
        {
            webSearchLogInDal.ModuleUserLogout(MULRID);
        }

        public bool InsertLoginHistory(LoginHistory obj)
        {
            var isSaved = false;
            if (obj !=null)
            {
                isSaved= webSearchLogInDal.InsertLoginHistory(obj);
            }
           
            return isSaved;
        }

    }

    }