using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;
using TAD_ExtentionMethods;

namespace KargahProject.Admin.Slides
{
    public partial class Default : System.Web.UI.Page
    {
        private SlideManager SlideManager;

        public Default()
        {
            SlideManager = new SlideManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //تعیین صفحه درخواستی، متن جستجو و اندازه صفحه
            int _Page = 1;
            int _PageSize = 10;
            string _Search = Request.QueryString["Search"];
            if (Request.QueryString["Page"] != null)
                int.TryParse(Request.QueryString["Page"], out _Page);
            if (Request.QueryString["PageSize"] != null)
                int.TryParse(Request.QueryString["PageSize"], out _PageSize);

            //قرار دادن مقادیر جستجو شده در صفحه 
            SearchInput.Value = _Search;
            var SelectedOption = PageSize.Items.FindByValue(_PageSize.ToString());
            if(SelectedOption == null)
            {
                SelectedOption = PageSize.Items[0];
                _PageSize = int.Parse(SelectedOption.Value);
            }
            SelectedOption.Selected = true;

            //گرفتن آیتمهای مورد نیاز با توجه به پارامتر های جستجو
            var model = SlideManager.GetSearchedItem(_Page, _PageSize, _Search);
            var Slides = model.Items;
            if (Slides.Count() == 0)
            {
                TableContainer.Attributes["class"] = "text-center text-danger";
                TableContainer.InnerHtml = "داده ای برای نمایش یافت نشد!";
                return;
            }
            GenerateTable(Slides);
            GeneratePagination(model.PageCount, model.CurrentPage);
        }



        /// <summary>
        /// ساخت جدول 
        /// </summary>
        /// <param name="Slides">لیستی از اسلاید ها</param>
        public void GenerateTable(List<Slide> Slides)
        {
            GenerateView GenerateView = new GenerateView();

            Table table = new Table();
            table.Attributes.Add("class", "table");

            //ایجاد هدر برای جدول - THead
            List<string> Headers = new List<string>() { "تصویر", "عنوان", "وضعیت", "تاریخ شروع نمایش", "تاریخ پایان نمایش", "ترتیب", ""};
            table.Rows.Add(GenerateView.GenerateTableHeader(Headers));


            //ایجاد بدنه جدول - tbody
            TableRow row;
            TableCell cell;

            foreach (var item in Slides)
            {
                row = new TableRow();
                row.TableSection = TableRowSection.TableBody;

                //ستون اول - تصویر
                cell = new TableCell();
                HtmlImage img = new HtmlImage();
                img.Src = item.ThumbPath;
                cell.Controls.Add(img);
                row.Cells.Add(cell);

                //ستون دوم - عنوان اسلاید
                cell = new TableCell();
                cell.Text = item.Title;
                row.Cells.Add(cell);

                //ستون سوم - وضعیت
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

                //ستون چهارم - تاریخ شروع نمایش
                cell = new TableCell();
                cell.Text = item.PersianStartDate;
                row.Cells.Add(cell);

                //ستون پنجم - تاریخ پایان نمایش
                cell = new TableCell();
                cell.Text = item.PersianEndDate;
                row.Cells.Add(cell);

                //ستون ششم - ترتیب
                cell = new TableCell();
                cell.Text = item.Order.ToString();
                row.Cells.Add(cell);
                
                //ستون آخر
                var ControlCell = GenerateView.GenerateRowControlCell("Slides", item.Id);
                row.Cells.Add(ControlCell);


                table.Rows.Add(row);
            }
            TableContainer.Controls.Add(table);
        }
        
        




        /// <summary>
        /// ساخت پیجینیشن
        /// </summary>
        /// <param name="PageCount">تعداد صفحات</param>
        /// <param name="CurrentPage">صفحه درخواستی</param>
        public void GeneratePagination(int PageCount , int CurrentPage)
        {
            int PrevPage = CurrentPage - 1;
            int NextPage = CurrentPage + 1;
            if (PrevPage <= 0)
                PrevPage = PageCount;
            if (NextPage > PageCount)
                NextPage = 1;

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "prev-arrow page-item");
            HtmlAnchor anc = new HtmlAnchor();
            anc.Attributes.Add("rel", PrevPage.ToString());
            anc.InnerHtml = "<";
            li.Controls.Add(anc);
            Pagination.Controls.Add(li);

            for (int i = 1; i <= PageCount; i++)
            {
                li = new HtmlGenericControl("li");
                if (i == CurrentPage)
                    li.Attributes.Add("class", "active page-item");
                else
                    li.Attributes.Add("class", "page-item");
                anc = new HtmlAnchor();
                anc.Attributes.Add("rel", i.ToString());
                anc.InnerHtml = i.ToString();
                li.Controls.Add(anc);
                Pagination.Controls.Add(li);

            }


            li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "next-arrow page-item");
            anc = new HtmlAnchor();
            anc.Attributes.Add("rel", NextPage.ToString());
            anc.InnerHtml = ">";
            li.Controls.Add(anc);
            Pagination.Controls.Add(li);
        }


    }
}