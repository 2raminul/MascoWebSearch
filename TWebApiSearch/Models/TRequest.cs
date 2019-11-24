using System.Collections.Generic;

namespace TWebApiSearch.Models
{
        public class TRequest
        {
            public string sp { get; set; }

            // public Dictionary<string,string> dict { get; set; }
            public List<TParam> dict { get; set; }

            public List<List<TParam>> dictList { get; set; }

            public string db { get; set; }
        }
    
}