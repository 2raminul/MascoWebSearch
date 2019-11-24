using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

namespace TWebApiSearch.Models
{
    public static class ReportFormat
    {
        public static dynamic getReport(string reportType, System.Data.DataSet ds, DocumentDownloadSection param)
        {
            string mimeType = "", extension = "";
            Stream stream = new MemoryStream();
            ReportClass report = new ReportClass();
            bool isValid = true;
            var rptSource = ds;
            if (string.IsNullOrEmpty(param.OperationName))
            {
                isValid = false;
            }
            if (isValid)
            {
                string strRptPath = string.Empty;
                //if (param.OperationName.Equals("rptCostCenterWiseIncomeTaxYearly") || param.OperationName.Equals("rptCostCenterWiseIncomeNonTaxYearly"))
                //{
                //    strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/WebReport/Reports/") + param.OperationName + ".rpt";
                //}
                //else
                //{
                    
                //}

                strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/WebReport/Reports/") + param.OperationName + ".rpt";

                //dsReport = rptSource.Copy();
                report.FileName = strRptPath;
                //===========Data set=====
                //var path = "~//WebReport//DataSet//dsLeaveForm.xsd";
                //ds.WriteXmlSchema(HostingEnvironment.MapPath(path));

                report.Load(strRptPath);
                if (ds != null && ds.GetType().ToString() != "System.String")
                {
                    report.SetDataSource(ds);
                    if (param.OperationName.Equals("rptCostCenterWiseIncomeTaxYearly") || param.OperationName.Equals("rptCostCenterWiseIncomeNonTaxYearly"))
                    {
                        report.SetParameterValue("Option", param.IncomeTaxYear);
                        report.SetParameterValue("Option1", param.FromMonth);
                        report.SetParameterValue("Option2", param.ToMonth);
                        report.SetParameterValue("Option3", param.AYear);
                    }
                }


                if (reportType == "pdf")
                {
                    mimeType = "application/pdf";
                    extension = ".pdf";
                    stream = report.ExportToStream(ExportFormatType.PortableDocFormat);
                }
                else if (reportType == "excel")
                {
                    mimeType = "application/vnd.ms-excel";
                    extension = ".xls";
                    stream = report.ExportToStream(ExportFormatType.Excel);
                }
                else if (reportType == "word")
                {
                    mimeType = "application/msword";
                    extension = ".doc";
                    stream = report.ExportToStream(ExportFormatType.WordForWindows);
                }
                else if (reportType == "RichText")
                {
                    mimeType = "application/rtf";
                    extension = ".rtf";
                    stream = report.ExportToStream(ExportFormatType.RichText);
                }
                report.Close();
                report.Dispose();
              
                return new { mimeType = mimeType, stream = stream, extension = extension };
            }
            else
            {
                return "<H2>Nothing Found; </H2>";
            }
        }

    }
}