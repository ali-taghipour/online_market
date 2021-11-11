using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KargahProject.Models.ViewModels
{
    public class ValidateResultViewModel
    {
        /// <summary>
        /// آیا اطلاعات وارد شده صحیح بوده اند؟
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// متن خطاها در صورت وجود
        /// </summary>
        public string Errors { get; set; }
    }
}