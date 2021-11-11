using DAL;
using Entities;
using KargahProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TAD_ExtentionMethods;
using TAD_ImageResizer;


namespace BLL
{
    /// <summary>
    /// Summary description for SlideManager
    /// </summary>
    public class SlideManager
    {
        private SlideRepository Repo;
        public SlideManager()
        {
            Repo = new SlideRepository();
        }


        /// <summary>
        /// گرفتن اسلاید با آیدی
        /// </summary>
        /// <param name="Id">آیدی اسلاید</param>
        /// <returns></returns>
        public Slide GetById(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetById((int)Id);
            return ToDataModel(DataRow);
        }



        /// <summary>
        /// گرفتن اسلایدر به همراه ادمین درج کننده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Slide GetByIdWithJoins(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetByIdWithJoins((int)Id);
            return ToDataModel(DataRow);
        }





        /// <summary>
        /// گرفتن لیست همه اسلایدها
        /// </summary>
        /// <returns></returns>
        public List<Slide> GetAll()
        {
            DataTable DataTable = Repo.GetAll();
            return ToDataModel(DataTable);
        }




        /// <summary>
        /// گرفتن لیست اسلایدهای سرچ شده
        /// </summary>
        /// <param name="Page">شماره صفحه. پیشفرض 1 است.</param>
        /// <param name="PageSize">تعداد آیتم های صفحه. پیشفرض 10 است.</param>
        /// <param name="SearchText">متن جستجو شده</param>
        /// <returns></returns>
        public SearchResultViewModel<Slide> GetSearchedItem(int? Page , int? PageSize , string SearchText)
        {
            DataSet DataSet = Repo.GetSearchedItem(Page, PageSize, SearchText);
            //تعداد کل ایتم ها
            int TotalCount = int.Parse(DataSet.Tables[1].Rows[0][0].ToString());
            //تعداد صفحه هایی که خواهیم داشت
            int PageCount = 1;
            if (PageSize != null)
            {
                PageCount = TotalCount / (int)PageSize;
                if (TotalCount % (int)PageSize != 0)
                    PageCount++;
            }
            int CurrentPage = Page ?? 1;

            SearchResultViewModel<Slide> model = new SearchResultViewModel<Slide>()
            {
                CurrentPage = CurrentPage,
                PageCount = PageCount,
                Items = ToDataModel(DataSet.Tables[0])
            };
            return model;
        }




        /// <summary>
        /// گرفتن همه اسلایدهایی که در حال حاضر قابل نمایش هستند
        /// </summary>
        /// <returns></returns>
        public List<Slide> GetAllActive()
        {
            DataTable DataTable = Repo.GetAllActive();
            return ToDataModel(DataTable);
        }




        /// <summary>
        /// ایجاد اسلاید جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Slide Slide)
        {
            if (Slide.File != null && Slide.File.ContentLength > 0 && Slide.File.IsImage())
            {
                string FileName = Guid.NewGuid().ToString().GetImgUrlFriendly();
                string LargeName = FileName + Slide.File.GetExtention();
                string ThumbName = FileName + "-th" + Slide.File.GetExtention();

                string LargePath = "~/Uploads/Slides/" + LargeName;
                string ThumbPath = "~/Uploads/Slides/" + ThumbName;

                Slide.File.SaveAs(HttpContext.Current.Server.MapPath(LargePath));
                Slide.File.SaveAs(HttpContext.Current.Server.MapPath(ThumbPath));

                ImageResizer.ResizeImage(HttpContext.Current.Server.MapPath(LargePath), HttpContext.Current.Server.MapPath(LargePath), 1170, 400, 90);
                ImageResizer.ResizeImage(HttpContext.Current.Server.MapPath(ThumbPath), HttpContext.Current.Server.MapPath(ThumbPath), 130, 70, 90);

                Slide.LargePath = LargePath;
                Slide.ThumbPath = ThumbPath;
            }
            Slide.CreateDate = DateTime.Now;
            return Repo.Create(Slide);
        }




        /// <summary>
        /// آپدیت اسلاید
        /// </summary>
        /// <returns></returns>
        public bool Update(Slide Slide)
        {
            if (Slide.File != null && Slide.File.ContentLength > 0 && Slide.File.IsImage())
            {
                string FileName = Guid.NewGuid().ToString().GetImgUrlFriendly();
                string LargeName = FileName + Slide.File.GetExtention();
                string ThumbName = FileName + "-" + "th" + Slide.File.GetExtention();

                string LargePath = "~/Uploads/Slides/" + LargeName;
                string ThumbPath = "~/Uploads/Slides/" + ThumbName;

                Slide.File.SaveAs(HttpContext.Current.Server.MapPath(LargePath));
                Slide.File.SaveAs(HttpContext.Current.Server.MapPath(ThumbPath));

                ImageResizer.ResizeImage(HttpContext.Current.Server.MapPath(LargePath), HttpContext.Current.Server.MapPath(LargePath), 1170, 400, 90);
                ImageResizer.ResizeImage(HttpContext.Current.Server.MapPath(ThumbPath), HttpContext.Current.Server.MapPath(ThumbPath), 130, 70, 90);

                Slide.LargePath = LargePath;
                Slide.ThumbPath = ThumbPath;
            }
            return Repo.Update(Slide);
        }




