using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entities
{
    /// <summary>
    /// Summary description for Comment
    /// </summary>
    public class Comment
    {
        public Comment()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int Id { get; set; }

        /// <summary>
        /// متن کامنت
        /// </summary>
        public string Text { get; set; }


        /// <summary>
        /// ایمیل کاربر مهمان
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// نام کامل کاربر مهمان
        /// </summary>
        public string FullName { get; set; }


        /// <summary>
        /// کامنت بررسی شده است
        /// </summary>
        public bool IsReaded { get; set; }

        /// <summary>
        /// کامنت برای نمایش در سایت تایید شده است
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// تاریخ درج کامنت 
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// کاربری که کامنت گذاشته است (برای کاربرانی که لاگین کرده اند)
        /// </summary>
        public int? UserId { get; set; }
        public User User { get; set; }
        public string UserFullName { get; set; }
        public UserType? UserType { get; set; }


        /// <summary>
        /// محصولی که کامنت برای ان درج شده است
        /// </summary>
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductTitle { get; set; }


    }
}