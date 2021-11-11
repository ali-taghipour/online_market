using BLL;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD_ExtentionMethods;
using TAD_Security;

namespace KargahProject.Admin.Users
{
    public partial class Edit : System.Web.UI.Page
    {
        private UserManager UserManager;
        private int? id;
        public Edit()
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

            if (!IsPostBack)
            {
                var User = UserManager.GetById(id);
                if (User == null)
                {
                    Response.Redirect("~/admin/Users");
                    return;
                }

                FirstName.Value = User.FirstName;
                LastName.Value = User.LastName;
                Username.Value = User.Username;
                Email.Value = User.Email;
                Address.Value = User.Address;
                PostalCode.Value = User.PostalCode.ToString();
                if (User.IsEnabled)
                    Enabled.Checked = true;
                else
                    Disabled.Checked = true;

                if (User.IsMale)
                    Male.Checked = true;
                else
                    Female.Checked = true;

                OldPassword.Value = "";

                //نوع کاربر
                var model = ExtentionMethods.ToEnumViewModel<UserType>();
                foreach (var item in model)
                {
                    ListItem it = new ListItem(item.Title, item.Id.ToString());
                    if (item.Id == (int)User.Type)
                        it.Selected = true;
                    UserType.Items.Add(it);
                }
                
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
        /// ویرایش اسلاید
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditUser_ServerClick(object sender, EventArgs e)
        {
            var _User = UserManager.GetById(id);
            _User.FirstName = FirstName.Value;
            _User.LastName = LastName.Value;
            _User.Username = Username.Value;
            _User.Email = Email.Value;
            _User.Address = Address.Value;
            _User.PostalCode = PostalCode.Value.GetInt();
            _User.IsEnabled = Enabled.Checked;
            _User.IsMale = Male.Checked;
            _User.Type = (UserType)UserType.Items[UserType.SelectedIndex].Value.GetInt();
            _User.File = Request.Files["file"];

            var Result = UserManager.Validate(_User,OldPassword.Value , NewPassword.Value , RePassword.Value, true);
            if (!Result.IsValid)
            {
                ErrorDiv.InnerHtml = Result.Errors;
                return;
            }
            if(!string.IsNullOrEmpty(NewPassword.Value))
                _User.Password = NewPassword.Value.GetHash();
            var IsSuccess = UserManager.Update(_User);
            if (!IsSuccess)
            {
                ErrorDiv.InnerHtml = "ویرایش کاربر با خطا همراه بوده است!";
                return;
            }
            else
                Response.Redirect("~/Admin/Users");
        }

        



        /// <summary>
        /// حذف تصویر کاربر
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool DeleteUserPic(int UserId)
        {
            UserManager _UserManager = new UserManager();
            bool IsSuccess = _UserManager.DeleteUserPic(UserId);
            return IsSuccess;
        }
    }
}