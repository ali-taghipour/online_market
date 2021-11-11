using BLL;
using DAL;
using Entities;
using Enums;
using KargahProject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KargahProject.Models.BLL
{
    public class BasketManager
    {
        private BasketRepository Repo;
        public BasketManager()
        {
            Repo = new BasketRepository();
        }



        /// <summary>
        /// گرفتن آیدی سبد باز فعلی
        /// اگر سبد باز در کوکی بود، از کوکی میگیرد
        /// اگر کاربر لاگین کرده باشد سبد باز کاربر را میگیرد.
        /// در غیر این صورت نال برمیگرداند
        /// </summary>
        public int? GetOpenBasketId()
        {
            //اگر سبد خرید در کوکی بود برگرداند
            int BasketId;
            if (HttpContext.Current.Request.Cookies["BasketId"] != null && int.TryParse(HttpContext.Current.Request.Cookies["BasketId"].Value , out BasketId))
                return BasketId;


            //اگر کاربر لاگین کرده بود، سبد بازی که دارد را برگرداند
            if(HttpContext.Current.Request.Cookies["User"] != null)
            {
                int UserId = int.Parse(HttpContext.Current.Request.Cookies["User"]["Id"].ToString());
                var OpenBasket = GetUserOpenBasket(UserId).FirstOrDefault();
                if (OpenBasket != null)
                    return OpenBasket.Id;
            }

            return null;
        }




        /// <summary>
        /// گرفتن سبد با آیدی
        /// </summary>
        /// <param name="Id">آیدی سبد</param>
        /// <returns></returns>
        public Basket GetById(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetById((int)Id);
            return ToDataModel(DataRow);
        }






        /// <summary>
        /// گرفتن سبد های خرید به همراه کاربر درج کننده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Basket GetByIdWithJoins(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetById((int)Id);
            return ToDataModel(DataRow);
        }




        /// <summary>
        /// گرفتن لیست همه سبدها
        /// </summary>
        /// <returns></returns>
        public List<Basket> GetAll()
        {
            DataTable DataTable = Repo.GetAll();
            return ToDataModel(DataTable);
        }


        



        /// <summary>
        /// گرفتن لیست همه سبدها با وضعیت مشخص
        /// </summary>
        /// <returns></returns>
        public List<Basket> GetAll(BasketStatus Status)
        {
            DataTable DataTable = Repo.GetAll();
            return ToDataModel(DataTable);
        }







        /// <summary>
        /// گرفتن همه سبدهای یک کاربر
        /// </summary>
        /// <returns></returns>
        public List<Basket> GetByUserId(int? UserId)
        {
            if (UserId == null)
                return null;
            DataTable DataTable = Repo.GetByUserId((int)UserId);
            return ToDataModel(DataTable);
        }




        /// <summary>
        /// گرفتن همه سبدهای باز یک کاربر
        /// </summary>
        /// <returns></returns>
        public List<Basket> GetUserOpenBasket(int? UserId)
        {
            if (UserId == null)
                return null;
            DataTable DataTable = Repo.GetUserOpenBasket((int)UserId);
            return ToDataModel(DataTable);
        }





        /// <summary>
        /// ایجاد سبد جدید
        /// </summary>
        /// <returns></returns>
        public int? Create(Basket Basket)
        {
            return Repo.Create(Basket);
        }




        /// <summary>
        /// ایجاد سبد جدید برای کاربر خاص
        /// </summary>
        /// <returns></returns>
        public int? Create(int UserId)
        {
            Basket Basket = new Basket()
            {
                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                UserId = UserId,
                Status = BasketStatus.Open                
            };
            return Repo.Create(Basket);
        }





        /// <summary>
        /// ایجاد سبد جدید برای کاربر موجود در کوکی
        /// یا یک سبد خالی بدون کاربر
        /// </summary>
        /// <returns></returns>
        public int? Create()
        {
            int? UserId = null;
            if (HttpContext.Current.Request.Cookies["User"] != null)
                UserId = int.Parse(HttpContext.Current.Request.Cookies["User"]["Id"]);
            Basket Basket = new Basket()
            {
                CreateDate = DateTime.Now,
                Status = BasketStatus.Open,
                UserId = UserId                
            };
            return Repo.Create(Basket);
        }




        /// <summary>
        /// آپدیت کامنت
        /// </summary>
        /// <returns></returns>
        public bool Update(Basket Basket)
        {
            return Repo.Update(Basket);
        }





        /// <summary>
        /// حذف  سبد
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int? Id)
        {
            if (Id == null)
                return false;
            return Repo.Delete((int)Id);
        }







        /// <summary>
        /// تبدیل یک سطر از جدول سبدها به یک آبجکت سبد
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public Basket ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            var Basket = new Basket()
            {
                Id = DataRow.Field<int>("Id"),
                TotalPrice = DataRow.Field<int?>("TotalPrice"),
                UserId = DataRow.Field<int?>("UserId"),
                CreateDate = DataRow.Field<DateTime>("CreateDate"),
                LastUpdateDate = DataRow.Field<DateTime?>("LastUpdateDate"),
                Status = DataRow.Field<BasketStatus>("Status")
            };

            //اگر نیاز به گرفتن مشخصات کاربر بود
            if (Basket.UserId != null && DataRow.Table.Columns.Contains("UserFullName"))
                Basket.UserFullName = DataRow.Field<string>("UserFullName");


            if (Basket.UserId != null && DataRow.Table.Columns.Contains("UserType"))
                Basket.UserType = DataRow.Field<UserType?>("UserType");

            return Basket;
        }





        /// <summary>
        /// تبدیل چند سطر از جدول سبدها به یک لیست از آبجکت سبد
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<Basket> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new Basket
            {
                Id = dr.Field<int>("Id"),
                TotalPrice = dr.Field<int?>("TotalPrice"),
                UserId = dr.Field<int?>("UserId"),
                CreateDate = dr.Field<DateTime>("CreateDate"),
                LastUpdateDate = dr.Field<DateTime?>("LastUpdateDate"),
                Status = dr.Field<BasketStatus>("Status"),
                UserFullName = dr.Table.Columns.Contains("UserFullName") ? dr.Field<string>("UserFullName") : null,
                UserType = dr.Table.Columns.Contains("UserType") ? dr.Field<UserType?>("UserType") : null
            }).ToList();
        }




    }
}