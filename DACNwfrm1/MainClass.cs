using Guna.UI2.WinForms;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace DACNwfrm1
{
    class MainClass
    {
        public static readonly string con_string = "Data Source=QUOCBAO\\MSSQL; Database=STDB; Trusted_Connection=True;"; //Initial Catalog  
        public static SqlConnection con = new SqlConnection(con_string);

        public static void StopBuffering(Guna.UI2.WinForms.Guna2Panel ctr, bool doubleBuffer)
        {
            try
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null, ctr, new object[] { doubleBuffer });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        //method to check user validation
        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;

            string qry = @"SELECT * FROM users WHERE uUsername = @user AND uPass = @pass";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0) 
            {
             isValid = true;
                USER = dt.Rows[0]["uName"].ToString();
                ROLE = dt.Rows[0]["uRole"].ToString();

                Byte[] imageArray = (byte[])dt.Rows[0]["uImage"];
                byte[] imageByteArry = imageArray;
                IMG = System.Drawing.Image.FromStream(new MemoryStream(imageArray));
            }

            return isValid;
        }

        // Thuộc tính để lưu vai trò
        private static string role;
        public static string ROLE
        {
            get { return role; }
            private set { role = value; }
        }

        //create property for username
        public static string user;
        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        public static System.Drawing.Image img;
        public static System.Drawing.Image IMG
        {
            get { return img; }
            private set { img = value; }
        }

        //method for curd operation
        public static int SQL(string qry, Hashtable ht)
        {
            int res = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;

                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (con.State == ConnectionState.Closed) { con.Open(); }
                res = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                con.Close();
            }
            return res;
        }

        //for loading data from database
        public static void LoadData(string qry, DataGridView gv, System.Windows.Forms.ListBox lb)
        {
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string colNam1 = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colNam1].DataPropertyName = dt.Columns[i].ToString();
                }

                gv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }

        public static void SetRolePermissions(Form form)
        {
            foreach (Control ctrl in form.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.ToString() != "")
                {
                    string requiredRole = ctrl.Tag.ToString();

                    // Nếu vai trò của người dùng không phù hợp, ẩn control
                    if (!ROLE.Equals(requiredRole, StringComparison.OrdinalIgnoreCase))
                    {
                        ctrl.Visible = false;
                    }
                }
            }
        }

        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;

            foreach (DataGridViewRow row in gv.Rows) 
            {
            count++;
            row.Cells[0].Value = count;
            }
        }

        public static void  BlurBackground(Form Model)
        {
            Form Background = new Form();
            using (Model)
            {
                Background.StartPosition = FormStartPosition.Manual;
                Background.FormBorderStyle = FormBorderStyle.None;
                Background.Opacity = 0.5d;
                Background.BackColor = Color.Black;
                Background.Size = FrmMain.Instance.Size;
                Background.Location = FrmMain.Instance.Location;
                Background.ShowInTaskbar = false;
                Background.Show();
                Model.Owner = Background;
                Model.ShowDialog(Background);
                Background.Dispose();
            }
        }

        //for cb fill
        public static void CBFill(string qry,ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }

        public static bool Validation(Form F)
        {
            bool isValid = false;

            int count = 0;

            foreach (Control c in F.Controls)
            {
                //using tag of the control to check if we want to validate it or not
                if (Convert.ToString(c.Tag) !="" && Convert.ToString(c.Tag) != null)
                { 
                    //for textbox
                if(c is Guna.UI2.WinForms.Guna2TextBox) 
                    {
                        Guna.UI2.WinForms.Guna2TextBox t = (Guna.UI2.WinForms.Guna2TextBox)c;
                        if(t.Text.Trim() == "") 
                        { 
                        t.BorderColor = Color.Red;
                        t.FocusedState.BorderColor = Color.Red;
                        t.HoverState.BorderColor = Color.Red;
                        count++;
                        }
                        else
                        {
                            t.BorderColor = Color.FromArgb(213, 218, 223);
                            t.FocusedState.BorderColor = Color.FromArgb(50, 205, 50);
                            t.HoverState.BorderColor = Color.FromArgb(50, 205, 50);
                        }
                    }
                    //for combobox
                    if (c is Guna.UI2.WinForms.Guna2ComboBox)
                    {
                        Guna.UI2.WinForms.Guna2ComboBox t = (Guna.UI2.WinForms.Guna2ComboBox)c;
                        if (t.SelectedIndex ==-1)
                        {
                            t.BorderColor = Color.Red;
                            t.FocusedState.BorderColor = Color.Red;
                            t.HoverState.BorderColor = Color.Red;
                            count++;
                        }
                        else
                        {
                            t.BorderColor = Color.FromArgb(213, 218, 223);
                            t.FocusedState.BorderColor = Color.FromArgb(50, 205, 50);
                            t.HoverState.BorderColor = Color.FromArgb(50, 205, 50);
                        }
                    }
                }

                if (count == 0)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
