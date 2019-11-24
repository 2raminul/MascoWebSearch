using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TWebApiSearch.Models
{
    public class UserView
    {
        public string NumberofView { get; set; }
        public string NumberofListedUser { get; set; }
        public string CurrentlyViewing { get; set; }
        public string TodaysView { get; set; }
    }
}