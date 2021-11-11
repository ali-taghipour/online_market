using BLL;
using Entities;
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
    public partial class Edit : System.Web.UI.Page
    {
        private SlideManager SlideManager;
        private int? id;
        public Edit()
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

            if (!IsPostBack)
            {
                var Slide = SlideManager.GetById(id);
                if (Slide == null)
                {
                    Response.Redirect("~/admin/Slides");
                    return;
                }

                SlideTitle.Value = Slide.Title;
                Link.Value = Slide.Link;
                Order.Value = Slide.Order.ToString();
                StartDate.Value = Slide.StartDate.ToPersianDateTime()?.ToShortDateString();
                EndDate.Value = Slide.EndDate.ToPersianDateTime()?.ToShortDateString();
                if (Slide.IsEnabled)
                    Enabled.Checked = true;
                else
                    Disabled.Checked = true;
                if (!string.IsNullOrEmpty(Slide.LargePath))
                {
                    HtmlImage img = new HtmlImage();
                    img.Src = Slide.LargePath;
                    SlideImage.Controls.Add(img);
                }
            }
        }
        


        /// <summary>
        /// ویرایش اسلاید
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditSlide_ServerClick(object sender, EventArgs e)
        {
            var _Slide = SlideManager.GetById(id);
            _Slide.Link = Link.Value;
            _Slide.Title = SlideTitle.Value;
            _Slide.Order = Order.Value.GetInt();
            _Slide.IsEnabled = Enabled.Checked;
            _Slide.StartDate = StartDate.Value.ToDateTime(true);
            _Slide.EndDate = EndDate.Value.ToDateTime(false);
            _Slide.File = Request.Files["file"];
            
            var Result = SlideManager.Validate(_Slide);
            if (!Result.IsValid)
            {
                ErrorDiv.InnerHtml = Result.Errors;
                return;
            }
            var IsSuccess = SlideManager.Update(_Slide);
            if (!IsSuccess)
            {
                ErrorDiv.InnerHtml = "ویرایش اسلایدر با خطا همراه بوده است!";
                return;
            }
            else
                Response.Redirect("~/Admin/Slides");
        }

    }
}