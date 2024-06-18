using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RemakePaint
{
    public partial class ShareFBForm : Form
    {
        private string accessToken = "EAAOMF1Q1yXkBOx0CBA5rMzxEyHtIVyAxHgPapPlelwXd80Y0OibPLrxgdT7OCOzSwj6gT8UzpboqTdUuqBrqBu8DIXZBFEFDSQqZCVBFptrB0aHfa51A9lYjJXmwjzIID5QHvGz4rNrUYHM3DRegY7kM7qS4zRZAsZA51aO3iCECisZAeRiWlJ7NfIvzTgd2MQvMfgJywdIUjiCBEZBmz7JUL1";
        private static readonly HttpClient client = new HttpClient();
        public ShareFBForm()
        {
            InitializeComponent();
        }
        private async void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Lấy đường dẫn ảnh từ PictureBox
            Image img = pbLoading.Image;

            lbLoading.Visible = true;
            pbLoading.Visible = true;
            string imgUrl = await UploadImageToImgur(RemakePaint.instance.pbMainScreen);
            await PostToFacebookAsync("351166211408939", accessToken, tbCaption.Text, imgUrl, true);
            this.Close();
        }
        public async Task PostToFacebookAsync(string pageId, string accessToken, string message, string urlimg, bool published)
        {
            var url = $"https://graph.facebook.com/v20.0/{pageId}/photos";
            var content = new
            {
                message = message,
                url = urlimg,
                published = published.ToString().ToLower(),
            };

            var jsonContent = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await client.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Post Image Successfully");
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Error: " + response.StatusCode);
                MessageBox.Show("Response: " + responseBody);
            }
        }
        public async Task<string> UploadImageToImgur(PictureBox pictureBox)
        {
            string clientId = "b10c52dec5cd5e6"; // Thay thế bằng Imgur Client ID của bạn
            byte[] imageBytes = ImageToByteArray(pictureBox, System.Drawing.Imaging.ImageFormat.Png);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", clientId);

                using (var content = new MultipartFormDataContent())
                {
                    var imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
                    content.Add(imageContent, "image");

                    var response = await client.PostAsync("https://api.imgur.com/3/upload", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = JObject.Parse(responseString);
                        return jsonResponse["data"]["link"].ToString();
                    }
                    else
                    {
                        throw new Exception("Failed to upload image: " + responseString);
                    }
                }
            }
        }

        private byte[] ImageToByteArray(PictureBox pictureBox, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                pictureBox.Image.Save(ms, format);
                return ms.ToArray();
            }
        }
    }
}
