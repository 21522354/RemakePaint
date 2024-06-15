using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemakePaint
{
    public partial class ImageEditor : Form
    {
        Bitmap oldImg;
        public ImageEditor()
        {
            InitializeComponent();
            oldImg = (Bitmap)RemakePaint.instance.PbImgEditor.Image;
        }

        #region Brightness Saturation and Contrast
        //    private static Bitmap AdjustBrightness(Image image, float brightness)
        //    {
        //        // Make the ColorMatrix.
        //        float b = brightness;
        //        ColorMatrix cm = new ColorMatrix(new float[][]
        //            {
        //        new float[] {b, 0, 0, 0, 0},
        //        new float[] {0, b, 0, 0, 0},
        //        new float[] {0, 0, b, 0, 0},
        //        new float[] {0, 0, 0, 1, 0},
        //        new float[] {0, 0, 0, 0, 1},
        //            });
        //        ImageAttributes attributes = new ImageAttributes();
        //        attributes.SetColorMatrix(cm);

        //        // Draw the image onto the new bitmap while applying
        //        // the new ColorMatrix.
        //        Point[] points =
        //        {
        //    new Point(0, 0),
        //    new Point(image.Width, 0),
        //    new Point(0, image.Height),
        //};
        //        Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

        //        // Make the result bitmap.
        //        Bitmap bm = new Bitmap(image.Width, image.Height);
        //        using (Graphics gr = Graphics.FromImage(bm))
        //        {
        //            gr.DrawImage(image, points, rect,
        //                GraphicsUnit.Pixel, attributes);
        //        }

        //        // Return the result.
        //        return bm;
        //    }
        private Bitmap AdjustBrightness(Bitmap image, float brightnessFactor)
        {
            // Tạo một Bitmap mới có cùng kích thước với ảnh gốc
            Bitmap adjustedImage = new Bitmap(image.Width, image.Height);

            // Duyệt qua từng pixel trong ảnh
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    // Lấy giá trị màu của pixel hiện tại
                    Color originalColor = image.GetPixel(x, y);

                    // Điều chỉnh giá trị màu sắc dựa trên hệ số độ sáng
                    int r = (int)(originalColor.R * brightnessFactor);
                    int g = (int)(originalColor.G * brightnessFactor);
                    int b = (int)(originalColor.B * brightnessFactor);

                    // Đảm bảo giá trị màu sắc nằm trong khoảng từ 0 đến 255
                    r = Math.Min(255, Math.Max(0, r));
                    g = Math.Min(255, Math.Max(0, g));
                    b = Math.Min(255, Math.Max(0, b));

                    // Tạo màu mới từ giá trị đã điều chỉnh
                    Color adjustedColor = Color.FromArgb(r, g, b);

                    // Đặt pixel trong ảnh mới
                    adjustedImage.SetPixel(x, y, adjustedColor);
                }
            }

            return adjustedImage;
        }
        private static Bitmap AdjustSaturation(Image image, float saturation)
        {

            Bitmap TempBitmap = (Bitmap)image;

            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);

            Graphics NewGraphics = Graphics.FromImage(NewBitmap);

            saturation = (float)saturation + 255 / 255.0f;
            float rWeight = 0.3086f;
            float gWeight = 0.6094f;
            float bWeight = 0.0820f;

            float a = (1.0f - saturation) * rWeight + saturation;
            float b = (1.0f - saturation) * rWeight;
            float c = (1.0f - saturation) * rWeight;
            float d = (1.0f - saturation) * gWeight;
            float e = (1.0f - saturation) * gWeight + saturation;
            float f = (1.0f - saturation) * gWeight;
            float g = (1.0f - saturation) * bWeight;
            float h = (1.0f - saturation) * bWeight;
            float i = (1.0f - saturation) * bWeight + saturation;

            float[][] FloatColorMatrix = {
                new float[] {a,  b,  c,  0, 0},
                new float[] {d,  e,  f,  0, 0},
                new float[] {g,  h,  i,  0, 0},
                new float[] {0,  0,  0,  1, 0},
                new float[] {0, 0, 0, 0, 1}
            };
            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);

            ImageAttributes Attributes = new ImageAttributes();

            Attributes.SetColorMatrix(NewColorMatrix);

            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), 0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);

            Attributes.Dispose();

            NewGraphics.Dispose();

            return NewBitmap;
        }
        public static Bitmap AdjustContrast(Image Image, float Value)
        {
            Value = (100.0f + Value) / 100.0f;
            Value *= Value;
            Bitmap NewBitmap = (Bitmap)Image.Clone();
            BitmapData data = NewBitmap.LockBits(
                new Rectangle(0, 0, NewBitmap.Width, NewBitmap.Height),
                ImageLockMode.ReadWrite,
                NewBitmap.PixelFormat);
            int Height = NewBitmap.Height;
            int Width = NewBitmap.Width;

            unsafe
            {
                for (int y = 0; y < Height; ++y)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
                    int columnOffset = 0;
                    for (int x = 0; x < Width; ++x)
                    {
                        byte B = row[columnOffset];
                        byte G = row[columnOffset + 1];
                        byte R = row[columnOffset + 2];

                        float Red = R / 255.0f;
                        float Green = G / 255.0f;
                        float Blue = B / 255.0f;
                        Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                        Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                        Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                        int iR = (int)Red;
                        iR = iR > 255 ? 255 : iR;
                        iR = iR < 0 ? 0 : iR;
                        int iG = (int)Green;
                        iG = iG > 255 ? 255 : iG;
                        iG = iG < 0 ? 0 : iG;
                        int iB = (int)Blue;
                        iB = iB > 255 ? 255 : iB;
                        iB = iB < 0 ? 0 : iB;

                        row[columnOffset] = (byte)iB;
                        row[columnOffset + 1] = (byte)iG;
                        row[columnOffset + 2] = (byte)iR;

                        columnOffset += 4;
                    }
                }
            }

            NewBitmap.UnlockBits(data);

            return NewBitmap;
        }
        #endregion

        private void TrackBarBrightness_Scroll_1(object sender, ScrollEventArgs e)
        {
            float value = TrackBarBrightness.Value;
            Bitmap bmp = AdjustBrightness((Bitmap)RemakePaint.instance.PbImgEditor.Image, value / 10);
            RemakePaint.instance.PbImgEditor.Image = bmp;
        }

        private void TrackBarSaturation_Scroll(object sender, ScrollEventArgs e)
        {
            float value = TrackBarBrightness.Value;
            Bitmap bmp = AdjustSaturation(RemakePaint.instance.PbImgEditor.Image, value / 20);
            RemakePaint.instance.PbImgEditor.Image = bmp;
        }

        private void TrackBarContrast_Scroll(object sender, ScrollEventArgs e)
        {
            float value = TrackBarBrightness.Value;
            Bitmap bmp = AdjustContrast(RemakePaint.instance.PbImgEditor.Image, value);
            RemakePaint.instance.PbImgEditor.Image = bmp;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            RemakePaint.instance.PbImgEditor.Image = oldImg;
            TrackBarBrightness.Value = 10;
            TrackBarContrast.Value = 0; 
            TrackBarSaturation.Value = 0;  
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            Image temp = RemakePaint.instance.PbImgEditor.Image;
            temp.RotateFlip(RotateFlipType.Rotate90FlipXY);
            RemakePaint.instance.PbImgEditor.Size = new Size(RemakePaint.instance.PbImgEditor.Height, RemakePaint.instance.PbImgEditor.Width);
            RemakePaint.instance.PbImgEditor.Image = temp;
            RemakePaint.instance.ResetLocationPbSizeTool();
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            Image temp = RemakePaint.instance.PbImgEditor.Image;
            temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            RemakePaint.instance.PbImgEditor.Size = new Size(RemakePaint.instance.PbImgEditor.Height, RemakePaint.instance.PbImgEditor.Width);
            RemakePaint.instance.PbImgEditor.Image = temp;
            RemakePaint.instance.ResetLocationPbSizeTool();
        }
        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {
            Image temp = RemakePaint.instance.PbImgEditor.Image;
            temp.RotateFlip(RotateFlipType.Rotate180FlipX);
            RemakePaint.instance.PbImgEditor.Size = new Size(RemakePaint.instance.PbImgEditor.Width, RemakePaint.instance.PbImgEditor.Height);
            RemakePaint.instance.PbImgEditor.Image = temp;
            RemakePaint.instance.ResetLocationPbSizeTool();
        }
        private void btnFlipVertical_Click(object sender, EventArgs e)
        {
            Image temp = RemakePaint.instance.PbImgEditor.Image;
            temp.RotateFlip(RotateFlipType.Rotate180FlipY);
            RemakePaint.instance.PbImgEditor.Size = new Size(RemakePaint.instance.PbImgEditor.Width, RemakePaint.instance.PbImgEditor.Height);
            RemakePaint.instance.PbImgEditor.Image = temp;
            RemakePaint.instance.ResetLocationPbSizeTool();
        }

    }
}
