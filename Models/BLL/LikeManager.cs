using DAL;
using Entities;
using Enums;
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
    /// Summary description for LikeManager
    /// </summary>
    public class LikeManager
    {
        private LikeRepository Repo;
        public LikeManager()
        {
            Repo = new LikeRepository();
        }


        /// <summary>
        /// گرفتن لایک های یک محصول
        /// </summary>
        /// <param name="ProductId">آیدی محصول</param>
        /// <returns></returns>
        public int? GetProductLikeCount(int? ProductId)
        {
            if (ProductId == null)
                return null;
            return Repo.GetProductLikeCount((int)ProductId);
        }



        /// <summary>
        /// گرفتن لایک به همراه ادمین درج کننده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Like GetByIdWithJoins(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetByIdWithJoins((int)Id);
            return ToDataModel(DataRow);
        }





        /// <summary>
        /// آیا کاربر محصول را لایک کرده است؟
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public bool UserLikeProduct(int? UserId , int? ProductId)
        {
            if (UserId == null || ProductId == null)
                return false;
            return Repo.UserLikeProduct(UserId, ProductId);
        }





        /// <summary>
        /// ایجاد لایک جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Like Like)
        {
            Like.CreateDate = DateTime.Now;
            return Repo.Create(Like);
        }


        /// <summary>
        /// ایجاد لایک جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(int UserId, int ProductId)
        {
            return Repo.Create(UserId, ProductId);
        }




        /// <summary>
        /// تغییر وضعیت لایک 
        /// اگر لایک نکرده بود، لایک شود
        /// اگر لایک کرده بود، دیسلایک شود
        /// </summary>
        /// <returns></returns>
        public ToggleLikeViewModel ToggleLike(int UserId, int ProductId)
        {
            DataRow DataRow = Repo.ToggleLike(UserId, ProductId);
            if (DataRow == null)
                return null;
            ToggleLikeViewModel model = new ToggleLikeViewModel()
            {
                IsLiked = DataRow.Field<bool>("IsLiked"),
                LikeCount = DataRow.Field<int>("LikeCount")
            };
            return model;
        }






        /// <summary>
        /// حذف لایک
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            return Repo.Delete(Id);
        }




        /// <summary>
        /// حذف لایک
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int UserId, int ProductId)
        {
            return Repo.Delete(UserId, ProductId);
        }





        /// <summary>
        /// تبدیل یک سطر از جدول لایکها به یک آبجکت لایک
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public Like ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            var Like = new Like()
            {
                Id = DataRow.Field<int>("Id"),
                UserId = DataRow.Field<int?>("UserId"),
                CreateDate = DataRow.Field<DateTime>("CreateDate"),
                ProductId = DataRow.Field<int?>("ProductId")
            };

            //اگر نیاز به گرفتن مشخصات کاربر بود
            if (Like.UserId != null && DataRow.Table.Columns.Contains("UserFullName"))
                Like.UserFullName = DataRow.Field<string>("UserFullName");


            if (Like.UserId != null && DataRow.Table.Columns.Contains("UserType"))
                Like.UserType = DataRow.Field<UserType?>("UserType");

            if (Like.ProductId != null && DataRow.Table.Columns.Contains("ProductTitle"))
                Like.ProductTitle = DataRow.Field<string>("ProductTitle");

            return Like;
        }





        /// <summary>
        /// تبدیل چند سطر از جدول لایکها به یک لیست از آبجکت لایک
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<Like> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new Like
            {
                Id = dr.Field<int>("Id"),
                UserId = dr.Field<int?>("UserId"),
                CreateDate = dr.Field<DateTime>("CreateDate"),
                ProductId = dr.Field<int?>("ProductId"),
                ProductTitle = dr.Table.Columns.Contains("ProductTitle") ? dr.Field<string>("ProductTitle") : null,
                UserFullName = dr.Table.Columns.Contains("UserFullName") ? dr.Field<string>("UserFullName") : null,
                UserType = dr.Table.Columns.Contains("UserType") ? dr.Field<UserType?>("UserType") : null
            }).ToList();
        }


    }
}
