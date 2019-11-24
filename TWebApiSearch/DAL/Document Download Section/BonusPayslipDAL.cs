using System;
using System.Collections.Generic;
using System.Data;
using TWebApiSearch.Models;

namespace TWebApiSearch.DAL.Document_Download_Section
{
    public class BonusPayslipDAL
    {       
        private DataTable _dt = new DataTable();
        private Dictionary<string, object> _paramList = new Dictionary<string, object>();
        private DocumentDownloadSection _model = new DocumentDownloadSection();
        public DataTable GetFinYear()
        {
            _dt = new DataTable();
            _dt = Models.DAL.GetDataTable(HRStoredProcedureName.sp_FinalYear_Select, DBName.HR);
            return _dt;
        }

        public DataTable GetMonthByFinYear(string finYear,int EmpID)
        {
            _model = new DocumentDownloadSection();
            _model.OperationName = "GetBonusMonth";
            _model.FinYear =Convert.ToInt32(finYear);
            _model.EMP_ID = EmpID;
            _paramList = getParam(_model);
            
            return Models.DAL.GetDataTable(SearchStoreProcedureName.GetMonthFinYear, _paramList, DBName.SearchET);
        }

        private Dictionary<string, object> getParam(DocumentDownloadSection _model)
        {
            _paramList = new Dictionary<string, object>();
            _paramList.Add("@OperationName", _model.OperationName);
            _paramList.Add("@FinYear", _model.FinYear);
            _paramList.Add("@EMP_ID", _model.EMP_ID);
            return _paramList;
        }

        internal string GetDataForReport(DocumentDownloadSection data, string empCode)
        {
            string error = string.Empty;
            _paramList = new Dictionary<string, object>();
            _paramList.Add("@MonthName", data.FromMonth);
            _paramList.Add("@EMP_CODE", empCode);
            _paramList.Add("@FinYearNo", data.FinYear);
            _paramList.Add("@BonusTypeNo", data.BonusTypeNo);

            try
            {
                var num = Models.DAL.storedProc(HRStoredProcedureName.BonusPaySlipForEmail, _paramList, DBName.HR);
                error = "Send";
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return error;
        }
    }
}