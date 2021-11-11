
//باز و بسته کردن منو آکاردیون در حالت ریسپانسیو
$(document).on("click", ".close_btn", function () {
    if ($(this).hasClass("active")) {
        $(this).removeClass("active");
        $(".right_menu").removeClass("open");
    }
    else {
        $(this).addClass("active");
        $(".right_menu").addClass("open");
    }
});




//اگر صفحه در کامپیوتر لود شد منو باز باشد
if ($(window).width() < 990) {
    $(this).removeClass("active");
    $(".right_menu").removeClass("open");
    //$("table.datatable").css({ "width": "100% !important" });
}




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




//کلیک روی ایتم های صفحه برای رفتن به صفحه جدید در گرید ویو پنل ادمین
$(document).on("click", ".pagination .page-item a", function (e) {
    e.preventDefault();
    var CurrentPage = $(".pagination .page-item.active a").attr("rel");
    var Page = $(this).attr("rel");
    if (Page == CurrentPage)
        return;
    var PageSize = $(".page-size").val();
    var SearchText = $(".search-input").val();
    var url = window.location.href.split('?')[0] + "?Page=" + Page + "&PageSize=" + PageSize;
    if (SearchText)
        url += "&Search=" + SearchText;
    window.location.href = url;
});





//کلیک روی دکمه سرچ در گرید ویو پنل ادمین
$(document).on("click", ".search-btn", function () {
    var Page = 1;
    var PageSize = $(".page-size").val();
    var SearchText = $(".search-input").val();
    var url = window.location.href.split('?')[0] + "?Page=" + Page + "&PageSize=" + PageSize;
    if (SearchText)
        url += "&Search=" + SearchText;
    window.location.href = url;
});




//تغییر تعداد ایتم های نمایش داده شده در یک صفحه
$(document).on("change", ".page-size", function () {
    var Page = 1;
    var PageSize = $(".page-size").val();
    var SearchText = $(".search-input").val();
    var url = window.location.href.split('?')[0] + "?Page=" + Page + "&PageSize=" + PageSize;
    if (SearchText)
        url += "&Search=" + SearchText;
    window.location.href = url;
});



//باز بسته شدن باکس تغییر کلمه عبور
var ToggleSlide = function(el){
    if ($(el).hasClass("open")) {
        $(el).removeClass("open");
    }
    else {
        $(el).addClass("open");
    }

}



//حذف عکس کاربر
var DeleteUserPic = function (UserId) {
    swal({
        title: "حذف تصویر کاربر",
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
                url: "/Admin/Users/Edit.aspx/DeleteUserPic",
                data: '{"UserId":' + UserId + '}',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d)
                        $(".img-box").remove();
                    else
                        FailedAlert("حذف تصویر با خطا همراه بوده است.");
                },
                error: function () {
                    FailedAlert("حذف تصویر دچار خطا شده است.");
                }
            });
        }
    });
}





//حذف عکس محصول
var DeleteProductPic = function (PicId) {
    swal({
        title: "حذف تصویر محصول",
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
                url: "/Admin/Products/Edit.aspx/DeleteProductPic",
                data: '{"PicId":' + PicId + '}',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d)
                        $(".img-box[rel='"+PicId+"']").remove();
                    else
                        FailedAlert("حذف تصویر با خطا همراه بوده است.");
                },
                error: function () {
                    FailedAlert("حذف تصویر دچار خطا شده است.");
                }
            });
        }
    });
}







//تعیین تصویر اصلی محصول
var SetProductMainPic = function (PicId, ProductId) {
    var data = {
        "PicId": PicId,
        "ProductId": ProductId
    };
    $.ajax({
        type: "POST",
        url: "/Admin/Products/Edit.aspx/SetProductMainPic",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d) {
                var OldMainId = $(".img-box.main").attr("rel");
                var btn = $("<button />").attr("type", "button")
                                        .addClass("btn btn-sm btn-success")
                                        .text("تصویر اصلی شود")
                                        .attr('onClick', 'SetProductMainPic(' + OldMainId + ',' + ProductId + ');');
                $(".img-box.main").removeClass("main").append(btn);
                $(".img-box[rel='" + PicId + "']").addClass("main")
                $(".img-box[rel='" + PicId + "'] .btn-success").remove();
            }
            else
                FailedAlert("عملیات با خطا همراه بوده است.");
        },
        error: function () {
            FailedAlert("عملیات دچار خطا شده است.");
        }
    });
}




//حذف یک محصول با ajax
$(document).on("click", ".product-ajax-delete a.delete", function (e) {
    e.preventDefault();
    var ProductId = $(this).attr("rel");
    swal({
        title: "حذف محصول",
        text: "آیا از حذف این مورد اطمینان دارید؟",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn btn-danger",
        cancelButtonClass: "btn btn-defult",
        confirmButtonText: "بله، حذف شود",
        cancelButtonText: "لغو",
        closeOnConfirm: false,
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                url: "/Admin/Products/Default.aspx/DeleteProduct",
                data: '{"ProductId":' + ProductId + '}',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d) {
                        swal({
                            title: "انجام شد",
                            text: "محصول مورد نظر حذف کردید.",
                            type: "success",
                            showCancelButton: false,
                            confirmButtonClass: "btn btn-success",
                            confirmButtonText: "باشه",
                            buttonsStyling: false,
                        }, function (isConfirm) {
                            window.location.href = window.location.href;
                        });
                    }
                    else
                        FailedAlert("حذف محصول با خطا همراه بوده است.");
                },
                error: function () {
                    FailedAlert("حذف محصول دچار خطا شده است.");
                }
            });
        }
    });
});







