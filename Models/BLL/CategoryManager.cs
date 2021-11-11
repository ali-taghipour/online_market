using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using TAD_ExtentionMethods;
using TAD_ImageResizer;
using TAD_Security;



namespace BLL
{
    /// <summary>
    /// Summary description for CategoryManager
    /// </summary>
    public class CategoryManager
    {
        private CategoryRepository Repo;
        public CategoryManager()
        {
            Repo = new CategoryRepository();
        }


        /// <summary>
        /// گرفتن دسته بندی با آیدی
        /// </summary>
        /// <param name="Id">آیدی دسته بندی</param>
        /// <returns></returns>
        public Category GetById(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetById((int)Id);
            return ToDataModel(DataRow);
        }





        /// <summary>
        /// گرفتن لیست همه دسته بندی ها
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAll()
        {
            DataTable DataTable = Repo.GetAll();
            return ToDataModel(DataTable);
        }


        

        /// <summary>
        /// گرفتن سر دسته ها
        /// </summary>
        /// <returns></returns>
        public List<Category> GetParentCategories(bool JustIsEnabled)
        {
            DataTable DataTable = Repo.GetParentCategories(JustIsEnabled);
            return ToDataModel(DataTable);
        }



        /// <summary>
        /// گرفتن همه سر دسته ها به همراه زیر دسته
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategoriesWithChilds(bool JustIsEnabled)
        {
            List<Category> Categories = GetParentCategories(JustIsEnabled);
            if (Categories == null) return null;
            foreach (var item in Categories)
                item.Childs = GetChildCategories(item.Id, JustIsEnabled);
            return Categories;
        }
        




        /// <summary>
        /// گرفتن زیر دسته های یک دسته بندی
        /// </summary>
        /// <returns></returns>
        public List<Category> GetChildCategories(int? ParentId, bool JustIsEnabled)
        {
            if (ParentId == null)
                return null;
            DataTable DataTable = Repo.GetChildCategories((int)ParentId, JustIsEnabled);
            var Categories = ToDataModel(DataTable);
            if (Categories == null && Categories.Count() == 0)
                return null;
            foreach (var item in Categories)
                item.Childs = GetChildCategories(item.Id, JustIsEnabled);
            return Categories;
        }






        /// <summary>
        /// ایجاد دسته بندی جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Category Category)
        {
            return Repo.Create(Category);
        }




        /// <summary>
        /// آپدیت دسته بندی
        /// </summary>
        /// <returns></returns>
        public bool Update(Category Category)
        {
            return Repo.Update(Category);
        }




        /// <summary>
        /// حذف دسته بندی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            if (HasChild(Id))
                return false;
            return Repo.Delete(Id);
        }



        /// <summary>
        /// آیا دسته بندی مورد نظر، زیر دسته دارد؟
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool HasChild(int? Id)
        {
            if (Id == null)
                return false;
            return Repo.HasChild((int)Id);
        }




        /// <summary>
        /// تبدیل یک سطر از جدول دسته بندی ها به یک آبجکت دسته بندی
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public Category ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            var Category = new Category()
            {
                Id = DataRow.Field<int>("Id"),
                Title = DataRow.Field<string>("Title"),
                IsEnabled = DataRow.Field<bool>("IsEnabled"),
                ParentId = DataRow.Field<int?>("ParentId")
            };
            if (DataRow.Table.Columns.Contains("ParentTitle"))
                Category.ParentTitle = DataRow.Field<string>("ParentTitle");
            return Category;

        }





        /// <summary>
        /// تبدیل چند سطر از جدول دسته بندی ها به یک لیست از آبجکت دسته بندی
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<Category> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new Category
            {
                Id = dr.Field<int>("Id"),
                Title = dr.Field<string>("Title"),
                IsEnabled = dr.Field<bool>("IsEnabled"),
                ParentId = dr.Field<int?>("ParentId")
            }).ToList();
        }


    }
}