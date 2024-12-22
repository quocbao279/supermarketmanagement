using DACNwfrm1.Model;
using System;
using System.Collections;
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
    public partial class FrmSaleView : SampleView
    {
        public FrmSaleView()
        {
            InitializeComponent();
        }

        private void FrmSaleView_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new FrmSaleAdd());
            LoadData();
        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvdate);
            lb.Items.Add(dgvCusID);
            lb.Items.Add(dgvCustomer);
            lb.Items.Add(dgvAmount);

            string qry = @"select dMainID , mdate , m.mSupCusID,c.cusName, SUM(d.amount) from tblMian m 
                            inner join tblDetails d on d.dMainID = m.MainID
                            inner join Customer c on c.cusID = m.mSupCusID
                            where m.mType = 'SAL' and cusName like '%" + txtSearch.Text + "%'" +
                           " group by dMainID , mdate , m.mSupCusID,c.cusName ";
            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //view
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                FrmSaleAdd frm = new FrmSaleAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.cusID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvCusID"].Value);
                MainClass.BlurBackground(frm);
                LoadData();
                
            }

            //delete
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvDel")
            {
                //confirm delete
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                if (guna2MessageDialog1.Show("Bạn có chắc muốn xoá dòng này?") == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);

                    string qry = "Delete from tblMian where MainID = " + id + "";
                    string qry2 = "Delete from tblDetails where dMainID = " + id + "";
                    Hashtable ht = new Hashtable();
                    MainClass.SQL(qry, ht);
                    if (MainClass.SQL(qry2, ht) > 0)
                    {
                        guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                        guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                        guna2MessageDialog1.Show("xoá thành công..");
                        LoadData();
                    }
                }
            }
        }

    }
}
