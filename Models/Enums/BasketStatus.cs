using System.ComponentModel;


namespace Enums
{
    /// <summary>
    /// وضعیت های سبد خرید
    /// </summary>
    public enum BasketStatus
    {
        [Description("باز")]
        Open = 0,
        [Description("پرداخت شده")]
        Payed = 1,
        [Description("در حال آماده سازی")]
        Preparing = 2,
        [Description("در حال ارسال")]
        Sending = 3,
        [Description("تحویل داده شده")]
        Delivered = 4
    }
}