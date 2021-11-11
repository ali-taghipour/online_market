using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KargahProject.Models.DAL
{
    public class PaymentRepository : RepositoryBase
    {

        
        /// <summary>
        /// ایجاد پرداخت جدید
        /// </summary>
        /// <param name="Amount">مبلغ</param>
        /// <param name="UserId">کاربر پرداخت کننده</param>
        /// <param name="Description">توضیحات</param>
        /// <returns>آیدی پرداخت ایجاد شده</returns>
        public int? Create(Payment Payment)
        {
            string cmdText = "insert into Payments (IsSuccess,Amount,UserId,CreateDate,Description,BasketId) "
                                      + " values (@IsSuccess,@Amount, @UserId , @CreateDate,@Description,@BasketId);"
                                      + " select IDENT_CURRENT('Payments') ";
            var Params = new Dictionary<string, object>();
            Params.Add("@IsSuccess", Payment.IsSuccess);
            Params.Add("@UserId", Payment.UserId);
            Params.Add("@CreateDate", Payment.CreateDate);
            Params.Add("@Description", Payment.Description);
            Params.Add("@BasketId", Payment.BasketId);
            Params.Add("@Amount", Payment.Amount);
            DataSet ds = GetData(cmdText, CommandType.Text, Params);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return null;
        }






        /// <summary>
        /// گرفتن اطلاعات پرداخت
        /// </summary>
        public DataRow GetById(int Id)
        {
            SqlCommand cmd = new SqlCommand("select * from Payments where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }





        /// <summary>
        /// گرفتن اطلاعات پرداخت
        /// </summary>
        public DataRow GetByIdWithJoin(int Id)
        {
            string cmdText = "select payments.* , Users.FirstName + ' ' + Users.LastName as UserFullName, Users.Type as UserType "
                            + " from payments left join Users on UserId = Users.Id"
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
            string cmdText = "select payments.* , Users.FirstName + ' ' + Users.LastName as UserFullName, Users.Type as UserType"
                            + " from payments left join Users on UserId = Users.Id"
                            + "  order by Id desc ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        




        /// <summary>
        /// اعلام موفقیت آمیز بودن پرداخت
        /// </summary>
        /// <returns></returns>
        public bool Update(Payment Payment)
        {
            string cmdText = "update Payments set IsSuccess=@IsSuccess, Description=@Description, PaymentKey=@PaymentKey, "
                            + " CreateDate=@CreateDate, Amount=@Amount, StatusCode=@StatusCode where Id = @Id";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Payment.Id);
            Params.Add("@IsSuccess", Payment.IsSuccess);
            Params.Add("@Description", Payment.Description);
            Params.Add("@PaymentKey", Payment.PaymentKey);
            Params.Add("@CreateDate", Payment.CreateDate);
            Params.Add("@Amount", Payment.Amount);
            Params.Add("@StatusCode", Payment.StatusCode);
            return SetData(cmdText, CommandType.Text, Params);
        }





        /// <summary>
        /// حذف پرداخت
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            string cmdText = "delete from Payments where Id = @id";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }







    }
}