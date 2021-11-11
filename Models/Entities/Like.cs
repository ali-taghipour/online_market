using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entities
{
    /// <summary>
    /// Summary description for Like
    /// </summary>
    public class Like
    {
        public Like()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int Id { get; set; }
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// کاربری که لایک کرده است
        /// </summary>
        public int? UserId { get; set; }
        public User User { get; set; }
        public string UserFullName { get; set; }
        public UserType? UserType { get; set; }


        /// <summary>
        /// محصولی که لایک شده است
        /// </summary>
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductTitle { get; set; }
    }
}