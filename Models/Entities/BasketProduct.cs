using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entities
{
    /// <summary>
    /// محصولاتی که در سبد خرید وجود دارند
    /// </summary>
    public class BasketProduct
    {
        public BasketProduct()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int Id { get; set; }

        /// <summary>
        /// آیدی سبد خرید
        /// </summary>
        public int BasketId { get; set; }

        /// <summary>
        /// سبد خرید
        /// </summary>
        public Basket Basket { get; set; }

        /// <summary>
        /// آیدی محصول
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// محصول
        /// </summary>
        public Product Product { get; set; }


        /// <summary>
        /// تعداد محصول خریداری شده
        /// </summary>
        public int Count { get; set; }


        /// <summary>
        /// قیمت موقع خرید
        /// </summary>
        public int? Price { get; set; }


        /// <summary>
        /// تاریخ افزودن محصول به سبد خرید
        /// </summary>
        public DateTime CreateDate { get; set; }
    }

}