using Entities;
using KargahProject.Models.BLL;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Payments
{
    public partial class Default : System.Web.UI.Page
    {
        private PaymentManager PaymentManager;

        public Default()
        {
            PaymentManager = new PaymentManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var Payments = PaymentManager.GetAll();
            GenerateTable(Payments);
        }



        /// <summary>
        /// ساخت جدول 
        /// </summary>
        /// <param name="Payments">لیستی از اسلاید ها</param>
        public void GenerateTable(List<Payment> Payments)
        {
            GenerateView GenerateView = new GenerateView();

            Table table = new Table();
            table.Attributes.Add("class", "table datatable");

            //ایجاد هدر برای جدول - THead
            List<string> Headers = new List<string>() { "آیدی", "کاربر", "مبلغ(تومان)", "کلید پرداخت", "توضیحات", "وضعیت", "تاریخ" };
            table.Rows.Add(GenerateView.GenerateTableHeader(Headers));


            //ایجاد بدنه جدول - tbody
            TableRow row;
            TableCell cell;

            foreach (var item in Payments)
            {
                row = new TableRow();
                row.TableSection = TableRowSection.TableBody;

                //ستون اول - آیدی
                cell = new TableCell();
                cell.Text = item.Id.ToString();
                row.Cells.Add(cell);

                //ستون دوم - کاربر
                cell = new TableCell();
                if (item.UserId != null) {
                    HtmlAnchor Anc = new HtmlAnchor();
                    Anc.InnerHtml = item.UserFullName;
                    Anc.HRef = "~/Admin/Users/Details.aspx?id=" + item.UserId;
                    Anc.Attributes["target"] = "_blank";
                    cell.Controls.Add(Anc);
                }
                else
                    cell.Text = "---";
                row.Cells.Add(cell);

                //ستون سوم - مبلغ 
                cell = new TableCell();
                cell.Text = item.Amount.GetToomanPriceFormat();
                row.Cells.Add(cell);

                //ستون چهارم - کلید پرداخت 
                cell = new TableCell();
                cell.Text = item.PaymentKey.ToString();
                row.Cells.Add(cell);

                //ستون پنجم - توضیحات
                cell = new TableCell();
                cell.Text = item.Description;
                row.Cells.Add(cell);


                //ستون ششم - وضعیت
                cell = new TableCell();
                if (item.IsSuccess)
                {
                    cell.Text = "موفق";
                    cell.Attributes.Add("class", "text-success");
                }
                else
                {
                    cell.Text = "نا موفق";
                    cell.Attributes.Add("class", "text-danger");
                }
                row.Cells.Add(cell);

                //ستون پنجم - زمان
                cell = new TableCell();
                cell.Text = item.CreateDate.ToPersianDateTime().ToString();
                row.Cells.Add(cell);

                table.Rows.Add(row);
            }
            TableContainer.Controls.Add(table);
        }


    }
}