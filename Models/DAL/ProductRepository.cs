using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAD_Security;


namespace DAL
{
    /// <summary>
    /// Summary description for ProductRepository
    /// </summary>
    public class ProductRepository : RepositoryBase
    {
        public ProductRepository()
        {
        }


        /// <summary>
        /// گرفتن محصول با آیدی
        /// </summary>
        /// <param name="Id">آیدی محصول</param>
        /// <returns></returns>
        public DataRow GetById(int Id)
        {
            string cmdText = "select Products.* , Pictures.ThumbPath as MainPic , Categories.Title as CategoryTitle"
                            + " from Products left join Pictures on IsMain = 1 and ProductId = Products.Id "
                            + " left join Categories on CategoryId = Categories.Id "
                            + " where Products.Id = @id order by Products.Id desc";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }





        /// <summary>
        /// گرفتن همه اطلاعات محصول شامل کامنتها، تصاویر، تعداد لایک و... با آیدی
        /// </summary>
        /// <param name="Id">آیدی محصول</param>
        /// <returns></returns>
        public DataSet GetFullDetailsById(int Id)
        {
            //اطلاعات محصول
            string cmdText = "select Products.* , Pictures.ThumbPath as MainPic , Categories.Title as CategoryTitle"
                            + " from Products left join Pictures on IsMain = 1 and ProductId = Products.Id "
                            + " left join Categories on CategoryId = Categories.Id "
                            + " where Products.Id = @id order by Products.Id desc; ";

            //کامنت ها
            cmdText += " select Comments.* , Users.FirstName + ' ' + Users.LastName as UserFullName , Users.Type as UserType"
                            + " from Comments left join Users on UserId = Users.Id"
                            + " where productId = @id and IsApproved = 1 order by Id; ";

            // تصاویر
            cmdText += " select * from Pictures where ProductId = @id; ";

            //تعداد لایک ها
            cmdText += " select count(*) from Likes where productId = @id; ";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds;
            return null;
        }





        /// <summary>
        /// گرفتن لیست همه محصولات یک دسته بندی
        /// </summary>
        /// <returns></returns>
        public DataTable GetByCategoryId(int CategoryId)
        {
            string cmdText = "select Products.* , Pictures.ThumbPath as MainPic , Categories.Title as CategoryTitle"
                            + " from Products left join Pictures on IsMain = 1 and ProductId = Products.Id "
                            + " left join Categories on CategoryId = Categories.Id "
                            + " where CategoryId = @CategoryId order by Products.Id desc";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }





        /// <summary>
        /// گرفتن لیست همه محصولات
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {
            string cmdText = "select Products.* , Pictures.ThumbPath as MainPic , Categories.Title as CategoryTitle"
                            + " from Products left join Pictures on IsMain = 1 and ProductId = Products.Id "
                            + " left join Categories on CategoryId = Categories.Id "
                            + " order by Products.Id desc";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }




        /// <summary>
        /// گرفتن تعداد همه محصولات
        /// </summary>
        /// <returns></returns>
        public int? GetAllCount()
        {
            string cmdText = "select Count(*) "
                            + " from Products ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return null;
        }





        /// <summary>
        /// گرفتن همه محصولات فعال
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActive()
        {
            string cmdText = "select Products.* , Pictures.ThumbPath as MainPic , Categories.Title as CategoryTitle"
                            + " from Products left join Pictures on IsMain = 1 and ProductId = Products.Id "
                            + " left join Categories on CategoryId = Categories.Id "
                            + " where IsEnabled = 1 order by Products.Id desc";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }






