using Entities;
using KargahProject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KargahProject.Models.BLL
{
    public class BasketProductManager
    {
        BasketProductRepository Repo;
        public BasketProductManager()
        {
            Repo = new BasketProductRepository();
        }



        /// <summary>
        /// افزودن محصول به سبد
        /// </summary>
        /// <param name="BasketId">آیدی سبد</param>
        /// <returns></returns>
        public int? AddToBasket(int BasketId, int ProductId)
        {
            return Repo.AddToBasket(BasketId, ProductId);
        }




        /// <summary>
        /// ویرایش یک آیتم
        /// </summary>
        /// <returns></returns>
        public bool Update(BasketProduct BasketProduct)
        {
            return Repo.Update(BasketProduct);
        }





        /// <summary>
        /// حذف محصول از سبد
        /// </summary>
        public bool DeleteFromBasket(int Id)
        {
            return Repo.DeleteFromBasket(Id);
        }





        /// <summary>
        /// گرفتن محصولات سبد با آیدی
        /// </summary>
        /// <param name="BasketId">آیدی سبد</param>
        /// <returns></returns>
        public List<BasketProduct> GetBasketProducts(int? BasketId)
        {
            if (BasketId == null)
                return null;
            DataTable DataTable = Repo.GetBasketProducts((int)BasketId);
            return ToDataModel(DataTable);
        }





        /// <summary>
        /// گرفتن تعداد محصولات سبد با آیدی
        /// </summary>
        /// <param name="BasketId">آیدی سبد</param>
        /// <returns></returns>
        public int GetBasketItemCount(int? BasketId)
        {
            if (BasketId == null)
                return 0;
            int? Count = Repo.GetBasketItemCount((int)BasketId);
            return Count ?? 0;
        }





        /// <summary>
        /// آیا محصول در سبد خرید وجود دارد؟
        /// </summary>
        public bool ProductIsExist(int BasketId, int ProductId)
        {
            return Repo.ProductIsExist(BasketId, ProductId);
        }




        /// <summary>
        /// تبدیل یک سطر از جدول سبدها به یک آبجکت سبد
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public BasketProduct ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            var BasketProduct = new BasketProduct()
            {
                Id = DataRow.Field<int>("Id"),
                Count = DataRow.Field<int>("Count"),
                Price = DataRow.Field<int?>("Price"),
                BasketId = DataRow.Field<int>("BasketId"),
                ProductId = DataRow.Field<int?>("ProductId"),
                CreateDate = DataRow.Field<DateTime>("CreateDate")
            };

            //اگر نیاز به گرفتن مشخصات محصول بود
            if (BasketProduct.ProductId != null && DataRow.Table.Columns.Contains("Title"))
                BasketProduct.Product = new Product()
                {
                    Title = DataRow.Field<string>("Title"),
                    Inventory = DataRow.Field<int?>("Inventory"),
                    MainPrice = DataRow.Field<int>("MainPrice"),
                    OffPrice = DataRow.Field<int?>("OffPrice"),
                    MainPic = DataRow.Field<string>("MainPic")
                };

            return BasketProduct;
        }





        /// <summary>
        /// تبدیل چند سطر از جدول سبدها به یک لیست از آبجکت سبد
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<BasketProduct> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new BasketProduct
            {
                Id = dr.Field<int>("Id"),
                Count = dr.Field<int>("Count"),
                Price = dr.Field<int?>("Price"),
                BasketId = dr.Field<int>("BasketId"),
                ProductId = dr.Field<int?>("ProductId"),
                CreateDate = dr.Field<DateTime>("CreateDate"),
                Product = (dr.Field<int?>("ProductId")!= null && !dr.Table.Columns.Contains("Title")) ?  null : new Product
                {
                    Title = dr.Field<string>("Title"),
                    Inventory = dr.Field<int?>("Inventory"),
                    MainPrice = dr.Field<int>("MainPrice"),
                    OffPrice = dr.Field<int?>("OffPrice"),
                    MainPic = dr.Field<string>("MainPic")
                }
            }).ToList();
        }




    }
}