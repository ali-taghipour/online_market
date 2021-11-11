//متغیر سراسری دیتاتیبل
// هرجا درون صفحه یک دیتاتیبل  فراخوانی میشود دورن این متغیر قرار میگیرد
// تا در این فایل در دسترس باشد. مثل بخش کامنت ها برای حذف یک سطر از دیتا تیبل
var DataTable;


$(document).ready(function () {
    DataTable = $('.datatable').DataTable({
        language: {
            url: "/Content/DataTable/fa-lang.json"
        },
        "pagingType": "full_numbers",
        "lengthMenu": [
            [10, 25, 50, -1],
            [10, 25, 50, "All"]
        ],
        "columnDefs": [
            { "orderable": false, "targets": 5 }
        ],
        "order": [[0, "desc"]],
        "responsive": true
    });
});


//تغییر وضعیت بررسی کامنت
var ToggleCommentReaded = function (CommentId, DeleteRow) {
    var $this = $(event.srcElement);
    $.ajax({
        type: "POST",
        url: "/Admin/Comments/Default.aspx/ToggleCommentReaded",
        data: '{"CommentId":' + CommentId + '}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d) {
                if (DeleteRow && DataTable) {
                    DataTable.row($this.parents('tr')).remove().draw();
                }
                else {
                    if ($this.hasClass("btn-success"))
                        $this.removeClass("btn-success").addClass("btn-warning").html("بررسی نشد");
                    else
                        $this.removeClass("btn-warning").addClass("btn-success").html("بررسی شد");
                }
            }
            else
                FailedAlert("تغییر وضعیت کامنت با خطا همراه بوده است.");
        },
        error: function () {
            FailedAlert("تغییر وضعیت کامنت دچار خطا شده است.");
        }
    });

}





//تغییر وضعیت تایید کامنت
var ToggleCommentApproved = function (CommentId, DeleteRow) {
    var $this = $(event.srcElement);
    $.ajax({
        type: "POST",
        url: "/Admin/Comments/Default.aspx/ToggleCommentApproved",
        data: '{"CommentId":' + CommentId + '}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d) {
                if (DeleteRow && DataTable) {
                    DataTable.row($this.parents('tr')).remove().draw();
                }
                else {
                    if ($this.hasClass("btn-success")) {
                        $this.parents('td').find(".readed").removeClass("btn-warning").addClass("btn-success").html("بررسی شد");
                        $this.removeClass("btn-success").addClass("btn-warning").html("تایید نشد");
                    }
                    else {
                        $this.parents('td').find(".readed").removeClass("btn-warning").addClass("btn-success").html("بررسی شد");
                        $this.removeClass("btn-warning").addClass("btn-success").html("تایید شد");
                    }
                }
            }
            else
                FailedAlert("تغییر وضعیت کامنت با خطا همراه بوده است.");
        },
        error: function () {
            FailedAlert("تغییر وضعیت کامنت دچار خطا شده است.");
        }
    });

}






//مشاهده جزییات کامنت
var CommentDetails = function (CommentId) {
    var $this = $(event.srcElement);
    $.ajax({
        type: "POST",
        url: "/Admin/Comments/Default.aspx/CommentDetails",
        data: '{"CommentId":' + CommentId + '}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d != null) {
                $(".CommentText").html(data.d.Text);
                $(".ProductTitle").html(data.d.ProductTitle);
                $(".UserFullName").html(data.d.UserFullName);
                $(".CreateDate").html(data.d.CreateDate);
                OpenModal();
            }
            else
                FailedAlert("خواندن جزییات کامنت با خطا همراه بوده است.");
        },
        error: function () {
            FailedAlert("خواندن جزییات کامنت دچار خطا شده است.");
        }
    });

}




//حذف کامنت
var DeleteComment = function (CommentId) {
    var $this = $(event.srcElement);
    swal({
        title: "حذف کامنت",
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
                url: "/Admin/Comments/Default.aspx/DeleteComment",
                data: '{"CommentId":' + CommentId + '}',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d) {
                        swal({
                            title: "انجام شد",
                            text: "کامنت مورد نظر حذف کردید.",
                            type: "success",
                            showCancelButton: false,
                            confirmButtonClass: "btn btn-success",
                            confirmButtonText: "باشه",
                            buttonsStyling: false,
                        }, function (isConfirm) {
                            DataTable.row($this.parents('tr')).remove().draw();
                        });
                    }
                    else
                        FailedAlert("حذف کامنت با خطا همراه بوده است.");
                },
                error: function () {
                    FailedAlert("حذف کامنت دچار خطا شده است.");
                }
            });
        }
    });
}





///////////////////////////////////////////
///////  بخش مربوط به مدال //////////////
//////////////////////////////////////////
$(document).on("click", ".close-modal", function () {
    CloseModal();
});


function OpenModal() {
    $(".modal").addClass("show");
}

function CloseModal() {
    $(".modal").removeClass("show");
}