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
    /// Summary description for CategoryRepository
    /// </summary>
    public class CategoryRepository : RepositoryBase
    {
        public CategoryRepository()
        {
        }


        /// <summary>
        /// گرفتن دسته بندی با آیدی
        /// </summary>
        /// <param name="Id">آیدی دسته بندی</param>
        /// <returns></returns>
        public DataRow GetById(int Id)
        {
            string cmdText = "select * from Categories where Id = @id";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }






        /// <summary>
        /// گرفتن لیست همه دسته بندیها
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {
            string cmdText = "select Categories.* from Categories ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }





        /// <summary>
        /// گرفتن سر دسته ها
        /// </summary>
        /// <returns></returns>
        public DataTable GetParentCategories(bool JustIsEnabled)
        {
            string cmdText = "select * from Categories where ParentId is NULL";
            if (JustIsEnabled)
                cmdText += " and IsEnabled = 1";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds!= null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }






        /// <summary>
        /// گرفتن زیر دسته های یک دسته بندی
        /// </summary>
        /// <returns></returns>
        public DataTable GetChildCategories(int? ParentId, bool JustIsEnabled)
        {
            string cmdText = "select * from Categories where ParentId = @ParentId";
            if (JustIsEnabled)
                cmdText += " and IsEnabled = 1";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@ParentId", ParentId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }





        /// <summary>
        /// ایجاد دسته بندی جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Category Category)
        {
            string cmdText = "insert into Categories (Title,ParentId,IsEnabled) "
                                      + "values (@Title,@ParentId,@IsEnabled)";
            var Params = new Dictionary<string, object>();
            Params.Add("@Title", Category.Title);
            Params.Add("@ParentId", Category.ParentId);
            Params.Add("@IsEnabled", Category.IsEnabled);
            return SetData(cmdText, CommandType.Text, Params);
        }





        /// <summary>
        /// آپدیت دسته بندی
        /// </summary>
        /// <returns></returns>
        public bool Update(Category Category)
        {
            string cmdText = "update Categories set Title=@Title, IsEnabled=@IsEnabled "
                                            + " where Id = @Id";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Category.Id);
            Params.Add("@Title", Category.Title);
            Params.Add("@IsEnabled", Category.IsEnabled);
            return SetData(cmdText, CommandType.Text, Params);
        }






        /// <summary>
        /// حذف دسته بندی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            string cmdText = "update Categories set ParentId=NULL where Id = @Id; "
                            + "delete from Categories where Id = @id";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }



        /// <summary>
        /// آیا زیر دسته دارد؟
        /// </summary>
        /// <param name="Id">آیدی دسته بندی</param>
        /// <returns></returns>
        public bool HasChild(int Id)
        {
            string cmdText = "select Count(*) from categories where ParentId = @id ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString() != "0";
            return false;
        }



    }

}
