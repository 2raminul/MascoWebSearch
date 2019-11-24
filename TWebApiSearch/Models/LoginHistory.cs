using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TWebApiSearch.Models
{
    [Serializable]
    public class LoginHistory
    {
        [Key]
        public int Id { get; set; }
        public string EMP_CODE { get; set; }
        public DateTime LoginDate { get; set; }
    }
}