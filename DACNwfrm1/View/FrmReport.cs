using DACNwfrm1.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DACNwfrm1.View
{
    public partial class FrmReport : Sample
    {
        public FrmReport()
        {
            InitializeComponent();
        }

        private DataTable dTable(string qry)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection("Data Source=QUOCBAO\\MSSQL; Database=STDB; Trusted_Connection=True;")) 
            {
                SqlCommand cmd = new SqlCommand(qry, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(dt);
            }
            return dt;
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            string qry = @"Select* from Product p inner join Category c on p.pCatID = c.catID";
            DataTable dt = dTable(qry);
            FrmPrint frm = new FrmPrint();
            rptProduct cr = new rptProduct();

            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            string qry = @"
SELECT 
    proID, 
    pName,
    COALESCE(
        (SELECT SUM(qty) 
         FROM tblDetails d 
         INNER JOIN tblMian m ON m.MainID = d.dMainID 
         WHERE m.mType = 'PUR' AND d.productID = proID), 0) - 
    COALESCE(
        (SELECT SUM(qty) 
         FROM tblDetails d 
         INNER JOIN tblMian m ON m.MainID = d.dMainID 
         WHERE m.mType = 'SAL' AND d.productID = proID), 0) AS Quantity,
    COALESCE(
        (SELECT TOP 1 m.mdate 
         FROM tblDetails d 
         INNER JOIN tblMian m ON m.MainID = d.dMainID 
         WHERE m.mType = 'PUR' AND d.productID = proID 
         ORDER BY m.mdate DESC), NULL) AS PurDate,
    DATEADD(DAY, DATEDIFF(DAY, 0, COALESCE(
        (SELECT TOP 1 m.mdate 
         FROM tblDetails d 
         INNER JOIN tblMian m ON m.MainID = d.dMainID 
         WHERE m.mType = 'PUR' AND d.productID = proID 
         ORDER BY m.mdate DESC), NULL)) + 
        CASE 
            WHEN pExp LIKE '%tháng%' THEN CAST(REPLACE(pExp, 'tháng', '') AS INT) * 30
            WHEN pExp LIKE '%năm%' THEN CAST(REPLACE(pExp, 'năm', '') AS INT) * 365
            ELSE 0
        END, 0) AS ExpDate
FROM Product";

            
            FrmPrint frm = new FrmPrint();
            rptStock cr = new rptStock();

            cr.SetDataSource(dTable(qry));
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT * FROM Customer ORDER BY cusID DESC";
            DataTable dt = dTable(qry);
            FrmPrint frm = new FrmPrint();
            rptCustomer cr = new rptCustomer();

            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT * FROM Supplier ORDER BY supID DESC";
            DataTable dt = dTable(qry);
            FrmPrint frm = new FrmPrint();
            rptSupplier cr = new rptSupplier();

            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT 
    tm.mdate AS OrderDate,        
    p.pName AS ProductName,       
    c.cusName AS CustomerName,    
    td.qty AS Quantity,           
    td.price AS UnitPrice,        
    td.amount AS TotalAmount      
FROM 
    tblMian tm
JOIN 
    tblDetails td ON tm.MainID = td.dMainID
JOIN 
    Product p ON td.productID = p.proID
JOIN 
    Customer c ON tm.mSupCusID = c.cusID
WHERE 
    tm.mType = 'SAL';             
";

            FrmPrint frm = new FrmPrint();
            rptSale cr = new rptSale();

            cr.SetDataSource(dTable(qry));
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }
    }
}
