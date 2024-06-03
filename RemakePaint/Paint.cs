using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemakePaint
{
    public partial class Paint : Form
    {
        public Graphics g; // đồ hoạ từ màn hình vẽ chính
        public Bitmap bm;
        Pen p; // bút vẽ chính
        Pen eraser; // tẩy
        Point px, py;
        int SelectedMode; // mỗi giá trị là mỗi mode vẽ lên màn hình chính
        bool AllowPaint = false; // nếu là true thì cho phép vẽ lên màn hình chính
        bool isPainted = false;
        Color currentColor = Color.Black;
        VeHinh veHinh;
        //
        //
        //
        public bool isDownPanel = false;// biến chỉ ra các dáu chấm thay đổi kích thước có nhấn ko
        public Point oldPoint = new Point();
        public bool isSaved = false;
        public static string path = "";// bien string luu duong dan luu file
        public string tenFileTieuDe = "Untitled";

        public bool isGridLine = false;
        public bool isFullScreen = false;
        //
        public static Paint instance;

        //stack
        public Stack<Bitmap> UndoStack = new Stack<Bitmap>();
        public Stack<Bitmap> RedoStack = new Stack<Bitmap>();
        //

        //crop variable
        bool isCropping = false;
        int cropX;
        int cropY;
        int cropWidth;
        int cropHeight;
        public Pen cropPen;
        public DashStyle cropDashStyle = DashStyle.DashDot;
        //
        //
        bool isBold = false;
        bool isItalic = false;
        bool isUnderline = false;

        public const int MainScreen_Location_X = 120;
        public const int MainScreen_Location_Y = 60;
        public Size MainScreenNormalSize = new Size(1000, 500);

        Guna2Panel pnLeft, pnDown, pnCorner;

        public Paint()
        {
            instance = this;
            InitializeComponent();
            InitBtncolorEvent();
            InitGraphic();
            LoadFontAndSize();
            InitPaintEvent();
            InitSizeEvent();
            InitBtnShapeEvent();
        }



        #region Size Event
        private void InitSizeEvent()
        {
            // Left
            pnLeft = new Guna2Panel();
            pnLeft.Size = new Size(8, 8);
            pnLeft.Location = new Point(MainScreen_Location_X + pb_mainScreen.Width, MainScreen_Location_Y + pb_mainScreen.Height / 2);
            pnLeft.BackColor = Color.White;
            pnLeft.BorderColor = Color.Black;
            pnLeft.BorderThickness = 1;
            pnLeft.Cursor = Cursors.SizeWE; 
            pnPaintRegion.Controls.Add(pnLeft);
            pnLeft.MouseDown += (sender, e) =>
            {
                isDownPanel = true;
                oldPoint = e.Location;
            };
            pnLeft.MouseMove += (sender, e) =>
            {
                if (isDownPanel)
                {
                    if (oldPoint != e.Location)
                    {
                        this.pb_mainScreen.Width = (e.X + pnLeft.Location.X - MainScreen_Location_X);
                        Image temp = pb_mainScreen.Image;
                        bm = new Bitmap(pb_mainScreen.Width, pb_mainScreen.Height);
                        g = Graphics.FromImage(bm);
                        g.Clear(Color.White);
                        g.DrawImage(temp, new Point(0, 0));
                        pb_mainScreen.Image = bm;
                    }
                    ResetLocationSizeTool();
                }
                oldPoint = e.Location;   
            };
            pnLeft.MouseUp += (sender, e) =>
            {
                isDownPanel = false;
            };

            // Down
            pnDown = new Guna2Panel();
            pnDown.Size = new Size(8, 8);
            pnDown.Location = new Point(MainScreen_Location_X + pb_mainScreen.Width / 2, MainScreen_Location_Y + pb_mainScreen.Height);
            pnDown.BackColor = Color.White;
            pnDown.BorderColor = Color.Black;
            pnDown.BorderThickness = 1;
            pnDown.Cursor = Cursors.SizeNS;
            pnPaintRegion.Controls.Add(pnDown);
            pnDown.MouseDown += (sender, e) =>
            {
                isDownPanel = true;
                oldPoint = e.Location;
            };
            pnDown.MouseMove += (sender, e) =>
            {
                if (isDownPanel)
                {
                    if (oldPoint != e.Location)
                    {
                        this.pb_mainScreen.Height = (e.Y + pnDown.Location.Y - MainScreen_Location_Y);
                        Image temp = pb_mainScreen.Image;
                        bm = new Bitmap(pb_mainScreen.Width, pb_mainScreen.Height);
                        g = Graphics.FromImage(bm);
                        g.Clear(Color.White);
                        g.DrawImage(temp, new Point(0, 0));
                        pb_mainScreen.Image = bm;
                    }
                    ResetLocationSizeTool();
                }
                oldPoint = e.Location;
            };
            pnDown.MouseUp += (sender, e) =>
            {
                isDownPanel = false;
            };

            // Corner
            pnCorner = new Guna2Panel();
            pnCorner.Size = new Size(8, 8);
            pnCorner.Location = new Point(MainScreen_Location_X + pb_mainScreen.Width, MainScreen_Location_Y + pb_mainScreen.Height);
            pnCorner.BackColor = Color.White;
            pnCorner.BorderColor = Color.Black;
            pnCorner.BorderThickness = 1;
            pnCorner.Cursor = Cursors.SizeNWSE;
            pnPaintRegion.Controls.Add(pnCorner);
            pnCorner.MouseDown += (sender, e) =>
            {
                isDownPanel = true;
                oldPoint = e.Location;
            };
            pnCorner.MouseMove += (sender, e) =>
            {
                if (isDownPanel)
                {
                    if (oldPoint != e.Location)
                    {
                        this.pb_mainScreen.Width = (e.X + pnLeft.Location.X - MainScreen_Location_X);
                        this.pb_mainScreen.Height = (e.Y + pnDown.Location.Y - MainScreen_Location_Y);
                        Image temp = pb_mainScreen.Image;
                        bm = new Bitmap(pb_mainScreen.Width, pb_mainScreen.Height);
                        g = Graphics.FromImage(bm);
                        g.Clear(Color.White);
                        g.DrawImage(temp, new Point(0, 0));
                        pb_mainScreen.Image = bm;
                    }
                    ResetLocationSizeTool();
                }
                oldPoint = e.Location;
            };
            pnCorner.MouseUp += (sender, e) =>
            {
                isDownPanel = false;
            };
            pb_mainScreen.SizeChanged += (sender, e) =>
            {
                StatusPaintSize.Text = pb_mainScreen.Width + " x " + pb_mainScreen.Height + "px";
            };
        }


        private void ResetLocationSizeTool()
        {
            pnLeft.Location = new Point(MainScreen_Location_X + pb_mainScreen.Width, MainScreen_Location_Y + pb_mainScreen.Height / 2);
            pnDown.Location = new Point(MainScreen_Location_X + pb_mainScreen.Width / 2, MainScreen_Location_Y + pb_mainScreen.Height);
            pnCorner.Location = new Point(MainScreen_Location_X + pb_mainScreen.Width, MainScreen_Location_Y + pb_mainScreen.Height);
        }
        #endregion
        #region Init color event
        private void InitBtncolorEvent()
        {
            btnColor1.Click += BtnColor_Click;
            btnColor2.Click += BtnColor_Click;
            btnColor3.Click += BtnColor_Click;
            btnColor4.Click += BtnColor_Click;
            btnColor5.Click += BtnColor_Click;
            btnColor6.Click += BtnColor_Click;
            btnColor7.Click += BtnColor_Click;
            btnColor8.Click += BtnColor_Click;
            btnColor9.Click += BtnColor_Click;
            btnColor10.Click += BtnColor_Click;
            btnColor11.Click += BtnColor_Click;
            btnColor12.Click += BtnColor_Click;
            btnColor13.Click += BtnColor_Click;
            btnColor14.Click += BtnColor_Click;
            btnColor15.Click += BtnColor_Click;
            btnColor16.Click += BtnColor_Click;
            btnColor17.Click += BtnColor_Click;
            btnColor18.Click += BtnColor_Click;
            btnColor19.Click += BtnColor_Click;
            btnColor20.Click += BtnColor_Click;
            btnColor21.Click += BtnColor_Click;
            btnColor22.Click += BtnColor_Click;
            btnColor23.Click += BtnColor_Click;
            btnColor24.Click += BtnColor_Click;
        }
        private void BtnColor_Click(object sender, EventArgs e)
        {
            Guna2CircleButton btn = sender as Guna2CircleButton;
            currentColor = btn.FillColor;   
            pbCurrentColor.FillColor = currentColor;
            p.Color = currentColor;
        }
        #endregion
        #region Init paint event
        private void InitPaintEvent()
        {
            pb_mainScreen.MouseDown += Pb_mainScreen_MouseDown;
            pb_mainScreen.MouseMove += Pb_mainScreen_MouseMove;
            pb_mainScreen.MouseUp += Pb_mainScreen_MouseUp;
            pb_mainScreen.Paint += Pb_mainScreen_Paint;
        }

        private void Pb_mainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            AllowPaint = true;
            if (textBox1.Visible == true)
            {
                UndoStack.Push(new Bitmap(bm));
                textBox1.Visible = false;
                g.DrawString(textBox1.Text, textBox1.Font, new SolidBrush(textBox1.ForeColor), new Point(textBox1.Location.X, textBox1.Location.Y + 3));
                textBox1.Text = "";
                pnTextTools.Visible = false;
            }
            if (pb_mainScreen.Cursor == Cursors.IBeam)
            {
                UndoStack.Push(new Bitmap(bm));
                textBox1.Location = new Point(e.X - 10 , e.Y - 10);
                textBox1.Visible = true;
                textBox1.Focus();
                pnTextTools.Visible = true;
            }
            if (SelectedMode == 25) 
            {
                if(e.Button == MouseButtons.Left)
                {
                    this.pb_mainScreen.Width = pb_mainScreen.Width + pb_mainScreen.Width / 3;
                    this.pb_mainScreen.Height = pb_mainScreen.Height + pb_mainScreen.Height / 3;
                    bm = new Bitmap(bm, pb_mainScreen.Width, pb_mainScreen.Height);
                    g = Graphics.FromImage(bm);
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    pb_mainScreen.Image = bm;
                    ResetLocationSizeTool();
                }
                else
                {
                    this.pb_mainScreen.Width = pb_mainScreen.Width - pb_mainScreen.Width / 3;
                    this.pb_mainScreen.Height = pb_mainScreen.Height - pb_mainScreen.Height / 3;
                    bm = new Bitmap(bm, pb_mainScreen.Width, pb_mainScreen.Height);
                    g = Graphics.FromImage(bm);
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    pb_mainScreen.Image = bm;
                    ResetLocationSizeTool();
                }
                return;
            }
            px = e.Location;
            if (SelectedMode == 3)
            {
                UndoStack.Push(new Bitmap(bm));
                Fill(bm, e.X - 10, e.Y + 10, currentColor);
                pb_mainScreen.Refresh();
                return;
            }
            if (SelectedMode == 4)
            {
                if (pb_mainScreen.Cursor != Cursors.Default)
                {
                    currentColor = ((Bitmap)(pb_mainScreen.Image)).GetPixel(e.X - 10, e.Y + 10);
                    p.Color = currentColor;
                    pbCurrentColor.FillColor = currentColor;
                    pb_mainScreen.Cursor = Cursors.Default;
                }
                return;
            }
            UndoStack.Push(new Bitmap(bm)); 
        }
        private void Pb_mainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            py = e.Location;    
            StatusCursor.Text = e.X + " : " + e.Y + "px";
            if (AllowPaint == false) return;
            if (SelectedMode == 1)
            {
                g.DrawLine(p, new Point(px.X - 10, px.Y + 10), new Point(py.X - 10, py.Y + 10));
                px = py;
            }
            if(SelectedMode == 2)
            {
                g.DrawLine(eraser, new Point(px.X - 10, px.Y + 10), new Point(py.X - 10, py.Y + 10));
                px = py;
            }
            pb_mainScreen.Refresh();
        }
        private void Pb_mainScreen_MouseUp(object sender, MouseEventArgs e)
        {
            AllowPaint = false;
            if (SelectedMode == 5)
            {
                veHinh.DrawLine(p, g, px, py);
            }
            if (SelectedMode == 6)
            {
                veHinh.DrawEllipse(p, g, px, py);
            }
            if (SelectedMode == 7)
            {
                veHinh.DrawRect(p, g, px, py);
            }
            if (SelectedMode == 8)
            {
                veHinh.DrawTriangle(p, g, px, py);
            }
            if (SelectedMode == 9)
            {
                veHinh.DrawRightTriangle(p, g, px, py);
            }
            if (SelectedMode == 10)
            {
                veHinh.DrawRoundedRectangle(p, g, px, py);
            }
            if (SelectedMode == 11)
            {
                veHinh.DrawDiamond(p, g, px, py);
            }
            if (SelectedMode == 12)
            {
                veHinh.DrawPentagon(p, g, px, py);
            }
            if (SelectedMode == 13)
            {
                veHinh.DrawHexagon(p, g, px, py);
            }
            if (SelectedMode == 14)
            {
                veHinh.DrawUpArrow(p, g, px, py);
            }
            if (SelectedMode == 15)
            {
                veHinh.DrawLeftArrow(p, g, px, py);
            }
            if (SelectedMode == 16)
            {
                veHinh.DrawRightArrow(p, g, px, py);
            }
            if (SelectedMode == 17)
            {
                veHinh.DrawDownArrow(p, g, px, py);
            }
            if (SelectedMode == 18)
            {
                veHinh.MinhHoaPolygon(p, g, px, py);
            }
            if (SelectedMode == 19)
            {
                veHinh.DrawFivePointStar(p, g, px, py);
            }
        }

        private void Pb_mainScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics gx = e.Graphics;
            gx.DrawImage(bm, new Point(0, 0));
            if (AllowPaint)
            {
                if (SelectedMode == 5)
                {
                    veHinh.DrawLine(p, gx, px, py);
                }
                if (SelectedMode == 6)
                {
                    veHinh.DrawEllipse(p, gx, px, py);
                }
                if (SelectedMode == 7)
                {
                    veHinh.DrawRect(p, gx, px, py);
                }
                if (SelectedMode == 8)
                {
                    veHinh.DrawTriangle(p, gx, px, py);
                }
                if (SelectedMode == 9)
                {
                    veHinh.DrawRightTriangle(p, gx, px, py);
                }
                if (SelectedMode == 10)
                {
                    veHinh.DrawRoundedRectangle(p, gx, px, py);
                }
                if (SelectedMode == 11)
                {
                    veHinh.DrawDiamond(p, gx, px, py);
                }
                if (SelectedMode == 12)
                {
                    veHinh.DrawPentagon(p, gx, px, py);
                }
                if (SelectedMode == 13)
                {
                    veHinh.DrawHexagon(p, gx, px, py);
                }
                if (SelectedMode == 14)
                {
                    veHinh.DrawUpArrow(p, gx, px, py);
                }
                if (SelectedMode == 15)
                {
                    veHinh.DrawLeftArrow(p, gx, px, py);
                }
                if (SelectedMode == 16)
                {
                    veHinh.DrawRightArrow(p, gx, px, py);
                }
                if (SelectedMode == 17)
                {
                    veHinh.DrawDownArrow(p, gx, px, py);
                }
                if (SelectedMode == 18)
                {
                    veHinh.MinhHoaPolygon(p, gx, px, py);
                }
                if (SelectedMode == 19)
                {
                    veHinh.DrawFivePointStar(p, gx, px, py);
                }
            }
        }

        #endregion
        #region Tools Event
        private void btnPencil_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(RemakePaint.Properties.Resources.IcPencilMainscreen);
            pb_mainScreen.Cursor = new Cursor(temp.GetHicon());
            SelectedMode = 1;
            TrackBarPen.Visible = true; 
            TrackBarPen.Value = 40 - (int)p.Width;
        }
        private void btnEraser_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(RemakePaint.Properties.Resources.IcEraserMainscreen);
            pb_mainScreen.Cursor = new Cursor(temp.GetHicon());
            SelectedMode = 2;
            TrackBarPen.Visible = true;
            TrackBarPen.Value = 40 - (int)eraser.Width;
        }
        private void btnFill_Click(object sender, EventArgs e)
        {
            SelectedMode = 3;
            TrackBarPen.Visible = false;
            Bitmap bm = new Bitmap(RemakePaint.Properties.Resources.icons8_fill_color_48);
            pb_mainScreen.Cursor = new Cursor(bm.GetHicon());
        }
        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            SelectedMode = 4;
            TrackBarPen.Visible = false;
            Bitmap bm = new Bitmap(RemakePaint.Properties.Resources.icons8_color_picker_48);
            pb_mainScreen.Cursor = new Cursor(bm.GetHicon());
        }
        private void TrackBarPen_Scroll(object sender, ScrollEventArgs e)
        {
            int size = 40 - TrackBarPen.Value;
            if(SelectedMode == 1)
            {
                p = new Pen(currentColor, size);
            }
            else
            {
                eraser = new Pen(Color.White, size);
            }
        }
        private void Cb_Font_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = int.Parse(cb_Size.Text);
                Font font = new Font(cb_Font.Text, i);
                textBox1.SelectionColor = currentColor;
                textBox1.Font = font;

            }
            catch
            {
                MessageBox.Show("Lỗi font chữ");
            }
        }
        private void cb_Size_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = int.Parse(cb_Size.Text);
                Font font = new Font(cb_Font.Text, i);
                textBox1.SelectionColor = currentColor;
                textBox1.Font = font;

            }
            catch
            {
                MessageBox.Show("Lỗi font chữ");
            }
        }
        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            Image temp = pb_mainScreen.Image;
            temp.RotateFlip(RotateFlipType.Rotate90FlipXY);
            pb_mainScreen.Size = new Size(pb_mainScreen.Height, pb_mainScreen.Width);
            bm = new Bitmap(pb_mainScreen.Width, pb_mainScreen.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            g.DrawImage(temp, new Point(0, 0));
            pb_mainScreen.Image = bm;
            ResetLocationSizeTool();
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            Image temp = pb_mainScreen.Image;
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pb_mainScreen.Size = new Size(pb_mainScreen.Height, pb_mainScreen.Width);
            bm = new Bitmap(pb_mainScreen.Width, pb_mainScreen.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            g.DrawImage(temp, new Point(0, 0));
            pb_mainScreen.Image = bm;
            ResetLocationSizeTool();
        }
        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {
            Image temp = pb_mainScreen.Image;
            temp.RotateFlip(RotateFlipType.Rotate180FlipX);
            bm = new Bitmap(pb_mainScreen.Width, pb_mainScreen.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            g.DrawImage(temp, new Point(0, 0));
            pb_mainScreen.Image = bm;
            ResetLocationSizeTool();
        }
        private void btnFlipVertical_Click(object sender, EventArgs e)
        {
            Image temp = pb_mainScreen.Image;
            temp.RotateFlip(RotateFlipType.Rotate180FlipY);
            bm = new Bitmap(pb_mainScreen.Width, pb_mainScreen.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            g.DrawImage(temp, new Point(0, 0));
            pb_mainScreen.Image = bm;
            ResetLocationSizeTool();
        }
        private void InitBtnShapeEvent()
        {
            btnLine.Click += (sender, e) =>
            {
                SelectedMode = 5;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnCircle.Click += (sender, e) =>
            {
                SelectedMode = 6;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnRectangle.Click += (sender, e) =>
            {
                SelectedMode = 7;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnTriangle.Click += (sender, e) =>
            {
                SelectedMode = 8;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnRightTriangle.Click += (sender, e) =>
            {
                SelectedMode = 9;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnRadiusRectangle.Click += (sender, e) =>
            {
                SelectedMode = 10;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnRhombus.Click += (sender, e) =>
            {
                SelectedMode = 11;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnPentagon.Click += (sender, e) =>
            {
                SelectedMode = 12;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnHexagon.Click += (sender, e) =>
            {
                SelectedMode = 13;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnArrowUp.Click += (sender, e) =>
            {
                SelectedMode = 14;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnArrowDown.Click += (sender, e) =>
            {
                SelectedMode = 17;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnArrowLeft.Click += (sender, e) =>
            {
                SelectedMode = 15;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnArrowRight.Click += (sender, e) =>
            {
                SelectedMode = 16;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnPolygon.Click += (sender, e) =>
            {
                SelectedMode = 18;
                pb_mainScreen.Cursor = Cursors.Default;
            };
            btnStar.Click += (sender, e) =>
            {
                SelectedMode = 19;
                pb_mainScreen.Cursor = Cursors.Default;
            };
        }
        private void btnText_Click(object sender, EventArgs e)
        {
            SelectedMode = 24;
            pb_mainScreen.Cursor = Cursors.IBeam;
            TrackBarPen.Visible = false;    
        }
        private void btnZoom_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(RemakePaint.Properties.Resources.icons8_magnifying_glass_24);
            pb_mainScreen.Cursor = new Cursor(bm.GetHicon());
            SelectedMode = 25;
            TrackBarPen.Visible = false;
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Bạn có muốn lưu file với tên " + tenFileTieuDe + " không", "Thông Báo", MessageBoxButtons.YesNoCancel))
            {
                // truong hop nguoi dung muon luu anh dang ve lai va open file sau
                case DialogResult.Yes:
                    {
                        saveToolStripMenuItem_Click(sender, e);
                        tenFileTieuDe = "Untitled";
                        isPainted = false;
                        g.Clear(Color.White);
                        pb_mainScreen.Refresh();
                        pb_mainScreen.Image = bm;
                        pb_mainScreen.Size = MainScreenNormalSize;
                        break;
                    }
                // truong hop nguoi dung khong muon luu anh dang ve ma muon open file luon
                case DialogResult.No:
                    {
                        tenFileTieuDe = "Untitled";
                        isPainted = false;
                        g.Clear(Color.White);
                        pb_mainScreen.Refresh();
                        pb_mainScreen.Image = bm;
                        pb_mainScreen.Size = MainScreenNormalSize;
                        break;
                    }

                case DialogResult.Cancel:
                    return;
            }
            ResetLocationSizeTool();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // truong hop chua save lan nao 
            if (isSaved == false)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "File *.png, *jpg, *.bmp, *.gif|*.png; *.jpg; *.bmp; *.gif ", Title = "Save image" };
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    isSaved = true;
                    path = saveFileDialog.FileName;

                    pb_mainScreen.Image.Save(path);
                }
            }
            // truong hop da save se goi ham saveas
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            ResetLocationSizeTool();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSaved)
            {
                pb_mainScreen.Image.Save(path);
                MessageBox.Show("Lưu thành công!", "TNT Paint");
            }
            else
            {
                saveToolStripMenuItem_Click(sender, e);
            }
            ResetLocationSizeTool();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "File *.png, *jpg, *.bmp, *.gif|*.png; *.jpg; *.bmp; *.gif ", Title = "Open image" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                tenFileTieuDe = openFileDialog.SafeFileName;
                Image img = Image.FromFile(fileName);
                bm = new Bitmap(img, new Size(img.Width, img.Height));
                pb_mainScreen.Size = new Size(img.Width, img.Height);
                pb_mainScreen.Refresh();
                pb_mainScreen.Image = bm;
                g = Graphics.FromImage(bm);
                ResetLocationSizeTool();
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (isBold == false)
            {
                isBold = true;

                //
                if (isItalic == true)
                {
                    if (isUnderline == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold | FontStyle.Italic);
                    }
                }
                else if (isItalic == false)
                {
                    if (isUnderline == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold | FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold);
                    }
                }
            }
            else
            {
                isBold = false;

                if (isItalic == true)
                {
                    if (isUnderline == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Italic | FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Italic);
                    }
                }
                else if (isItalic == false)
                {
                    if (isUnderline == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Regular);
                    }
                }
            }
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            if (isItalic == false)
            {
                isItalic = true;

                //
                if (isBold == true)
                {
                    if (isUnderline == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold | FontStyle.Italic);
                    }
                }
                else if (isBold == false)
                {
                    if (isUnderline == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Italic | FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Italic);
                    }
                }
            }
            else
            {
                isItalic = false;

                if (isBold == true)
                {
                    if (isUnderline == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold | FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Italic);
                    }
                }
                else if (isItalic == false)
                {
                    if (isUnderline == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Regular);
                    }
                }
            }
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            if (isUnderline == false)
            {
                isUnderline = true;

                //
                if (isBold == true)
                {
                    if (isItalic == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold | FontStyle.Italic);
                    }
                }
                else if (isBold == false)
                {
                    if (isItalic == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Italic | FontStyle.Underline);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Underline);
                    }
                }
            }
            else
            {
                isUnderline = false;

                if (isBold == true)
                {
                    if (isItalic == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Italic | FontStyle.Bold);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Bold);
                    }
                }
                else if (isBold == false)
                {
                    if (isItalic == true)
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Italic);
                    }
                    else
                    {
                        textBox1.Font = new Font(cb_Font.Text, Convert.ToInt32(cb_Size.Text), FontStyle.Regular);
                    }
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Bạn có muốn lưu file với tên " + tenFileTieuDe + " không", "Thông Báo", MessageBoxButtons.YesNoCancel))
            {
                // truong hop nguoi dung muon luu anh dang ve lai va open file sau
                case DialogResult.Yes:
                    {
                        saveToolStripMenuItem_Click(sender, e);
                        tenFileTieuDe = "Untitled";
                        isPainted = false;
                        g.Clear(Color.White);
                        pb_mainScreen.Refresh();
                        pb_mainScreen.Image = bm;
                        pb_mainScreen.Size = MainScreenNormalSize;
                        break;
                    }
                // truong hop nguoi dung khong muon luu anh dang ve ma muon open file luon
                case DialogResult.No:
                    {
                        tenFileTieuDe = "Untitled";
                        isPainted = false;
                        g.Clear(Color.White);
                        pb_mainScreen.Refresh();
                        pb_mainScreen.Image = bm;
                        pb_mainScreen.Size = MainScreenNormalSize;
                        break;
                    }

                case DialogResult.Cancel:
                    return;
            }
            this.Close();
        }

        #endregion
        #region Custom Function
        private void LoadFontAndSize()
        {
            InstalledFontCollection installedFont = new InstalledFontCollection();
            FontFamily[] fontFamilies = installedFont.Families;
            foreach (FontFamily ff in fontFamilies)
            {
                cb_Font.Items.Add(ff.Name);
            }
            cb_Size.Items.Add("8");
            cb_Size.Items.Add("10");
            cb_Size.Items.Add("12");
            cb_Size.Items.Add("15");
            cb_Size.Items.Add("18");
            cb_Size.Items.Add("22");
            cb_Size.Items.Add("26");
            cb_Size.Items.Add("30");
            cb_Size.Items.Add("48");
            cb_Size.Items.Add("72");
            cb_Size.SelectedItem = "10";
            cb_Font.SelectedItem = "Arial";
            cb_Font.SelectedIndexChanged += Cb_Font_SelectedIndexChanged;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pb_mainScreen.Width = pb_mainScreen.Width + pb_mainScreen.Width / 3;
            this.pb_mainScreen.Height = pb_mainScreen.Height + pb_mainScreen.Height / 3;
            bm = new Bitmap(bm, pb_mainScreen.Width, pb_mainScreen.Height);
            g = Graphics.FromImage(bm);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pb_mainScreen.Image = bm;
            ResetLocationSizeTool();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pb_mainScreen.Width = pb_mainScreen.Width - pb_mainScreen.Width / 3;
            this.pb_mainScreen.Height = pb_mainScreen.Height - pb_mainScreen.Height / 3;
            bm = new Bitmap(bm, pb_mainScreen.Width, pb_mainScreen.Height);
            g = Graphics.FromImage(bm);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pb_mainScreen.Image = bm;
            ResetLocationSizeTool();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (UndoStack.Count > 0)
            {
                RedoStack.Push((Bitmap)pb_mainScreen.Image.Clone());
                Image image = UndoStack.Pop();
                bm = new Bitmap(image, new Size(image.Width, image.Height));
                pb_mainScreen.Size = new Size(image.Width, image.Height);
                pb_mainScreen.Image = bm;
                g = Graphics.FromImage(bm);
                ResetLocationSizeTool();
            }
            else
            {
                MessageBox.Show("Nothing to Undo");
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (RedoStack.Count > 0)
            {
                UndoStack.Push((Bitmap)pb_mainScreen.Image.Clone());
                Image image = RedoStack.Pop();
                bm = new Bitmap(image, new Size(image.Width, image.Height));
                pb_mainScreen.Size = new Size(image.Width, image.Height);
                pb_mainScreen.Refresh();
                pb_mainScreen.Image = bm;
                g = Graphics.FromImage(bm);
                ResetLocationSizeTool();
            }
            else
            {
                MessageBox.Show("Nothing to Redo");
            }
        }


        private void InitGraphic()
        {
            bm = new Bitmap(pb_mainScreen.Width, pb_mainScreen.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pb_mainScreen.Image = bm;
            p = new Pen(Color.Black, 1);
            eraser = new Pen(Color.White, 20);
            SelectedMode = 0; // chọn bút chì làm mặc định
            veHinh = new VeHinh();
            pb_mainScreen.Controls.Add(textBox1);
            StatusPaintSize.Text = pb_mainScreen.Width + " x " + pb_mainScreen.Height + "px";
            UndoStack.Push(new Bitmap(bm));
        }
        private void Fill(Bitmap bm, int x, int y, Color newColor)
        {
            Color oldColor = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, newColor);
            if (oldColor.ToArgb() == newColor.ToArgb()) { return; }

            while (pixel.Count > 0)
            {
                Point pt = pixel.Pop();
                if (pt.X > 0 && pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1)
                {
                    validate(bm, pixel, pt.X - 1, pt.Y, oldColor, newColor);
                    validate(bm, pixel, pt.X, pt.Y - 1, oldColor, newColor);
                    validate(bm, pixel, pt.X + 1, pt.Y, oldColor, newColor);
                    validate(bm, pixel, pt.X, pt.Y + 1, oldColor, newColor);
                }
            }
        }

        private void validate(Bitmap bm, Stack<Point> sp, int x, int y, Color oldColor, Color newColor)
        {
            Color cx = bm.GetPixel(x, y);
            if (cx == oldColor)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, newColor);
            }
        }
        #endregion
    }
}
