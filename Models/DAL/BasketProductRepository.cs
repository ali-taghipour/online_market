using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KargahProject.Models.DAL
{
    public class BasketProductRepository : RepositoryBase
    {
        public BasketProductRepository()
        {

        }




        /// <summary>
        /// افزودن محصول به سبد
        /// </summary>
        /// <param name="BasketId">آیدی سبد</param>
        /// <returns></returns>
        public int? AddToBasket(int BasketId, int ProductId)
        {
            string cmdText = "insert into BasketProducts (BasketId , ProductId , [Count] , CreateDate)"
                            + " values(@BasketId , @ProductId , @Count , @CreateDate); "
                            + " select count(*) from basketProducts where BasketId = @BasketId;";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@BasketId", BasketId);
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            cmd.Parameters.AddWithValue("@Count", 1);
            cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return null;
        }






        /// <summary>
        /// ویرایش یک آیتم
        /// </summary>
        /// <returns></returns>
        public bool Update(BasketProduct BasketProduct)
        {
            string cmdText = "update basketproducts set Price = @Price , [Count] = @Count where id = @Id";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Id", BasketProduct.Id);
            cmd.Parameters.AddWithValue("@Count", BasketProduct.Count);
            cmd.Parameters.AddWithValue("@Price", BasketProduct.Price);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }





        /// <summary>
        /// حذف محصول از سبد
        /// </summary>
        public bool DeleteFromBasket(int Id)
        {
            string cmdText = "delete from basketproducts where id = @Id";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.CommandType = CommandType.Text;
            return SetData(cmd);
        }





        /// <summary>
        /// گرفتن محصولات سبد با آیدی
        /// </summary>
        /// <param name="BasketId">آیدی سبد</param>
        /// <returns></returns>
        public DataTable GetBasketProducts(int BasketId)
        {
            string cmdText = "select basketproducts.*, Products.Title, Products.Inventory, Products.MainPrice, Products.OffPrice, Pictures.ThumbPath as MainPic "
                            + " from BasketProducts left join Products on basketproducts.ProductId = Products.Id "
                            + " left join Pictures on IsMain = 1 and Pictures.ProductId = Products.Id "
                            + " where BasketId = @BasketId ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@BasketId", BasketId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            return null;
        }





        /// <summary>
        /// گرفتن تعداد محصولات سبد با آیدی
        /// </summary>
        /// <param name="BasketId">آیدی سبد</param>
        /// <returns></returns>
        public int? GetBasketItemCount(int BasketId)
        {
            string cmdText = "select count(*) from BasketProducts where BasketId = @BasketId";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@BasketId", BasketId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return null;
        }





        /// <summary>
        /// آیا محصول در سبد خرید وجود دارد؟
        /// </summary>
        public bool ProductIsExist(int BasketId , int ProductId)
        {
            string cmdText = "select count(*) from BasketProducts where BasketId = @BasketId and ProductId = @ProductId";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@BasketId", BasketId);
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds != null && ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString()) != 0;
            return true;
        }




    }
}