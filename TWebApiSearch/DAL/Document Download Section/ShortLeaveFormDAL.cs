using System.Collections.Generic;
using System.Data;
using TWebApiSearch.Models;

namespace TWebApiSearch.DAL.Document_Download_Section
{
    public class ShortLeaveFormDAL
    {
        Dictionary<string, object> _paramList = new Dictionary<string, object>();
        DocumentDownloadSection _model = new DocumentDownloadSection();
        internal DataTable GetData(int EmpID)
        {
            _model = new DocumentDownloadSection();
            _model.OperationName = "GetEmpInfoByEmpId";
            _model.EMP_ID = EmpID;
            _paramList = getParam(_model);

            return Models.DAL.GetDataTable(SearchStoreProcedureName.GetLeaveFromData, _paramList, DBName.SearchET);
        }

        private Dictionary<string, object> getParam(DocumentDownloadSection _model)
        {
            _paramList = new Dictionary<string, object>();
            _paramList.Add("@OperationName", _model.OperationName);
            _paramList.Add("@EMP_ID", _model.EMP_ID);
            return _paramList;
        }
    }
}