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
        //VeHinh veHinh;
        //
        //
        //
        public bool isDown = false;// biến chỉ ra các dáu chấm thay đổi kích thước có nhấn ko
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

        public Paint()
        {
            instance = this;
            InitializeComponent();
            InitBtncolorEvent();
            InitGraphic();
            LoadFontAndSize();
            InitPaintEvent();
        }


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
            px = e.Location;
            if (SelectedMode == 3)
            {
                Fill(bm, e.X - 10, e.Y + 10, currentColor);
                pb_mainScreen.Refresh();    
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
            }
        }
        private void Pb_mainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            py = e.Location;    
            if(AllowPaint == false) return;
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
        }

        private void Pb_mainScreen_Paint(object sender, PaintEventArgs e)
        {
            //
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
                MessageBox.Show("Loi font chu");
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
                MessageBox.Show("Loi font chu");
            }
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
        private void InitGraphic()
        {
            bm = new Bitmap(pb_mainScreen.Width, pb_mainScreen.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pb_mainScreen.Image = bm;
            p = new Pen(Color.Black, 1);
            eraser = new Pen(Color.White, 20);
            SelectedMode = 1; // chọn bút chì làm mặc định
        }
        private void Fill(Bitmap bm, int x, int y, Color newColor)
        {
            Color oldColor = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, newColor);
            if (oldColor == newColor) { return; }

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
