using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemakePaint
{
    public partial class AskAIForm : Form
    {
        public AskAIForm()
        {
            InitializeComponent();
        }

        private async void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Đặt khóa API của bạn vào đây
            var apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNTUyMTZjODEtYzY0MC00NjY4LThkNzQtZTRkMGJhOTQxODRlIiwidHlwZSI6ImFwaV90b2tlbiJ9.nGhgn-URblWpVK6RQUb0FRub4ilkQySVdHD5hw9nAQM";

            // URL của API
            var url = "https://api.edenai.run/v2/image/generation";

            // Tạo RestClient và RestRequest
            var client = new RestClient(url);
            var request = new RestRequest("/", Method.Post);

            // Thêm các header
            request.AddHeader("authorization", $"Bearer {apiKey}");
            request.AddHeader("content-type", "application/json");

            // Tạo nội dung body
            var body = new
            {
                providers = "openai,deepai,stabilityai",
                text = tbPrompt.Text,
                resolution = "512x512",
                num_images = 1
            };

            // Thêm body vào request
            request.AddJsonBody(body);

            lbLoading.Visible = true;
            pbLoading.Visible = true;

            // Gửi yêu cầu và nhận phản hồi
            var response = await client.ExecuteAsync(request);

            // Kiểm tra và hiển thị kết quả
            if (response.IsSuccessful)
            {
                string[] content = response.Content.Split('"');
                string base64 = content[11];
                Image img = ConvertToImage(base64);

                RemakePaint.instance.bm = new Bitmap(img, new Size(img.Width, img.Height));
                RemakePaint.instance.pbMainScreen.Size = new Size(img.Width, img.Height);
                RemakePaint.instance.pbMainScreen.Refresh();
                RemakePaint.instance.pbMainScreen.Image = RemakePaint.instance.bm;
                RemakePaint.instance.g = Graphics.FromImage(RemakePaint.instance.bm);
                RemakePaint.instance.ResetLocationSizeTool();

                MessageBox.Show("Image create successfully");
                lbLoading.Visible = false;
                pbLoading.Visible = false;
            }
            else
            {
                MessageBox.Show("Image create false");
            }
        }
        public static Image ConvertToImage(string base64String)
        {
            // Chuyển đổi base64 string thành mảng byte
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // Tạo một MemoryStream từ mảng byte
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                // Đọc hình ảnh từ MemoryStream
                Image image = Image.FromStream(ms);

                // Trả về đối tượng hình ảnh đã đọc
                return image;
            }
        }
    }
}
