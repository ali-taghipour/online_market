using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD_ExtentionMethods;

namespace TAD
{
    public class GenerateView
    {

        /// <summary>
        /// ایجاد هدر در جداول
        /// Thead
        /// </summary>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public TableRow GenerateTableHeader(List<string> Headers)
        {
            TableRow tableRow = new TableRow();
            tableRow.TableSection = TableRowSection.TableHeader;
            foreach (var item in Headers)
            {
                TableCell tableCell = new TableHeaderCell();
                tableCell.Text = item;
                if(Headers.IndexOf(item) == Headers.Count() - 1)
                    tableCell.Attributes["class"] = "disabled-sorting";
                tableRow.Cells.Add(tableCell);
            }
            return tableRow;
        }





        /// <summary>
        /// ایجاد دکمه های ویرایش، جزییات و حذف در انتهای هر سطر
        /// </summary>
        /// <param name="FolderName">مثلا Slides</param>
        /// <param name="id">مثلا 1</param> 
        /// <returns></returns>
        public TableCell GenerateRowControlCell(string FolderName , int id)
        {
            TableCell cell = new TableCell();
            cell.Attributes.Add("class", "row-control");

            //ویرایش
            HtmlAnchor anc1 = new HtmlAnchor();
            anc1.HRef = "~/Admin/" + FolderName + "/Edit.aspx?id=" + id;
            anc1.Attributes["rel"] = id.ToString();     
            anc1.Attributes["title"] = "ویرایش";
            anc1.Attributes.Add("class", "edit");

            HtmlImage img1 = new HtmlImage();
            img1.Src = "~/Content/Admin/images/icons/Edit.png";
            anc1.Controls.Add(img1);
            cell.Controls.Add(anc1);


            //جزییات
            HtmlAnchor anc2 = new HtmlAnchor();
            anc2.Attributes.Add("class", "details");
            anc2.Attributes["rel"] = id.ToString();
            anc2.HRef = "~/Admin/" + FolderName + "/Details.aspx?id=" + id;
            anc2.Attributes["title"] = "جزییات";

            HtmlImage img2 = new HtmlImage();
            img2.Src = "~/Content/Admin/images/icons/info.png";
            anc2.Controls.Add(img2);
            cell.Controls.Add(anc2);


            //حذف
            HtmlAnchor anc3 = new HtmlAnchor();
            anc3.Attributes.Add("class", "delete");
            anc3.Attributes["rel"] = id.ToString();
            anc3.HRef = "~/Admin/" + FolderName + "/Delete.aspx?id=" + id;
            anc3.Attributes["title"] = "حذف";

            HtmlImage img3 = new HtmlImage();
            img3.Src = "~/Content/Admin/images/icons/delete.png";
            anc3.Controls.Add(img3);
            cell.Controls.Add(anc3);

            return cell;
        }





