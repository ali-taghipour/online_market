using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using TAD_ExtentionMethods;
using TAD_ImageResizer;
using TAD_Security;



namespace BLL
{
    /// <summary>
    /// Summary description for PictureManager
    /// </summary>
    public class PictureManager
    {
        private PictureRepository Repo;
        public PictureManager()
        {
            Repo = new PictureRepository();
        }


        /// <summary>
        /// گرفتن تصویر با آیدی
        /// </summary>
        /// <param name="Id">آیدی تصویر</param>
        /// <returns></returns>
        public Picture GetById(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetById((int)Id);
            return ToDataModel(DataRow);
        }





        /// <summary>
        /// گرفتن لیست همه تصاویر
        /// </summary>
        /// <returns></returns>
        public List<Picture> GetAll()
        {
            DataTable DataTable = Repo.GetAll();
            return ToDataModel(DataTable);
        }





        /// <summary>
        /// گرفتن لیست همه تصاویر یک محصول خاص
        /// </summary>
        /// <returns></returns>
        public List<Picture> GetByProductId(int? ProductId)
        {
            if (ProductId == null)
                return new List<Picture>();
            DataTable DataTable = Repo.GetByProductId((int)ProductId);
            return ToDataModel(DataTable);
        }








        /// <summary>
        /// آپلود همه تصاویر کاربر
        /// </summary>
        /// <returns></returns>
        public bool UploadAllPics(int ProductId , HttpFileCollection Files)
        {
            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i] != null && Files[i].ContentLength > 0 && Files[i].IsImage())
                {
                    var Pic = new Picture()
                    {
                        CreateDate = DateTime.Now,
                        ProductId = ProductId,
                        IsMain = false
                    };
                    Create(Pic, Files[i]);
                }
            }
            return true;
        }








        /// <summary>
        /// ایجاد تصویر جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Picture Picture, HttpPostedFile File)
        {
            if (File != null && File.ContentLength > 0 && File.IsImage())
            {
                string FileName = Guid.NewGuid().ToString().GetImgUrlFriendly();
                string LargeName = FileName + File.GetExtention();
                string ThumbName = FileName + "-" + "th" + File.GetExtention();

                string LargePath = "~/Uploads/Pictures/Larges/" + LargeName;
                string ThumbPath = "~/Uploads/Pictures/Thumbs/" + ThumbName;

                File.SaveAs(HttpContext.Current.Server.MapPath(LargePath));
                File.SaveAs(HttpContext.Current.Server.MapPath(ThumbPath));

                //resize
                ImageResizer.OnlyResizeImage(HttpContext.Current.Server.MapPath(LargePath), HttpContext.Current.Server.MapPath(LargePath), 800, 400, 90);
                //crop
                ImageResizer.ResizeImage(HttpContext.Current.Server.MapPath(ThumbPath), HttpContext.Current.Server.MapPath(ThumbPath), 360, 240, 90);

                Picture.LargePath = LargePath;
                Picture.ThumbPath = ThumbPath;
            }
            Picture.CreateDate = DateTime.Now;
            return Repo.Create(Picture);
        }




        /// <summary>
        /// آپدیت تصویر
        /// </summary>
        /// <returns></returns>
        public bool Update(Picture Picture, HttpPostedFile File)
        {
            if (File != null && File.ContentLength > 0 && File.IsImage())
            {
                string FileName = Guid.NewGuid().ToString().GetImgUrlFriendly();
                string LargeName = FileName + File.GetExtention();
                string ThumbName = FileName + "-" + "th" + File.GetExtention();

                string LargePath = "~/Uploads/Pictures/Larges/" + LargeName;
                string ThumbPath = "~/Uploads/Pictures/Thumbs/" + ThumbName;

                File.SaveAs(HttpContext.Current.Server.MapPath(LargePath));
                File.SaveAs(HttpContext.Current.Server.MapPath(ThumbPath));

                ImageResizer.OnlyResizeImage(HttpContext.Current.Server.MapPath(LargePath), HttpContext.Current.Server.MapPath(LargePath), 600, 600, 90);
                ImageResizer.ResizeImage(HttpContext.Current.Server.MapPath(ThumbPath), HttpContext.Current.Server.MapPath(ThumbPath), 150, 150, 90);

                Picture.LargePath = LargePath;
                Picture.ThumbPath = ThumbPath;
            }
            return Repo.Update(Picture);
        }




        /// <summary>
        /// حذف تصویر
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            DataRow DataRow = Repo.Delete(Id);
            var Picture = ToDataModel(DataRow);
            if (Picture == null)
                return false;
            if (!string.IsNullOrEmpty(Picture.LargePath) && File.Exists(HttpContext.Current.Server.MapPath(Picture.LargePath)))
                File.Delete(HttpContext.Current.Server.MapPath(Picture.LargePath));
            if (!string.IsNullOrEmpty(Picture.ThumbPath) && File.Exists(HttpContext.Current.Server.MapPath(Picture.ThumbPath)))
                File.Delete(HttpContext.Current.Server.MapPath(Picture.ThumbPath));
            return true;
        }




        /// <summary>
        /// تعیین تصویر اصلی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool SetMainPic(int Id , int ProductId)
        {
            return Repo.SetMainPic(Id, ProductId);
        }





        /// <summary>
        /// تبدیل یک سطر از جدول تصاویر به یک آبجکت تصویر
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public Picture ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            return new Picture()
            {
                Id = DataRow.Field<int>("Id"),
                LargePath = DataRow.Field<string>("LargePath"),
                ThumbPath = DataRow.Field<string>("ThumbPath"),
                IsMain = DataRow.Field<bool>("IsMain"),
                ProductId = DataRow.Field<int?>("ProductId"),
                CreateDate = DataRow.Field<DateTime>("CreateDate")
            };
        }





        /// <summary>
        /// تبدیل چند سطر از جدول تصاویر به یک لیست از آبجکت تصویر
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<Picture> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new Picture
            {
                Id = dr.Field<int>("Id"),
                LargePath = dr.Field<string>("LargePath"),
                ThumbPath = dr.Field<string>("ThumbPath"),
                IsMain = dr.Field<bool>("IsMain"),
                ProductId = dr.Field<int?>("ProductId"),
                CreateDate = dr.Field<DateTime>("CreateDate")
            }).ToList();
        }


    }
}