using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TWebApiSearch.Models
{
  
    public static class DBConnection
    {
        public static string GetConectionString(string DatabaseName)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[DatabaseName].ConnectionString;
        }
    }
}