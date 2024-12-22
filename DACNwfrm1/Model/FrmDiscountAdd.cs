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
using System.Xml.Linq;

namespace DACNwfrm1.Model
{
    public partial class FrmDiscountAdd : SampleAdd
    {
        public FrmDiscountAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmDiscountAdd_Load(object sender, EventArgs e)
        {
            txtondate.Value = DateTime.Now; 
            txtoutdate.Value = DateTime.Now;
            
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
                    qry = @"INSERT INTO Discount (disName, disDetail, disOndate, disOutdate)
                    VALUES (@name, @detail, @ondate, @outdate)";
                }
                else
                {
                    qry = @"UPDATE Discount SET disName = @name,
                                         disDetail = @detail,
                                         disOndate = @ondate,
                                         disOutdate = @outdate
                     WHERE disID = @id";
                }

                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@name", txtname.Text);
                ht.Add("@detail", txtValue.Text);
                ht.Add("@ondate", txtondate.Value); 
                ht.Add("@outdate", txtoutdate.Value);

                if (MainClass.SQL(qry, ht) > 0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Show("Lưu dữ liệu thành công");
                    id = 0;
                    txtname.Clear();
                    txtValue.Clear();
                    txtondate.Value = DateTime.Now;
                    txtoutdate.Value = DateTime.Now;
                    txtname.Focus();
                }
            }
        }
    }
}
