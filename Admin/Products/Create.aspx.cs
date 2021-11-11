using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Products
{
    public partial class Create : System.Web.UI.Page
    {
        private ProductManager ProductManager;

        public object SelectedCategoryId { get; private set; }
        public object ProductTitle { get; private set; }

        public Create()
        {
            ProductManager = new ProductManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }



        protected void CreateProduct_ServerClick(object sender, EventArgs e)
        {
            Product Product = new Product()
            {
                CreateDate = DateTime.Now,
                CategoryId = SelectedCategoryId.Value.GetInt(),
                Title = ProductTitle.Value,
                Description = Description.Value,
                Summary = Summary.Value,
                IsEnabled = Enabled.Checked,
                OffPrice = OffPrice.Value.GetInt(),
                Inventory = Inventory.Value.GetInt() ?? 0
            };
            
            var Result = ProductManager.Validate(Product,MainPrice.Value);
            if (!Result.IsValid)
            {
                ErrorDiv.InnerHtml = Result.Errors;
                return;
            }
            Product.MainPrice = (int)MainPrice.Value.GetInt();
            Product OutProduct = ProductManager.Create(Product);
            if (OutProduct == null)
            {
                ErrorDiv.InnerHtml = "ثبت کاربر با خطا همراه بوده است!";
                return;
            }

            if(Request.Files.Count > 0)
            {
                PictureManager PictureManager = new PictureManager();
                PictureManager.UploadAllPics(OutProduct.Id, Request.Files);
            }
            Response.Redirect("~/Admin/Products/Details.aspx?id=" + OutProduct.Id);
        }
    }
}