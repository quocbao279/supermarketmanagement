using DACNwfrm1.View;
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
    public partial class FrmMain : Sample
    {
        static FrmMain _obj;
        
        public static FrmMain Instance
        {
            get
            {
                if (_obj == null || _obj.IsDisposed)
                {
                    _obj = new FrmMain();
                }
                return _obj;
            }
        }
        public FrmMain()
        {
            InitializeComponent();
            this.guna2MessageDialog1 = new Guna.UI2.WinForms.Guna2MessageDialog();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            _obj = this;
            

            MainClass.SetRolePermissions(this);
            //lblRole.Text = $"Vai trò: {MainClass.ROLE}";
            lbluser.Text = MainClass.USER;
            guna2CirclePictureBox1.Image = MainClass.img;
            btnHome.PerformClick();
        }

        public void AddControls(Form F)
        {
            this.CenterPanel.Controls.Clear();
            F.Dock = DockStyle.Fill;
            F.TopLevel = false;
            CenterPanel.Controls.Add(F);
            F.Show();
        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddControls(new FrmDashboard());
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            if (MainClass.ROLE == "Admin")
            {
                AddControls(new FrmUserView());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager")
            {
                AddControls(new FrmCategoryView());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager")
            {
                AddControls(new FrmSupplierView());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager" || MainClass.ROLE == "Employee")
            {
                AddControls(new FrmCustomerView());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager")
            {
                AddControls(new FrmProductView());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager" || MainClass.ROLE == "Employee")
            {
                AddControls(new FrmPurcharseView());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Employee")
            {
                AddControls(new FrmSaleView());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                Application.Exit(); // Thoát hoàn toàn ứng dụng khi không còn form nào mở
            }
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager" || MainClass.ROLE == "Employee")
            {
                AddControls(new FrmStock());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin();
            loginForm.Show();

            // Đóng form hiện tại
            //Application.Exit();
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager")
            {
                AddControls(new FrmReport());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            if (MainClass.ROLE == "Admin" || MainClass.ROLE == "Manager")
            {
                AddControls(new FrmDiscountView());
            }
            else
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Bạn không có quyền truy cập tính năng này!");
            }
        }
    }
}
