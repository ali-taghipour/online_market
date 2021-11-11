 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entities
{
    /// <summary>
    /// Summary description for Product
    /// </summary>
    public class Product
    {
        public Product()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int Id { get; set; }

        /// <summary>
        /// عنوان محصول
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// خلاصه محصول
        /// </summary>
        public string Summary { get; set; }


        /// <summary>
        /// توضیحات کامل
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// موجودی انبار
        /// </summary>
        public int? Inventory { get; set; }


        /// <summary>
        /// قیمت اصلی محصول
        /// </summary>
        public int MainPrice { get; set; }

        /// <summary>
        /// قیمت با تخفیف محصول
        /// </summary>
        public int? OffPrice { get; set; }


        /// <summary>
        /// قیمت نهایی
        /// </summary>
        public int? FinalPrice
        {
            get
            {
                if (OffPrice != null)
                    return OffPrice;
                else
                    return MainPrice;
            }
        }


        /// <summary>
        /// آیا محصول فعال است و در سایت نشان داده میشود؟
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// تاریخ ایجاد محصول
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// آیدی ادمینی که محصول را درج کرده است.
        /// </summary>
        public int? AdminId { get; set; }
        public User Admin { get; set; }

        /// <summary>
        /// دسته بندی محصول
        /// </summary>
        public int? CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public Category Category { get; set; }


        /// <summary>
        /// تصویر اصلی پست
        /// </summary>
        public string MainPic { get; set; }


        /// <summary>
        /// تصاویر محصول
        /// </summary>
        public List<Picture> Pictures { get; set; }


        /// <summary>
        /// سبد خریدهایی که محصول در آنها وجود دارد
        /// </summary>
        public List<BasketProduct> BasketProducts { get; set; }


        /// <summary>
        /// تعداد لایک های محصول
        /// </summary>
        public int LikeCount { get; set; }


        /// <summary>
        /// کامنت های محصول
        /// </summary>
        public List<Comment> Comments { get; set; }

    }
}
