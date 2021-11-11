using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;

namespace Shop
{
    public partial class captcha : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["CaptchaImageText"] = GenerateRandomCode();
            Helper.RandomImage ci = new Helper.RandomImage(this.Session["CaptchaImageText"].ToString(), 300, 75);

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            ci.Dispose();
        }
        private string GenerateRandomCode()
        {
            Random r = new Random();
            string s = "";
            for (int j = 0; j < 5; j++)
            {
                int i = r.Next(3);
                int ch;

                ch = r.Next(0, 9);
                s = s + ch.ToString();
                r.NextDouble();
                r.Next(100, 1999);
            }
            return s;
        }
    }
}