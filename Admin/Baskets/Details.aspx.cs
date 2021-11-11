using Entities;
using KargahProject.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Baskets
{
    public partial class Details : System.Web.UI.Page
    {
        private int? BasketId;

        protected void Page_Load(object sender, EventArgs e)
        {
            BasketId = Request.QueryString["id"].GetInt();
            if (BasketId == null)
            {
                Response.Redirect("~/admin/Baskets");
                return;
            }
            BasketProductManager BPManager = new BasketProductManager();
            var Items = BPManager.GetBasketProducts(BasketId);
            if (Items.Count() == 0)
            {
                TableContainer.Attributes["class"] = "text-center text-danger";
                TableContainer.InnerHtml = "داده ای برای نمایش یافت نشد!";
                return;
            }
            GenerateTable(Items);
        }



        /// <summary>
        /// ساخت جدول 
        /// </summary>
        /// <param name="Baskets">لیستی از اسلاید ها</param>
        public void GenerateTable(List<BasketProduct> Items)
        {
            BasketManager BasketManager = new BasketManager();
            var Basket = BasketManager.GetById(BasketId);

            GenerateView GenerateView = new GenerateView();

            Table table = new Table();
            table.Attributes.Add("class", "table");

            //ایجاد هدر برای جدول - THead
            List<string> Headers = new List<string>() { "تصویر", "عنوان", "قیمت(تومان)", "تعداد" };
            table.Rows.Add(GenerateView.GenerateTableHeader(Headers));


            //ایجاد بدنه جدول - tbody
            TableRow row;
            TableCell cell;

            foreach (var item in Items)
            {
                row = new TableRow();
                row.TableSection = TableRowSection.TableBody;

                //ستون اول - تصویر
                cell = new TableCell();
                HtmlImage img = new HtmlImage();
                img.Src = item.Product.MainPic;
                cell.Controls.Add(img);
                row.Cells.Add(cell);

                //ستون دوم - عنوان محصول
                cell = new TableCell();
                HtmlAnchor anc = new HtmlAnchor();
                anc.InnerHtml = item.Product.Title;
                anc.HRef = "~/Admin/Products/Details.aspx?id=" + item.ProductId;
                anc.Attributes["target"] = "_blank";
                cell.Controls.Add(anc);
                row.Cells.Add(cell);

                //ستون سوم - قیمت
                cell = new TableCell();
                if(Basket.Status == Enums.BasketStatus.Open)
                    cell.Text = item.Product.MainPrice.GetToomanPriceFormat();
                else
                    cell.Text = item.Price.GetToomanPriceFormat();
                row.Cells.Add(cell);

                //ستون چهارم - تعداد
                cell = new TableCell();
                cell.Text = item.Count.ToString();
                row.Cells.Add(cell);
                               

                table.Rows.Add(row);
            }
            TableContainer.Controls.Add(table);
        }

    }
}