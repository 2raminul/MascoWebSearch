namespace TWebApiSearch.Models
{
    
        public class TResult
        {
            public string ID { get; set; }
            public System.Data.DataTable data { get; set; }

            public TResult(System.Data.DataTable dt)
            {
                data = dt;
            }
        }
    
}