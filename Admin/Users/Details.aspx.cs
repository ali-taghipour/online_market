using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Users
{
    public partial class Details : System.Web.UI.Page
    {
        private UserManager UserManager;
        private int? id;
        public Details()
        {
            UserManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            id = Request.QueryString["id"].GetInt();
            if (id == null)
            {
                Response.Redirect("~/admin/Users");
                return;
            }
            var User = UserManager.GetById(id);
            if (User == null)
            {
                Response.Redirect("~/admin/Users");
                return;
            }

            FullName.InnerHtml = User.FullName;
            Username.InnerHtml = User.Username;
            Email.InnerHtml = User.Email;
            Address.InnerHtml = User.Address;
            PostalCode.InnerHtml = User.PostalCode.ToString();
            UserType.InnerHtml = User.Type.GetEnumDescription();
            IsMale.InnerHtml = User.IsMale ? "مرد" : "زن";
            IsEnabled.InnerHtml = User.IsEnabled ? "فعال" : "غیر فعال";
            IsEnabled.Attributes["class"] = User.IsEnabled ? "text-success" : "text-danger";
            CreateDate.InnerHtml = User.CreateDate.ToPersianDateTime().ToString();

            if (!string.IsNullOrEmpty(User.Pic))
            {
                HtmlImage img = new HtmlImage();
                img.Src = User.Pic;
                UserImage.Controls.Add(img);
            }
        }
    }
}