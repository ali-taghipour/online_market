using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Slides
{
    public partial class Delete : System.Web.UI.Page
    {
        private SlideManager SlideManager;
        private int? id;
        public Delete()
        {
            SlideManager = new SlideManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            id = Request.QueryString["id"].GetInt();
            if (id == null)
            {
                Response.Redirect("~/admin/Slides");
                return;
            }
            var Slide = SlideManager.GetById(id);
            if (Slide == null)
            {
                Response.Redirect("~/admin/Slides");
                return;
            }

            SlideTitle.InnerHtml = Slide.Title;
            Link.InnerHtml = Slide.Link;
            IsEnabled.InnerHtml = Slide.IsEnabled ? "فعال" : "غیر فعال";
            IsEnabled.Attributes["class"] = Slide.IsEnabled ? "text-success" : "text-danger";
            if (!string.IsNullOrEmpty(Slide.LargePath))
            {
                HtmlImage img = new HtmlImage();
                img.Src = Slide.LargePath;
                SlideImage.Controls.Add(img);
            }
        }


        protected void DeleteSlide_ServerClick(object sender, EventArgs e)
        {
            var IsSuccess = SlideManager.Delete((int)id);
            if (IsSuccess)
                Response.Redirect("~/Admin/Slides");
            else
                ErrorDiv.InnerHtml = "حذف اسلاید با خطا همراه بوده است! مجددا اقدام نمایید.";
            return;
        }
    }
}