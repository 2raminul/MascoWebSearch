using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using TWebApiSearch.DAL.Document_Download_Section;
using TWebApiSearch.Models;

namespace TWebApiSearch.BLL.Document_Download_Section
{
    public class SalaryPayslipBLL
    {
        private readonly SalaryPayslipDAL _objDAl = new SalaryPayslipDAL();
        DataTable dt = new DataTable();
        List<ComboData> lCmbData = new List<ComboData>();
        public List<ComboData> LoadMonthDropDown()
        {
            lCmbData = new List<ComboData>();
            dt = new DataTable();
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < months.Length; i++)
            {
                ComboData obj = new ComboData();
                obj.DisplayName = months[i];
                obj.Value = Convert.ToInt32(i.ToString());
                lCmbData.Add(obj);
            }
            return lCmbData;
        }

        internal string GetDataForReport(DocumentDownloadSection data, string EmpCode)
        {
            string error = string.Empty;
            error = _objDAl.GetDataForReport(data,EmpCode);
            return error;
        }
    }
}