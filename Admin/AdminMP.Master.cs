using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.Admin
{
    public partial class AdminMP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserManager UserManager = new UserManager();
            var Res = UserManager.UserIsValidForAdminPanel();
            if(!Res.IsValid)
                Response.Redirect("~/Admin/LogOut.aspx");
            AdminName.InnerHtml = Server.UrlDecode(Request.Cookies["Admin"]["FullName"]);

        }
    }
}