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
    /// Summary description for PictureRepository
    /// </summary>
    public class PictureRepository : RepositoryBase
    {
        public PictureRepository()
        {
        }


        /// <summary>
        /// گرفتن تصویر با آیدی
        /// </summary>
        /// <param name="Id">آیدی تصویر</param>
        /// <returns></returns>
        public DataRow GetById(int Id)
        {
            SqlCommand cmd = new SqlCommand("select * from Pictures where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }




        /// <summary>
        /// گرفتن لیست همه تصاویر
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {
            SqlCommand cmd = new SqlCommand("select * from Pictures order by Id desc");
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }





        /// <summary>
        /// گرفتن لیست همه تصاویر یک محصول خاص
        /// </summary>
        /// <returns></returns>
        public DataTable GetByProductId(int ProductId)
        {
            SqlCommand cmd = new SqlCommand("select * from Pictures where ProductId = @ProductId");
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }




        /// <summary>
        /// ایجاد تصویر جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Picture Picture)
        {
            string cmdText = "insert into Pictures (LargePath,ThumbPath,IsMain,ProductId,CreateDate) "
                                      + "values (@LargePath,@ThumbPath,@IsMain,@ProductId,@CreateDate)";
            var Params = new Dictionary<string, object>();
            Params.Add("@LargePath", Picture.LargePath);
            Params.Add("@ThumbPath", Picture.ThumbPath);
            Params.Add("@IsMain", Picture.IsMain);
            Params.Add("@ProductId", Picture.ProductId);
            Params.Add("@CreateDate", Picture.CreateDate);

            return SetData(cmdText, CommandType.Text, Params);
        }





        /// <summary>
        /// آپدیت تصویر
        /// </summary>
        /// <returns></returns>
        public bool Update(Picture Picture)
        {
            string cmdText = "update Pictures set LargePath=@LargePath, ThumbPath=@ThumbPath, IsMain=@IsMain "
                                            + "where Id = @Id";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Picture.Id);
            Params.Add("@LargePath", Picture.LargePath);
            Params.Add("@ThumbPath", Picture.ThumbPath);
            Params.Add("@IsMain", Picture.IsMain);
            return SetData(cmdText, CommandType.Text, Params);
        }






        /// <summary>
        /// حذف تصویر
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataRow Delete(int Id)
        {
            SqlCommand cmd = new SqlCommand("delete Pictures OUTPUT DELETED.* from Pictures where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }



        /// <summary>
        /// تعیین تصویر اصلی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool SetMainPic(int Id, int ProductId)
        {
            SqlCommand cmd = new SqlCommand("update Pictures set IsMain = 0 where ProductId = @ProductId; update Pictures set IsMain = 1 where Id = @id");
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }


    }

}