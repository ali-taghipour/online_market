using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD_ExtentionMethods;
using TAD_Security;

namespace KargahProject
{
    public partial class EditProfile : System.Web.UI.Page
    {
        private UserManager UserManager;
        private int id;
        public EditProfile()
        {
            UserManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Cookies["User"] == null || !int.TryParse(HttpContext.Current.Request.Cookies["User"]["Id"], out id))
                Response.Redirect("~/LogOut.aspx");
            
            if (id <= 0)
                Response.Redirect("~/LogOut.aspx");

            if (!IsPostBack)
            {
                var User = UserManager.GetById(id);
                if (User == null)
                    Response.Redirect("~/LogOut.aspx");

                FirstName.Value = User.FirstName;
                LastName.Value = User.LastName;
                Username.Value = User.Username;
                Email.Value = User.Email;
                Address.Value = User.Address;
                PostalCode.Value = User.PostalCode.ToString();
                
                if (User.IsMale)
                    Male.Checked = true;
                else
                    Female.Checked = true;

                OldPassword.Value = "";
                
                //تصویر کاربر
                if (!string.IsNullOrEmpty(User.Pic))
                {
                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes["class"] = "img-box";

                    HtmlImage img = new HtmlImage();
                    img.Src = User.Pic;
                    div.Controls.Add(img);

                    HtmlButton Button = new HtmlButton();
                    Button.Attributes["type"] = "button";
                    Button.Attributes["class"] = "btn btn-sm btn-danger";
                    Button.InnerText = "حذف تصویر";
                    Button.Attributes["onclick"] = "DeleteUserPic(" + User.Id + ")";
                    div.Controls.Add(Button);

                    UserImage.Controls.Add(div);
                }
            }
        }



        /// <summary>
        /// ویرایش کاربر
        /// </summary>
        protected void EditUser_ServerClick(object sender, EventArgs e)
        {
            var _User = UserManager.GetById(id);
            _User.FirstName = FirstName.Value;
            _User.LastName = LastName.Value;
            _User.Username = Username.Value;
            _User.Email = Email.Value;
            _User.Address = Address.Value;
            _User.PostalCode = PostalCode.Value.GetInt();
            _User.IsMale = Male.Checked;
            _User.File = Request.Files["file"];

            var Result = UserManager.Validate(_User, OldPassword.Value, NewPassword.Value, RePassword.Value, true);
            if (!Result.IsValid)
            {
                ErrorDiv.InnerHtml = Result.Errors;
                return;
            }
            if (!string.IsNullOrEmpty(NewPassword.Value))
                _User.Password = NewPassword.Value.GetHash();
            var IsSuccess = UserManager.Update(_User);
            if (!IsSuccess)
            {
                ErrorDiv.InnerHtml = "ویرایش کاربر با خطا همراه بوده است!";
                return;
            }



            _User = UserManager.GetById(id);
            if (HttpContext.Current.Request.Cookies["User"] != null)
                HttpContext.Current.Response.Cookies.Remove("User");

            HttpCookie Cookie = new HttpCookie("User");
            Cookie.Values["Id"] = _User.Id.ToString();
            Cookie.Values["Username"] = _User.Username;
            Cookie.Values["FullName"] = HttpContext.Current.Server.UrlEncode(_User.FullName);
            Cookie.Values["Pic"] = _User.Pic;
            Cookie.Values["Type"] = ((int)_User.Type).ToString();
            Cookie.Expires = DateTime.Now.AddMonths(3);
            HttpContext.Current.Response.Cookies.Add(Cookie);


            Response.Redirect("~/");
        }



        
    }
}