using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAD_Security;


namespace DAL
{
    /// <summary>
    /// Summary description for SlideRepository
    /// </summary>
    public class SlideRepository : RepositoryBase
    {
        public SlideRepository()
        {
        }


        /// <summary>
        /// گرفتن اسلاید با آیدی
        /// </summary>
        /// <param name="Id">آیدی اسلاید</param>
        /// <returns></returns>
        public DataRow GetById(int Id)
        {
            SqlCommand cmd = new SqlCommand("select * from Slides where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }







        /// <summary>
        /// گرفتن اسلایدر به همراه ادمین درج کننده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataRow GetByIdWithJoins(int Id)
        {
            SqlCommand cmd = new SqlCommand("select Slides.* , Users.FirstName as AdminFirstName , Users.LastName as AdminLastName "
                                            + " from Slides left join Users on AdminId = Users.Id where Slides.Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }





        /// <summary>
        /// گرفتن لیست همه اسلایدها
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {
            SqlCommand cmd = new SqlCommand("select * from Slides order by [Order]");
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }




        /// <summary>
        /// گرفتن لیست اسلایدهای سرچ شده
        /// </summary>
        /// <param name="Page">شماره صفحه. پیشفرض 1 است.</param>
        /// <param name="PageSize">تعداد آیتم های صفحه. پیشفرض 10 است.</param>
        /// <param name="SearchText">متن جستجو شده</param>
        /// <returns></returns>
        public DataSet GetSearchedItem(int? Page, int? PageSize, string SearchText)
        {
            SqlCommand cmd = new SqlCommand();

            //اگر متنی برای جستجو وجود داشته باشد
            string SearchQuery = "";
            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchQuery = " where Title Like N'%' + @SearchText + '%' ";
                cmd.Parameters.AddWithValue("@SearchText", SearchText);
            }

            //اگر تعداد آیتم های صفحه مشخص شده باشد آنگاه صفحه بندی
            //معنی دارد. در غیر اینصورت همه آیتم ها بر میگردد.
            string PaginationQuery = "";
            if (PageSize != null)
            {
                int page = Page ?? 1;
                PaginationQuery = " OFFSET @Start ROWS FETCH NEXT @End ROWS ONLY ";
                cmd.Parameters.AddWithValue("@Start", (page - 1) * PageSize);
                cmd.Parameters.AddWithValue("@End", PageSize);
            }

            string CmdText = "SELECT * FROM Slides " +SearchQuery+ " ORDER BY [Order] "+ PaginationQuery + "; "
                            + " select Count(*) as TotalCount from Slides " + SearchQuery;
            
            cmd.CommandText = CmdText;
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds;
            return null;
        }





        /// <summary>
        /// گرفتن همه اسلایدهایی که در حال حاضر قابل نمایش هستند
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActive()
        {
            string cmdText = "select * from Slides where IsEnabled = 1 and "
                            + " ((StartDate is NULL or StartDate < GETDATE()) "
                            + " and (EndDate is NULL or EndDate > GETDATE())) order by [Order], id desc ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }







        /// <summary>
        /// ایجاد اسلاید جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(Slide Slide)
        {
            string cmdText = "insert into Slides (LargePath,ThumbPath,Title,Link,[Order],StartDate,EndDate,IsEnabled,AdminId,CreateDate) "
                                      + "values (@LargePath,@ThumbPath,@Title,@Link,@Order,@StartDate,@EndDate,@IsEnabled,@AdminId,@CreateDate)";
            var Params = new Dictionary<string, object>();
            Params.Add("@LargePath", Slide.LargePath);
            Params.Add("@ThumbPath", Slide.ThumbPath);
            Params.Add("@Title", Slide.Title);
            Params.Add("@Link", Slide.Link);
            Params.Add("@Order", Slide.Order);
            Params.Add("@StartDate", Slide.StartDate);
            Params.Add("@EndDate", Slide.EndDate);
            Params.Add("@IsEnabled", Slide.IsEnabled);
            Params.Add("@AdminId", Slide.AdminId);
            Params.Add("@CreateDate", Slide.CreateDate);

            return SetData(cmdText, CommandType.Text, Params);
        }





        /// <summary>
        /// آپدیت اسلاید
        /// </summary>
        /// <returns></returns>
        public bool Update(Slide Slide)
        {
            string cmdText = "update Slides set LargePath=@LargePath, ThumbPath=@ThumbPath, Title=@Title, Link=@Link, "
                                            + "[Order]=@Order, StartDate=@StartDate, EndDate=@EndDate, IsEnabled=@IsEnabled "
                                            + "where Id = @Id";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Slide.Id);
            Params.Add("@LargePath", Slide.LargePath);
            Params.Add("@ThumbPath", Slide.ThumbPath);
            Params.Add("@Title", Slide.Title);
            Params.Add("@Link", Slide.Link);
            Params.Add("@Order", Slide.Order);
            Params.Add("@StartDate", Slide.StartDate);
            Params.Add("@EndDate", Slide.EndDate);
            Params.Add("@IsEnabled", Slide.IsEnabled);
            return SetData(cmdText, CommandType.Text, Params);
        }






        /// <summary>
        /// حذف اسلاید
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataRow Delete(int Id)
        {
            SqlCommand cmd = new SqlCommand("delete Slides OUTPUT DELETED.* from Slides where Id = @id");
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.CommandType = CommandType.Text;
            var ds = GetData(cmd);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0];
            return null;
        }



    }
}