

//لود کردن همه دسته بندی ها برای treeView
//JustIsEnabled = true => همه دسته بندی های فعال
//JustIsEnabled = false => همه دسته بندی ها. چه فعال و چه غیر فعال
function RenderAllCategory(JustIsEnabled) {
    ResetForm();
    $(".category_menu").html("لطفا صبر کنید ...");
    $.ajax({
        type: "POST",
        url: "/Admin/Categories/Default.aspx/GetAllCategories",
        data: '{"JustIsEnabled" : ' + JustIsEnabled + '}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $(".category_menu").html("");
            if (data.d) {
                CreateTreeView(data.d);
            }
            else
                FailedAlert("خواندن دسته بندی ها دچار خطا شده است.");
        },
        error: function () {
            $(".category_menu").html("");
            FailedAlert("خواندن دسته بندی ها دچار خطا شده است.");
        }
    });
}




//ساخت آیتم های دسته بندی
function CreateTreeView(data) {
    if (data.length == 0) {
        var $li = $("<li/>").text("دسته بندی برای نمایش یافت نشد!");
        $(".category_menu").append($li);
    }
    $.each(data, function (e, el) {
        $(".category_menu").append(CreateTreeViewItem(el));
    });
}

function CreateTreeViewItem(item){
    var ProductCategoryId = $(".SelectedCategoryId").val();
    var $li = $("<li/>");
    var $anc = $("<a/>").text(item.Title)
                        .attr("rel", item.Id)
                        .attr("data-Childs", item.Childs.length);
    if (ProductCategoryId == item.Id) {
        $anc.addClass("select");
    }
    if(!item.IsEnabled)
        $anc.addClass("disabled");
    $li.append($anc);

    if (item.Childs != null && item.Childs.length > 0) {
        var $ul = $("<ul/>");
        $.each(item.Childs, function (e, el) {
            $ul.append(CreateTreeViewItem(el));
        });
        $li.append($ul);
        var $span = $("<span/>").text("+");
        $li.append($span);
    }
    return $li;
}




//انتخاب یک دسته بندی
$(document).on("click", ".category_menu  a", function () {
    ResetForm();
    if ($(this).hasClass("select")) {
        $(this).removeClass("select");
        if ($(".SelectedCategoryId"))
            $(".SelectedCategoryId").val("")
    }
    else {
        $(".category_menu  a").removeClass("select");
        $(this).addClass("select");
        if ($(".SelectedCategoryId"))
            $(".SelectedCategoryId").val($(this).attr("rel"))
    }

})



//حذف یک دسته بندی
function DeleteCategory() {
    var item = $(".category_menu  a.select");

    if (item.length == 0) {
        FailedAlert("دسته بندی مورد نظر را انتخاب کنید!");
        return;
    }

    if (item.length > 1) {
        FailedAlert("تنها یک دسته بندی را انتخاب کنید!");
        return;
    }

    var ChildsCount = item.attr("data-Childs");
    if (ChildsCount != "0") {
        FailedAlert("دسته بندی انتخاب شده دارای " + ChildsCount + " زیر دسته می باشد. لطفا ابتدا زیر دسته ها را حذف کنید!");
        return;
    }

    var CategoryId = item.attr("rel");
    if (!CategoryId) {
        FailedAlert("دسته بندی به درستی انتخاب نشده است!");
        return;
    }

    swal({
        title: "حذف دسته بندی",
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
                url: "/Admin/Categories/Default.aspx/DeleteCategory",
                data: '{"Id":' + CategoryId + '}',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d) {
                        swal({
                            title: "انجام شد",
                            text: "دسته بندی مورد نظر اضافه شد.",
                            type: "success",
                            showCancelButton: false,
                            confirmButtonClass: "btn btn-success",
                            confirmButtonText: "باشه",
                        }, function (isConfirm) {
                            RenderAllCategory(false);
                        });
                    }
                    else
                        FailedAlert("حذف دسته بندی با خطا همراه بوده است.");
                },
                error: function () {
                    FailedAlert("حذف دسته بندی دچار خطا شده است.");
                }
            });
        }
    });
}





