using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DACNwfrm1.View
{
    public partial class FrmStock : Sample
    {
        public FrmStock()
        {
            InitializeComponent();
        }

        private void FrmStock_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvname);
            lb.Items.Add(dgvqty);
            lb.Items.Add(dgvPurdate);
            lb.Items.Add(dgvExpdate);


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

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }
    }
}
