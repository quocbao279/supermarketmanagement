using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DACNwfrm1.Model
{
    public partial class ucProduct : UserControl
    {
        public event EventHandler onSelect = null;
        public ucProduct()
        {
            InitializeComponent();
        }

        private void txtPic_Click(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);
        }

        private void lblpname_Click(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);
        }

        public int id { get; set; }
        public string PCost { get; set; }
        public string PName
        {
            get { return lblpname.Text; }
            set { lblpname.Text = value; }
        }

        public string Price
        {
            get { return lblprice.Text; }
            set { lblprice.Text = value; }
        }

        public Image PImage
        {
            get { return txtPic.Image; }
            set { txtPic.Image = value; }
        }
    }
}
