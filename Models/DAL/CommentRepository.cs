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
    /// Summary description for CommentRepository
    /// </summary>
    public class CommentRepository : RepositoryBase
    {
        public CommentRepository()
        {
        }


        /// <summary>
        /// گرفتن کامنت با آیدی
        /// </summary>
        /// <param name="Id">آیدی کامنت</param>
        /// <returns></returns>
        public DataRow GetById(int Id)
        {
            SqlCommand cmd = new SqlCommand("select * from Comments where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }







        /// <summary>
        /// گرفتن کامنت به همراه کاربر درج کننده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataRow GetByIdWithJoins(int Id)
        {
            string cmdText = "select Comments.* , Users.FirstName + ' ' + Users.LastName as UserFullName, Users.Type as UserType , Products.Title as ProductTitle"
                            + " from Comments left join Users on UserId = Users.Id"
                            + " left join Products on ProductId = Products.Id"
                            + "  where Comments.Id = @id ";
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
        /// گرفتن لیست همه کامنتها
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {
            string cmdText = "select Comments.* , Users.FirstName + ' ' + Users.LastName as UserFullName, Users.Type as UserType , Products.Title as ProductTitle"
                            + " from Comments left join Users on UserId = Users.Id"
                            + " left join Products on ProductId = Products.Id"
                            + "  order by Id desc ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }



        /// <summary>
        /// گرفتن تعداد کامنتهای بررسی نشده
        /// </summary>
        /// <returns></returns>
        public int? GetUnreadCommentsCount()
        {
            string cmdText = "select Count(*) "
                            + " from Comments where IsReaded = 0 ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return null;
        }





        /// <summary>
        /// گرفتن لیست همه کامنتهای خوانده نشده
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnreadComments()
        {
            string cmdText = "select Comments.* , Users.FirstName + ' ' + Users.LastName as UserFullName, Users.Type as UserType , Products.Title as ProductTitle"
                            + " from Comments left join Users on UserId = Users.Id"
                            + " left join Products on ProductId = Products.Id"
                            + "  where IsReaded = 0 order by Id desc ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }






        /// <summary>
        /// گرفتن همه کامنتهای یک محصول
        /// </summary>
        /// <returns></returns>
        public DataTable GetByProductId(int ProductId)
        {
            string cmdText = "select Comments.* , Users.FirstName + ' ' + Users.LastName as UserFullName , Users.Type as UserType"
                            + " from Comments left join Users on UserId = Users.Id"
                            + " where ProductId = @id and IsApproved = 1 order by Id ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@id",ProductId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }





        /// <summary>
        /// ایجاد کامنت جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Comment Comment)
        {
            string cmdText = "insert into Comments (Text,FullName,Email,IsReaded,IsApproved,CreateDate,UserId,ProductId) "
                                      + "values (@Text,@FullName,@Email,@IsReaded,@IsApproved,@CreateDate,@UserId,@ProductId)";
            var Params = new Dictionary<string, object>();
            Params.Add("@Text", Comment.Text);
            Params.Add("@FullName", Comment.FullName);
            Params.Add("@Email", Comment.Email);
            Params.Add("@IsReaded", Comment.IsReaded);
            Params.Add("@IsApproved", Comment.IsApproved);
            Params.Add("@CreateDate", Comment.CreateDate);
            Params.Add("@UserId", Comment.UserId);
            Params.Add("@ProductId", Comment.ProductId);
            return SetData(cmdText, CommandType.Text, Params);
        }





        /// <summary>
        /// آپدیت کامنت
        /// </summary>
        /// <returns></returns>
        public bool Update(Comment Comment)
        {
            string cmdText = "update Comments set Text=@Text, FullName=@FullName, Email=@Email, IsReaded=@IsReaded, "
                                            + "IsApproved=@IsApproved, CreateDate=@CreateDate, UserId=@UserId, ProductId=@ProductId "
                                            + "where Id = @Id";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Comment.Id);
            Params.Add("@Text", Comment.Text);
            Params.Add("@FullName", Comment.FullName);
            Params.Add("@Email", Comment.Email);
            Params.Add("@IsReaded", Comment.IsReaded);
            Params.Add("@IsApproved", Comment.IsApproved);
            Params.Add("@CreateDate", Comment.CreateDate);
            Params.Add("@UserId", Comment.UserId);
            Params.Add("@ProductId", Comment.ProductId);
            return SetData(cmdText, CommandType.Text, Params);
        }






        /// <summary>
        /// حذف کامنت
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            SqlCommand cmd = new SqlCommand("delete from Comments where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }




        /// <summary>
        /// تغییر وضعیت تایید کامنت
        /// </summary>
        /// <param name="Id">آیدی کامنت</param>
        /// <returns></returns>
        public bool ToggleCommentApproved(int Id)
        {
            SqlCommand cmd = new SqlCommand("update Comments set IsApproved = isapproved ^ 1 , IsReaded = 1 where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }




        /// <summary>
        /// تغییر وضعیت تایید کامنت
        /// </summary>
        /// <param name="Id">آیدی کامنت</param>
        /// <returns></returns>
        public bool ToggleCommentReaded(int Id)
        {
            SqlCommand cmd = new SqlCommand("update Comments set IsReaded = IsReaded ^ 1 where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }


    }
}