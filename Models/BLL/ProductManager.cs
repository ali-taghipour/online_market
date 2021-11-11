 using DAL;
using Entities;
using KargahProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TAD_ExtentionMethods;
using TAD_ImageResizer;
using TAD_Security;


namespace BLL
{

    /// <summary>
    /// Summary description for ProductManager
    /// </summary>
    public class ProductManager
    {
        private ProductRepository Repo;
        public ProductManager()
        {
            Repo = new ProductRepository();
        }


        /// <summary>
        /// گرفتن محصول با آیدی
        /// </summary>
        /// <param name="Id">آیدی محصول</param>
        /// <returns></returns>
        public Product GetById(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetById((int)Id);
            return ToDataModel(DataRow);
        }




        /// <summary>
        /// گرفتن همه اطلاعات محصول شامل کامنتها، تصاویر، تعداد لایک و... با آیدی
        /// </summary>
        /// <param name="Id">آیدی محصول</param>
        /// <returns></returns>
        public Product GetFullDetailsById(int? Id)
        {
            if (Id == null)
                return null;
            DataSet DataSet = Repo.GetFullDetailsById((int)Id);
            if (DataSet == null)
                return null;
            var Product = ToDataModel(DataSet.Tables[0].Rows[0]);
            if (Product == null)
                return null;

            CommentManager CommentManager = new CommentManager();
            Product.Comments = CommentManager.ToDataModel(DataSet.Tables[1]);

            PictureManager PictureManager = new PictureManager();
            Product.Pictures = PictureManager.ToDataModel(DataSet.Tables[2]);

            LikeManager LikeManager = new LikeManager();
            Product.LikeCount = int.Parse(DataSet.Tables[3].Rows[0][0].ToString());
            
            return Product;
        }





        /// <summary>
        /// گرفتن لیست همه محصولات
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAll()
        {
            DataTable DataTable = Repo.GetAll();
            return ToDataModel(DataTable);
        }




        /// <summary>
        /// گرفتن تعداد همه محصولات
        /// </summary>
        /// <returns></returns>
        public int? GetAllCount()
        {
            return Repo.GetAllCount();
        }

        


        /// <summary>
        ///با ایدی گرفتن لیست همه محصولات
        /// </summary>
        /// <returns></returns>
        public List<Product> GetByCategoryId(int? CategoryId)
        {
            DataTable DataTable = Repo.GetByCategoryId((int)CategoryId);
            return ToDataModel(DataTable);
        }




        /// <summary>
        /// گرفتن همه محصولاتیی که در حال حاضر قابل نمایش هستند
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllActive()
        {
            DataTable DataTable = Repo.GetAllActive();
            return ToDataModel(DataTable);
        }





        /// <summary>
        /// گرفتن لیست محصولات سرچ شده
        /// </summary>
        /// <param name="Page">شماره صفحه. پیشفرض 1 است.</param>
        /// <param name="PageSize">تعداد آیتم های صفحه. پیشفرض 12 است.</param>
        /// <param name="SearchText">متن جستجو شده</param>
        /// <returns></returns>
        public SearchResultViewModel<Product> GetSearchedItem(int? CategoryId, int? Page, int? PageSize, string SearchText)
        {
            DataSet DataSet = Repo.GetSearchedItem(CategoryId, Page, PageSize, SearchText);
            if (DataSet == null)
                return null;
            //تعداد کل ایتم ها
            int TotalCount = int.Parse(DataSet.Tables[1].Rows[0][0].ToString());
            //تعداد صفحه هایی که خواهیم داشت
            int PageCount = 1;
            if (PageSize != null)
            {
                PageCount = TotalCount / (int)PageSize;
                if (TotalCount % (int)PageSize != 0)
                    PageCount++;
            } 
            int CurrentPage = Page ?? 1;

            SearchResultViewModel<Product> model = new SearchResultViewModel<Product>()
            {
                CurrentPage = CurrentPage,
                PageCount = PageCount,
                Items = ToDataModel(DataSet.Tables[0])
            };
            return model;
        }




        /// <summary>
        /// گرفتن جدیدترین محصولاتی که قابل نمایش هستند
        /// </summary>
        /// <returns></returns>
        public List<Product> GetNewPosts(int Count)
        {
            DataTable DataTable = Repo.GetNewPosts(Count);
            return ToDataModel(DataTable);
        }





        /// <summary>
        /// ایجاد محصول جدید
        /// </summary>
        /// <returns></returns>
        public Product Create(Product Product)
        {
            Product.CreateDate = DateTime.Now;
            Product.MainPrice *= 10;
            if (Product.OffPrice != null)
                Product.OffPrice *= 10;
            DataRow DataRow = Repo.Create(Product);
            return ToDataModel(DataRow);
        }




