using BLL;
using Entities;
using KargahProject.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TAD;
using TAD_ExtentionMethods;

namespace Shop
{
    public partial class Basket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
                BasketManager BasketManager = new BasketManager();
                int? _BasketId = BasketManager.GetOpenBasketId();

                BasketProductManager BPManager = new BasketProductManager();
                var Items = BPManager.GetBasketProducts(_BasketId);

                if (Items == null || Items.Count() == 0)
                {
                    TableContainer.Attributes["class"] = "show-error";
                    TableContainer.InnerHtml = "داده ای برای نمایش یافت نشد!";
                    PaymentSection.Style["display"] = "none";
                    TotalPriceSection.Style["display"] = "none";
                    return;
                }
                GenerateTable(Items);
                BasketId.Value = _BasketId.ToString();
            
        }





        /// <summary>
        /// ایجاد جدول محصولات سبد خرید
        /// </summary>
        public void GenerateTable(List<BasketProduct> Items)
        {
            GenerateView GenerateView = new GenerateView();

            Table table = new Table();
            table.Attributes.Add("class", "cart-table");

            //ایجاد هدر برای جدول - THead
            List<string> Headers = new List<string>() { "کالا", "تعداد", "قیمت واحد (تومان)", "قیمت کل (تومان)", "حذف"};
            table.Rows.Add(GenerateView.GenerateTableHeader(Headers));


            //ایجاد بدنه جدول - tbody
            TableRow row;
            TableCell cell;

            foreach (var item in Items)
            {
                row = new TableRow();
                row.TableSection = TableRowSection.TableBody;
                row.Attributes["rel"] = item.Id.ToString();

                //ستون اول - تصویر و نام کالا
                cell = new TableCell();
                HtmlAnchor anc = new HtmlAnchor();
                if(item.ProductId != null)
                    anc.HRef = "~/ShowProduct.aspx?id=" + item.ProductId;

                HtmlImage img = new HtmlImage();
                img.Src = item.Product?.MainPic;

                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerHtml = item.Product?.Title;

                anc.Controls.Add(img);
                anc.Controls.Add(span);
                cell.Controls.Add(anc);
                row.Cells.Add(cell);

                //ستون دوم - تعداد
                cell = new TableCell();
                TextBox inp = new TextBox();
                inp.ID = "item" + item.Id;
                inp.Attributes.Add("name", "item" + item.Id);
                inp.Attributes.Add("runat", "Server");
                inp.Attributes.Add("type", "number");
                inp.Attributes["min"] = "1";
                inp.Text = item.Count.ToString();
                cell.Controls.Add(inp);
                row.Cells.Add(cell);

                //ستون سوم - قیمت واحد
                cell = new TableCell();
                cell.Text = item.Product?.FinalPrice.GetToomanPriceFormat();
                cell.Attributes["class"] = "single-price";
                cell.Attributes["rel"] = (item.Product?.FinalPrice / 10).ToString();
                row.Cells.Add(cell);

                //ستون چهارم - قیمت کل
                cell = new TableCell();
                cell.Text = (item.Product?.FinalPrice * item.Count).GetToomanPriceFormat();
                cell.Attributes["class"] = "total-price";
                cell.Attributes["rel"] = ((item.Product?.FinalPrice * item.Count) / 10).ToString();
                row.Cells.Add(cell);

                //ستون پنجم - حذف
                cell = new TableCell();
                HtmlGenericControl span2 = new HtmlGenericControl("span");
                span2.Attributes.Add("class", "remove");
                span2.Attributes["onclick"] = "DeleteBasketProduct(this , " + item.Id + ")";
                HtmlImage img2 = new HtmlImage();
                img.Src = "~/Content/img/close.png";
                span2.Controls.Add(img);
                cell.Controls.Add(span2);
                row.Cells.Add(cell);                

                table.Rows.Add(row);
            }
            TableContainer.Controls.Add(table);
        }






        /// <summary>
        /// آغاز پرداخت
        /// </summary>
        protected void StartPayment_ServerClick(object sender, EventArgs e)
        {
            int? UserId;
            if (Request.Cookies["User"] == null)
            {
                PaymentError.InnerHtml = "ابتدا وارد حساب کاربری خود شوید!";
                PaymentError.Attributes["class"] = "show-error";
                return;
            }
            UserId = int.Parse(Request.Cookies["User"]["Id"]);

            //پیدا کردن سبد
            int _BasketId = int.Parse(BasketId.Value); 
            BasketManager BasketManager = new BasketManager();
            var Basket = BasketManager.GetById(_BasketId);

            //گرفتن محصولات سبد
            BasketProductManager BPManager = new BasketProductManager();
            var Items = BPManager.GetBasketProducts(_BasketId);

            //نهایی کردن تعداد و قیمت محصولات
            int TotalPrice = 0;
            bool RequestedCountIsValid = true;
            string CountError = "";
            foreach (var item in Items)
            {
                TextBox inp = BasketForm.FindControl("item" + item.Id) as TextBox;
                if(inp != null && inp.Text != null)
                    item.Count = int.Parse(inp.Text);
                else
                {
                    RequestedCountIsValid = false;
                    CountError += "تعداد محصول \"" + item.Product.Title + "\" را مشخص کنید. (موجودی : " + item.Product.Inventory + " عدد) <br/>";
                }
                item.Price = item.Product?.FinalPrice * item.Count;
                TotalPrice += (int)item.Price;

                if (item.Count > item.Product.Inventory)
                {
                    RequestedCountIsValid = false;
                    CountError += "تعداد درخواست شده برای محصول \"" + item.Product.Title + "\" موجود نمی باشد. (موجودی : " + item.Product.Inventory + " عدد) <br/>";
                }
                else
                    BPManager.Update(item);
            }

            //اگر موجودی کالا به تعداد کافی نبود
            if (!RequestedCountIsValid)
            {
                PaymentError.InnerHtml = CountError;
                PaymentError.Attributes["class"] = "show-error";
                return;
            }


            //اپدیت قیمت نهایی سبد خرید
            Basket.TotalPrice = TotalPrice;
            BasketManager.Update(Basket);


            System.Net.ServicePointManager.Expect100Continue = false;
            KargahProject.Sandbox.Zarinpal.PaymentGatewayImplementationServicePortTypeClient zp =
                new KargahProject.Sandbox.Zarinpal.PaymentGatewayImplementationServicePortTypeClient();
            string Authority;
            string Description = "پرداخت سبد خرید با آیدی " + Basket.Id;
            int Amount = TotalPrice / 10;

            // create payment to local database
            var paymentManager = new PaymentManager();
            var PaymentId = paymentManager.Create(TotalPrice, (int)UserId , (int)_BasketId, Description);

            string RetUrl = "Http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/VerifyPayment.aspx?PaymentId=" + PaymentId;

            //int Status = zp.PaymentRequest("7ce69436-482a-11e8-9c9a-024056a271be", Amount, Description, "you@yoursite.com", "09123456789", RetUrl, out Authority);
            int Status = zp.PaymentRequest("YOUR-ZARINPAL-MERCHANT-CODE", Amount, Description, "you@yoursite.com", "09123456789", RetUrl, out Authority);

            if (Status == 100)
            {
                //Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
                Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
            }
            else
            {
                PaymentError.InnerHtml = "اتصال به درگاه دچار خطا شده است.";
                PaymentError.Attributes["class"] = "show-error";
                return;
            }

        }










        /// <summary>
        /// افزودن محصول به سبد خرید
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object AddToBasket(int? ProductId)
        {
            if (ProductId == null)
                return new { Status = false, Message = "محصول را انتخاب کنید." };
            BasketManager BasketManager = new BasketManager();

            //گرفتن سبد باز فعلی
            int? BasketId = BasketManager.GetOpenBasketId();

            //اگر سبد باز وجود نداشت یک سبد ایجاد کن
            if (BasketId == null)
            {
                BasketId = BasketManager.Create();
                if (BasketId == null)
                    return new { Status = false, Message = "ساخت سبد خرید دچار خطا شده است." };

                HttpCookie BasketCookie = new HttpCookie("BasketId");
                BasketCookie.Value = BasketId.ToString();
                BasketCookie.Expires = DateTime.Now.AddMonths(3);
                HttpContext.Current.Response.Cookies.Add(BasketCookie);
            }

            BasketProductManager BPManager = new BasketProductManager();
            //ایا محصول از قبل در سبد وجود دارد؟
            if (BPManager.ProductIsExist((int)BasketId, (int)ProductId))
                return new { Status = false, Message = "محصول در سبد خرید وجود دارد." };
            else
            {
                int? BasketItemCount = BPManager.AddToBasket((int)BasketId, (int)ProductId);
                if(BasketItemCount == null)
                    return new { Status = false, Message = "خطا رخ داده است." };
                else
                    return new { Status = true, Count = BasketItemCount };


            }
        }







        /// <summary>
        /// حذف محصول از سبد خرید
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object DeleteBasketProduct(int? BasketProductId)
        {
            if (BasketProductId == null)
                return new { Status = false, Message = "آیتم مورد نظر را انتخاب کنید." };
            
            BasketProductManager BPManager = new BasketProductManager();
            var IsSuccess = BPManager.DeleteFromBasket((int)BasketProductId);
            if (!IsSuccess)
                return new { Status = false, Message = "خطا رخ داده است." };
            else
                return new { Status = true, Message = "" };
        }







        /// <summary>
        /// گرفتن تعداد ایتم های موجود در سبد خرید
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int? GetBasketItemCount()
        {
            BasketManager BasketManager = new BasketManager();
            int? BasketId = BasketManager.GetOpenBasketId();
            BasketProductManager BPManager = new BasketProductManager();
            return BPManager.GetBasketItemCount(BasketId);
        }

       
    }
}