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
    public partial class FrmProductAdd : SampleAdd
    {
        public FrmProductAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int catID = 0;
        private void FrmProductAdd_Load(object sender, EventArgs e)
        {
            string qry = "SELECT catID 'id' , catName 'name' FROM Category";
            MainClass.CBFill(qry, cbCategory);

            if (id > 0)
            {
                qry = @"SELECT pExp FROM Product WHERE proID = @id";

                try
                {
                    using (SqlCommand cmd = new SqlCommand(qry, MainClass.con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        // Open connection if it's not already open
                        if (MainClass.con.State != System.Data.ConnectionState.Open)
                        {
                            MainClass.con.Open();
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    txtExp.Text = dr["pExp"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Dữ liệu lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Ensure the connection is closed
                    if (MainClass.con.State == System.Data.ConnectionState.Open)
                    {
                        MainClass.con.Close();
                    }
                }

                cbCategory.SelectedValue = catID;
                LoadImage();
            }
        }

        public string filePath = "";
        Byte[] imageByteArray;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png)|*.png; *jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtPic.Image = new Bitmap(filePath);
            }
        }

        private void LoadImage()
        {
            string qry = @"Select pImage from Product where proID = " + id + "";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Byte[] imageArray = (byte[])dt.Rows[0]["pImage"];
                byte[] imageByteArray = imageArray;
                txtPic.Image = Image.FromStream(new MemoryStream(imageArray));
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
            else
            {
                string qry = "";
                if (id == 0)
                {
                    qry = @"Insert into Product (pName, pCatID, pBarcode, pCost, pPrice, pImage, pExp) values
          (@name, @pCatID, @barcode, @cost, @saleprice, @image, @expiry)";
                }
                else
                {
                    qry = @"UPDATE Product set pName =@name,
                    pCatID =@pCatID,
                    pBarcode =@barcode,
                    pCost =@cost,
                    pPrice =@saleprice,
                    pImage =@image,
                    pExp = @expiry
                    where proID = @id";
                }

                Image temp = new Bitmap(txtPic.Image);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageByteArray = ms.ToArray();

                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@name", txtName.Text);
                ht.Add("@pCatID", Convert.ToInt32(cbCategory.SelectedValue));
                ht.Add("@barcode", txtbarcode.Text);
                ht.Add("@cost", Convert.ToDouble(txtcost.Text));
                ht.Add("@saleprice", Convert.ToDouble(txtprice.Text));
                ht.Add("@image", imageByteArray);
                ht.Add("@expiry", txtExp.Text);

                if (MainClass.SQL(qry, ht) > 0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Show("Lưu dữ liệu thành công");
                    id = 0;
                    txtName.Text = "";
                    txtbarcode.Text = "";
                    cbCategory.SelectedIndex = 0;
                    cbCategory.SelectedIndex = -1;
                    txtcost.Text = "";
                    txtprice.Text = "";
                    txtPic.Image = DACNwfrm1.Properties.Resources.Shop;
                    txtExp.Text = "";
                    txtName.Focus();
                }
            }
        }


    }
}