        /// <summary>
        /// آپدیت محصول
        /// </summary>
        /// <returns></returns>
        public bool Update(Product Product)
        {
            Product.MainPrice *= 10;
            if (Product.OffPrice != null)
                Product.OffPrice *= 10;
            return Repo.Update(Product);
        }






        /// <summary>
        /// آپدیت تعداد موجودی محصول
        /// </summary>
        /// <returns></returns>
        public bool UpdateInventory(int ProductId , int NewInventory)
        {
            return Repo.UpdateInventory(ProductId, NewInventory);
        }




        /// <summary>
        /// حذف محصول
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            return Repo.Delete(Id);
        }





        /// <summary>
        /// ولیدیت کردن آبجکت محصول
        /// </summary>
        public ValidateResultViewModel Validate(Product Product , string MainPrice)
        {
            bool IsValid = true;
            string Errors = "";

            if (string.IsNullOrEmpty(MainPrice))
            {
                IsValid = false;
                Errors += "- قیمت محصول نمیتواند خالی باشد. <br />";
            }
            else {
                Product.MainPrice = (int)MainPrice.GetInt();
                if (Product.MainPrice < 0)
                {
                    IsValid = false;
                    Errors += "- قیمت محصول نمیتواند کمتر از 0 باشد. <br />";
                }
            }

            

            if (Product.OffPrice != null && Product.OffPrice < 0)
            {
                IsValid = false;
                Errors += "- قیمت محصول نمیتواند کمتر از 0 باشد. <br />";
            }

            if (string.IsNullOrEmpty(Product.Title))
            {
                IsValid = false;
                Errors += "- عنوان محصول را وارد کنید. <br />";
            }
            else if (Product.Title.Length > 300)
            {
                IsValid = false;
                Errors += "- عنوان محصول حداکثر 300 کاراکتر باشد. <br />";
            }


            if (Product.Summary.Length > 300)
            {
                IsValid = false;
                Errors += "- خلاصه محصول حداکثر 1000 کاراکتر باشد. <br />";
            }


            if (Product.CategoryId == null)
            {
                IsValid = false;
                Errors += "- دسته بندی را انتخاب کنید. <br />";
            }

            return new ValidateResultViewModel()
            {
                IsValid = IsValid,
                Errors = Errors
            };
        }








        /// <summary>
        /// تبدیل یک سطر از جدول محصولات به یک آبجکت محصول
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public Product ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            var Product = new Product()
            {
                Id = DataRow.Field<int>("Id"),
                Title = DataRow.Field<string>("Title"),
                CategoryId = DataRow.Field<int?>("CategoryId"),
                Description = DataRow.Field<string>("Description"),
                AdminId = DataRow.Field<int?>("AdminId"),
                Summary = DataRow.Field<string>("Summary"),
                OffPrice = DataRow.Field<int?>("OffPrice"),
                MainPrice = DataRow.Field<int>("MainPrice"),
                CreateDate = DataRow.Field<DateTime>("CreateDate"),
                IsEnabled = DataRow.Field<bool>("IsEnabled"),
                Inventory = DataRow.Field<int>("Inventory")
            };
            if (DataRow.Table.Columns.Contains("MainPic"))
                Product.MainPic = DataRow.Field<string>("MainPic");
            if (DataRow.Table.Columns.Contains("CategoryTitle"))
                Product.CategoryTitle = DataRow.Field<string>("CategoryTitle");
            return Product;
        }





        /// <summary>
        /// تبدیل چند سطر از جدول محصولات به یک لیست از آبجکت محصول
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<Product> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new Product
            {
                Id = dr.Field<int>("Id"),
                Title = dr.Field<string>("Title"),
                Summary = dr.Field<string>("Summary"),
                Description = dr.Field<string>("Description"),
                CategoryId = dr.Field<int?>("CategoryId"),
                AdminId = dr.Field<int?>("AdminId"),
                OffPrice = dr.Field<int?>("OffPrice"),
                MainPrice = dr.Field<int>("MainPrice"),
                CreateDate = dr.Field<DateTime>("CreateDate"),
                IsEnabled = dr.Field<bool>("IsEnabled"),
                Inventory = dr.Field<int>("Inventory"),
                MainPic = dr.Table.Columns.Contains("MainPic") ? dr.Field<string>("MainPic") : "",
                CategoryTitle = dr.Table.Columns.Contains("CategoryTitle") ? dr.Field<string>("CategoryTitle") : ""
            }).ToList();
        }


    }
}