//ریست کردن فرم 
function ResetForm() {
    $(".toggle-slide").removeClass("open");
    $(".form-title").html("");
    $(".ParentId").val("0");
    $(".CategoryId").val("");
    $(".CatTitle").val("");
    $(".Enabled").prop("checked", true);
    $(".Disabled").prop("checked", false);
}



//شروع فرآیند افزودن دسته بندی
function StartAddCategory() {
    ResetForm();
    var ParentId = $(".category_menu  a.select").attr("rel");
    if (ParentId) {
        $(".ParentId").val(ParentId);
        $(".form-title").html('افزودن زیر گروه به "' + $(".category_menu  a.select").html() + '"');
    }
    else
        $(".form-title").html("افزودن سر گروه جدید");
    ToggleSlide(".toggle-slide");
}




//افزودن دسته بندی جدید
function AddCategory() {
    var Title = $(".CatTitle").val();
    var ParentId = $(".ParentId").val();
    var IsEnabled = $("[name='IsEnabled']:checked").val();
    if (!Title) {
        FailedAlert("عنوان دسته بندی را وارد کنید.");
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Admin/Categories/Default.aspx/AddCategory",
        data: '{"Title":"' + Title + '", "ParentId":' + ParentId + ', "IsEnabled":' + IsEnabled + ' }',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d) {
                swal({
                    title: "انجام شد",
                    text: "دسته بندی مورد نظر اضافه شد.",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonClass: "btn btn-success",
                    confirmButtonText: "باشه",
                }, function (isConfirm) {
                    RenderAllCategory(false);
                });
            }
            else
                FailedAlert("افزودن دسته بندی با خطا همراه بوده است.");
        },
        error: function () {
            FailedAlert("افزودن دسته بندی دچار خطا شده است.");
        }
    });
}






//شروع فرآیند ویرایش دسته بندی
function StartUpdateCategory() {
    ResetForm();
    var item = $(".category_menu  a.select");
    var CategoryId = item.attr("rel");
    if (!CategoryId) {
        FailedAlert("دسته بندی مورد نظر را انتخاب کنید!");
        return;
    }
    
    var CategoryTitle = item.html();
    $(".CategoryId").val(CategoryId);
    $(".form-title").html('ویرایش گروه "' + CategoryTitle + '"');
    $(".CatTitle").val(CategoryTitle);
    if (item.hasClass("disabled")) {
        $(".Enabled").prop("checked", false);
        $(".Disabled").prop("checked", true);
    }
    else {
        $(".Enabled").prop("checked", true);
        $(".Disabled").prop("checked", false);
    }
    ToggleSlide(".toggle-slide");
}




//ویرایش دسته بندی
function UpdateCategory() {
    var Id = $(".CategoryId").val();
    var Title = $(".CatTitle").val();
    var IsEnabled = $("[name='IsEnabled']:checked").val();

    if (!Title) {
        FailedAlert("عنوان دسته بندی را وارد کنید.");
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Admin/Categories/Default.aspx/UpdateCategory",
        data: '{"Id":' + Id + ',"Title":"' + Title + '", "IsEnabled":' + IsEnabled + ' }',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d) {
                swal({
                    title: "انجام شد",
                    text: "دسته بندی مورد نظر ویرایش شد.",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonClass: "btn btn-success",
                    confirmButtonText: "باشه",
                }, function (isConfirm) {
                    RenderAllCategory(false);
                });
            }
            else
                FailedAlert("ویرایش دسته بندی با خطا همراه بوده است.");
        },
        error: function () {
            FailedAlert("ویرایش دسته بندی دچار خطا شده است.");
        }
    });
}




//افزودن یا ویرایش دسته بندی
function AddOrUpdateCategory() {
    var Id = $(".CategoryId").val();
    if (!Id)
        AddCategory();
    else
        UpdateCategory();
}


