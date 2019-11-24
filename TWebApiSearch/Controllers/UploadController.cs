using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers
{
    [Startup.NoCacheAttribute]
    public class UploadController : ApiController
    {

        public string ImageDirPath { get; set; }

        public UploadController()
        {
            ImageDirPath = WebConfigurationManager.AppSettings["ImageDirPath"];
        }

        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public dynamic ImageToDir(TRequest json)
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

                // Convert Base64 String to byte[]
                byte[] imageBytes = Convert.FromBase64String(defultdr["@imageString"]);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                image.Save(ImageDirPath + defultdr["@imageName"]);

                //saving data to database

                string emp_id_mine = defultdr["@emp_id_mine"];
                string emp_id_other = defultdr["@emp_id_other"];

                Dictionary<string, dynamic> dict_param = new Dictionary<string, dynamic>();
                dict_param.Add("@Message", defultdr["@imageName"]);
                dict_param.Add("@mFrom", Int32.Parse(emp_id_mine));
                dict_param.Add("@mTo", Int32.Parse(emp_id_other));
                dict_param.Add("@isImage", 1);

                try
                {
                    Models.DAL.GetDataTableFromStoredProc("usp_m_chatMessage_Insert", dict_param, DBName.SCM);
                    //DAL.GetDataTable("usp_m_chatMessage_Insert", dict_param, System.Configuration.ConfigurationManager.ConnectionStrings["SCM"].ConnectionString);
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                
                Dictionary<string, string> retDict = new Dictionary<string, string>();
                retDict.Add("result", "Save Successful...");

                return Request.CreateResponse<Dictionary<string, string>>(HttpStatusCode.OK, retDict);
            }
            catch (Exception e)
            {
                Dictionary<string, string> retDict = new Dictionary<string, string>();
                retDict.Add("result", e.Message);
                return Request.CreateResponse<Dictionary<string, string>>(HttpStatusCode.OK, retDict);
            }
        }

        [HttpPost]
        public dynamic ImageToDir2(TRequest json)
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
                //string fileString = System.IO.Path.GetFileName(defultdr["@fileString"]);
                byte[] imageBytes = Convert.FromBase64String(defultdr["@fileString"]);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                string fileName = System.IO.Path.GetFileName(defultdr["@fileName"]);
                File.WriteAllBytes(ImageDirPath + fileName, imageBytes);

                string emp_id_mine = defultdr["@emp_id_mine"];
                string emp_id_other = defultdr["@emp_id_other"];

                Dictionary<string, dynamic> dict_param = new Dictionary<string, dynamic>();
                dict_param.Add("@Message", fileName);
                dict_param.Add("@mFrom", Int32.Parse(emp_id_mine));
                dict_param.Add("@mTo", Int32.Parse(emp_id_other));
                dict_param.Add("@isImage", 1);

                try
                {
                    Models.DAL.GetDataTableFromStoredProc("usp_m_chatMessage_Insert", dict_param, DBName.SCM);
                    //DAL.GetDataTable("usp_m_chatMessage_Insert", dict_param, System.Configuration.ConfigurationManager.ConnectionStrings["SCM"].ConnectionString);
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }

                Dictionary<string, string> retDict = new Dictionary<string, string>();
                retDict.Add("result", "Save Successful...");

                return Request.CreateResponse<Dictionary<string, string>>(HttpStatusCode.OK, retDict);

            }
            catch (Exception e)
            {
                Dictionary<string, string> retDict = new Dictionary<string, string>();
                retDict.Add("result", e.Message);
                return Request.CreateResponse<Dictionary<string, string>>(HttpStatusCode.OK, retDict);
            }
        }
        [HttpPost]
        public dynamic GetImageFromDir(TRequest json)
        {
            Dictionary<string, dynamic> defultdr = new Dictionary<string, dynamic>();
            if (json.dict != null)
            {
                foreach (var item in json.dict)
                {
                    defultdr.Add(item.Key, item.Value);
                }
            }
            string filepath = ImageDirPath + defultdr["@fileName"];
            byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
            return Request.CreateResponse<byte[]>(HttpStatusCode.OK, imageArray);
            
        }

    }
}
