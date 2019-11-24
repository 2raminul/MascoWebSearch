using System;
using System.Collections.Generic;
using System.Data;
using TWebApiSearch.Models;

namespace TWebApiSearch.DAL.Document_Download_Section
{
    public class SalaryPayslipDAL
    {
        private DataTable _dt = new DataTable();
        private Dictionary<string, object> _paramList = new Dictionary<string, object>();
        private DocumentDownloadSection _model = new DocumentDownloadSection();

        internal string GetDataForReport(DocumentDownloadSection data, string empCode)
        {
            string error = string.Empty;
            data.EMP_CODE = empCode;
            _paramList = getParam(data);
            try
            {
                var num = Models.DAL.storedProc(HRStoredProcedureName.GetEmpPaySlipForEmail, _paramList, DBName.HR);                
                error = "Send";
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            return error;
        }
        private Dictionary<string, object> getParam(DocumentDownloadSection _model)
        {
            _paramList = new Dictionary<string, object>();
            _paramList.Add("@MonthName", _model.FromMonth);
            _paramList.Add("@EMP_CODE", _model.EMP_CODE);
            _paramList.Add("@FinYearNo", _model.FinYear);
            return _paramList;
        }
    }
}