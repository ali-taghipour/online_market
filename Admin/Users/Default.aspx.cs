using BLL;
using Entities;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Users
{
    public partial class Default : System.Web.UI.Page
    {
        private UserManager UserManager;

        public Default()
        {
            UserManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var Users = UserManager.GetAll();
            GenerateTable(Users);
        }



        /// <summary>
        /// ساخت جدول 
        /// </summary>
        /// <param name="Users">لیستی از اسلاید ها</param>
        public void GenerateTable(List<User> Users)
        {
            GenerateView GenerateView = new GenerateView();

            Table table = new Table();
            table.Attributes.Add("class", "table datatable");

            //ایجاد هدر برای جدول - THead
            List<string> Headers = new List<string>() { "آیدی", "تصویر", "نام", "نام کاربری", "نوع کاربر", "وضعیت", "" };
            table.Rows.Add(GenerateView.GenerateTableHeader(Headers));


            //ایجاد بدنه جدول - tbody
            TableRow row;
            TableCell cell;

            foreach (var item in Users)
            {
                row = new TableRow();
                row.TableSection = TableRowSection.TableBody;

                //ستون اول - آیدی
                cell = new TableCell();
                cell.Text = item.Id.ToString();
                row.Cells.Add(cell);

                //ستون دوم - تصویر
                cell = new TableCell();
                HtmlImage img = new HtmlImage();
                img.Src = item.Pic;
                cell.Controls.Add(img);
                row.Cells.Add(cell);

                //ستون سوم - نام کامل 
                cell = new TableCell();
                cell.Text = item.FullName;
                row.Cells.Add(cell);

                //ستون چهارم - نام کاربری 
                cell = new TableCell();
                cell.Text = item.Username;
                row.Cells.Add(cell);

                //ستون پنجم - نوع کاربری
                cell = new TableCell();
                cell.Text = item.Type.GetEnumDescription();
                row.Cells.Add(cell);


                //ستون ششم - وضعیت
                cell = new TableCell();
                if (item.IsEnabled)
                {
                    cell.Text = "فعال";
                    cell.Attributes.Add("class", "text-success");
                }
                else
                {
                    cell.Text = "غیر فعال";
                    cell.Attributes.Add("class", "text-danger");
                }
                row.Cells.Add(cell);
                
                //ستون آخر
                var ControlCell = GenerateView.GenerateRowControlCell("Users", item.Id);
                row.Cells.Add(ControlCell);

                table.Rows.Add(row);
            }
            TableContainer.Controls.Add(table);
        }




    }
}