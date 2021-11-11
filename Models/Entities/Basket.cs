using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entities
{
    /// <summary>
    /// سبدهای خرید کاربران
    /// </summary>
    public class Basket
    {
        public Basket()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Id { get; set; }

        /// <summary>
        /// تاریخ ایجاد سبد خرید
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر وضعیت سبد خرید
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }


        /// <summary>
        /// وضعیت سبد خرید
        /// </summary>
        public BasketStatus Status { get; set; }


        /// <summary>
        /// مجموع مبلغ سبد خرید که بعد از نهایی شدن سبد خرید مشخص میشود
        /// </summary>
        public int? TotalPrice { get; set; }

        /// <summary>
        /// آیدی کاربر
        /// </summary>
        public int? UserId { get; set; }
        public User User { get; set; }
        public UserType? UserType { get; set; }
        public string UserFullName { get; set; }

        /// <summary>
        /// محصولات موجود در سبد خرید
        /// </summary>
        public List<BasketProduct> BasketProducts { get; set; }
    }

}

