using BLL;
using System;
using System.Web.UI.HtmlControls;


namespace Shop
{
    public partial class SiteMP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ساخت دسته بندی ها
            GenerateCategories();

            
            //قرار دادن متن جستجو شده در اینپوت جستجو 
            string _Search = Request.QueryString["Search"];
            SearchInput.Value = _Search;

            //وضعیت لاگین کاربر
            UserManager UserManager = new UserManager();
            int UserId;
            if (Request.Cookies["User"] != null && int.TryParse(Request.Cookies["User"]["Id"], out UserId))
            {
                LoginButton.Attributes["class"] = "login-link hide";
                EditProfileButton.InnerHtml = Server.UrlDecode(Request.Cookies["User"]["FullName"]);
            }
            else
            {
                EditProfileButton.Attributes["class"] = "login-link hide";
                LogOutButton.Attributes["class"] = "login-link hide";
            }
            


        }




        //افزودن سر دسته ها به منو اصلی
        public void GenerateCategories()
        {
            CategoryManager CategoryManager = new CategoryManager();
            var Categories = CategoryManager.GetParentCategories(true);
            foreach (var item in Categories)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HtmlAnchor anc = new HtmlAnchor();
                anc.InnerText = item.Title;
                anc.HRef = "~/ListProduct.aspx?catid=" + item.Id;
                li.Controls.Add(anc);
                MainMenu.Controls.Add(li);
            }
        }
        
    }
}