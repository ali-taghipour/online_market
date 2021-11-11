using BLL;
using Entities;
using System;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Slides
{
    public partial class Create : System.Web.UI.Page
    {
        private SlideManager SlideManager;

        public Create()
        {
            SlideManager = new SlideManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateSlide_ServerClick(object sender, EventArgs e)
        {
            Slide _Slide = new Slide()
            {
                CreateDate = DateTime.Now,
                Link = Link.Value,
                Title = SlideTitle.Value,
                Order = Order.Value.GetInt(),
                IsEnabled = Enabled.Checked,
                StartDate = StartDate.Value.ToDateTime(true),
                EndDate = EndDate.Value.ToDateTime(false),
                File = Request.Files["file"]
            };

            var Result = SlideManager.Validate(_Slide);
            if (!Result.IsValid)
            {
                ErrorDiv.InnerHtml = Result.Errors;
                return;
            }
            var IsSuccess = SlideManager.Create(_Slide);
            if (!IsSuccess)
            {
                ErrorDiv.InnerHtml = "ثبت اسلایدر با خطا همراه بوده است!";
                return;
            }
            else
                Response.Redirect("~/Admin/Slides");
        }
    }
}