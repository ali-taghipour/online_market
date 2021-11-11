using Entities;
using KargahProject.Models.BLL;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Baskets
{
    public partial class Default : System.Web.UI.Page
    {
        private BasketManager BasketManager;

        public Default()
        {
            BasketManager = new BasketManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var Baskets = BasketManager.GetAll();
            GenerateTable(Baskets);
        }



        /// <summary>
        /// ساخت جدول 
        /// </summary>
        /// <param name="Baskets">لیستی از اسلاید ها</param>
        public void GenerateTable(List<Basket> Baskets)
        {
            GenerateView GenerateView = new GenerateView();

            Table table = new Table();
            table.Attributes.Add("class", "table datatable");

            //ایجاد هدر برای جدول - THead
            List<string> Headers = new List<string>() { "آیدی", "کاربر", "مبلغ(تومان)", "وضعیت", "آخرین بروزرسانی", ""};
            table.Rows.Add(GenerateView.GenerateTableHeader(Headers));


            //ایجاد بدنه جدول - tbody
            TableRow row;
            TableCell cell;

            foreach (var item in Baskets)
            {
                row = new TableRow();
                row.TableSection = TableRowSection.TableBody;

                //ستون اول - آیدی
                cell = new TableCell();
                cell.Text = item.Id.ToString();
                row.Cells.Add(cell);

                //ستون دوم - کاربر
                cell = new TableCell();
                if (item.UserId != null)
                {
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
                cell.Text = item.TotalPrice.GetToomanPriceFormat();
                row.Cells.Add(cell);

                //ستون چهارم - وضعیت 
                cell = new TableCell();
                cell.Text = item.Status.GetEnumDescription();
                row.Cells.Add(cell);

                //ستون پنجم - آخرین بروزرسانی
                cell = new TableCell();
                cell.Text = item.CreateDate.ToPersianDateTime().ToString();
                row.Cells.Add(cell);
                
                //دکمه جزییات
                cell = new TableCell();
                HtmlAnchor anc2 = new HtmlAnchor();
                anc2.Attributes.Add("class", "details");
                anc2.Attributes["rel"] = item.Id.ToString();
                anc2.HRef = "~/Admin/Baskets/Details.aspx?id=" + item.Id;
                anc2.Attributes["title"] = "جزییات";

                HtmlImage img2 = new HtmlImage();
                img2.Src = "~/Content/Admin/images/icons/info.png";
                anc2.Controls.Add(img2);
                cell.Controls.Add(anc2);
                row.Cells.Add(cell);
                

                table.Rows.Add(row);
            }
            TableContainer.Controls.Add(table);
        }


    }

}