        /// <summary>
        /// حذف اسلاید
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            DataRow DataRow = Repo.Delete(Id);
            var Slide = ToDataModel(DataRow);
            if (Slide == null)
                return false;
            if (!string.IsNullOrEmpty(Slide.LargePath) && File.Exists(HttpContext.Current.Server.MapPath(Slide.LargePath)))
                File.Delete(HttpContext.Current.Server.MapPath(Slide.LargePath));
            if (!string.IsNullOrEmpty(Slide.ThumbPath) && File.Exists(HttpContext.Current.Server.MapPath(Slide.ThumbPath)))
                File.Delete(HttpContext.Current.Server.MapPath(Slide.ThumbPath));
            return true;
        }





        /// <summary>
        /// ولیدیت کردن آبجکت اسلاید
        /// </summary>
        public ValidateResultViewModel Validate(Slide Slide)
        {
            bool IsValid = true;
            string Errors = "";
            if (Slide.LargePath == null)
            {
                if (Slide.File == null || Slide.File.ContentLength <= 0)
                {
                    IsValid = false;
                    Errors += "- تصویر اسلاید را انتخاب کنید.  <br />";
                }
            }

            if (Slide.File != null && Slide.File.ContentLength > 0 && !Slide.File.IsImage())
            {
                IsValid = false;
                Errors += "- فرمت فایل انتخاب شده صحیح نمی باشد. <br />";
            }

            if (!string.IsNullOrEmpty(Slide.Link))
            {
                string UrlRegx = @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";
                Regex regx = new Regex(UrlRegx);
                if (!regx.IsMatch(Slide.Link))
                {
                    IsValid = false;
                    Errors += "- فرمت وارد شده برای لینک صحیح نمی باشد. <br />";
                }
            }


            return new ValidateResultViewModel()
            {
                IsValid = IsValid,
                Errors = Errors
            };
        }







        /// <summary>
        /// تبدیل یک سطر از جدول اسلایدها به یک آبجکت اسلاید
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public Slide ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            var Slide = new Slide()
            {
                Id = DataRow.Field<int>("Id"),
                Title = DataRow.Field<string>("Title"),
                Link = DataRow.Field<string>("Link"),
                Order = DataRow.Field<int?>("Order"),
                AdminId = DataRow.Field<int?>("AdminId"),
                StartDate = DataRow.Field<DateTime?>("StartDate"),
                EndDate = DataRow.Field<DateTime?>("EndDate"),
                CreateDate = DataRow.Field<DateTime>("CreateDate"),
                IsEnabled = DataRow.Field<bool>("IsEnabled"),
                LargePath = DataRow.Field<string>("LargePath"),
                ThumbPath = DataRow.Field<string>("ThumbPath"),
            };
            

            //اگر نیاز به گرفتن مشخصات ادمین بود
            if (Slide.AdminId != null && DataRow.Table.Columns.Contains("AdminFirstName"))
            {
                var AdminFirstName = DataRow.Field<string>("AdminFirstName");
                var AdminLastName = DataRow.Field<string>("AdminLastName");
                if (!string.IsNullOrEmpty(AdminFirstName) && !string.IsNullOrEmpty(AdminLastName))
                    Slide.AdminName = AdminFirstName + " " + AdminLastName;
            }
            return Slide;
        }





        /// <summary>
        /// تبدیل چند سطر از جدول اسلایدها به یک لیست از آبجکت اسلاید
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<Slide> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new Slide
            {
                Id = dr.Field<int>("Id"),
                Title = dr.Field<string>("Title"),
                Link = dr.Field<string>("Link"),
                Order = dr.Field<int?>("Order"),
                AdminId = dr.Field<int?>("AdminId"),
                StartDate = dr.Field<DateTime?>("StartDate"),
                EndDate = dr.Field<DateTime?>("EndDate"),
                CreateDate = dr.Field<DateTime>("CreateDate"),
                IsEnabled = dr.Field<bool>("IsEnabled"),
                LargePath = dr.Field<string>("LargePath"),
                ThumbPath = dr.Field<string>("ThumbPath")
            }).ToList();
        }


    }
}
