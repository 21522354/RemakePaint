namespace RemakePaint
{
    partial class ImageEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEditor));
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.TrackBarSaturation = new Guna.UI2.WinForms.Guna2TrackBar();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.TrackBarContrast = new Guna.UI2.WinForms.Guna2TrackBar();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnFlipVertical = new Guna.UI2.WinForms.Guna2Button();
            this.btnFlipHorizontal = new Guna.UI2.WinForms.Guna2Button();
            this.btnRotateRight = new Guna.UI2.WinForms.Guna2Button();
            this.btnRotateLeft = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2Button();
            this.TrackBarBrightness = new Guna.UI2.WinForms.Guna2TrackBar();
            this.btnReset = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(27, 58);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(89, 22);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Brightness:";
            // 
            // TrackBarSaturation
            // 
            this.TrackBarSaturation.Location = new System.Drawing.Point(137, 111);
            this.TrackBarSaturation.Minimum = -100;
            this.TrackBarSaturation.Name = "TrackBarSaturation";
            this.TrackBarSaturation.Size = new System.Drawing.Size(339, 23);
            this.TrackBarSaturation.TabIndex = 3;
            this.TrackBarSaturation.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(113)))), ((int)(((byte)(255)))));
            this.TrackBarSaturation.Value = 0;
            this.TrackBarSaturation.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TrackBarSaturation_Scroll);
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(27, 111);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(84, 22);
            this.guna2HtmlLabel2.TabIndex = 2;
            this.guna2HtmlLabel2.Text = "Saturation:";
            // 
            // TrackBarContrast
            // 
            this.TrackBarContrast.Location = new System.Drawing.Point(137, 158);
            this.TrackBarContrast.Minimum = -100;
            this.TrackBarContrast.Name = "TrackBarContrast";
            this.TrackBarContrast.Size = new System.Drawing.Size(339, 23);
            this.TrackBarContrast.TabIndex = 5;
            this.TrackBarContrast.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(113)))), ((int)(((byte)(255)))));
            this.TrackBarContrast.Value = 0;
            this.TrackBarContrast.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TrackBarContrast_Scroll);
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(27, 158);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(72, 22);
            this.guna2HtmlLabel3.TabIndex = 4;
            this.guna2HtmlLabel3.Text = "Contrast:";
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(17, 11);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(55, 27);
            this.guna2HtmlLabel4.TabIndex = 6;
            this.guna2HtmlLabel4.Text = "Color";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.guna2Panel1.Location = new System.Drawing.Point(-5, 202);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(536, 1);
            this.guna2Panel1.TabIndex = 7;
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(17, 246);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(155, 27);
            this.guna2HtmlLabel5.TabIndex = 8;
            this.guna2HtmlLabel5.Text = "Flip and Rotate:";
            // 
            // btnFlipVertical
            // 
            this.btnFlipVertical.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFlipVertical.BorderRadius = 5;
            this.btnFlipVertical.BorderThickness = 1;
            this.btnFlipVertical.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFlipVertical.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFlipVertical.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFlipVertical.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFlipVertical.FillColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFlipVertical.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFlipVertical.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnFlipVertical.HoverState.BorderColor = System.Drawing.Color.DimGray;
            this.btnFlipVertical.Image = global::RemakePaint.Properties.Resources.icons8_flip_vertical_40;
            this.btnFlipVertical.ImageSize = new System.Drawing.Size(30, 30);
            this.btnFlipVertical.Location = new System.Drawing.Point(336, 265);
            this.btnFlipVertical.Name = "btnFlipVertical";
            this.btnFlipVertical.Size = new System.Drawing.Size(40, 40);
            this.btnFlipVertical.TabIndex = 12;
            this.btnFlipVertical.Click += new System.EventHandler(this.btnFlipVertical_Click);
            // 
            // btnFlipHorizontal
            // 
            this.btnFlipHorizontal.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFlipHorizontal.BorderRadius = 5;
            this.btnFlipHorizontal.BorderThickness = 1;
            this.btnFlipHorizontal.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFlipHorizontal.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFlipHorizontal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFlipHorizontal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFlipHorizontal.FillColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFlipHorizontal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFlipHorizontal.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnFlipHorizontal.HoverState.BorderColor = System.Drawing.Color.DimGray;
            this.btnFlipHorizontal.Image = global::RemakePaint.Properties.Resources.icons8_flip_horizontal_40;
            this.btnFlipHorizontal.ImageSize = new System.Drawing.Size(30, 30);
            this.btnFlipHorizontal.Location = new System.Drawing.Point(289, 265);
            this.btnFlipHorizontal.Name = "btnFlipHorizontal";
            this.btnFlipHorizontal.Size = new System.Drawing.Size(40, 40);
            this.btnFlipHorizontal.TabIndex = 11;
            this.btnFlipHorizontal.Click += new System.EventHandler(this.btnFlipHorizontal_Click);
            // 
            // btnRotateRight
            // 
            this.btnRotateRight.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRotateRight.BorderRadius = 5;
            this.btnRotateRight.BorderThickness = 1;
            this.btnRotateRight.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRotateRight.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRotateRight.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRotateRight.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRotateRight.FillColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRotateRight.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRotateRight.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnRotateRight.HoverState.BorderColor = System.Drawing.Color.DimGray;
            this.btnRotateRight.Image = global::RemakePaint.Properties.Resources.icons8_rotate_right_50;
            this.btnRotateRight.ImageSize = new System.Drawing.Size(30, 30);
            this.btnRotateRight.Location = new System.Drawing.Point(336, 216);
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(40, 40);
            this.btnRotateRight.TabIndex = 10;
            this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRotateLeft.BorderRadius = 5;
            this.btnRotateLeft.BorderThickness = 1;
            this.btnRotateLeft.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRotateLeft.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRotateLeft.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRotateLeft.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRotateLeft.FillColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRotateLeft.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRotateLeft.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnRotateLeft.HoverState.BorderColor = System.Drawing.Color.DimGray;
            this.btnRotateLeft.Image = global::RemakePaint.Properties.Resources.icons8_rotate_left_50;
            this.btnRotateLeft.ImageSize = new System.Drawing.Size(30, 30);
            this.btnRotateLeft.Location = new System.Drawing.Point(289, 216);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(40, 40);
            this.btnRotateLeft.TabIndex = 9;
            this.btnRotateLeft.Click += new System.EventHandler(this.btnRotateLeft_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.guna2Panel2.Location = new System.Drawing.Point(-17, 319);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(536, 1);
            this.guna2Panel2.TabIndex = 13;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BorderRadius = 10;
            this.btnXacNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXacNhan.FillColor = System.Drawing.SystemColors.Highlight;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(196, 328);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(109, 45);
            this.btnXacNhan.TabIndex = 14;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // TrackBarBrightness
            // 
            this.TrackBarBrightness.Location = new System.Drawing.Point(137, 58);
            this.TrackBarBrightness.Maximum = 40;
            this.TrackBarBrightness.Minimum = -20;
            this.TrackBarBrightness.Name = "TrackBarBrightness";
            this.TrackBarBrightness.Size = new System.Drawing.Size(339, 23);
            this.TrackBarBrightness.TabIndex = 15;
            this.TrackBarBrightness.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(113)))), ((int)(((byte)(255)))));
            this.TrackBarBrightness.Value = 10;
            this.TrackBarBrightness.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TrackBarBrightness_Scroll_1);
            // 
            // btnReset
            // 
            this.btnReset.BorderRadius = 10;
            this.btnReset.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReset.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReset.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReset.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReset.FillColor = System.Drawing.Color.RosyBrown;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(367, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(109, 45);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ImageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(512, 397);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.TrackBarBrightness);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.btnFlipVertical);
            this.Controls.Add(this.btnFlipHorizontal);
            this.Controls.Add(this.btnRotateRight);
            this.Controls.Add(this.btnRotateLeft);
            this.Controls.Add(this.guna2HtmlLabel5);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Controls.Add(this.TrackBarContrast);
            this.Controls.Add(this.guna2HtmlLabel3);
            this.Controls.Add(this.TrackBarSaturation);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageEditor";
            this.Text = "Image Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TrackBar TrackBarSaturation;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2TrackBar TrackBarContrast;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2Button btnFlipVertical;
        private Guna.UI2.WinForms.Guna2Button btnFlipHorizontal;
        private Guna.UI2.WinForms.Guna2Button btnRotateRight;
        private Guna.UI2.WinForms.Guna2Button btnRotateLeft;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Button btnXacNhan;
        private Guna.UI2.WinForms.Guna2TrackBar TrackBarBrightness;
        private Guna.UI2.WinForms.Guna2Button btnReset;
    }
}