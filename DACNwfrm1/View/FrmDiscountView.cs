using DACNwfrm1.Model;
using System;
using System.Collections;
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
    public partial class FrmDiscountView : SampleView
    {
        public FrmDiscountView()
        {
            InitializeComponent();
        }

        private void FrmDiscount_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new FrmDiscountAdd());
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
            lb.Items.Add(dgvname);
            lb.Items.Add(dgvexclu);
            lb.Items.Add(dgvondate);
            lb.Items.Add(dgvoutdate);


            string qry = @"SELECT * FROM Discount
                             WHERE disName LIKE '%" + txtSearch.Text + "%' ORDER BY disID DESC";
            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //update
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                FrmDiscountAdd frm = new FrmDiscountAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtname.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvname"].Value);
                frm.txtdetail.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvexclu"].Value);
                frm.txtondate.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvondate"].Value);
                frm.txtoutdate.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvoutdate"].Value);
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
                    string qry = "Delete from Discount where disID = " + id + "";
                    Hashtable ht = new Hashtable();
                    if (MainClass.SQL(qry, ht) > 0)
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
