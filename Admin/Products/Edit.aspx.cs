using BLL;
using System;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Products
{
    public partial class Edit : System.Web.UI.Page
    {
        private ProductManager ProductManager;
        private int? id;
        public Edit()
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

            if (!IsPostBack)
            {
                var Product = ProductManager.GetById(id);
                if (Product == null)
                {
                    Response.Redirect("~/admin/Products");
                    return;
                }

                SelectedCategoryId.Value = Product.CategoryId.ToString();
                ProductTitle.Value = Product.Title;
                Description.Value = Product.Description;
                Summary.Value = Product.Summary;
                Inventory.Value = Product.Inventory.ToString();
                if(Product.OffPrice != null)
                    OffPrice.Value = (Product.OffPrice / 10).ToString();
                MainPrice.Value = (Product.MainPrice / 10).ToString();
                if (Product.IsEnabled)
                    Enabled.Checked = true;
                else
                    Disabled.Checked = true;


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
                    Button1.Attributes["onclick"] = "DeleteProductPic(" + item.Id +")";
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



        /// <summary>
        /// ویرایش محصول
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditProduct_ServerClick(object sender, EventArgs e)
        {
            var _Product = ProductManager.GetById(id);
            _Product.Title = ProductTitle.Value;
            _Product.OffPrice = OffPrice.Value.GetInt();
            _Product.IsEnabled = Enabled.Checked;
            _Product.Summary = Summary.Value;
            _Product.Description = Description.Value;
            _Product.CategoryId = SelectedCategoryId.Value.GetInt();
            _Product.Inventory = Inventory.Value.GetInt() ?? 0;

            var Result = ProductManager.Validate(_Product , MainPrice.Value);
            if (!Result.IsValid)
            {
                ErrorDiv.InnerHtml = Result.Errors;
                return;
            }

            _Product.MainPrice = (int)MainPrice.Value.GetInt();
            var IsSuccess = ProductManager.Update(_Product);
            if (!IsSuccess)
            {
                ErrorDiv.InnerHtml = "ویرایش محصول با خطا همراه بوده است!";
                return;
            }

            if (Request.Files.Count > 0)
            {
                PictureManager PictureManager = new PictureManager();
                PictureManager.UploadAllPics(_Product.Id, Request.Files);
            }
            Response.Redirect("~/Admin/Products/Details.aspx?id=" + _Product.Id);
        }








        /// <summary>
        /// حذف تصویر محصول
        /// </summary>
        /// <param name="PicId">آیدی تصویر</param>
        /// <returns></returns>
        [WebMethod]
        public static bool DeleteProductPic(int? PicId)
        {
            if (PicId == null)
                return false;
            PictureManager PictureManager = new PictureManager();
            bool IsSuccess = PictureManager.Delete((int)PicId);
            return IsSuccess;
        }









        /// <summary>
        /// تعیین تصویر اصلی محصول
        /// </summary>
        /// <param name="PicId">آیدی تصویر</param>
        /// <param name="ProductId">آیدی محصول</param>
        /// <returns></returns>
        [WebMethod]
        public static bool SetProductMainPic(int? PicId , int? ProductId)
        {
            if (PicId == null || ProductId == null)
                return false;
            PictureManager PictureManager = new PictureManager();
            bool IsSuccess = PictureManager.SetMainPic((int)PicId , (int)ProductId);
            return IsSuccess;
        }
    }
}