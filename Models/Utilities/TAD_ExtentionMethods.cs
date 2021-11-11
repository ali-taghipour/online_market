//using Antlr.Runtime.Misc;
using MD.PersianDateTime;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TAD_ExtentionMethods
{
    public static class ExtentionMethods
    {
        /// <summary>
        /// حذف کاراکتر های نامناسب از اسم فایل
        /// </summary>
        public static string GetImgUrlFriendly(this string word)
        {
            return word.Replace("  ", "")
                        .Replace(" ", "")
                        .Replace("_", "")
                        .Replace("%", "")
                        .Replace(":", "")
                        .Replace("?", "")
                        .Replace(";", "")
                        .Replace("*", "")
                        .Replace("=", "")
                        .Replace("^", "")
                        .Replace("#", "")
                        .Replace("/", "")
                        .Replace(".", "")
                        .Replace("\"", "")
                        .Replace("\'", "");
        }



        /// <summary>
        /// آیا فایل یک تصویر است؟
        /// </summary>
        public static bool IsImage(this HttpPostedFile file)
        {
            if (file == null) return false;
            string ThExt = file.GetExtention();
            return (ThExt == ".jpg" || ThExt == ".png" || ThExt == ".jpeg" || ThExt == ".bmp" || ThExt == ".gif" || ThExt == ".tiff");
        }


        /// <summary>
        /// گرفتن پسوند فایل
        /// </summary>
        public static string GetExtention(this HttpPostedFile file)
        {
            return Path.GetExtension(file.FileName).ToLower();
        }



        /// <summary>
        /// گرفتن خلاصه از متن با تعداد کاراکتر های دلخواه
        /// </summary>
        /// <returns></returns>
        public static string GetSummary(this string Text , int Count)
        {
            if (string.IsNullOrEmpty(Text)) return "";
            if (Text.Length >= Count)
                return Text.Substring(0, Count) + " [...]";
            else
                return Text;
        }


        /// <summary>
        /// تبدیل استرینگ به عدد
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static int? GetInt(this string Text)
        {
            if (string.IsNullOrEmpty(Text))
                return null;
            int Num;
            if (int.TryParse(Text, out Num))
                return Num;
            else
                return null;
        }



        #region قرار دادن ویرگول بین عدد

        /// <summary>
        /// قرار دادن ویرگول بین عدد برای خوانایی
        /// 12345678 => 12,345,678
        /// </summary>
        public static string GetPriceFormat(this string price)
        {
            if (string.IsNullOrEmpty(price))
                return null;
            int InPrice;
            if(int.TryParse(price , out InPrice))
                return String.Format("{0:n0}", InPrice);
            return null;
        }


        public static string GetPriceFormat(this int price)
        {
            return String.Format("{0:n0}", price);
        }



        /// <summary>
        /// تبدیل قیمت به فرمت 95,000
        /// </summary>
        /// <returns></returns>
        public static string GetToomanPriceFormat(this int Rialprice)
        {
            int InPrice = Rialprice / 10;
            return String.Format("{0:n0}", InPrice);            
        }


        public static string GetToomanPriceFormat(this int? Rialprice)
        {
            if (Rialprice == null)
                return null;
            return GetToomanPriceFormat((int)Rialprice);
        }


        public static string GetToomanPriceFormat(this string Rialprice)
        {
            if (string.IsNullOrEmpty(Rialprice))
                return null;
            int InPrice;
            if (int.TryParse(Rialprice, out InPrice))
            {
                InPrice = InPrice / 10;
                return String.Format("{0:n0}", InPrice);
            }
            return null;
        }
        #endregion



        
        /// <summary>
        /// چک میکند که ایا دو تاریخ در یک روز هستند؟
        /// </summary>
        /// <returns></returns>
        public static bool IsInSameDay(this DateTime FirstDate, DateTime SecondDate)
        {
            return FirstDate.Year.Equals(SecondDate.Year)
                    && FirstDate.Month.Equals(SecondDate.Month)
                    && FirstDate.Day.Equals(SecondDate.Day);
        }




        /// <summary>
        /// تبدیل تاریخ شمسی بصورت رشته به تاریخ میلادی
        /// </summary>
        /// <param name="PersianDate">مثلا 1397/07/12</param>
        /// <param name="MinTime">
        /// true => ساعت = 00:00:00
        /// false => ساعت = 23:59:59
        /// </param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string PersianDate, bool WithMinTime)
        {
            try
            {
                if (string.IsNullOrEmpty(PersianDate))
                    return null;
                var Array = PersianDate.Trim().Split('/');
                if (Array.Length != 3)
                    return null;
                int pYear = int.Parse(Array[0]);
                int pMonth = int.Parse(Array[1]);
                int pDay = int.Parse(Array[2]);
                PersianDateTime pd;
                if (WithMinTime)
                    pd = new PersianDateTime(pYear, pMonth, pDay) { EnglishNumber = true };
                else
                    pd = new PersianDateTime(pYear, pMonth, pDay, 23, 59, 59) { EnglishNumber = true };
                return pd.ToDateTime();
            }
            catch
            {
                return null;
            }
        }





        /// <summary>
        /// تبدیل تاریخ و زمان شمسی بصورت رشته به تاریخ میلادی
        /// </summary>
        /// <param name="PersianDate">1397/07/12 12:08:00</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string PersianDateTime)
        {
            try
            {
                if (string.IsNullOrEmpty(PersianDateTime))
                    return null;

                var Array1 = PersianDateTime.Trim().Split(' ');

                //تاریخ
                var Array2 = Array1[0].Split('/');
                if (Array2.Length != 3)
                    return null;
                int pYear = int.Parse(Array2[0]);
                int pMonth = int.Parse(Array2[1]);
                int pDay = int.Parse(Array2[2]);

                //ساعت
                var Array3 = Array1[1].Split(':');
                if (Array3.Length < 3)
                    return null;
                int pHour = int.Parse(Array3[0]);
                int pMin = int.Parse(Array3[1]);
                int pSec = int.Parse(Array3[2]);

                PersianDateTime pd = new PersianDateTime(pYear, pMonth, pDay, pHour, pMin, pSec) { EnglishNumber = true };
                return pd.ToDateTime();
            }
            catch
            {
                return null;
            }
        }





        /// <summary>
        /// گرفتن تاریخ در قالب شمسی به صورت
        /// سه شنبه، 5 دی 1396 
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static string GetPersianFormat(this DateTime Date)
        {
            PersianDateTime pd = new PersianDateTime(Date) { EnglishNumber = true};
            return pd.GetLongDayOfWeekName + "، " + pd.Day + " " + pd.MonthName + " " + pd.Year;
        }



        /// <summary>
        /// گرفتن تاریخ در قالب ابجکت تاریخ شمسی
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static PersianDateTime ToPersianDateTime(this DateTime Date)
        {
            var pd = new PersianDateTime(Date) { EnglishNumber = true};
            return pd;
        }



        /// <summary>
        /// گرفتن تاریخ در قالب ابجکت تاریخ شمسی
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static PersianDateTime? ToPersianDateTime(this DateTime? Date)
        {
            if (Date == null)
                return null;
            var pd = new PersianDateTime(Date);
            pd.EnglishNumber = true;
            return pd;
        }




        /// <summary>
        /// گرفتن یک نام فارسی از Description یک enum
        /// get userfriendly name from Description attr of enum 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        
        /// <summary>
        /// نحوه استفاده 
        /// ExtentionMethods.ToEnumViewModel<EnumName>()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumViewModel> ToEnumViewModel<T> ()
        {
            var model = Enum.GetValues(typeof(T)).Cast<int>()
                .Select(id => new EnumViewModel
                {
                    Id = id,
                    Key = Enum.GetName(typeof(T), id),
                    Title = ((Enum)Enum.Parse(typeof(T), Enum.GetName(typeof(T), id), true)).GetEnumDescription()
                }).ToList();
            return model;
        }



        /// <summary>
        /// تبدیل اعداد با کاراکتر های فارسی به کاراکتر های انگلیسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToEnglishNumber(this string input)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
            string[] english = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            for (int i = 0; i < 10; i++)
            {
                input = input.Replace(persian[i], english[i]);
            }
            return input;
        }


    }


    /// <summary>
    /// ویو مدل برای گرفتن Enum
    /// </summary>
    public class EnumViewModel
    {
        /// <summary>
        /// عدد مربوطه
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// متن انگلیسی یا KeyWord
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// متن فارسی نوشته شده در description
        /// </summary>
        public string Title { get; set; }
    }


    

}