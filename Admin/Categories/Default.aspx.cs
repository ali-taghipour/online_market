using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KargahProject.Admin.Categories
{
    public partial class Default : System.Web.UI.Page
    {
        CategoryManager CategoryManager;

        public Default()
        {
            CategoryManager = new CategoryManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }




        /// <summary>
        /// گرفتن لیست سردسته ها به همراه زیر دسته هایشان
        /// </summary>
        /// <param name="JustIsEnabled">
        /// true => فقط دسته بندی های فعال
        /// false => هم دسته بندی ها
        /// </param>
        /// <returns></returns>
        [WebMethod]
        public static List<Category> GetAllCategories(bool JustIsEnabled)
        {
            CategoryManager CategoryManager = new CategoryManager();
            var Categories = CategoryManager.GetCategoriesWithChilds(JustIsEnabled);
            return Categories;
        }



        /// <summary>
        /// ایجاد دسته بندی
        /// </summary>
        [WebMethod]
        public static bool AddCategory(string Title , int? ParentId , bool IsEnabled)
        {
            Category Category = new Category()
            {
                Title = Title,
                ParentId = ParentId == 0 ? null : ParentId,
                IsEnabled = IsEnabled
            };
            CategoryManager CategoryManager = new CategoryManager();
            return CategoryManager.Create(Category);
        }




        /// <summary>
        /// ویرایش دسته بندی
        /// </summary>
        [WebMethod]
        public static bool UpdateCategory(int? Id, string Title, bool IsEnabled)
        {
            CategoryManager CategoryManager = new CategoryManager();
            Category Category = CategoryManager.GetById(Id);
            if (Category == null)
                return false;
            Category.Title = Title;
            Category.IsEnabled = IsEnabled;
            return CategoryManager.Update(Category);
        }




        /// <summary>
        /// حذف دسته بندی
        /// </summary>
        [WebMethod]
        public static bool DeleteCategory(int? Id)
        {
            if (Id == null)
                return false;
            CategoryManager CategoryManager = new CategoryManager();
            return CategoryManager.Delete((int)Id);
        }
    }
}