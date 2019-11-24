using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers
{
    [Startup.NoCacheAttribute]
    public class TServiceController : ApiController
    {
        public string Get(int id)
        {
            return "value";
        }
            
         

          
        public IEnumerable<string> GetVal()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public HttpResponseMessage Test()
        {
            return Request.CreateResponse<string>(HttpStatusCode.OK, "i m tahmid");
        }

        private Dictionary<string, object> deserializeToDictionary(string jo)
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(jo);
            var values2 = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> d in values)
            {
                // if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                if (d.Value is JObject)
                {
                    values2.Add(d.Key, deserializeToDictionary(d.Value.ToString()));
                }
                else
                {
                    values2.Add(d.Key, d.Value);
                }
            }
            return values2;
        }

        [HttpGet]
        public HttpResponseMessage Autocomplete(string text)
        {
            Dictionary<string, dynamic> dr = new Dictionary<string, dynamic>();
            dr.Add("@text", text);
            System.Data.DataTable dt = Models.DAL.GetDataTableFromStoredProc("GetAutoText", dr, DBName.SearchET);
            return Request.CreateResponse<System.Data.DataTable>(HttpStatusCode.OK, dt);
        }

        [HttpGet]
        public dynamic Autocomplete2(string term = "")
        {
            Dictionary<string, dynamic> dr = new Dictionary<string, dynamic>();
            dr.Add("@text", term.Replace("-", " ").Replace("/", " ").Replace("_", " ").Replace(",", " ").Replace("&", " "));
            System.Data.DataTable dt = Models.DAL.GetDataTableFromStoredProc("GetAutoText", dr,DBName.SearchET);
            return Json<System.Data.DataTable>(dt);
        }

        [HttpGet]
        public dynamic ResponseTSearchET(string term = "")
        {
            if (!string.IsNullOrEmpty(term))
            {
                Dictionary<string, dynamic> dr = new Dictionary<string, dynamic>();
                dr.Add("@str", term);
                //GetResult
                System.Data.DataTable dt = Models.DAL.GetDataTableFromStoredProc("GetResult", dr, DBName.SearchET);
                return Json<System.Data.DataTable>(dt);
            }
            return NotFound();
        }

        [HttpPost]
        public dynamic GetData(TRequest json)
        {
            Dictionary<string, dynamic> defultdr = new Dictionary<string, dynamic>();
            if (json.dict != null)
            {
                // defultdr = json.dict;//deserializeToDictionary(json.dict);
                foreach (var item in json.dict)
                {
                    defultdr.Add(item.Key, item.Value);
                }
            }
            try
            {
                System.Data.DataTable dt = Models.DAL.GetDataTableFromStoredProc(json.sp, defultdr, json.db);

                return Request.CreateResponse<TResult>(HttpStatusCode.OK, new TResult(dt));
            }
            catch (Exception e)
            {
                Dictionary<string, string> retDict = new Dictionary<string, string>();
                retDict.Add("result", e.Message);
                return Request.CreateResponse<Dictionary<string, string>>(HttpStatusCode.OK, retDict);
            }
        }
        [HttpPost]
        public dynamic GetDataSqlParam(TRequest json)
        {
            Dictionary<string, dynamic> defultdr = new Dictionary<string, dynamic>();
            if (json.dict != null)
            {
                // defultdr = json.dict;//deserializeToDictionary(json.dict);
                foreach (var item in json.dict)
                {
                    defultdr.Add(item.Key, item.Value);
                }
            }
            try
            {
                System.Data.DataTable dt = Models. DAL.GetDataTable(json.sp, defultdr, json.db);

                return Request.CreateResponse<TResult>(HttpStatusCode.OK, new TResult(dt));
            }
            catch (Exception e)
            {
                Dictionary<string, string> retDict = new Dictionary<string, string>();
                retDict.Add("result", e.Message);
                return Request.CreateResponse<Dictionary<string, string>>(HttpStatusCode.OK, retDict);
            }
        }
        [HttpPost]
        public dynamic GetDataSql(TRequest json)
        {
           
            try
            {
                System.Data.DataTable dt = Models.DAL.GetDataTable(json.sp, json.db);

                return Request.CreateResponse<TResult>(HttpStatusCode.OK, new TResult(dt));
            }
            catch (Exception e)
            {
                Dictionary<string, string> retDict = new Dictionary<string, string>();
                retDict.Add("result", e.Message);
                return Request.CreateResponse<Dictionary<string, string>>(HttpStatusCode.OK, retDict);
            }
        }

        [HttpPost]
        public dynamic SaveData(TRequest json)
        {
            Dictionary<string, dynamic> defultdr = new Dictionary<string, dynamic>();
            if (json.dict != null)
            {
                //defultdr = json.dict;
                foreach (var item in json.dict)
                {
                    defultdr.Add(item.Key, item.Value);
                }
            }

            try
            {
                int ret = Models.DAL.storedProc(json.sp, defultdr, json.db);
                Dictionary<string, int> retDict = new Dictionary<string, int>();
                retDict.Add("result", ret);
                return Request.CreateResponse<Dictionary<string, int>>(HttpStatusCode.OK, retDict);
            }
            catch (Exception e)
            {
                Dictionary<string, string> retDict = new Dictionary<string, string>();
                retDict.Add("result", e.Message);
                return Request.CreateResponse<Dictionary<string, string>>(HttpStatusCode.OK, retDict);
            }
        }

        [HttpPost]
        public dynamic SaveDataList(TRequest json)
        {
            int ret = 1;
            foreach (List<TParam> itemList in json.dictList)
            {
                Dictionary<string, dynamic> defultdr = new Dictionary<string, dynamic>();
                if (itemList != null)
                {
                    //defultdr = json.dict;
                    foreach (var item in itemList)
                    {
                        defultdr.Add(item.Key, item.Value);
                    }
                }

                int retInner = Models.DAL.storedProc(json.sp, defultdr, json.db);
                if (retInner == 0)
                {
                    ret = 0;
                }
            }
            try
            {
                Dictionary<string, int> retDict = new Dictionary<string, int>();
                retDict.Add("result", ret);
                return Request.CreateResponse<Dictionary<string, int>>(HttpStatusCode.OK, retDict);
            }
            catch (Exception e)
            {
                Dictionary<string, string> retDict = new Dictionary<string, string>();
                retDict.Add("result", e.Message);
                return Request.CreateResponse<Dictionary<string, string>>(HttpStatusCode.OK, retDict);
            }
        }
        [HttpPost]
        public void DragonCatcher(dynamic json)
        {
            string ss = "";
        }


        [HttpGet]
        public dynamic GetSession(string key)
        {
            Dictionary<string, dynamic> retDict = new Dictionary<string, dynamic>();

            if (HttpContext.Current.Session[key] != null)
            {
                retDict.Add("result", HttpContext.Current.Session[key]);
                return Request.CreateResponse<Dictionary<string, dynamic>>(HttpStatusCode.OK, retDict);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public dynamic SaveSession(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
            Dictionary<string, dynamic> retDict = new Dictionary<string, dynamic>();
            retDict.Add("result", "True");
            return Request.CreateResponse<Dictionary<string, dynamic>>(HttpStatusCode.OK, retDict);
        }
    }
}