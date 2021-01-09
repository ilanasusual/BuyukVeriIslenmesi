using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

namespace BigDataHw1_152120161034
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public DataSet GetData(string b, string a)
        {
            string strConn = a;
            SqlConnection conn = new SqlConnection(strConn);
            DataSet ds = new DataSet();
            conn.Open();
            string str = b;
            String cmdStr = str;
            SqlCommand cmd = new SqlCommand(cmdStr, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            conn.Close();

            return ds;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            tbConnStr.ReadOnly = true;
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
           // ReportViewer1.LocalReport.ReportPath = "/Report1.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("ReportParameterMin1", tbMin.Text));
            reportParameters.Add(new ReportParameter("ReportParameterMax1", tbMax.Text));
            this.ReportViewer1.LocalReport.SetParameters(reportParameters);
            //this.ReportViewer1.ref

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = ("Report1.rdlc");
            string str = $"SELECT ProductID, Name, ProductNumber, ListPrice FROM Production.Product Where ListPrice > {tbMin.Text} and ListPrice <{tbMax.Text}";
            string strConn = tbConnStr.Text;
            DataSet ttt = GetData(str, strConn);


            ReportViewer1.LocalReport.DataSources.Clear();

            if (ttt.Tables[0].Rows.Count > 0)
            {
                ReportDataSource item = new ReportDataSource("DataSet1", ttt.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Add(item);
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}