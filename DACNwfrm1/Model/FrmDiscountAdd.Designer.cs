namespace DACNwfrm1.Model
{
    partial class FrmDiscountAdd
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtondate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txtname = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtoutdate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txtdetail = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(175, 25);
            this.label1.Text = "Thêm chương trình";
            // 
            // txtondate
            // 
            this.txtondate.AutoRoundedCorners = true;
            this.txtondate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
            this.txtondate.BorderRadius = 17;
            this.txtondate.BorderThickness = 1;
            this.txtondate.Checked = true;
            this.txtondate.FillColor = System.Drawing.Color.White;
            this.txtondate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtondate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtondate.Location = new System.Drawing.Point(54, 149);
            this.txtondate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.txtondate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.txtondate.Name = "txtondate";
            this.txtondate.Size = new System.Drawing.Size(179, 36);
            this.txtondate.TabIndex = 14;
            this.txtondate.Value = new System.DateTime(2024, 11, 6, 22, 9, 19, 222);
            // 
            // txtname
            // 
            this.txtname.Animated = true;
            this.txtname.AutoRoundedCorners = true;
            this.txtname.BorderRadius = 23;
            this.txtname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtname.DefaultText = "";
            this.txtname.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtname.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtname.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtname.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtname.FocusedState.BorderColor = System.Drawing.Color.LimeGreen;
            this.txtname.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtname.HoverState.BorderColor = System.Drawing.Color.LimeGreen;
            this.txtname.Location = new System.Drawing.Point(54, 224);
            this.txtname.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtname.Name = "txtname";
            this.txtname.PasswordChar = '\0';
            this.txtname.PlaceholderText = "";
            this.txtname.SelectedText = "";
            this.txtname.Size = new System.Drawing.Size(255, 48);
            this.txtname.TabIndex = 15;
            this.txtname.Tag = "";
            this.txtname.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 23);
            this.label4.TabIndex = 17;
            this.label4.Text = "Chương trình";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "Ngày bắt đầu";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtoutdate
            // 
            this.txtoutdate.AutoRoundedCorners = true;
            this.txtoutdate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
            this.txtoutdate.BorderRadius = 17;
            this.txtoutdate.BorderThickness = 1;
            this.txtoutdate.Checked = true;
            this.txtoutdate.FillColor = System.Drawing.Color.White;
            this.txtoutdate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtoutdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtoutdate.Location = new System.Drawing.Point(256, 149);
            this.txtoutdate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.txtoutdate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.txtoutdate.Name = "txtoutdate";
            this.txtoutdate.Size = new System.Drawing.Size(179, 36);
            this.txtoutdate.TabIndex = 18;
            this.txtoutdate.Value = new System.DateTime(2024, 11, 6, 22, 9, 19, 222);
            // 
            // txtdetail
            // 
            this.txtdetail.Animated = true;
            this.txtdetail.AutoRoundedCorners = true;
            this.txtdetail.BorderRadius = 23;
            this.txtdetail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtdetail.DefaultText = "";
            this.txtdetail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtdetail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtdetail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtdetail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtdetail.FocusedState.BorderColor = System.Drawing.Color.LimeGreen;
            this.txtdetail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtdetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtdetail.HoverState.BorderColor = System.Drawing.Color.LimeGreen;
            this.txtdetail.Location = new System.Drawing.Point(335, 224);
            this.txtdetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdetail.Name = "txtdetail";
            this.txtdetail.PasswordChar = '\0';
            this.txtdetail.PlaceholderText = "";
            this.txtdetail.SelectedText = "";
            this.txtdetail.Size = new System.Drawing.Size(179, 48);
            this.txtdetail.TabIndex = 19;
            this.txtdetail.Tag = "";
            this.txtdetail.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(331, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 23);
            this.label3.TabIndex = 21;
            this.label3.Text = "Ưu đãi";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 23);
            this.label5.TabIndex = 20;
            this.label5.Text = "Ngày kết thúc";
            // 
            // FrmDiscountAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 450);
            this.Controls.Add(this.txtoutdate);
            this.Controls.Add(this.txtdetail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtondate);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Name = "FrmDiscountAdd";
            this.Text = "FrmDiscountAdd";
            this.Load += new System.EventHandler(this.FrmDiscountAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public Guna.UI2.WinForms.Guna2TextBox txtname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        public Guna.UI2.WinForms.Guna2TextBox txtdetail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        public Guna.UI2.WinForms.Guna2DateTimePicker txtondate;
        public Guna.UI2.WinForms.Guna2DateTimePicker txtoutdate;
    }
}