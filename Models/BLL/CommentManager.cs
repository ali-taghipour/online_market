using DAL;
using Entities;
using Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace BLL
{
    /// <summary>
    /// Summary description for CommentManager
    /// </summary>
    public class CommentManager
    {
        private CommentRepository Repo;
        public CommentManager()
        {
            Repo = new CommentRepository();
        }


        /// <summary>
        /// گرفتن کامنت با آیدی
        /// </summary>
        /// <param name="Id">آیدی کامنت</param>
        /// <returns></returns>
        public Comment GetById(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetById((int)Id);
            return ToDataModel(DataRow);
        }



        /// <summary>
        /// گرفتن کامنت به همراه ادمین درج کننده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Comment GetByIdWithJoins(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetByIdWithJoins((int)Id);
            return ToDataModel(DataRow);
        }





        /// <summary>
        /// گرفتن لیست همه کامنتها
        /// </summary>
        /// <returns></returns>
        public List<Comment> GetAll()
        {
            DataTable DataTable = Repo.GetAll();
            return ToDataModel(DataTable);
        }




        /// <summary>
        /// گرفتن تعداد کامنتهای بررسی نشده
        /// </summary>
        /// <returns></returns>
        public int? GetUnreadCommentsCount()
        {
            return Repo.GetUnreadCommentsCount();
        }





        /// <summary>
        /// گرفتن لیست همه کامنتهای خوانده نشده
        /// </summary>
        /// <returns></returns>
        public List<Comment> GetUnreadComments()
        {
            DataTable DataTable = Repo.GetUnreadComments();
            return ToDataModel(DataTable);
        }


      


        /// <summary>
        /// گرفتن همه کامنتهای یک محصول
        /// </summary>
        /// <returns></returns>
        public List<Comment> GetByProductId(int ProductId)
        {
            DataTable DataTable = Repo.GetByProductId(ProductId);
            return ToDataModel(DataTable);
        }






        /// <summary>
        /// ایجاد کامنت جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Comment Comment)
        {
            Comment.CreateDate = DateTime.Now;
            return Repo.Create(Comment);
        }




        /// <summary>
        /// آپدیت کامنت
        /// </summary>
        /// <returns></returns>
        public bool Update(Comment Comment)
        {
            return Repo.Update(Comment);
        }





        /// <summary>
        /// حذف کامنت
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            return Repo.Delete(Id);
        }





        /// <summary>
        /// تغییر وضعیت تایید کامنت
        /// </summary>
        /// <param name="CommentId">آیدی کامنت</param>
        /// <returns></returns>
        public bool ToggleCommentApproved(int Id)
        {
            return Repo.ToggleCommentApproved(Id);
        }






        /// <summary>
        /// تغییر وضعیت بررسی کامنت
        /// </summary>
        /// <param name="CommentId">آیدی کامنت</param>
        /// <returns></returns>
        public bool ToggleCommentReaded(int Id)
        {
            return Repo.ToggleCommentReaded(Id);
        }





        /// <summary>
        /// تبدیل یک سطر از جدول کامنتها به یک آبجکت کامنت
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public Comment ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            var Comment = new Comment()
            {
                Id = DataRow.Field<int>("Id"),
                Text = DataRow.Field<string>("Text"),
                Email = DataRow.Field<string>("Email"),
                FullName = DataRow.Field<string>("FullName"),
                UserId = DataRow.Field<int?>("UserId"),
                CreateDate = DataRow.Field<DateTime>("CreateDate"),
                IsReaded = DataRow.Field<bool>("IsReaded"),
                IsApproved = DataRow.Field<bool>("IsApproved"),
                ProductId = DataRow.Field<int?>("ProductId")
            };

            //اگر نیاز به گرفتن مشخصات کاربر بود
            if (Comment.UserId != null && DataRow.Table.Columns.Contains("UserFullName"))
                Comment.UserFullName = DataRow.Field<string>("UserFullName");


            if (Comment.UserId != null && DataRow.Table.Columns.Contains("UserType"))
                Comment.UserType = DataRow.Field<UserType?>("UserType");

            if (Comment.ProductId != null && DataRow.Table.Columns.Contains("ProductTitle"))
                Comment.ProductTitle = DataRow.Field<string>("ProductTitle");

            return Comment;
        }





        /// <summary>
        /// تبدیل چند سطر از جدول کامنتها به یک لیست از آبجکت کامنت
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<Comment> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new Comment
            {
                Id = dr.Field<int>("Id"),
                Text = dr.Field<string>("Text"),
                Email = dr.Field<string>("Email"),
                FullName = dr.Field<string>("FullName"),
                UserId = dr.Field<int?>("UserId"),
                CreateDate = dr.Field<DateTime>("CreateDate"),
                IsReaded = dr.Field<bool>("IsReaded"),
                IsApproved = dr.Field<bool>("IsApproved"),
                ProductId = dr.Field<int?>("ProductId"),
                ProductTitle = dr.Table.Columns.Contains("ProductTitle") ? dr.Field<string>("ProductTitle") : null,
                UserFullName = dr.Table.Columns.Contains("UserFullName") ? dr.Field<string>("UserFullName") : null,
                UserType = dr.Table.Columns.Contains("UserType") ? dr.Field<UserType?>("UserType") : null
            }).ToList();
        }


    }
}
