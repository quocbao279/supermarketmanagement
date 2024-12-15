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
    public partial class FrmProductView : SampleView
    {
        public FrmProductView()
        {
            InitializeComponent();
        }

        private void FrmProductView_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager")
            {
                // Proceed with the add product functionality
                MainClass.BlurBackground(new FrmProductAdd());
                LoadData();
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
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
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvCategory);
            lb.Items.Add(dgvbarcode);
            lb.Items.Add(dgvcost);
            lb.Items.Add(dgvsale);
            lb.Items.Add(dgvExp);


            string qry = @"select proID, pName, pCatID, catName, pBarcode, pCost, pPrice, pExp from Product
                            inner join Category on catID = pCatID
                         where pName like '%" + txtSearch.Text + "%' order by proID desc";
            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //update
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager")
                {
                    FrmProductAdd frm = new FrmProductAdd();
                    frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                    frm.txtName.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvname"].Value);
                    frm.catID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvcatID"].Value);
                    frm.txtbarcode.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvbarcode"].Value);
                    frm.txtcost.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvcost"].Value);
                    frm.txtprice.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvsale"].Value);
                    frm.txtExp.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvexp"].Value);

                    MainClass.BlurBackground(frm);
                    LoadData();
                }
                else
                {
                    // Notify the user they don't have permission
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                    guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
                }
            }


            //delete
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvDel")
            {
                if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager")
                {
                    // Confirm delete
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    if (guna2MessageDialog1.Show("Bạn có chắc muốn xoá dòng này?") == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                        string qry = "Delete from Product where proID = " + id;
                        Hashtable ht = new Hashtable();
                        if (MainClass.SQL(qry, ht) > 0)
                        {
                            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                            guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                            guna2MessageDialog1.Show("Xoá thành công.");
                            LoadData();
                        }
                    }
                }
                else
                {
                    // Notify the user they don't have permission
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                    guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
                }
            }
        }
    }
}
