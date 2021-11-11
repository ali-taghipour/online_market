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
    /// Summary description for UserRepository
    /// </summary>
    public class UserRepository : RepositoryBase
    {
        public UserRepository()
        {
        }


        /// <summary>
        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="Id">آیدی کاربر</param>
        /// <returns></returns>
        public DataRow GetById(int Id)
        {
            SqlCommand cmd = new SqlCommand("select * from Users where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }




        /// <summary>
        /// گرفتن کاربر با یوزرنیم و پسورد
        /// </summary>
        /// <returns></returns>
        public DataRow GetByUsernameAndPassword(string Username, string Password)
        {
            SqlCommand cmd = new SqlCommand("select * from Users where Username = @Username and Password = @Password ");
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }





        /// <summary>
        /// گرفتن لیست همه کاربران
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {
            SqlCommand cmd = new SqlCommand("select * from Users order by Id desc");
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }





        /// <summary>
        /// گرفتن تعداد همه کاربران
        /// </summary>
        /// <returns></returns>
        public int? GetAllCount()
        {
            string cmdText = "select Count(*) "
                            + " from Users ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return null;
        }




        /// <summary>
        /// ایجاد کاربر جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(User User)
        {
            string cmdText = "insert into Users (Username,Password,FirstName,LastName,Email,Address,PostalCode,Pic,IsMale,Type,IsEnabled,CreateDate) "
                                      + "values (@Username,@Password,@FirstName,@LastName,@Email,@Address,@PostalCode,@Pic,@IsMale,@Type,@IsEnabled,@CreateDate)";
            var Params = new Dictionary<string, object>();
            Params.Add("@Username", User.Username);
            Params.Add("@Password", User.Password);
            Params.Add("@FirstName", User.FirstName);
            Params.Add("@LastName", User.LastName);
            Params.Add("@Email", User.Email);
            Params.Add("@Address", User.Address);
            Params.Add("@PostalCode", User.PostalCode);
            Params.Add("@Pic", User.Pic);
            Params.Add("@IsMale", User.IsMale);
            Params.Add("@Type", User.Type);
            Params.Add("@IsEnabled", User.IsEnabled);
            Params.Add("@CreateDate", User.CreateDate);

            return SetData(cmdText, CommandType.Text, Params);
        }





        /// <summary>
        /// آپدیت کاربر
        /// </summary>
        /// <returns></returns>
        public bool Update(User User)
        {
            string cmdText = "update Users set Username=@Username, Password=@Password, FirstName=@FirstName, LastName=@LastName, Address=@Address, "
                                            + " PostalCode=@PostalCode, Email=@Email, Pic=@Pic, IsMale=@IsMale, Type=@Type, IsEnabled=@IsEnabled "
                                            + "where Id = @Id";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", User.Id);
            Params.Add("@Username", User.Username);
            Params.Add("@Password", User.Password);
            Params.Add("@FirstName", User.FirstName);
            Params.Add("@LastName", User.LastName);
            Params.Add("@Email", User.Email);
            Params.Add("@Address", User.Address);
            Params.Add("@PostalCode", User.PostalCode);
            Params.Add("@Pic", User.Pic);
            Params.Add("@IsMale", User.IsMale);
            Params.Add("@Type", User.Type);
            Params.Add("@IsEnabled", User.IsEnabled);
            return SetData(cmdText, CommandType.Text, Params);
        }






        /// <summary>
        /// آیا نام کاربری انتخاب شده برای کاربر جدید معتبر است؟
        /// </summary>
        public bool UsernameIsValid(string Username)
        {
            SqlCommand cmd = new SqlCommand("select count(*) as TotalCount from Users where Username = @Username");
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                if (ds.Tables[0].Rows[0].Field<int>("TotalCount") == 0)
                    return true;
            return false;
        }






        /// <summary>
        /// آیا نام کاربری انتخاب شده برای کاربری که در حال ویرایش است، معتبر است؟
        /// </summary>
        public bool UsernameIsValid(string Username, int UserId)
        {
            SqlCommand cmd = new SqlCommand("select count(*) as TotalCount from Users where Username = @Username and Id != @UserId");
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                if (ds.Tables[0].Rows[0].Field<int>("TotalCount") == 0)
                    return true;
            return false;
        }





        /// <summary>
        /// حذف کاربر
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataRow Delete(int Id)
        {
            SqlCommand cmd = new SqlCommand("DeleteUser");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.StoredProcedure;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }




        /// <summary>
        /// حذف تصویر کاربر
        /// </summary>
        /// <param name="UserId">آیدی کاربر</param>
        /// <returns></returns>
        public string DeleteUserPic(int UserId)
        {
            string cmdText = "declare @Pic NVARCHAR (100); "
                            + " select @Pic = pic from Users where id =@id ;"
                            + " update Users set pic = NULL where id =@id; "
                            + " select @Pic; ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@id", UserId);
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
            return null;
        }


    }

}
