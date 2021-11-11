using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Comments
{
    public partial class Archive : System.Web.UI.Page
    {

        private CommentManager CommentManager;

        public Archive()
        {
            CommentManager = new CommentManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var Comments = CommentManager.GetAll();
            GenerateTable(Comments);
        }



        /// <summary>
        /// ساخت جدول 
        /// </summary>
        /// <param name="Comments">لیستی از کامنت ها</param>
        public void GenerateTable(List<Comment> Comments)
        {
            GenerateView GenerateView = new GenerateView();

            Table table = new Table();
            table.Attributes.Add("class", "table datatable");

            //ایجاد هدر برای جدول - THead
            List<string> Headers = new List<string>() { "آیدی", "متن کامنت", "عنوان پست", "نام کاربر", "تاریخ درج", "" };
            table.Rows.Add(GenerateView.GenerateTableHeader(Headers));


            //ایجاد بدنه جدول - tbody
            TableRow row;
            TableCell cell;

            if (Comments != null)
            {
                foreach (var item in Comments)
                {
                    row = new TableRow();
                    row.TableSection = TableRowSection.TableBody;

                    //ستون اول - آیدی
                    cell = new TableCell();
                    cell.Text = item.Id.ToString();
                    row.Cells.Add(cell);

                    //ستون دوم - متن کامنت
                    cell = new TableCell();
                    cell.Text = item.Text.GetSummary(70);
                    row.Cells.Add(cell);

                    //ستون سوم - عنوان پست 
                    cell = new TableCell();
                    HtmlAnchor anc1 = new HtmlAnchor();
                    anc1.HRef = "~/Admin/Products/Details.aspx?id=" + item.ProductId;
                    anc1.Attributes["target"] = "_blank";
                    anc1.InnerText = item.ProductTitle.GetSummary(30);
                    cell.Controls.Add(anc1);
                    row.Cells.Add(cell);

                    //ستون چهارم - نام کاربر 
                    cell = new TableCell();
                    if (item.UserId != null)
                    {
                        HtmlAnchor anc2 = new HtmlAnchor();
                        anc2.HRef = "~/Admin/Users/Details.aspx?id=" + item.UserId;
                        anc2.Attributes["target"] = "_blank";
                        anc2.InnerText = item.UserFullName + " (" + item.UserType.GetEnumDescription() + ")";
                        cell.Controls.Add(anc2);
                    }
                    else
                        cell.Text = item.FullName;
                    row.Cells.Add(cell);


                    //ستون پنجم - تاریخ درج 
                    cell = new TableCell();
                    cell.Text = item.CreateDate.ToPersianDateTime().ToString();
                    row.Cells.Add(cell);


                    //ستون آخر
                    cell = new TableCell();

                    //دکمه بررسی شد
                    HtmlButton btn1 = new HtmlButton();
                    btn1.Attributes["onclick"] = "ToggleCommentReaded(" + item.Id + " , false)";
                    if (!item.IsReaded)
                    {
                        btn1.InnerText = "بررسی نشد ";
                        btn1.Attributes["class"] = "readed btn btn-sm btn-warning";
                    }
                    else
                    {
                        btn1.InnerText = "بررسی شد ";
                        btn1.Attributes["class"] = "readed btn btn-sm btn-success";
                    }
                    cell.Controls.Add(btn1);

                    //دکمه تایید
                    HtmlButton btn2 = new HtmlButton();
                    btn2.Attributes["onclick"] = "ToggleCommentApproved(" + item.Id + " , false)";
                    if (!item.IsApproved)
                    {
                        btn2.InnerText = "تایید نشد ";
                        btn2.Attributes["class"] = "approved btn btn-sm btn-warning";
                    }
                    else
                    {
                        btn2.InnerText = "تایید شد ";
                        btn2.Attributes["class"] = "approved btn btn-sm btn-success";
                    }
                    cell.Controls.Add(btn2);

                    //دکمه جزییات
                    HtmlButton btn3 = new HtmlButton();
                    btn3.Attributes["onclick"] = "CommentDetails(" + item.Id + ")";
                    btn3.InnerText = "جزییات";
                    btn3.Attributes["class"] = "details btn btn-sm btn-info";
                    cell.Controls.Add(btn3);


                    //دکمه حدف
                    HtmlButton btn4 = new HtmlButton();
                    btn4.Attributes["onclick"] = "DeleteComment(" + item.Id + ")";
                    btn4.InnerText = "حذف";
                    btn4.Attributes["class"] = "delete btn btn-sm btn-danger";
                    cell.Controls.Add(btn4);

                    row.Cells.Add(cell);

                    table.Rows.Add(row);
                }
            }
            TableContainer.Controls.Add(table);
        }

    }
}