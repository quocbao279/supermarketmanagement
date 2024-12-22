using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DACNwfrm1.Model
{
    public partial class FrmSaleAdd : Sample
    {
        public FrmSaleAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int cusID = 0;
        public int disID = 0;

        private void FrmSaleAdd_Load(object sender, EventArgs e)
        {
            string qry = @"Select cusID 'id' , cusName 'name' from Customer";
            MainClass.CBFill(qry, cbCustomer);
            qry = @"Select disID 'id' , disName 'name' from Discount";
            MainClass.CBFill(qry, cbDiscount);
            txtdate.Value = DateTime.Now;

            if (cusID > 0)
            {
                cbCustomer.SelectedValue = cusID;
                LoadForEdit();
                
            }
            if (disID > 0) {
                cbDiscount.SelectedValue = disID;
                LoadForEdit();
                GrandTotal();
            }
            LoadProductsFromDatabase();
        }

        public void AddItems(string id, string name, string price, Image pimage, string cost)
        {
            var w = new ucProduct()
            {
                PName = name,
                Price = price,
                PImage = pimage,
                PCost = cost,
                id = Convert.ToInt32(id)
            };
            flowLayoutPanel1.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;
                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    if (Convert.ToInt32(item.Cells["dgvproid"].Value) == wdg.id)
                    {
                        item.Cells["dgvqty"].Value = int.Parse(item.Cells["dgvqty"].Value.ToString()) + 1;
                        item.Cells["dgvamount"].Value = Convert.ToInt32(item.Cells["dgvqty"].Value) *
                                Convert.ToInt32(item.Cells["dgvprice"].Value);
                        GrandTotal();
                        return;
                    }
                }

                // if not find the product in gv
                guna2DataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.PName, 1, wdg.Price, wdg.Price, wdg.PCost });
                GrandTotal();
            };
        }

        private void GrandTotal()
        {
            double tot = 0; // Tổng tiền sau khi áp dụng khuyến mãi

            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                if (item.Cells["dgvprice"].Value == null || item.Cells["dgvqty"].Value == null)
                    continue; // Bỏ qua dòng nếu thiếu dữ liệu

                // Giá trị gốc của sản phẩm
                double originalPrice = double.Parse(item.Cells["dgvprice"].Value.ToString());
                double quantity = double.Parse(item.Cells["dgvqty"].Value.ToString());
                double rowAmount = originalPrice * quantity;

                double discount = 0; // Giá trị khuyến mãi được áp dụng cho dòng hiện tại

                if (cbDiscount.SelectedValue != null)
                {
                    try
                    {
                        // Lấy giá trị khuyến mãi từ bảng Discount
                        int disID = Convert.ToInt32(cbDiscount.SelectedValue);
                        string qry = $"SELECT disDetail FROM Discount WHERE disID = {disID}";
                        using (SqlCommand cmd = new SqlCommand(qry, MainClass.con))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    double discountValue = Convert.ToDouble(dt.Rows[0]["disDetail"]); // Khuyến mãi (%)
                                    discount = (originalPrice * discountValue / 100) * quantity; // Tổng giảm giá
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi lấy dữ liệu giảm giá: {ex.Message}");
                    }
                }

                double discountedPrice = rowAmount - discount; // Giá trị sau khi trừ khuyến mãi
                item.Cells["dgvamount"].Value = discountedPrice; // Cập nhật giá trị hiển thị trong cột dgvamount
                tot += discountedPrice; // Cộng tổng tiền
            }

            // Cập nhật tổng tiền vào nhãn lbltotal với đơn vị VND
            lbltotal.Text = tot.ToString("N0") + " VND";
        }


        private void LoadProductsFromDatabase()
        {
            // Xóa tất cả các sản phẩm trước khi tải
            flowLayoutPanel1.Controls.Clear();

            string qry = "Select * From Product";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Byte[] imageArray = (byte[])row["PImage"];
                    byte[] imageByteArray = imageArray;

                    AddItems(row["proID"].ToString(), row["pName"].ToString(), row["pPrice"].ToString(),
                        Image.FromStream(new MemoryStream(imageArray)), row["pCost"].ToString());

                    ucProduct product = new ucProduct()
                    {
                        PName = row["pName"].ToString(),
                        Price = row["pPrice"].ToString(),
                        PImage = Image.FromStream(new MemoryStream(imageArray)),
                        PCost = row["pCost"].ToString(),
                        id = Convert.ToInt32(row["proID"])
                    };

                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            txtdate.Value = DateTime.Now;
            cbCustomer.SelectedIndex = 0;
            cbCustomer.SelectedIndex = -1;
            cbDiscount.SelectedIndex = 0;
            cbDiscount.SelectedIndex = -1;
            lbltotal.Text = "0,000";
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in flowLayoutPanel1.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtsearch.Text.ToLower());
            }
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
                    DataRow row = dt.Rows[0];
                    foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                    {
                        if (Convert.ToInt32(item.Cells["dgvproid"].Value) == int.Parse(row["proID"].ToString()))
                        {
                            item.Cells["dgvqty"].Value = int.Parse(item.Cells["dgvqty"].Value.ToString()) + 1;
                            item.Cells["dgvamount"].Value = int.Parse(item.Cells["dgvqty"].Value.ToString()) *
                                int.Parse(item.Cells["dgvprice"].Value.ToString());
                            GrandTotal();
                            txtbarcode.Text = "";
                            return;
                        }
                    }
                    guna2DataGridView1.Rows.Add(new object[] { 0, row["proID"].ToString()
                            , row["pName"].ToString(), 1, row["pPrice"].ToString(),
                            row["pPrice"].ToString(), row["pCost"].ToString() });
                }

            }
        }

        
        private void btnSave_Click(object sender, EventArgs e)
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

            if (id == 0) //insert
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
            cmd1.Parameters.AddWithValue("@id", id);
            cmd1.Parameters.AddWithValue("@date", Convert.ToDateTime(txtdate.Value).Date);
            cmd1.Parameters.AddWithValue("@type", "SAL");
            cmd1.Parameters.AddWithValue("@supID", Convert.ToInt32(cbCustomer.SelectedValue));
            if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }

            if (id == 0)
            {
                id = Convert.ToInt32(cmd1.ExecuteScalar());
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
                    qry2 = @"Insert into tblDetails (dMainID, productID, qty, price, amount, cost) 
VALUES (@mID, @proID, @qty, @price, @amount, @cost)";
                }
                else
                {
                    qry2 = @"Update tblDetails Set dMainID = @mID, productID = @proID,
                            qty= @qty, price = @price,amount = @amount ,cost = @cost
                            where detailID = @id";
                }

                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.con);
                cmd2.Parameters.AddWithValue("@id", did);
                cmd2.Parameters.AddWithValue("@mID", id);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproid"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvqty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToInt32(row.Cells["dgvprice"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToInt32(row.Cells["dgvamount"].Value)); 
                cmd2.Parameters.AddWithValue("@cost", Convert.ToInt32(row.Cells["dgvcost"].Value));
                record += cmd2.ExecuteNonQuery();

            }
            if (record > 0)
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Lưu thành công.");

                id = 0;
                cusID = 0;
                disID = 0;
                txtdate.Value = DateTime.Now;
                cbCustomer.SelectedIndex = 0;
                cbCustomer.SelectedIndex = -1;
                cbDiscount.SelectedIndex = 0;
                cbDiscount.SelectedIndex = -1;
                guna2DataGridView1.Rows.Clear();
                lbltotal.Text = "0,000";
            }

        }

        private void LoadForEdit()
        {
            string qry = "Select * from tblDetails inner join product on proID= productID where dMainID = " + id + " ";
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
                pid = row["proID"].ToString();
                qty = row["qty"].ToString();
                cost = row["price"].ToString();
                amt = row["amount"].ToString();
                //cost = row["cost"].ToString();

                //0 for serial and id
                guna2DataGridView1.Rows.Add(did, pid, pname, qty, cost, amt, cost);
            }
        }



        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //delete
            if (guna2DataGridView1.CurrentCell?.OwningColumn?.Name == "dgvDel")
            {
                if (guna2DataGridView1.CurrentRow != null)
                {
                    try
                    {
                        int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                        guna2DataGridView1.Rows.Remove(guna2DataGridView1.CurrentRow);

                        string qry = $"DELETE FROM tblMian WHERE MainID = {id}";
                        string qry2 = $"DELETE FROM tblDetails WHERE dMainID = {id}";
                        Hashtable ht = new Hashtable();
                        MainClass.SQL(qry, ht);
                        MainClass.SQL(qry2, ht);

                        GrandTotal();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xoá: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Không dòng nào được chọn.");
                }
            }

        }

        private void txtdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrandTotal();
        }
    }
}

