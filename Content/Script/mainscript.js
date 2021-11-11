
//نمایش پیغام خطا
var FailedAlert = function (msg) {
    swal({
        title: "خطا!",
        text: msg,
        type: "error",
        showCancelButton: false,
        confirmButtonClass: "btn btn-danger",
        confirmButtonText: "باشه",
        buttonsStyling: false
    });
}



//نمایش پیغام موفقیت آمیز
var SuccessAlert = function (msg) {
    swal({
        title: "انجام شد",
        text: msg,
        type: "success",
        showCancelButton: false,
        confirmButtonClass: "btn btn-success",
        confirmButtonText: "باشه",
        buttonsStyling: false
    });
}





//سرچ اصلی در هدر
var MainSearch = function () {
    var _Search = $(".SearchInput").val();
    window.location.href = "/ListProduct.aspx?Search=" + _Search;
}





//گرفتن تعداد آیتم های موجود در سبد خرید
GetBasketItemCount();
function GetBasketItemCount() {
    $.ajax({
        type: "POST",
        url: "/Basket.aspx/GetBasketItemCount",
        data: "",
        contentType: "application/json; charset=utf-8",
        success: function (r) {
            if (r.d != null)
                $(".BasketCount").html(r.d);
            else
                $(".BasketCount").html("");
        },
        error: function () {
            FailedAlert("خطا رخ داده است.");
        }
    });
}








    //لود کردن صفحه بندی و محصولات جستجو شده با اجکس
    function RenderProducts(Page) {
        $(".list-of-products").removeClass("show-error").html('<img class="loading" src="/Content/img/loading.gif" />');
        var PageSize = $(".page-size option:selected").val();
        var Search = $(".SearchInput").val();
        var data = {
            Page: Page,
            PageSize: PageSize,
            Search: Search
        };
        $.ajax({
            type: "POST",
            url: "/ListProduct.aspx/SearchProduct",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (r) {
                if (r.d.TotalCount == 0) {
                    $(".list-of-products").addClass("show-error").html("داده ای برای نمایش یافت نشد!");
                }
                else {
                    $(".list-of-products").empty().append(r.d.ProductHtml);
                    $(".paginate").empty().append(r.d.PageinationHtml);
                }
            },
            error: function () {
                $(".list-of-products").addClass("show-error").html("خطا رخ داده است.");
            }
        });

    }





    //تغییر تعداد ایتم های نمایش داده شده در یک صفحه
    $(document).on("change", ".page-size", function () {
        RenderProducts(1);
    });





    //کلیک روی ایتم های صفحه بندی برای رفتن به صفحه جدید 
    $(document).on("click", ".paginate a", function (e) {
        e.preventDefault();
        var CurrentPage = $(".paginate a.selected").attr("rel");
        var Page = $(this).attr("rel");
        if (Page == CurrentPage)
            return;
        RenderProducts(Page);
    });




    //با کلیک روی تصاویر کوچک در جزییات پست، بر روی تصویر بزرگ نشان داده شود.
    $(document).on("click", ".product-slider-nav > li", function () {
        var selectedImg = $(this).children("img").attr("src");
        $(".product-slider-show").children("img").attr("src", selectedImg);
    })





    //لایک یا دیسلایک محصول
    $(document).on("click", ".like-btn", function () {
        var UserId = $(".UserId").val();
        var ProductId = $(".ProductId").val();
        if (!UserId)
            FailedAlert("برای لایک کردن محصولات ابتدا لاگین کنید.");
        var data = {
            UserId: UserId,
            ProductId: ProductId
        };
        $.ajax({
            type: "POST",
            url: "/ShowProduct.aspx/ToggleLike",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (r) {
                if (r.d.Status) {
                    if (r.d.IsLiked) 
                        $(".like-btn").addClass("like");
                    else 
                        $(".like-btn").removeClass("like");
                    $(".like-count").html(r.d.LikeCount);
                }
                else
                    FailedAlert(r.d.Message);
            },
            error: function () {
                FailedAlert("خطا رخ داده است.");
            }
        });

    })





    //ارسال کامنت
    var SendComment = function () {
        if (!ValidateCommentForm())
            return;

        var UserId = $(".UserId").val();
        var ProductId = $(".ProductId").val();
        var FullName = $(".FullName").val();
        var Email = $(".Email").val();
        var Text = $(".Text").val();
        var Code = $(".Code").val();
    
        var data = {
            UserId: UserId,
            ProductId: ProductId,
            FullName: FullName,
            Email: Email,
            Text: Text,
            Code: Code
        };
        $.ajax({
            type: "POST",
            url: "/ShowProduct.aspx/InsertComment",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (r) {
                if (r.d.Status) {
                    $(".FullName").val("");
                    $(".Email").val("");
                    $(".Text").val("");
                    $(".Code").val("");
                    SuccessAlert("نظر شما با موفقیت درج گردید و پس از تایید ادمین در سایت نمایش داده خواهد شد.");
                }
                else
                    FailedAlert(r.d.Message);
            },
            error: function () {
                FailedAlert("خطا رخ داده است.");
            }
        });
    
    }





    //ولیدیت کردن فرم کامنت
    function ValidateCommentForm() {
        $(".comment-error").addClass("hide");

        var UserId = $(".UserId").val();
        var ProductId = $(".ProductId").val();
        var FullName = $(".FullName").val();
        var Email = $(".Email").val();
        var Text = $(".Text").val();
        var Code = $(".Code").val();

        var IsValid = true;
        var ErrorText = "";

        if (!ProductId) {
            ErrorText += "محصول یافت نشد. <br />";
            IsValid = false;
        }

        if (!UserId && !FullName) {
            ErrorText += "نام کامل خود را وارد کنید. <br />";
            IsValid = false;
        }
        if (!UserId) {
            if (!Email) {
                ErrorText += "ایمیل خود را وارد کنید. <br />";
                IsValid = false;
            }
            else {
                var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                if (!regex.test(Email)) {
                    ErrorText += "فرمت وارد شده برای ایمیل صحیح نیست. <br />";
                    IsValid = false;
                }
            }
        }


        if (!Text) {
            ErrorText += "متن کامنت را وارد کنید. <br />";
            IsValid = false;
        }
        if (!Code) {
            ErrorText += "کد امنیتی را وارد کنید. <br />";
            IsValid = false;
        }

        if (!IsValid) 
            $(".comment-error").html(ErrorText).removeClass("hide");

        return IsValid;
    }





//افزودن به سبد خرید
    var AddToBasket = function (ProductId) {
        var data = {
            ProductId: ProductId
        };
        $.ajax({
            type: "POST",
            url: "/Basket.aspx/AddToBasket",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (r) {
                if (r.d.Status) {
                    $(".BasketCount").html(r.d.Count);
                    SuccessAlert("محصول به سبد خرید اضافه شد.");
                }
                else
                    FailedAlert(r.d.Message);
            },
            error: function () {
                FailedAlert("خطا رخ داده است.");
            }
        });

    }


