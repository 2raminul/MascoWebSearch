using System;
using System.Collections.Generic;
using System.Data;
using TWebApiSearch.DAL.Document_Download_Section;
using TWebApiSearch.Models;

namespace TWebApiSearch.BLL.Document_Download_Section
{
    public class IncomeTaxCertificateBLL
    {
        private readonly IncomeTaxCertificateDAL _objDAl = new IncomeTaxCertificateDAL();
        DataTable dt = new DataTable();
        List<ComboData> lCmbData = new List<ComboData>();

        internal List<ComboData> LoadTaxYearDown()
        {
            lCmbData = new List<ComboData>();
            dt = new DataTable();
            dt = _objDAl.GetTaxYear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lCmbData = dt.DataTableToList<ComboData>();
                }
            }
            return lCmbData;
        }

        internal DataSet GetDataForReport(DocumentDownloadSection data,string EmpId)
        {
            DataSet ds = _objDAl.GetDataForReport(data,Convert.ToInt32(EmpId));
            ds.Tables[0].TableName = "dsSalaryAllowance";
            return ds;
        }
    }
}