using BLL;
using KargahProject.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KargahProject
{
    public partial class VerifyPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int PaymentId;
            //اگر تمامی مقادیر بازگشتی از درگاه صحیح بود
            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null 
                && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null
                && Request.QueryString["PaymentId"] != null && int.TryParse(Request.QueryString["PaymentId"], out PaymentId))
            {
                //اگر استاتوس اوکی بود
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {
                    var paymentManager = new PaymentManager();
                    var payment = paymentManager.GetById(PaymentId);
                    int Amount = (int)payment.Amount / 10;
                    long RefID;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    Sandbox.Zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Sandbox.Zarinpal.PaymentGatewayImplementationServicePortTypeClient();

                    //چک میکند که ایا پرداخت فعلی با مشخصات امده از درگاه مطابقت دارد؟
                    int Status = zp.PaymentVerification("YOUR-ZARINPAL-MERCHANT-CODE", Request.QueryString["Authority"], Amount, out RefID);

                    //اگر اطلاعات مطابقت داشت
                    if (Status == 100)
                    {
                        //آپدیت کردن اطلاعات خرید
                        payment.StatusCode = Status;
                        payment.IsSuccess = true;
                        payment.CreateDate = DateTime.Now;
                        payment.PaymentKey = RefID;
                        paymentManager.Update(payment);


                        //آپدیت کردن تعداد محصولات خریداری شده
                        //گرفتن محصولات سبد
                        BasketProductManager BPManager = new BasketProductManager();
                        var Items = BPManager.GetBasketProducts(payment.BasketId);

                        //نهایی کردن تعداد و قیمت محصولات
                        ProductManager ProductManager = new ProductManager();
                        foreach (var item in Items)
                        {
                            int NewInventory = (int)item.Product.Inventory - item.Count;
                            ProductManager.UpdateInventory((int)item.ProductId , NewInventory);
                        }


                        //آپدیت کردن سبد خرید
                        BasketManager BasketManager = new BasketManager();
                        var Basket = BasketManager.GetById(payment.BasketId);
                        Basket.Status = Enums.BasketStatus.Payed;
                        BasketManager.Update(Basket);

                        //پاک کردن سبد خرید از کوکی
                        if (Request.Cookies["BasketId"] != null)
                        {
                            HttpCookie TempCoockie = new HttpCookie("BasketId");
                            TempCoockie.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(TempCoockie);
                        }


                        PaymentMessage.Attributes["class"] = "show-success";
                        PaymentMessage.InnerHtml = "پرداخت با موفقیت انجام شد. <br />"
                                                    + " کد تایید : " + RefID;

                        return;
                    }
                    else
                    {
                        PaymentMessage.Attributes["class"] = "show-error";
                        PaymentMessage.InnerHtml = "پرداخت دچار خطا شده است! <br />"
                                                    + " کد وضعیت پرداخت : " + Status;
                        return;
                    }
                }
            }

            PaymentMessage.Attributes["class"] = "show-error";
            PaymentMessage.InnerHtml = "پرداخت دچار خطا شده است!";
            
        }
    }
}