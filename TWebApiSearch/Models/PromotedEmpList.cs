using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TWebApiSearch.Models
{
    public class PromotedEmpList
    {
        public string UnitName { get; set; }
        public string EmpName { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string PreviousDesignation { get; set; }
        public string PromotedDesignation { get; set; }
        public byte[] Photo { get; set; }
        public string img { get; set; }
    }
}