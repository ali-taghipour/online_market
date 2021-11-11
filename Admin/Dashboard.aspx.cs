using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductManager ProductManager = new ProductManager();
            AllProductCount.InnerHtml = ProductManager.GetAllCount().ToString();

            UserManager UserManager = new UserManager();
            AllUserCount.InnerHtml = UserManager.GetAllCount().ToString();

            CommentManager CommentManager = new CommentManager();
            UnreadCommentCount.InnerHtml = CommentManager.GetUnreadCommentsCount().ToString();
        }
    }
}