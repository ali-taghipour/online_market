using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    /// <summary>
    /// Summary description for LikeRepository
    /// </summary>
    public class LikeRepository : RepositoryBase
    {
        public LikeRepository()
        {
        }




        /// <summary>
        /// گرفتن لایک به همراه ادمین درج کننده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataRow GetByIdWithJoins(int Id)
        {
            string cmdText = "select Likes.* , Users.FirstName + ' ' + Users.LastName as UserFullName, Users.Type as UserType , Products.Title as ProductTitle"
                            + " from Likes left join Users on UserId = Users.Id"
                            + " left join Products on ProductId = Products.Id"
                            + "  where Likes.Id = @id ";
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
        /// آیا کاربر محصول را لایک کرده است؟
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public bool UserLikeProduct(int? UserId, int? ProductId)
        {
            string cmdText = "select count(*) from Likes where productId = @ProductId and UserId = @UserId ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0;
            return false;
        }





        /// <summary>
        /// گرفتن تعداد لایکهای یک محصول
        /// </summary>
        /// <returns></returns>
        public int? GetProductLikeCount(int ProductId)
        {
            string cmdText = "select count(*) from Likes where productId = @ProductId ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@ProductId" , ProductId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return null;
        }





        /// <summary>
        /// ایجاد لایک جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Like Like)
        {
            string cmdText = "insert into Likes (CreateDate,UserId,ProductId) "
                                      + "values (@CreateDate,@UserId,@ProductId)";
            var Params = new Dictionary<string, object>();
            Params.Add("@CreateDate", Like.CreateDate);
            Params.Add("@UserId", Like.UserId);
            Params.Add("@ProductId", Like.ProductId);
            return SetData(cmdText, CommandType.Text, Params);
        }




        /// <summary>
        /// ایجاد لایک جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(int UserId , int ProductId)
        {
            string cmdText = "insert into Likes (CreateDate,UserId,ProductId) "
                                      + "values (@CreateDate,@UserId,@ProductId)";
            var Params = new Dictionary<string, object>();
            Params.Add("@CreateDate", DateTime.Now);
            Params.Add("@UserId", UserId);
            Params.Add("@ProductId", ProductId);
            return SetData(cmdText, CommandType.Text, Params);
        }




        /// <summary>
        /// تغییر وضعیت لایک 
        /// اگر لایک نکرده بود، لایک شود
        /// اگر لایک کرده بود، دیسلایک شود
        /// </summary>
        /// <returns></returns>
        public DataRow ToggleLike(int UserId, int ProductId)
        {
            var Params = new Dictionary<string, object>();
            Params.Add("@CreateDate", DateTime.Now);
            Params.Add("@UserId", UserId);
            Params.Add("@ProductId", ProductId);
            var ds = GetData("ToggleLike", CommandType.StoredProcedure, Params);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0].Rows[0];
            return null;
            
        }




        /// <summary>
        /// حذف لایک
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            SqlCommand cmd = new SqlCommand("delete from Likes where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }




        /// <summary>
        /// حذف لایک
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int UserId , int ProductId)
        {
            SqlCommand cmd = new SqlCommand("delete from Likes where UserId = @UserId and ProductId = @ProductId");
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }


    }
}