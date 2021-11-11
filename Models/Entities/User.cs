using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entities
{
    /// <summary>
    /// Summary description for User
    /// </summary>
    public class User
    {
        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(FirstName) || !string.IsNullOrEmpty(LastName))
                    return FirstName + " " + LastName;
                else
                    return Username;
            }
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? PostalCode { get; set; }

        /// <summary>
        /// نوع کاربر
        /// </summary>
        public UserType Type { get; set; }



        /// <summary>
        /// تصویر کاربر
        /// </summary>
        public string Pic { get; set; }
        public HttpPostedFile File { get; set; }



        /// <summary>
        /// آیای کاربر مرد است؟
        /// true => مرد است
        /// false => زن است
        /// </summary>
        public bool IsMale { get; set; }

        /// <summary>
        /// آیا کاربر فعال است؟
        /// true => بله
        /// false => خیر
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreateDate { get; set; }


        /// <summary>
        /// سبدهای خرید کاربر
        /// </summary>
        public List<Basket> Baskets { get; set; }

    }

}