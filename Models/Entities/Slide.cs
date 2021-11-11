using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAD_ExtentionMethods;

namespace Entities
{
    /// <summary>
    /// Summary description for Slide
    /// </summary>
    public class Slide
    {
        public Slide()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Id { get; set; }

        /// <summary>
        /// آدرس تصویر اسلاید
        /// </summary>
        public string LargePath { get; set; }
        public string ThumbPath { get; set; }
        public HttpPostedFile File { get; set; }

        /// <summary>
        /// عنوان اسلاید
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// در صورت کلیک روی اسلاید به چه صفحه ای برود.
        /// </summary>
        public string Link { get; set; }


        /// <summary>
        /// ترتیب لود شدن اسلاید ها
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// تاریخ شروع نمایش اسلاید
        /// </summary>
        public DateTime? StartDate { get; set; }
        public string PersianStartDate
        {
            get
            {
                if (StartDate == null)
                    return null;
                return ((DateTime)StartDate).ToPersianDateTime().ToString();
            }
        }

        /// <summary>
        /// تاریخ پایان نمایش اسلاید
        /// </summary>
        public DateTime? EndDate { get; set; }
        public string PersianEndDate
        {
            get
            {
                if (EndDate == null)
                    return null;
                return ((DateTime)EndDate).ToPersianDateTime().ToString();
            }
        }


        /// <summary>
        /// اسلاید فعال است؟
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// تاریخ درج
        /// </summary>
        public DateTime CreateDate { get; set; }
        public string PersianCreateDate
        {
            get
            {
                return CreateDate.ToPersianDateTime().ToString();
            }
        }

        /// <summary>
        /// ادمین درج کننده اسلاید
        /// </summary>
        public int? AdminId { get; set; }
        public User Admin { get; set; }
        public string AdminName { get; set; }
    }

}