using System;
using System.Collections.Generic;
using System.Data;
using TWebApiSearch.DAL.Document_Download_Section;
using TWebApiSearch.Models;

namespace TWebApiSearch.BLL.Document_Download_Section
{
    public class BonusPayslipBLL
    {
        private readonly BonusPayslipDAL _objDAl = new BonusPayslipDAL();
        DataTable dt = new DataTable();
        List<ComboData> lCmbData = new List<ComboData>();
        public List<ComboData> LoadFinYearDropDown()
        {
            lCmbData = new List<ComboData>();
            dt = new DataTable();
            dt = _objDAl.GetFinYear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ComboData CmbData = new ComboData();
                    CmbData.Value = Int32.Parse(dt.Rows[i]["FinalYearNo"].ToString());
                    CmbData.DisplayName = dt.Rows[i]["FinalYearName"].ToString();
                    CmbData.IsSelected = dt.Rows[i]["IsActive"].ToString();
                    lCmbData.Add(CmbData);
                }
            }
            return lCmbData;
        }
        public List<ComboData> LoadMonthDropDown(string FinYear,string EMP_ID)
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