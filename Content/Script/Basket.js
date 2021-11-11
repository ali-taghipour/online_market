
//محاسبه قیمت کل و قیمت نهایی هر ایتم با توجه به تعداد محصول انتخاب شده
SetAllPrice();
function SetAllPrice() {
    var itemAllPrice = 0;
    $("td.single-price").each(function () {
        var itemPrice = $(this).attr("rel");
        var itemCount = $(this).parents("tr").find("input").val();
        itemPrice = parseInt(itemPrice) * itemCount;
        $(this).parents("tr").find(".total-price").html(PriceFormat(itemPrice));
        itemAllPrice += itemPrice;
    });
    $(".basket-pricing > span:nth-child(2)").html(PriceFormat(itemAllPrice));
}




//حذف آیتم
function DeleteBasketProduct(el , BasketProductId) {
    var $this = $(el)
    swal({
        title: "حذف محصول از سبد خرید",
        text: "آیا از حذف این مورد اطمینان دارید؟",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn btn-danger",
        cancelButtonClass: "btn btn-defult",
        confirmButtonText: "بله، حذف شود",
        cancelButtonText: "لغو"
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                url: "/Basket.aspx/DeleteBasketProduct",
                data: '{"BasketProductId":' + BasketProductId + '}',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d.Status) {
                        $this.closest("tr").remove();
                        var BasketCount = $(".BasketCount").html();
                        $(".BasketCount").html(BasketCount - 1);
                        //اگر سبد خرید خالی شد
                        if (BasketCount - 1 == 0) {
                            $("footer.cart-info").css({ "display": "none" });
                            $(".basket-pricing").css({ "display": "none" });
                            $("#CPH_TableContainer").addClass("show-error").html("موردی یافت نشد!");
                        }
                        SetAllPrice();
                    }
                    else
                        FailedAlert(r.d.Message);
                },
                error: function () {
                    FailedAlert("حذف محصول دچار خطا شده است.");
                }
            });
        }
    });

}





//تغییر تعداد محصول در سبد خرید
$(document).on("change", ".cart-table input", function () {
    SetAllPrice();
});




//گرفتن فرمت قیمت و قرار دادن ویرگول بین اعداد
function PriceFormat(n) {
    n = n + "";
    if (n !== '') {
        var num = n.replace(/[^\d]/g, '');
        if (num.length > 3)
            num = num.replace(/\B(?=(?:\d{3})+(?!\d))/g, ',');
        return num;
    }
}





//function StartPayment(BasketId) {
//    $.ajax({
//        type: "POST",
//        url: "/Basket.aspx/StartPayment",
//        data: '{"BasketId":' + BasketId + '}',
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            if (data.d) {
                
//                SetAllPrice();
//            }
//            else
//                FailedAlert("حذف محصول با خطا همراه بوده است.");
//        },
//        error: function () {
//            FailedAlert(" خطا رخ داده است.");
//        }
//    });
//}














