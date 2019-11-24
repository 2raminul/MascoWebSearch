using System;
using System.Collections.Generic;
using System.Data;
using TWebApiSearch.DAL.Document_Download_Section;
using TWebApiSearch.Models;

namespace TWebApiSearch.BLL.Document_Download_Section
{
    public class AdvancePayslipBLL
    {
        private readonly AdvancePayslipDAL _objDAl = new AdvancePayslipDAL();
        DataTable dt = new DataTable();
        List<ComboData> lCmbData = new List<ComboData>();
        public List<ComboData> LoadMonthDropDown(string FinYear, string EMP_ID)
        {
            lCmbData = new List<ComboData>();
            dt = new DataTable();
            dt = _objDAl.GetMonthByFinYear(FinYear, Convert.ToInt32(EMP_ID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lCmbData = dt.DataTableToList<ComboData>();
                }
            }
            return lCmbData;
        }

        internal string GetDataForReport(DocumentDownloadSection data, string EmpCode)
        {
            string error = string.Empty;
            error = _objDAl.GetDataForReport(data, EmpCode);
            return error;
        }
    }
}