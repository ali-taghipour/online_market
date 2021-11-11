//جلوگیری از 2 بار سابمیت شدن فرم ها
var FormIsSubmited = false;
$(document).on("submit", "form", function () {
    if (FormIsSubmited)
        return false;
    FormIsSubmited = Validate();
    return FormIsSubmited;
})


//ولیدیت کردن فرم ها
var Validate = function () {
    var IsValid = true;
    $(".form-item").find(".error").html("")


    //آیتم های اجباری
    var Items = $(".required");
    Items.each(function () {
        if (!$(this).val()) {
            $(this).parents(".form-item").find(".error").html("الزامی است.");
            IsValid = false;
        }
    });


    //آیتم های با طول کاراکتر مشخص
    Items = $(".length");
    Items.each(function () {
        var value = $(this).val();
        var length = $(this).attr("data-length");
        if (length && value) {
            if (value.length != length) {
                $(this).parents(".form-item").find(".error").html("باید به طول " + length + " کاراکتر باشد." );
                IsValid = false;
            }
        }
    });




    //آیتم های با حداقل طول کاراکتر مشخص
    Items = $(".min-length");
    Items.each(function () {
        var value = $(this).val();
        var minLength = $(this).attr("data-minlength");
        if (minLength && value) {
            if (value.length < minLength) {
                $(this).parents(".form-item").find(".error").html("باید حداقل " + minLength + " کاراکتر داشته باشد.");
                IsValid = false;
            }
        }
    });




    //آیتم های با حداکثر طول کاراکتر مشخص
    Items = $(".max-length");
    Items.each(function () {
        var value = $(this).val();
        var maxLength = $(this).attr("data-maxlength");
        if (maxLength && value) {
            if (value.length > maxLength) {
                $(this).parents(".form-item").find(".error").html("باید حداکثر " + maxLength + " کاراکتر داشته باشد.");
                IsValid = false;
            }
        }
    });



    //ولیدیت کردن موبایل
    Items = $(".mobile");
    Items.each(function () {
        var value = $(this).val();        
        if (value) {
            var regex = "^09[0-9]{9}$"
            if (!regex.test(value)) {
                $(this).parents(".form-item").find(".error").html("فرمت وارد شده برای تلفن همراه صحیح نیست.");
                IsValid = false;
            }
        }
    });



    //ولیدیت کردن فقط حروف فارسی و عدد
    Items = $(".fa");
    Items.each(function () {
        var value = $(this).val();
        if (value) {
            var regex = /^([\u0600-\u06FF]|[0-9]|\s)*$/;
            if (!regex.test(value)) {
                $(this).parents(".form-item").find(".error").html("تنها باید شامل حروف فارسی و عدد باشد.");
                IsValid = false;
            }
        }
    });


    

    //ولیدیت کردن فقط حروف انگلیسی و عدد
    Items = $(".en");
    Items.each(function () {
        var value = $(this).val();
        if (value) {
            var regex = /^([A-Z]|[a-z]|[0-9]|\s)*$/;
            if (!regex.test(value)) {
                $(this).parents(".form-item").find(".error").html("تنها باید شامل حروف انگلیسی و عدد باشد.");
                IsValid = false;
            }
        }
    });




    //ولیدیت کردن ایمیل
    Items = $(".email");
    Items.each(function () {
        var value = $(this).val();
        if (value) {
            var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!regex.test(value)) {
                $(this).parents(".form-item").find(".error").html("فرمت وارد شده برای ایمیل صحیح نیست.");
                IsValid = false;
            }
        }
    });



    //آیتم هایی که باید با ایتم دیگری برابر باشند.
    Items = $(".equal");
    Items.each(function () {
        var value = $(this).val();
        var EqualItem = $(this).attr("data-equal"); //نام ایتمی که باید با ان برابر باشد را میدهد.
        var equalValue = $(EqualItem).val();
        if (value && EqualItem && equalValue) {
            if (value != equalValue) {
                $(this).parents(".form-item").find(".error").html("تکرار صحیح نمی باشد.");
                IsValid = false;
            }
        }
    });

    return IsValid;
}

