using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;

namespace Shop
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateSlides();
            GenerateNewPosts();
        }




        //ساختن ایتم های اسلایدر
        public void GenerateSlides()
        {
            SlideManager SlideManager = new SlideManager();
            var Slides = SlideManager.GetAllActive();

            foreach (var item in Slides)
            {
                HtmlGenericControl div = new HtmlGenericControl("div");
                if (Slides.IndexOf(item) == 0)
                    div.Attributes.Add("class", "show");
                HtmlImage img = new HtmlImage();
                img.Src = item.LargePath;
                img.Alt = item.Title;
                div.Controls.Add(img);

                if (!string.IsNullOrEmpty(item.Title) || !string.IsNullOrEmpty(item.Link))
                {
                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerText = item.Title;
                    div.Controls.Add(span);

                    if (!string.IsNullOrEmpty(item.Link))
                    {
                        HtmlGenericControl InnerDiv = new HtmlGenericControl("div");
                        HtmlAnchor anc = new HtmlAnchor();
                        anc.InnerHtml = "بیشتر بخوانید";
                        anc.HRef = item.Link;
                        anc.Attributes["class"] = "btn btn-primary";
                        InnerDiv.Controls.Add(anc);
                        span.Controls.Add(InnerDiv);
                    }
                }
                Slider.Controls.Add(div);
            }


        }





        //ساختن آیتم های جدیدترین محصولات
        public void GenerateNewPosts()
        {
            ProductManager ProductManager = new ProductManager();
            var Posts = ProductManager.GetNewPosts(8);
            GenerateView GenerateView = new GenerateView();
            foreach (var item in Posts)
                ProductContainer.Controls.Add(GenerateView.ProductItem(item));
        }

    }
}