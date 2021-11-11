using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entities
{
    /// <summary>
    /// پرداخت های آنلاین انجام شده توسط کاربران
    /// </summary>
    public class Payment
    {
        public Payment()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsSuccess { get; set; }

        public int? Amount { get; set; }

        public string Description { get; set; }

        public long? PaymentKey { get; set; }

        public int? StatusCode { get; set; }

        public int? UserId { get; set; }
        public string UserFullName { get; set; }
        public UserType? UserType { get; set; }
        public User User { get; set; }

        public int? BasketId { get; set; }
        public Basket Basket { get; set; }

    }
}