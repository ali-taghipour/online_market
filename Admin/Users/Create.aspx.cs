using BLL;
using Entities;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TAD_ExtentionMethods;
using TAD_Security;

namespace KargahProject.Admin.Users
{
    public partial class Create : System.Web.UI.Page
    {
        private UserManager UserManager;

        public Create()
        {
            UserManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var model = ExtentionMethods.ToEnumViewModel<UserType>();
            foreach (var item in model)
            {
                ListItem it = new ListItem(item.Title, item.Id.ToString());
                if (item == model.First())
                    it.Selected = true;
                UserType.Items.Add(it);
            }
        }



        protected void CreateUser_ServerClick(object sender, EventArgs e)
        {
            User User = new User()
            {
                CreateDate = DateTime.Now,
                FirstName = FirstName.Value,
                LastName = LastName.Value,
                Email = Email.Value,
                Address = Address.Value,
                PostalCode = PostalCode.Value.GetInt(),
                IsEnabled = Enabled.Checked,
                IsMale = Male.Checked,
                Username = Username.Value,
                Password = Password.Value.GetHash(),
                File = Request.Files["file"]
            };
            if (UserType.Items[UserType.SelectedIndex] != null)
                User.Type = (UserType)int.Parse(UserType.Items[UserType.SelectedIndex].Value);

            var Result = UserManager.Validate(User, null, Password.Value, RePassword.Value , false);
            if (!Result.IsValid)
            {
                ErrorDiv.InnerHtml = Result.Errors;
                return;
            }
            var IsSuccess = UserManager.Create(User);
            if (!IsSuccess)
            {
                ErrorDiv.InnerHtml = "ثبت کاربر با خطا همراه بوده است!";
                return;
            }
            else
                Response.Redirect("~/Admin/Users");
        }
    }
}