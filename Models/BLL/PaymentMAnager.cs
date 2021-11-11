using DAL;
using Entities;
using Enums;
using KargahProject.Models.DAL;
using KargahProject.Models.ViewModels;
using MD.PersianDateTime;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TAD_ExtentionMethods;
using TAD_ImageResizer;

namespace KargahProject.Models.BLL
{
    public class PaymentManager
    {
        PaymentRepository Repo { get; set; }

        public PaymentManager()
        {
            Repo = new PaymentRepository();
        }


        /// <summary>
        /// ایجاد پرداخت جدید
        /// </summary>
        /// <param name="Amount">مبلغ</param>
        /// <param name="UserId">کاربر پرداخت کننده</param>
        /// <param name="Description">توضیحات</param>
        /// <returns>آیدی پرداخت ایجاد شده</returns>
        public int? Create(int Amount, int UserId , int BasketId, string Description)
        {
            Payment payment = new Payment
            {
                Amount = Amount,
                Description = Description,
                CreateDate = DateTime.Now,
                IsSuccess = false,
                UserId = UserId,
                BasketId = BasketId
            };
            return Create(payment);
        }

        




        /// <summary>
        /// ایجاد پرداخت جدید
        /// </summary>
        public int? Create(Payment Payment)
        {
            return Repo.Create(Payment);
        }




        

        /// <summary>
        /// گرفتن اطلاعات پرداخت
        /// </summary>
        public Payment GetById(int? id)
        {
            if (id == null)
                return null;
            return ToDataModel(Repo.GetById((int)id));
        }




        /// <summary>
        /// گرفتن همه پرداخت ها
        /// </summary>
        /// <returns></returns>
        public List<Payment> GetAll()
        {
            return ToDataModel(Repo.GetAll());
        }




        /// <summary>
        /// حذف پرداخت
        /// </summary>
        public bool Delete(int? id)
        {
            if (id == null)
                return false;
            return Repo.Delete((int)id);
        }




        /// <summary>
        /// اعلام موفقیت آمیز بودن پرداخت
        /// </summary>
        public bool Update(Payment Payment)
        {
            return Repo.Update(Payment);           
        }









        /// <summary>
        /// تبدیل یک سطر از جدول پرداختها به یک آبجکت پرداخت
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public Payment ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            var Payment = new Payment()
            {
                Id = DataRow.Field<int>("Id"),
                Amount = DataRow.Field<int?>("Amount"),
                UserId = DataRow.Field<int?>("UserId"),
                CreateDate = DataRow.Field<DateTime>("CreateDate"),
                Description = DataRow.Field<string>("Description"),
                BasketId = DataRow.Field<int?>("BasketId"),
                IsSuccess = DataRow.Field<bool>("IsSuccess"),
                PaymentKey = DataRow.Field<long?>("PaymentKey"),
                StatusCode = DataRow.Field<int?>("StatusCode")
            };

            //اگر نیاز به گرفتن مشخصات کاربر بود
            if (Payment.UserId != null && DataRow.Table.Columns.Contains("UserFullName"))
                Payment.UserFullName = DataRow.Field<string>("UserFullName");


            if (Payment.UserId != null && DataRow.Table.Columns.Contains("UserType"))
                Payment.UserType = DataRow.Field<UserType?>("UserType");

            return Payment;
        }





        /// <summary>
        /// تبدیل چند سطر از جدول پرداختها به یک لیست از آبجکت پرداخت
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<Payment> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new Payment
            {
                Id = dr.Field<int>("Id"),
                Amount = dr.Field<int?>("Amount"),
                UserId = dr.Field<int?>("UserId"),
                CreateDate = dr.Field<DateTime>("CreateDate"),
                Description = dr.Field<string>("Description"),
                IsSuccess = dr.Field<bool>("IsSuccess"),
                PaymentKey = dr.Field<long?>("PaymentKey"),
                BasketId = dr.Field<int?>("BasketId"),
                StatusCode = dr.Field<int?>("StatusCode"),
                UserFullName = dr.Table.Columns.Contains("UserFullName") ? dr.Field<string>("UserFullName") : null,
                UserType = dr.Table.Columns.Contains("UserType") ? dr.Field<UserType?>("UserType") : null
            }).ToList();
        }








    }
}