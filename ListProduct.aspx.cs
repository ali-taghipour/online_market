using BLL;
using Entities;
using KargahProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;

namespace Shop
{
    public partial class ListProduct : System.Web.UI.Page
    {
        static int CatId = 0;
        ProductManager ProductManager;
        public ListProduct()
        {
            ProductManager = new ProductManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.QueryString["catid"] != null)
                int.TryParse(HttpContext.Current.Request.QueryString["catid"], out CatId);
            else
                CatId = 0;

            //تعیین صفحه درخواستی، متن جستجو و اندازه صفحه
            int _Page = 1;
            int _PageSize = 12;
            string _Search = Request.QueryString["Search"];

            //گرفتن آیتمهای مورد نیاز با توجه به پارامتر های جستجو
            SearchResultViewModel<Product> model = new SearchResultViewModel<Product>();
            if (CatId <= 0)
                model = ProductManager.GetSearchedItem(null, _Page, _PageSize, _Search);
            else
            {
                CategoryManager CategoryManager = new CategoryManager();
                var Category = CategoryManager.GetById(CatId);
                CategoryTitle.InnerHtml = Category.Title;
                model = ProductManager.GetSearchedItem(CatId, _Page, _PageSize, _Search);
            }

            var Products = model.Items;
            if (model.Items.Count() == 0)
            {
                ProductContainer.Attributes["class"] = "show-error";
                ProductContainer.InnerHtml = "داده ای برای نمایش یافت نشد!";
                return;
            }
            GeneratePosts(model.Items);
            GeneratePagination(model.PageCount, model.CurrentPage);
        }





        //ساختن آیتم های محصولات
        public void GeneratePosts(List<Product> Products)
        {
            GenerateView GenerateView = new GenerateView();
            foreach (var item in Products)
                ProductContainer.Controls.Add(GenerateView.ProductItem(item));
        }





        /// <summary>
        /// ساخت پیجینیشن
        /// </summary>
        /// <param name="PageCount">تعداد صفحات</param>
        /// <param name="CurrentPage">صفحه درخواستی</param>
        public void GeneratePagination(int PageCount, int CurrentPage)
        {
            GenerateView GenerateView = new GenerateView();
            var PageItems = GenerateView.GetPaginationItems(PageCount, CurrentPage);
            foreach (var item in PageItems)
                Pagination.Controls.Add(item);
        }





        /// <summary>
        /// گرفتن لیست محصولات سرچ شده
        /// </summary>
        /// <param name="Page">شماره صفحه</param>
        /// <param name="PageSize">تعداد ایتم های هر صفحه</param>
        /// <param name="Search">متن جستجو</param>
        /// <returns></returns>
        [WebMethod]
        public static object SearchProduct(int? Page , int? PageSize , string Search)
        {
            ProductManager ProductManager = new ProductManager();
            int? CategoryId = CatId <= 0 ? null : (int?)CatId;
            var Res = ProductManager.GetSearchedItem(CategoryId, Page, PageSize, Search);
            GenerateView GenerateView = new GenerateView();

            string ProductHtml = "";
            foreach (var item in Res.Items) 
                ProductHtml += GenerateView.GetRawHtml(GenerateView.ProductItem(item));

            string PageinationHtml = "";
            var PageItems = GenerateView.GetPaginationItems(Res.PageCount , Res.CurrentPage);
            foreach (var item in PageItems)
                PageinationHtml += GenerateView.GetRawHtml(item);

            return new { TotalCount = Res.Items.Count(), ProductHtml = ProductHtml, PageinationHtml = PageinationHtml };
        }



    }
}