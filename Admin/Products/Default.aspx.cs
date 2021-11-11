using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Products
{
    public partial class Default : System.Web.UI.Page
    {
        private ProductManager ProductManager;

        public Default()
        {
            ProductManager = new ProductManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var Products = ProductManager.GetAll();
            GenerateTable(Products);
        }



        /// <summary>
        /// ساخت جدول 
        /// </summary>
        /// <param name="Products">لیستی از محصولات</param>
        public void GenerateTable(List<Product> Products)
        {
            GenerateView GenerateView = new GenerateView();

            Table table = new Table();
            table.Attributes.Add("class", "table datatable");

            //ایجاد هدر برای جدول - THead
            List<string> Headers = new List<string>() { "آیدی", "تصویر", "عنوان", "قیمت (تومان)", "دسته بندی", "موجودی", "وضعیت", "" };
            table.Rows.Add(GenerateView.GenerateTableHeader(Headers));


            //ایجاد بدنه جدول - tbody
            TableRow row;
            TableCell cell;

            if (Products != null)
            {
                foreach (var item in Products)
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
                    img.Src = item.MainPic;
                    cell.Controls.Add(img);
                    row.Cells.Add(cell);

                    //ستون سوم - عنوان 
                    cell = new TableCell();
                    cell.Text = item.Title;
                    row.Cells.Add(cell);

                    //ستون چهارم - قیمت 
                    cell = new TableCell();
                    cell.Text = item.FinalPrice.GetToomanPriceFormat();
                    row.Cells.Add(cell);

                    //ستون پنجم - دسته بندی
                    cell = new TableCell();
                    cell.Text = item.CategoryTitle;
                    row.Cells.Add(cell);

                    //ستون ششم - موجودی
                    cell = new TableCell();
                    cell.Text = item.Inventory.ToString();
                    row.Cells.Add(cell);

                    //ستون هفتم - وضعیت
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
                    var ControlCell = GenerateView.GenerateRowControlCell("Products", item.Id);
                    row.Cells.Add(ControlCell);

                    table.Rows.Add(row);
                }
            }
            TableContainer.Controls.Add(table);
        }







        /// <summary>
        /// حذف محصول
        /// </summary>
        /// <param name="PicId">آیدی محصول</param>
        /// <returns></returns>
        [WebMethod]
        public static bool DeleteProduct(int? ProductId)
        {
            if (ProductId == null)
                return false;
            ProductManager ProductManager = new ProductManager();
            bool IsSuccess = ProductManager.Delete((int)ProductId);
            return IsSuccess;
        }


    }
}