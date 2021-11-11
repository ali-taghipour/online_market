using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KargahProject.Models.ViewModels
{
    /// <summary>
    /// نتیجه جستجو با شماره صفحه، اندازه صفحه و متن 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchResultViewModel <T>
    {
        /// <summary>
        /// لیستی از ابجکت ها
        /// مثلا لیستی از اسلاید
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// تعداد صفحاتی که باید ساخته شود
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// صفحه فعلی که انتخاب شده است
        /// </summary>
        public int CurrentPage { get; set; }

    }
}