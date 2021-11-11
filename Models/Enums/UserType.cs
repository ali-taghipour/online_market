using System.ComponentModel;

namespace Enums
{
    /// <summary>
    /// نوع کاربر
    /// </summary>
    public enum UserType
    {
        [Description("کاربر عادی")]
        Customer = 0,
        [Description("ادمین کل")]
        Admin = 1
    }
    
    
}