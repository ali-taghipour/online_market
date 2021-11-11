using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KargahProject.Admin
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Admin"] != null)
            {
                HttpCookie TempCoockie = new HttpCookie("Admin");
                TempCoockie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(TempCoockie);
            }
            Response.Redirect("~/Admin");
        }
    }
}