        /// <summary>
        /// ساختن یک ایتم محصول در صفحه لیست محصولات
        /// </summary>
        public HtmlGenericControl ProductItem(Product Product)
        {
            HtmlGenericControl MainDiv = new HtmlGenericControl("div");
            MainDiv.Attributes["class"] = "col-md-3 col-sm-4 product-item";
            MainDiv.Attributes["rel"] = Product.Id.ToString();

            //تصویر و توضیحات محصول
            HtmlAnchor anc = new HtmlAnchor();
            anc.HRef = "~/ShowProduct.aspx?id=" + Product.Id;

            HtmlGenericControl figure = new HtmlGenericControl("figure");
            HtmlImage img = new HtmlImage();
            img.Src = Product.MainPic ?? "~/Content/img/image_placeholder.jpg";
            img.Alt = Product.Title;
            figure.Controls.Add(img);
            anc.Controls.Add(figure);

            HtmlGenericControl h2 = new HtmlGenericControl("h2");
            h2.InnerHtml = Product.Title;
            h2.Attributes["title"] = Product.Title;
            anc.Controls.Add(h2);

            HtmlGenericControl p = new HtmlGenericControl("p");
            p.InnerHtml = Product.Summary.GetSummary(80);
            anc.Controls.Add(p);

            MainDiv.Controls.Add(anc);

            //بخش هزینه محصول
            HtmlGenericControl PriceDiv = new HtmlGenericControl("div");
            PriceDiv.Attributes["class"] = "price-holder";

            if (Product.OffPrice != null)
            {
                HtmlGenericControl span1 = new HtmlGenericControl("span");
                span1.Attributes["class"] = "item-off-price";
                span1.InnerHtml = Product.MainPrice.GetToomanPriceFormat();
                PriceDiv.Controls.Add(span1);
            } 
            HtmlGenericControl span2 = new HtmlGenericControl("span");
            span2.Attributes["class"] = "item-price";
            span2.InnerHtml = Product.FinalPrice.GetToomanPriceFormat();
            
            HtmlGenericControl span3 = new HtmlGenericControl("span");
            span3.InnerHtml = " تومان";
            span2.Controls.Add(span3);

            PriceDiv.Controls.Add(span2);

            MainDiv.Controls.Add(PriceDiv);

            //سبد خرید
            if (Product.Inventory > 0)
            {
                HtmlGenericControl BasketDiv = new HtmlGenericControl("div");
                BasketDiv.Attributes["class"] = "add-basket";
                BasketDiv.Attributes["onclick"] = "AddToBasket(" + Product.Id + ")";

                HtmlImage img2 = new HtmlImage();
                img2.Src = "~/Content/img/icons8-buy-48.png";
                BasketDiv.Controls.Add(img2);

                MainDiv.Controls.Add(BasketDiv);
            }
            else
            {
                HtmlGenericControl InventoryDiv = new HtmlGenericControl("div");
                InventoryDiv.Attributes["class"] = "inventory";
                InventoryDiv.InnerHtml = "ناموجود";
                MainDiv.Controls.Add(InventoryDiv);
            }

            return MainDiv;
        }


        



        /// <summary>
        /// ساخت پیجینیشن
        /// </summary>
        /// <param name="PageCount">تعداد صفحات</param>
        /// <param name="CurrentPage">صفحه درخواستی</param>
        public List<HtmlContainerControl> GetPaginationItems(int PageCount, int CurrentPage)
        {
            List<HtmlContainerControl> PageItems = new List<HtmlContainerControl>();
            int PrevPage = CurrentPage - 1;
            int NextPage = CurrentPage + 1;
            if (PrevPage <= 0)
                PrevPage = PageCount;
            if (NextPage > PageCount)
                NextPage = 1;

            HtmlAnchor anc = new HtmlAnchor();
            anc.Attributes.Add("rel", PrevPage.ToString());
            anc.InnerHtml = "قبلی";
            PageItems.Add(anc);

            for (int i = 1; i <= PageCount; i++)
            {
                anc = new HtmlAnchor();
                if (i == CurrentPage)
                    anc.Attributes.Add("class", "selected");
                anc.Attributes.Add("rel", i.ToString());
                anc.InnerHtml = i.ToString();
                PageItems.Add(anc);

            }

            anc = new HtmlAnchor();
            anc.Attributes.Add("rel", NextPage.ToString());
            anc.InnerHtml = "بعدی";
            PageItems.Add(anc);

            return PageItems;
        }





        /// <summary>
        ///  گرفتن ایتم های ساخته شده در سی شارپ در قالب یک استرینگ برای اجکس
        /// </summary>
        public string GetRawHtml(HtmlContainerControl item)
        {
            StringBuilder generatedHtml = new StringBuilder();
            using (var htmlStringWriter = new StringWriter(generatedHtml))
            {
                using (var htmlTextWriter = new HtmlTextWriter(htmlStringWriter))
                {
                    item.RenderControl(htmlTextWriter);
                    return generatedHtml.ToString();
                }
            }
        }



    }
}