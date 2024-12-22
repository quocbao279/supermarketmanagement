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

namespace DACNwfrm1.Model
{
    public partial class FrmPurchaseAdd : SampleAdd
    {
        public FrmPurchaseAdd()
        {
            InitializeComponent();
        }

        public int mainID = 0;
        public int supID = 0;
        private void FrmPurchaseAdd_Load(object sender, EventArgs e)
        {
            txtdate.Value = DateTime.Now;
            //stop before product load from database
            cbProduct.SelectedIndexChanged -= new EventHandler(cbProduct_SelectedIndexChanged);
            string qry1 = "SELECT proID 'id' , pName 'name' FROM Product;";
            string qry2 = "SELECT supID 'id' , supName 'name' FROM Supplier";

            MainClass.CBFill(qry1, cbProduct);
            MainClass.CBFill(qry2, cbSupplier);

            if (supID > 0)
            {
                cbSupplier.SelectedValue = supID;
                LoadForEdit();
            }

            //re enable it
            //at load we need to stop production selection change event
            cbProduct.SelectedIndexChanged += new EventHandler(cbProduct_SelectedIndexChanged);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProduct.SelectedIndex != -1)
            {
                txtquanty.Text = "";
                GetDetail();
            }
        }

        private void GetDetail()
        {
            string qry = "Select * from Product where proID = " + Convert.ToInt32(cbProduct.SelectedValue) + "";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtcost.Text = dt.Rows[0]["pPrice"].ToString();
                Calculate();
            }
        }

        private void Calculate()
        {
            double qty = 0;
            double cost = 0;
            double amt = 0;

            double.TryParse(txtquanty.Text, out qty);
            double.TryParse(txtcost.Text, out cost);

            amt = qty * cost;
            txtamount.Text = amt.ToString();
        }

        private void txtquanty_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string qry = "select * from Product where pBarcode like '" + txtbarcode.Text + "'";
                SqlCommand cmd = new SqlCommand(qry, MainClass.con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cbProduct.SelectedValue = Convert.ToInt32(dt.Rows[0]["proID"].ToString());
                    Calculate();
                    txtbarcode.Text = "";
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string pid;
            string pname;
            string qty;
            string cost;
            string amt;

            pname = cbProduct.Text;
            pid = cbProduct.SelectedValue.ToString();
            qty = txtquanty.Text;
            cost = txtcost.Text;
            amt = txtamount.Text;

            //0 for serial and id
            guna2DataGridView1.Rows.Add(0, 0, pid, pname, qty, cost, amt);
            cbProduct.SelectedIndex = 0;
            cbProduct.SelectedIndex = -1;
            txtquanty.Text = "";
            txtcost.Text = "";
            txtamount.Text = "";
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public override void btnSave_Click(object sender, EventArgs e)
        {
            if (MainClass.Validation(this) == false)
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                guna2MessageDialog1.Show("Đã có lỗi!");
                return;
            }

            string qry1 = ""; //for main table
            string qry2 = ""; //for details table
            int record = 0;

            if (mainID == 0) //insert
            {
                qry1 = @"INSERT INTO tblMian VALUES(@date,@type,@supID);
                        SELECT SCOPE_IDENTITY()";
            }
            else
            {
                qry1 = @"UPDATE tblMian SET mdate = @date, mType= @type, mSupCusID =@supID
                            WHERE MainID = @id";
            }

            SqlCommand cmd1 = new SqlCommand(qry1, MainClass.con);
            cmd1.Parameters.AddWithValue("@id", mainID);
            cmd1.Parameters.AddWithValue("@date", Convert.ToDateTime(txtdate.Value).Date);
            cmd1.Parameters.AddWithValue("@type", "PUR");
            cmd1.Parameters.AddWithValue("@supID", Convert.ToInt32(cbSupplier.SelectedValue));
            if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }

            if (mainID == 0)
            {
                mainID = Convert.ToInt32(cmd1.ExecuteScalar());
            }
            else
            {
                cmd1.ExecuteNonQuery();
            }

            //insert details table
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                int did = Convert.ToInt32(row.Cells["dgvid"].Value);

                if (did == 0) //insert
                {
                    qry2 = @"Insert into tblDetails Values(@mID,@proID,@qty,@price,@amount,@cost)"; //@cost
                }
                else
                {
                    qry2 = @"Update tblDetails Set dMainID = @mID, productID = @proID,
                            qty= @qty, price = @price,amount = @amount, cost = @cost      
                            where detailID = @id"; 
                }

                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.con);
                cmd2.Parameters.AddWithValue("@id", did);
                cmd2.Parameters.AddWithValue("@mID", mainID);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproid"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvqty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToInt32(row.Cells["dgvcost"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToInt32(row.Cells["dgvamount"].Value));
                cmd2.Parameters.AddWithValue("@cost", Convert.ToInt32(row.Cells["dgvcost"].Value));
                record += cmd2.ExecuteNonQuery();

            }
            if (record > 0)
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Lưu thành công.");

                mainID = 0;
                supID = 0;
                txtdate.Value = DateTime.Now;
                cbSupplier.SelectedIndex = 0;
                cbSupplier.SelectedIndex = -1;
                guna2DataGridView1.Rows.Clear();
            }

        }

        private void LoadForEdit()
        {
            string qry = "Select * from tblDetails inner join product on proID= productID where dMainID = " + mainID + " ";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                string did;
                string pid;
                string pname;
                string qty;
                string cost;
                string amt;

                did = row["detailID"].ToString();
                pname = row["pName"].ToString();
                pid = row["productID"].ToString();
                qty = row["qty"].ToString();
                cost = row["price"].ToString();
                amt = row["amount"].ToString();

                //0 for serial and id
                guna2DataGridView1.Rows.Add(0, did, pid, pname, qty, cost, amt);
            }
        }

        private void txtcost_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
