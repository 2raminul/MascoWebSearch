using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TWebApiSearch.Models;

namespace TWebApiSearch.DAL
{
    public class WebSearchLogInDAL
    {
        public DataTable LoadUnitDropDown()
        {
            return Models.DAL.GetDataTable("sp_select_Unit", DBName.HR);
        }

        public DataTable LoadEmpIdByUnit(string unitNo)
        {
            Dictionary<string,object> paramList=new Dictionary<string, object>();
            paramList.Add("@id",unitNo);
            return Models.DAL.GetDataTable("sp_Emp_byUnitID_Select", paramList, DBName.HR);
        }
        public DataTable LoadEmpInfoByUnit(string empId)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@EmpId", empId);
            return Models.DAL.GetDataTable("sp_GetEmpInfoByEmpId", paramList, DBName.HR);
        }

        public int SaveUser(User user)
        {
            Dictionary<string,object> paramList=new Dictionary<string, object>();
            paramList.Add("@UserName", user.EmpCode);
            paramList.Add("@EmpCode", user.EmpCode);
            paramList.Add("@EmpId", user.EmpId);
            paramList.Add("@Password", EncryptDecrypt.EncryptText(user.PassWord));
            paramList.Add("@OfficeMail", user.OfficeMail);
            paramList.Add("@OfficeCell", !string.IsNullOrEmpty(user.OfficeCell)? user.OfficeCell:"");
            return Models.DAL.storedProc("sp_Insert_tblUser", paramList, DBName.ControlPanal);
        }

        public DataTable GetAdminLogIn(string userName,string password)
        {
            Dictionary<string,object> paramList=new Dictionary<string, object>();
            paramList.Add("@UserName",userName);
            paramList.Add("@PassWord", EncryptDecrypt.EncryptText(password));
            return Models.DAL.GetDataTable("sp_GetUserLogin", paramList, DBName.SearchET);
        }
        public DataTable GetUserLogIn(string userName, string password)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@EmpCode", userName);
            paramList.Add("@PassWord", EncryptDecrypt.EncryptText(password));
            return Models.DAL.GetDataTable("sp_GetUserLoginByEmpCode", paramList, DBName.SearchET);
        }
        public string GetPW(string empCode)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string,object> sqlParameterList = new Dictionary<string, object>();
                sqlParameterList.Add("@EmpCode", empCode);
                dt = Models.DAL.GetDataTable("sp_GetUserPasswordWebSearch", sqlParameterList, DBName.ControlPanal);
                return dt.Rows[0]["Status"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string SendPW(string empCode, string pW)
        {
            string ret = "";
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> sqlParameterList = new Dictionary<string, object>();
                sqlParameterList.Add("@EMP_CODE", empCode);
                sqlParameterList.Add("@pW", pW);
                dt = Models.DAL.GetDataTable("sp_WebSearch_ModuleUser_PWMail", sqlParameterList, DBName.ControlPanal);
                return dt.Rows[0]["Status"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int ModuleUserLogin(string userName, int _mainModuleId, string userPC, string userIP)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                try
                {
                    Dictionary<string,object> sqlParameterList = new Dictionary<string, object>();
                    sqlParameterList.Add("MainModuleId",_mainModuleId);
                    sqlParameterList.Add("UserName", userName);
                    sqlParameterList.Add("UserPC", userPC);
                    sqlParameterList.Add("UserIP", userIP);
                    return Models.DAL.SaveDataByStoreProcedure("usp_ModuleUserLoginWebSearch", sqlParameterList, DBName.ControlPanal);
                }
                catch (Exception ex)
                {
                    return 0;

                }
            }
            else
            {
                return 0;
            }

        }

        public void ModuleUserLogout(int MULRID)
        {
            try
            {
                Dictionary<string,object> sqlParameterList = new Dictionary<string, object>();
                sqlParameterList.Add("MULRID", MULRID);
                Models.DAL.storedProc("usp_ModuleUserLogout", sqlParameterList, DBName.ControlPanal);
            }
            catch (Exception ex)
            {

            }
        }

        public bool InsertLoginHistory(LoginHistory obj)
        {
            try
            {
                Dictionary<string, object> sqlParameterList = new Dictionary<string, object>();
                sqlParameterList.Add("EMP_CODE", obj.EMP_CODE);
                Models.DAL.storedProc("uspInsertLoginHistory_am", sqlParameterList, DBName.SearchET);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}