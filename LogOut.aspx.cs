using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KargahProject
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //حذف کوکی کاربر
            if (Request.Cookies["User"] != null)
            {
                HttpCookie TempCoockie = new HttpCookie("User");
                TempCoockie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(TempCoockie);
            }

            //حذف سبد خرید باز
            if (Request.Cookies["BasketId"] != null)
            {
                HttpCookie TempCoockie = new HttpCookie("BasketId");
                TempCoockie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(TempCoockie);
            }

            Response.Redirect("~/Sign.aspx");
        }
    }
}