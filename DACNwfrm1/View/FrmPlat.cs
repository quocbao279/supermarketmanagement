using DACNwfrm1.Model;
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
    public partial class FrmPlat : Sample
    {
        public FrmPlat()
        {
            InitializeComponent();
        }

        private void FrmPlat_Load(object sender, EventArgs e)
        {

        }

        /*private void LocationPanel_Click(object sender, EventArgs e)
        {
            // Lấy panel được click
            Guna.UI2.WinForms.Guna2CustomGradientPanel clickedPanel = sender as Guna.UI2.WinForms.Guna2CustomGradientPanel;

            if (clickedPanel != null)
            {
                // Lấy tên hoặc ID của vị trí
                string locationID = clickedPanel.Name;

                // Hiển thị form thêm sản phẩm
                FrmAddsheft frm = new FrmAddsheft(locationID);
                frm.ShowDialog();

                // Cập nhật thông tin sau khi thêm sản phẩm (nếu cần)
                UpdateLocationInfo(locationID, "Sản phẩm mới");
            }
        }*/
    }
}
