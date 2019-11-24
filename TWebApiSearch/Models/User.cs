using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TWebApiSearch.Models
{
    [Serializable]
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UnitNo { get; set; }
        [Required]
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }
        [Required]
        public string OfficeMail { get; set; }
        public string OfficeCell { get; set; }
        [Required]
        public string PassWord { get; set; }

        public string IsAdmin { get; set; }
        public string UserImage { get; set; }
        public string EMP_ENAME { get; set; }
        public string DesigEName { get; set; }
    }
    public class LoginImage
    {
        public string Emp_Photo { get; set; }
        public string Emp_Email { get; set; }

        public string Emp_Code { get; set; }
    }
}