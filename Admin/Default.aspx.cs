using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        UserManager UserManager;

        public Default()
        {
            UserManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Admin"] != null)
                {
                    var Res = UserManager.UserIsValidForAdminPanel();
                    if (Res.IsValid)
                        Response.Redirect("~/Admin/Dashboard.aspx");
                }
                error.InnerHtml = "Username : admin <br/> pass : 123123 ";
            }            
        }

        protected void Login_ServerClick(object sender, EventArgs e)
        {
            if (Session["CaptchaImageText"] == null || sec_text.Value != Session["CaptchaImageText"].ToString())
            {
                error.InnerHtml = "کد امنیتی صحیح نمی باشد.";
                return;
            }

            string Username = username.Value;
            string Password = password.Value;            

            var Res = UserManager.AdminPanelLogin(Username, Password);
            if (Res.IsValid)
                Response.Redirect("~/Admin/Dashboard.aspx");
            else
            {
                error.InnerHtml = Res.Errors;
                return;
            }
        }
    }
}