        /// <summary>
        /// گرفتن جدیدترین محصولاتی که قابل نمایش هستند
        /// </summary>
        /// <returns></returns>
        public DataTable GetNewPosts(int Count)
        {
            string cmdText = "select top(@Count) Products.* , Pictures.ThumbPath as MainPic , Categories.Title as CategoryTitle"
                            + " from Products left join Pictures on IsMain = 1 and ProductId = Products.Id "
                            + " left join Categories on CategoryId = Categories.Id "
                            + " where Products.IsEnabled = 1 order by Products.Id desc";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Count",Count);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }







        /// <summary>
        /// گرفتن لیست محصولات سرچ شده
        /// </summary>
        /// <param name="Page">شماره صفحه. پیشفرض 1 است.</param>
        /// <param name="PageSize">تعداد آیتم های صفحه. پیشفرض 10 است.</param>
        /// <param name="SearchText">متن جستجو شده</param>
        /// <returns></returns>
        public DataSet GetSearchedItem(int? CategoryId, int? Page, int? PageSize, string SearchText)
        {
            SqlCommand cmd = new SqlCommand();

            //اگر متنی برای جستجو وجود داشته باشد
            string SearchQuery = "";
            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchQuery = " and (Products.Title Like N'%' + @SearchText + '%' or summary Like N'%' + @SearchText + '%' or [description] Like N'%' + @SearchText + '%')  ";
                cmd.Parameters.AddWithValue("@SearchText", SearchText);
            }
            
            //اگر دسته بندی انتخاب شده باشد
            if(CategoryId != null)
            {
                SearchQuery = " and CategoryId = @CategoryId ";
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            }
            

            //اگر تعداد آیتم های صفحه مشخص شده باشد آنگاه صفحه بندی
            //معنی دارد. در غیر اینصورت همه آیتم ها بر میگردد.
            string PaginationQuery = "";
            if (PageSize != null)
            {
                int page = Page ?? 1;
                PaginationQuery = " OFFSET @Start ROWS FETCH NEXT @End ROWS ONLY ";
                cmd.Parameters.AddWithValue("@Start", (page - 1) * PageSize);
                cmd.Parameters.AddWithValue("@End", PageSize);
            }

            string CmdText = "select Products.* , Pictures.ThumbPath as MainPic , Categories.Title as CategoryTitle"
                            + " from Products left join Pictures on IsMain = 1 and ProductId = Products.Id "
                            + " left join Categories on CategoryId = Categories.Id "
                            + " where Products.IsEnabled = 1 " + SearchQuery + " order by Products.Id desc " + PaginationQuery + "; "
                            + " select Count(*) as TotalCount from Products where Products.IsEnabled = 1 " + SearchQuery;

            cmd.CommandText = CmdText;
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            return null;
        }







        /// <summary>
        /// ایجاد محصول جدید
        /// </summary>
        /// <returns></returns>
        public DataRow Create(Product Product)
        {
            string cmdText = "insert into Products (Title,Summary,Description,MainPrice,OffPrice,Inventory,IsEnabled,CategoryId,AdminId,CreateDate) OUTPUT Inserted.* "
                                      + "values (@Title,@Summary,@Description,@MainPrice,@OffPrice,@Inventory,@IsEnabled,@CategoryId,@AdminId,@CreateDate)";
            var Params = new Dictionary<string, object>();
            Params.Add("@Title", Product.Title);
            Params.Add("@Summary", Product.Summary);
            Params.Add("@Description", Product.Description);
            Params.Add("@MainPrice", Product.MainPrice);
            Params.Add("@OffPrice", Product.OffPrice);
            Params.Add("@Inventory", Product.Inventory);
            Params.Add("@IsEnabled", Product.IsEnabled);
            Params.Add("@CategoryId", Product.CategoryId);
            Params.Add("@AdminId", Product.AdminId);
            Params.Add("@CreateDate", Product.CreateDate);

            var ds = GetData(cmdText, CommandType.Text, Params);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }


 


        /// <summary>
        /// آپدیت محصول
        /// </summary>
        /// <returns></returns>
        public bool Update(Product Product)
        {
            string cmdText = "update Products set Title=@Title, Summary=@Summary, Description=@Description, MainPrice=@MainPrice, "
                                            + " Inventory=@Inventory, OffPrice=@OffPrice, CategoryId=@CategoryId, IsEnabled=@IsEnabled "
                                            + "where Id = @Id";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Product.Id);
            Params.Add("@Title", Product.Title);
            Params.Add("@Summary", Product.Summary);
            Params.Add("@Description", Product.Description);
            Params.Add("@MainPrice", Product.MainPrice);
            Params.Add("@OffPrice", Product.OffPrice);
            Params.Add("@Inventory", Product.Inventory);
            Params.Add("@IsEnabled", Product.IsEnabled);
            Params.Add("@CategoryId", Product.CategoryId);
            return SetData(cmdText, CommandType.Text, Params);
        }




        /// <summary>
        /// آپدیت تعداد موجودی محصول
        /// </summary>
        /// <returns></returns>
        public bool UpdateInventory(int ProductId, int NewInventory)
        {
            string cmdText = "update Products set Inventory = @NewInventory where Id = @Id";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", ProductId);
            Params.Add("@NewInventory", NewInventory);
            return SetData(cmdText, CommandType.Text, Params);
        }




        /// <summary>
        /// حذف محصول
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            SqlCommand cmd = new SqlCommand("DeleteProduct");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.StoredProcedure;
            return SetData(cmd);
        }



    }
}