namespace DACNwfrm1.Model
{
    partial class ucProduct
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.txtPic = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblpname = new System.Windows.Forms.Label();
            this.lblprice = new System.Windows.Forms.Label();
            this.guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPic)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.lblprice);
            this.guna2ShadowPanel1.Controls.Add(this.lblpname);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(15, 53);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(126, 140);
            this.guna2ShadowPanel1.TabIndex = 0;
            // 
            // txtPic
            // 
            this.txtPic.BackColor = System.Drawing.Color.Transparent;
            this.txtPic.Image = global::DACNwfrm1.Properties.Resources.Shop;
            this.txtPic.ImageRotate = 0F;
            this.txtPic.Location = new System.Drawing.Point(33, 3);
            this.txtPic.Name = "txtPic";
            this.txtPic.Size = new System.Drawing.Size(90, 90);
            this.txtPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.txtPic.TabIndex = 1;
            this.txtPic.TabStop = false;
            this.txtPic.UseTransparentBackground = true;
            this.txtPic.Click += new System.EventHandler(this.txtPic_Click);
            // 
            // lblpname
            // 
            this.lblpname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpname.Location = new System.Drawing.Point(3, 43);
            this.lblpname.Name = "lblpname";
            this.lblpname.Size = new System.Drawing.Size(120, 41);
            this.lblpname.TabIndex = 0;
            this.lblpname.Text = "Sản phẩm";
            this.lblpname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblprice
            // 
            this.lblprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprice.Location = new System.Drawing.Point(6, 97);
            this.lblprice.Name = "lblprice";
            this.lblprice.Size = new System.Drawing.Size(117, 28);
            this.lblprice.TabIndex = 1;
            this.lblprice.Text = "200";
            this.lblprice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.txtPic);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Name = "ucProduct";
            this.Size = new System.Drawing.Size(153, 196);
            this.guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2PictureBox txtPic;
        private System.Windows.Forms.Label lblprice;
        private System.Windows.Forms.Label lblpname;
    }
}
