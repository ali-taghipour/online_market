using Entities;
using Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace KargahProject.Models.DAL
{
    public class BasketRepository : RepositoryBase
    {
        public BasketRepository()
        {
        }



        /// <summary>
        /// گرفتن سبد با آیدی
        /// </summary>
        /// <param name="Id">آیدی سبد</param>
        /// <returns></returns>
        public DataRow GetById(int Id)
        {
            SqlCommand cmd = new SqlCommand("select * from Baskets where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }





        /// <summary>
        /// گرفتن سبد های خرید به همراه کاربر درج کننده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataRow GetByIdWithJoins(int Id)
        {
            string cmdText = "select basket.* , Users.FirstName + ' ' + Users.LastName as UserFullName, Users.Type as UserType "
                            + " from baskets left join Users on UserId = Users.Id"
                            + "  where baskets.Id = @id ";
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
        /// گرفتن لیست همه سبدها
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {
            string cmdText = "select baskets.* , Users.FirstName + ' ' + Users.LastName as UserFullName, Users.Type as UserType"
                            + " from baskets left join Users on UserId = Users.Id"
                            + "  order by Id desc ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }







        /// <summary>
        /// گرفتن لیست همه سبدها با وضعیت مشخص
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll(BasketStatus Status)
        {
            string cmdText = "select baskets.* , Users.FirstName + ' ' + Users.LastName as UserFullName, Users.Type as UserType"
                            + " from baskets left join Users on UserId = Users.Id"
                            + " where Status = @Status order by Id desc ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }







        /// <summary>
        /// گرفتن همه سبدهای یک کاربر
        /// </summary>
        /// <returns></returns>
        public DataTable GetByUserId(int UserId)
        {
            string cmdText = "select baskets.* "
                            + " from baskets "
                            + " where UserId = @UserId order by Id desc ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }






        /// <summary>
        /// گرفتن همه سبدهای باز یک کاربر
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserOpenBasket(int UserId)
        {
            string cmdText = "select baskets.* "
                            + " from baskets "
                            + " where Status = @Status and UserId = @UserId order by Id desc ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Status", BasketStatus.Open);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }








        /// <summary>
        /// ایجاد سبد جدید
        /// </summary>
        /// <returns></returns>
        public int? Create(Basket Basket)
        {
            string cmdText = "insert into Baskets (Status,UserId,CreateDate,LastUpdateDate) "
                                      + " values (@Status, @UserId , @CreateDate,@LastUpdateDate);"
                                      + " select IDENT_CURRENT('Baskets') ";
            var Params = new Dictionary<string, object>();
            Params.Add("@Status", Basket.Status);
            Params.Add("@UserId", Basket.UserId);
            Params.Add("@CreateDate", DateTime.Now);
            Params.Add("@LastUpdateDate", DateTime.Now);
            DataSet ds = GetData(cmdText, CommandType.Text, Params);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return null;
        }






        /// <summary>
        /// آپدیت سبد
        /// </summary>
        /// <returns></returns>
        public bool Update(Basket Basket)
        {
            string cmdText = "update baskets set Status=@Status, LastUpdateDate=@LastUpdateDate, TotalPrice=@TotalPrice, "
                                            + " UserId=@UserId where Id = @Id";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Basket.Id);
            Params.Add("@Status", Basket.Status);
            Params.Add("@LastUpdateDate", DateTime.Now);
            Params.Add("@TotalPrice", Basket.TotalPrice);
            Params.Add("@UserId", Basket.UserId);
            return SetData(cmdText, CommandType.Text, Params);
        }





        /// <summary>
        /// حذف  سبد
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            string cmdText = "update Payments set basketid = null where basketid = @id ;"
                                + "delete from basketproducts where basketid = @id ;"
                                + " delete from baskets where Id = @id";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }







    }
}