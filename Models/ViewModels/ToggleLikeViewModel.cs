using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KargahProject.Models.ViewModels
{
    /// <summary>
    /// برای تابع با همین اسم در مدیریت لایکها
    /// </summary>
    public class ToggleLikeViewModel
    {
        /// <summary>
        /// تعداد لایکها بعد از اعمال تغییرات
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// آیا لایک شده است یا لایک برداشته شده است
        /// </summary>
        public bool IsLiked { get; set; }
    }
}