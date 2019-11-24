using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TWebApiSearch.Models
{
    public class DocumentDownloadSection
    {
        public string OperationName { get; set; }
        public int FinYear { get; set; }
        public int EMP_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EmpName { get; set; }
        public string EmpSection { get; set; }
        public string Empdesignation { get; set; }
        public string DeptEName { get; set; }
        public string UnitEName { get; set; }
        public string UnitEAddress { get; set; }
        public string DOJ { get; set; }
        public string GroupEName { get; set; }        
        public string Date { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string TotalDays { get; set; }
        public string ApplicationDate { get; set; }
        public string LeaveType { get; set; }
        public string Reason { get; set; }
        public string AddressDuringLeave { get; set; }
        public string JoinAfterLeave { get; set; }
        public string ConcernPersonnameDesig { get; set; }
        public string OutstationPlaceName { get; set; }
        public string IncomeTaxYear { get; set; }
        public string TaxFinYearNo { get; set; }
        public string TaxStatus { get; set; }
        public string FromMonth { get; set; }
        public string ToMonth { get; set; }
        public string AYear { get; set; }
        public string BonusTypeNo { get; set; }
        public List<LeaveStatus> lstLeaveStatus { get; set; }

    }
    public  class LeaveStatus
    {
        public int LeaveTypeNo { get; set; }
        public double MaxBalance { get; set; }
        public double Avail { get; set; }
    }
    
}