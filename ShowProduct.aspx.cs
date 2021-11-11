using BLL;
using Entities;
using KargahProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD_ExtentionMethods;

namespace Shop
{
    public partial class ShowProduct : System.Web.UI.Page
    {
        ProductManager ProductManager;
        int _ProductId;
        public ShowProduct()
        {
            ProductManager = new ProductManager();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"], out _ProductId))
                Response.Redirect("~/ListProduct.aspx");
            ProductId.Value = _ProductId.ToString();

            int _UserId = 0;
            if (Request.Cookies["User"] != null
                && int.TryParse(HttpContext.Current.Request.Cookies["User"]["Id"], out _UserId))
            {
                UserId.Value = _UserId.ToString();
                CommentFullName.Style["display"] = "none";
                CommentEmail.Style["display"] = "none";
            }


            var Product = ProductManager.GetFullDetailsById(_ProductId);
            if(Product == null)
                Response.Redirect("~/ListProduct.aspx");
            ProductTitle.InnerHtml = Product.Title;
            Summary.InnerHtml = Product.Summary;
            Description.InnerHtml = Product.Description;
            if(Product.OffPrice != null)
                MainPrice.InnerHtml = Product.MainPrice.ToString();
            FinalPrice.InnerHtml = Product.FinalPrice.ToString() + " <span>تومان</span>";

            LikeManager LikeManager = new LikeManager();
            if (LikeManager.UserLikeProduct(_UserId, _ProductId))
                LikeButton.Attributes.Add("class", "like-btn like");
            LikeCount.InnerHtml = Product.LikeCount.ToString();

            if (Product.Inventory > 0)
                AddToBasketButton.Attributes["onclick"] = "AddToBasket(" + Product.Id + ")";
            else
            {
                AddToBasketButton.Attributes["class"] = "inventory";
                AddToBasketButton.InnerHtml = "ناموجود";
            }

            CreatePictures(Product.Pictures);
            CreateComments(Product.Comments);

        }





        /// <summary>
        /// ساخت تصویرهای پست
        /// </summary>
        /// <param name="Pictures"></param>
        public void CreatePictures(List<Picture> Pictures)
        {
            if (Pictures == null || Pictures.Count() == 0)
            {
                PictureSection.Style["display"] = "none";
                SummarySection.Attributes["class"] = "col-md-12";
                return;
            }
            HtmlGenericControl div1 = new HtmlGenericControl("div");
            div1.Attributes["class"] = "product-slider";

            HtmlGenericControl figure = new HtmlGenericControl("figure");
            figure.Attributes["class"] = "product-slider-show";

            HtmlImage LargeImg = new HtmlImage();
            LargeImg.Src = Pictures.First().LargePath;
            figure.Controls.Add(LargeImg);
            div1.Controls.Add(figure);

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Attributes["class"] = "product-slider-nav";
            
            foreach (var item in Pictures)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");

                HtmlImage ThumbImg = new HtmlImage();
                ThumbImg.Src = item.LargePath;

                li.Controls.Add(ThumbImg);
                ul.Controls.Add(li);
            }
            div1.Controls.Add(ul);
            PictureSection.Controls.Add(div1);
        }






        public void CreateComments(List<Comment> Comments)
        {
            if(Comments == null || Comments.Count() == 0)
            {
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes["class"] = "show-error";
                div.InnerHtml = "نظری برای نمایش یافت نشد.";
                CommentSection.Controls.Add(div);
                return;
            }


            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Attributes["class"] = "product-comment";

            foreach (var item in Comments)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HtmlGenericControl div1 = new HtmlGenericControl("div");

                HtmlGenericControl span1 = new HtmlGenericControl("span");
                span1.InnerHtml = item.UserId == null ? item.FullName : item.UserFullName;
                div1.Controls.Add(span1);

                HtmlGenericControl span2 = new HtmlGenericControl("span");
                span2.InnerHtml = item.CreateDate.ToPersianDateTime().ToString();
                div1.Controls.Add(span2);

                HtmlGenericControl div2 = new HtmlGenericControl("div");
                div2.InnerHtml = item.Text;

                li.Controls.Add(div1);
                li.Controls.Add(div2);
                ul.Controls.Add(li);
            }
            CommentSection.Controls.Add(ul);
        }






        /// <summary>
        /// لایک کردن یا برداشتن لایک از یک محصول توسط کاربر موجود در کوکی
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [WebMethod]
        public static object ToggleLike(int? ProductId , int? UserId)
        {
            if (ProductId == null)
                return new { Status = false, Message = "محصول یافت نشد." };

            if (UserId == null)
                return new { Status = false, Message = "برای لایک محصول ابتدا لاگین کنید." };
            
            LikeManager LikeManager = new LikeManager();
            ToggleLikeViewModel Res = LikeManager.ToggleLike((int)UserId, (int)ProductId);
            if(Res == null)
                return new { Status = false, Message = "خطا رخ داده است." };

            return new { Status = true, LikeCount = Res.LikeCount, IsLiked = Res.IsLiked };

        }






        /// <summary>
        /// درج کامنت
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object InsertComment(int? ProductId, int? UserId, string FullName,string Email , string Text , string Code)
        {
            if (ProductId == null)
                return new { Status = false, Message = "محصول یافت نشد." };
            
            if (HttpContext.Current.Session["CaptchaImageText"] == null || Code != HttpContext.Current.Session["CaptchaImageText"].ToString())
                return new { Status = true, Message = "کد امنیتی صحیح نمی باشد." };

            Comment Comment = new Comment()
            {
                CreateDate= DateTime.Now,
                Email = Email,
                FullName = FullName,
                IsApproved=false,
                IsReaded=false,
                ProductId = ProductId,
                UserId = UserId,
                Text = Text                
            };
            CommentManager CommentManager = new CommentManager();
            var IsSuccess = CommentManager.Create(Comment);
            if(IsSuccess)
                return new { Status = true, Message = "نظر شما ثبت شد." };
            else
                return new { Status = false, Message = "ثبت نظر با خطا همراه بوده است." };
        }





    }
}