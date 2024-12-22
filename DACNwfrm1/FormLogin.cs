using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DACNwfrm1
{
    public partial class FormLogin : Sample
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            if (MainClass.IsValidUser(txtUser.Text, txtPass.Text) == true)
            {
                this.Hide();
                FrmMain frm = new FrmMain();
                frm.Show();
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                guna2MessageDialog1.Show("Tên đăng nhập hoặc mật khẩu không hợp lệ");
            }
           
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
            guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
            if (guna2MessageDialog1.Show("Bạn có chắc muốn thoát hệ thống?") == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {
            // Đóng tất cả các form trước khi thoát
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Close();
            }
            // Đảm bảo thoát ứng dụng
            Application.Exit();
            
        }
    }
}
