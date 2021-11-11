using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Products
{
    public partial class Details : System.Web.UI.Page
    {
        private ProductManager ProductManager;
        private int? id;
        public Details()
        {
            ProductManager = new ProductManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            id = Request.QueryString["id"].GetInt();
            if (id == null)
            {
                Response.Redirect("~/admin/Products");
                return;
            }
            var Product = ProductManager.GetById(id);
            if (Product == null)
            {
                Response.Redirect("~/admin/Products");
                return;
            }

            ProductTitle.InnerHtml = Product.Title;
            MainPrice.InnerHtml = Product.MainPrice.GetToomanPriceFormat() + " تومان";
            if(Product.OffPrice != null)
                OffPrice.InnerHtml = Product.OffPrice.GetToomanPriceFormat() + " تومان";
            Summary.InnerHtml = Product.Summary;
            Description.InnerHtml = Product.Description;
            IsEnabled.InnerHtml = Product.IsEnabled ? "فعال" : "غیر فعال";
            IsEnabled.Attributes["class"] = Product.IsEnabled ? "text-success" : "text-danger";
            CreateDate.InnerHtml = Product.CreateDate.ToPersianDateTime().ToString();
            CategoryTitle.InnerHtml = Product.CategoryTitle;
            Inventory.InnerHtml = Product.Inventory.ToString();

            if (Product.AdminId != null)
            {
                UserManager UserManager = new UserManager();
                var Admin = UserManager.GetById(Product.AdminId);
                if(Admin != null)
                {
                    HtmlAnchor anc = new HtmlAnchor();
                    anc.HRef = "~/Admin/Users/Details.aspx?id="+ Admin.Id;
                    anc.InnerHtml = Admin.FullName;
                    AdminName.Controls.Add(anc);
                }
            }


            //تصاویر محصول
            PictureManager PictureManager = new PictureManager();
            var Pics = PictureManager.GetByProductId(id);
            foreach (var item in Pics)
            {
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes["class"] = "img-box";
                div.Attributes["rel"] = item.Id.ToString();
                if (item.IsMain)
                    div.Attributes["class"] = "img-box main";

                HtmlImage img = new HtmlImage();
                img.Src = item.ThumbPath;
                div.Controls.Add(img);

                HtmlButton Button1 = new HtmlButton();
                Button1.Attributes["type"] = "button";
                Button1.Attributes["class"] = "btn btn-sm btn-danger";
                Button1.InnerText = "حذف تصویر";
                Button1.Attributes["onclick"] = "DeleteProductPic(" + item.Id + ")";
                div.Controls.Add(Button1);

                if (!item.IsMain)
                {
                    HtmlButton Button2 = new HtmlButton();
                    Button2.Attributes["type"] = "button";
                    Button2.Attributes["class"] = "btn btn-sm btn-success";
                    Button2.InnerText = "تصویر اصلی شود";
                    Button2.Attributes["onclick"] = "SetProductMainPic(" + item.Id + "," + Product.Id + ")";
                    div.Controls.Add(Button2);
                }

                ProductImage.Controls.Add(div);
            }
        }
    }
}