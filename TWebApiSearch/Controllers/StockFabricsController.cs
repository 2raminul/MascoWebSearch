using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TWebApiSearch.App_Start;
using TWebApiSearch.BLL;
using TWebApiSearch.Models;

namespace TWebApiSearch.Controllers
{
    [Startup.NoCacheAttribute]
    public class StockFabricsController : Controller
    {
        // GET: StockFabrics
        [AuthorizeActionFilter]
        public ActionResult StockFabBalance()
        {
            if ((Session["UserName"]) != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public JsonResult GetBuyerList()
        {
            var buyer = Models.DAL.GetDataTable("usp_BuyerName_Select", DBName.MerchandisingDB);

            var arr = new ArrayList();

            foreach (DataRow item in buyer.Rows)
            {
                var obj = new
                {
                    ID = item["BuyerId"].ToString(),
                    Text = item["BuyerName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult GetJobListByBuyer(string buyerId)
        {
            Dictionary<string,object> paramList=new Dictionary<string, object>();
            paramList.Add("@BuyerId",buyerId);
            var job = Models.DAL.GetDataTable("sp_JobByBuyer",paramList, DBName.MerchandisingDB);

            var arr = new ArrayList();

            foreach (DataRow item in job.Rows)
            {
                var obj = new
                {
                    ID = item["JobNo"].ToString(),
                    Text = item["JobInfo"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult GetOrderListByBuyer(string buyerId)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@BuyerId", buyerId);
            var job = Models.DAL.GetDataTable("sp_OrderByBuyer_Running", paramList, DBName.MerchandisingDB);

            var arr = new ArrayList();

            foreach (DataRow item in job.Rows)
            {
                var obj = new
                {
                    ID = item["OrderId"].ToString(),
                    Text = item["OrderNo"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult GetStyleListByBuyer(string buyerId)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@BuyerId", buyerId);
            var job = Models.DAL.GetDataTable("sp_StyleByBuyer", paramList, DBName.MerchandisingDB);

            var arr = new ArrayList();

            foreach (DataRow item in job.Rows)
            {
                var obj = new
                {
                    ID = item["StyleId"].ToString(),
                    Text = item["StyleInfo"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult LoadOrderById(int iD, string type, Int32 buyerId)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@iD", iD);
            paramList.Add("@type", type);
            paramList.Add("@BuyerId", buyerId);
            var job = Models.DAL.GetDataTable("sp_LoadOrderNo_ViseVersa", paramList, DBName.KnittingDB);

            var arr = new ArrayList();

            foreach (DataRow item in job.Rows)
            {
                var obj = new
                {
                    ID = item["OrderId"].ToString(),
                    Text = item["OrderNo"].ToString(),
                    Count = item["CountOrder"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult LoadStyleById(int iD, string type, Int32 buyerId)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@iD", iD);
            paramList.Add("@type", type);
            paramList.Add("@BuyerId", buyerId);
            var job = Models.DAL.GetDataTable("sp_LoadStyleNo_ViseVersa", paramList, DBName.KnittingDB);

            var arr = new ArrayList();

            foreach (DataRow item in job.Rows)
            {
                var obj = new
                {
                    ID = item["StyleId"].ToString(),
                    Text = item["StyleInfo"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }

        public JsonResult GetFabricsType(string buyerId,string orderId,string jobId)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@buyerId", buyerId);
            paramList.Add("@orderId", orderId);
            paramList.Add("@jobId", jobId);
            var fab = Models.DAL.GetDataTable("sp_LoadItemType_fromKWO", paramList, DBName.KnittingDB);

            var arr = new ArrayList();

            foreach (DataRow item in fab.Rows)
            {
                var obj = new
                {
                    ID = item["IFID"].ToString(),
                    Text = item["IFName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult GetGSMList(string buyerId, string orderId, string jobId)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@buyerId", buyerId);
            paramList.Add("@orderId", orderId);
            paramList.Add("@jobId", jobId);
            var gsm = Models.DAL.GetDataTable("sp_LoadGSMList_fromKWO", paramList, DBName.KnittingDB);

            var arr = new ArrayList();

            foreach (DataRow item in gsm.Rows)
            {
                var obj = new
                {
                    ID = item["ISZID"].ToString(),
                    Text = item["ItemSizeName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult GetYarnType()
        {
            var yarn = Models.DAL.GetDataTable("sp_LoadItemHead_ItemWise", DBName.KnittingDB);

            var arr = new ArrayList();

            foreach (DataRow item in yarn.Rows)
            {
                var obj = new
                {
                    ID = item["IHID"].ToString(),
                    Text = item["IHName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult Getcount()
        {
            var count = Models.DAL.GetDataTable("sp_SelectYarnCount", DBName.MerchandisingDB);

            var arr = new ArrayList();

            foreach (DataRow item in count.Rows)
            {
                var obj = new
                {
                    ID = item["ISZID"].ToString(),
                    Text = item["ItemSizeName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult GetCompositionList(string buyerId,string orderId,string jobId,string fabId,string gsmId,string ihId)
        {
            Dictionary<string,object> paramList=new Dictionary<string, object>();
            paramList.Add("@BuyerID",buyerId);
            paramList.Add("@OrderId", orderId);
            paramList.Add("@JobId", jobId);
            paramList.Add("@FabId", fabId);
            paramList.Add("@GsmId", gsmId);
            paramList.Add("@IHID", ihId);
            var composition = Models.DAL.GetDataTable("CompositionLoadFilterd",paramList, DBName.SCM);

            var arr = new ArrayList();

            foreach (DataRow item in composition.Rows)
            {
                var obj = new
                {
                    ID = item["IHID"].ToString(),
                    Text = item["Specification"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1 };
        }
        public JsonResult GetTableData(string unitNo,string buyerId, string orderId, string jobId,
            string fabType, string gsmId, string ihId,string countId,string composition,string yarnStatus)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@UnitNo", unitNo);
            paramList.Add("@BuyerId", buyerId);
            paramList.Add("@OrderId", orderId);
            paramList.Add("@JobNo", jobId);
            paramList.Add("@FabType", fabType);
            paramList.Add("@GsmId", gsmId);
            paramList.Add("@YarnType", ihId);
            paramList.Add("@CountId", countId);
            paramList.Add("@Composition", composition);
            paramList.Add("@YarnStatus", yarnStatus);
            var data = Models.DAL.GetDataTable("GetStockFabricsBalanceView", paramList, DBName.KnittingDB);

            var arr = new ArrayList();

            foreach (DataRow row in data.Rows)
            {
                var obj = new
                {
                    BuyerName=row["BuyerName"].ToString(),
                    Job=row["JobInfo"].ToString(),
                    Order=row["OrderNo"].ToString(),
                    FabType=row["IFName"].ToString(),
                    GSM=row["ItemSizeName"].ToString(),
                    YarnName=row["YarnName"].ToString(),
                    Lot=row["LotNo"].ToString(),
                    Brand=row["ItemBrandName"].ToString(),
                    SL=row["StitchLengthDescription"].ToString(),
                    FinDia=row["FinishedDiaName"].ToString(),
                    FabColor=row["FabColor"].ToString(),
                    UOM=row["UOMDetails"].ToString(),
                    Qty=row["Qty"].ToString(),
                    Status=row["Status"].ToString(),
                    Unit=row["UnitEName"].ToString(),
                    Location=row["StoreName"].ToString()
                };
                arr.Add(obj);

            }


            var obj1 = new { Data = arr };

            return new JsonResult { Data = obj1,MaxJsonLength = 1000000};
        }
    }
}