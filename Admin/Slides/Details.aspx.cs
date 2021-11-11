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
    public partial class Details : System.Web.UI.Page
    {
        private SlideManager SlideManager;
        private int? id;
        public Details()
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
            var Slide = SlideManager.GetByIdWithJoins(id);
            if (Slide == null)
            {
                Response.Redirect("~/admin/Slides");
                return;
            }

            SlideTitle.InnerHtml = Slide.Title;
            Link.InnerHtml = Slide.Link;
            Order.InnerHtml = Slide.Order.ToString();
            StartDate.InnerHtml = Slide.StartDate.ToPersianDateTime()?.ToShortDateString();
            EndDate.InnerHtml = Slide.EndDate.ToPersianDateTime()?.ToShortDateString();
            IsEnabled.InnerHtml = Slide.IsEnabled ? "فعال" : "غیر فعال";
            IsEnabled.Attributes["class"] = Slide.IsEnabled ? "text-success" : "text-danger";
            CreateDate.InnerHtml = Slide.PersianCreateDate;

            if(Slide.AdminId != null)
            {
                HtmlAnchor anc = new HtmlAnchor();
                anc.InnerHtml = Slide.AdminName;
                anc.HRef = "~/Admin/Users/Details.aspx?id=" + Slide.AdminId;
                Admin.Controls.Add(anc);
            }

            if (!string.IsNullOrEmpty(Slide.LargePath))
            {
                HtmlImage img = new HtmlImage();
                img.Src = Slide.LargePath;
                SlideImage.Controls.Add(img);
            }
        }
    }
}