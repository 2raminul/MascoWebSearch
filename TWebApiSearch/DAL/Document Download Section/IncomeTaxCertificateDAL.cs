using System.Collections.Generic;
using System.Data;
using TWebApiSearch.Models;

namespace TWebApiSearch.DAL.Document_Download_Section
{
    public class IncomeTaxCertificateDAL
    {
        private DataTable _dt = new DataTable();
        private Dictionary<string, object> _paramList = new Dictionary<string, object>();
        private DocumentDownloadSection _model = new DocumentDownloadSection();
        internal DataTable GetTaxYear()
        {
            _dt = new DataTable();
            _dt = Models.DAL.GetDataTable(SearchStoreProcedureName.GetTaxYear, DBName.SearchET);
            return _dt;
        }

        internal DataSet GetDataForReport(DocumentDownloadSection data,int EmpID)
        {
            _model = new DocumentDownloadSection();
            _model.OperationName = "E.EMP_ID=" + EmpID + " and " + "TC.TaxFinYearNo=" + data.TaxFinYearNo;
            _model.TaxStatus = data.TaxStatus;
            _paramList = getParam(_model);

            DataSet ds = Models.DAL.GetDataSet(HRStoredProcedureName.IncomeTaxCertificate,_paramList, DBName.HR);
            return ds;
        }
        private Dictionary<string, object> getParam(DocumentDownloadSection _model)
        {
            _paramList = new Dictionary<string, object>();
            _paramList.Add("@Option", _model.OperationName);
            _paramList.Add("@Option1", _model.TaxStatus);
            return _paramList;
        }
    }
}