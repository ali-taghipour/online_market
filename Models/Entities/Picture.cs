using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entities
{
    /// <summary>
    /// Summary description for Picture
    /// </summary>
    public class Picture
    {
        public Picture()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Id { get; set; }

        /// <summary>
        /// آدرس تصویر بزرگ
        /// </summary>
        public string LargePath { get; set; }

        /// <summary>
        /// آدرس تصویر کوچک
        /// </summary>
        public string ThumbPath { get; set; }

        /// <summary>
        /// آیا تصویر اصلی برای محصول است؟
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// تاریخ درج تصویر
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// تصویر برای چه محصولی است؟
        /// </summary>
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}