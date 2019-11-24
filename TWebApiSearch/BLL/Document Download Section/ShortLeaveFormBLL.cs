using System;
using System.Data;
using TWebApiSearch.DAL.Document_Download_Section;
using TWebApiSearch.Models;

namespace TWebApiSearch.BLL.Document_Download_Section
{
    public class ShortLeaveFormBLL
    {
        private readonly ShortLeaveFormDAL _objDAl = new ShortLeaveFormDAL();
        DataTable dt = new DataTable();
        internal DocumentDownloadSection GetData(string EmpId)
        {
            DocumentDownloadSection obj = new DocumentDownloadSection();
            dt = new DataTable();
            dt = _objDAl.GetData(Convert.ToInt32(EmpId));
            if (dt.Rows.Count>0)
            {
                obj.lstLeaveStatus = dt.DataTableToList<LeaveStatus>();
                obj.EMP_CODE = dt.Rows[0]["EMP_CODE"].ToString();
                obj.EmpName = dt.Rows[0]["EMP_ENAME"].ToString();
                obj.DeptEName = dt.Rows[0]["DeptEName"].ToString();
                obj.EmpSection = dt.Rows[0]["SectEName"].ToString();
                obj.Empdesignation = dt.Rows[0]["DesigEName"].ToString();
                obj.UnitEName = dt.Rows[0]["UnitEName"].ToString();
                obj.UnitEAddress = dt.Rows[0]["UnitEAddress"].ToString();
                obj.GroupEName = dt.Rows[0]["GroupEName"].ToString();
                obj.DOJ = dt.Rows[0]["DOJ"].ToString();
            }
            return obj;
        }

        internal DataSet SetDataForReport(DocumentDownloadSection data)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Table");
            dt = GetColumnForReport(dt);
            dt = GetRowForReport(dt, data);
            ds.Tables.Add(dt);
            return ds;
        }
        private DataTable GetRowForReport(DataTable dt, DocumentDownloadSection data)
        {
            DataRow row = dt.NewRow();
            row["OperationName"] = data.OperationName;
            row["EMP_CODE"] = data.EMP_CODE;
            row["EmpName"] = data.EmpName;
            row["EmpSection"] = data.EmpSection;
            row["Empdesignation"] = data.Empdesignation;
            row["DeptEName"] = data.DeptEName;
            row["UnitEName"] = data.UnitEName;
            row["UnitEAddress"] = data.UnitEAddress;
            row["DOJ"] = data.DOJ;
            row["Date"] = data.Date;
            row["TimeFrom"] = data.TimeFrom;
            row["TimeTo"] = data.TimeTo;
            row["TotalDays"] = data.TotalDays;
            row["ApplicationDate"] = data.ApplicationDate;
            row["LeaveType"] = data.LeaveType;
            row["Reason"] = data.Reason;
            row["AddressDuringLeave"] = data.AddressDuringLeave;
            row["JoinAfterLeave"] = data.JoinAfterLeave;
            row["ConcernPersonnameDesig"] = data.ConcernPersonnameDesig;
            row["OutstationPlaceName"] = data.OutstationPlaceName;
            if (data.lstLeaveStatus != null)
            {
                row["TotalEntitle"] = data.lstLeaveStatus[0].MaxBalance;
                row["AlreadyEnjoy"] = data.lstLeaveStatus[0].Avail;
                row["Balance"] = data.lstLeaveStatus[0].MaxBalance - data.lstLeaveStatus[0].Avail;
            }
            dt.Rows.Add(row);
            return dt;
        }

        private DataTable GetColumnForReport(DataTable dt)
        {
            dt.Columns.Add("OperationName");
            dt.Columns.Add("EMP_CODE");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("EmpSection");
            dt.Columns.Add("Empdesignation");
            dt.Columns.Add("DeptEName");
            dt.Columns.Add("UnitEName");
            dt.Columns.Add("UnitEAddress");
            dt.Columns.Add("DOJ");
            dt.Columns.Add("Date");
            dt.Columns.Add("TimeFrom");
            dt.Columns.Add("TimeTo");
            dt.Columns.Add("TotalDays");
            dt.Columns.Add("ApplicationDate");
            dt.Columns.Add("LeaveType");
            dt.Columns.Add("Reason");
            dt.Columns.Add("AddressDuringLeave");
            dt.Columns.Add("JoinAfterLeave");
            dt.Columns.Add("ConcernPersonnameDesig");
            dt.Columns.Add("OutstationPlaceName");
            dt.Columns.Add("TotalEntitle");
            dt.Columns.Add("AlreadyEnjoy");
            dt.Columns.Add("Balance");
            return dt;
        }
    }
}