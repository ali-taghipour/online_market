
// get group id

$(document).on("click", ".category_menu li > span", function () {

    if (!$(this).parent("li").hasClass("open_menu")) {

        $(this).parent("li").children("ul").slideDown(600);
        $(this).parent("li").addClass("open_menu");
        $(this).html("-");
    }
    else {
        $(this).parent("li").children("ul").slideUp(300);
        $(this).parent("li").removeClass("open_menu");
        $(this).html("+");

    }

})

$(document).on("click", ".cat_close", function () {
    $(".cat_overlay").css({ top: "-100%" });
})
$(document).on("click", ".category_select", function () {
    $(".cat_overlay").css({ top: "0" });
})

$(document).on("click", ".category_menu > li a", function () {

    if ($(this).data("id") == null)
        return;

    cat_id = $(this).data("id");
    cat_name = $(this).html();

    $(".cat_value").val(cat_id);
    $(".category_select > span").html(cat_name);
    $(".category_select").addClass("is_select");

    $(".cat_overlay").css({ top: "-100%" });
})

//price
$('.price_count').val('1');
pr_cnt = 0;
for (i = 1 ; i <= $(".price_holder > div").length ; i++) {
    $(".price_holder > div:nth-of-type(" + i + ")").children("input").attr("name", "ListPrice" + i);
    $(".price_holder > div:nth-of-type(" + i + ")").children("select").attr("name", "GroupId" + i);
}

$(document).on("click", ".add_price > input", function () {
    if ($(".first_drop > option").length > $(".price_holder > div").length) {

        pr_cnt++;

        dropDown = $(".first_drop").html();
        priceHolder = "<div> <select> " + dropDown + " </select> ";
        priceHolder += " <span>:</span> <input type='text' class='notNull price_type' placeholder='قیمت به تومان' />";
        priceHolder += " <span>تومان</span>  <a class='del_price' title='حذف'> X </a> </div>";


        $(".price_holder").append(priceHolder);

        for (i = 1 ; i <= $(".price_holder > div").length ; i++) {
            $(".price_holder > div:nth-of-type(" + i + ")").children("input").attr("name", "ListPrice" + i);
            $(".price_holder > div:nth-of-type(" + i + ")").children("select").attr("name", "GroupId" + i);
        }

        
        var priceC = $('.price_count').val();
        priceC++;
        $('.price_count').val(priceC);

    }
    else {
        ModalBox("امکان اضافه کردن گروه بیشتر از لیست وجود ندارد", "alert");
    }

})
$(document).on("click", ".price_holder > div > .del_price", function () {

    $(this).parent("div").remove();

    for (i = 1 ; i <= $(".price_holder > div").length ; i++) {
        $(".price_holder > div:nth-of-type(" + i + ")").children("input").attr("name", "ListPrice" + i);
        $(".price_holder > div:nth-of-type(" + i + ")").children("select").attr("name", "GroupId" + i);

    }
    var priceC = $('.price_count').val();
    priceC--;
    $('.price_count').val(priceC);
})


$(document).on("click", ".checkbox_val input.checkbox-style[type='checkbox']", function () {

    if ($(this).is(":checked")) {
        $(this).val("True");
    } else {
        $(this).val("");

    }
})

$(document).on("keyup", ".price_type", function () {

    char = $(this).val();

    $(this).val(priceFormat(char));

})


function priceFormat(n) {
    n = n + "";
    if (n !== '') {
        var num = n.replace(/[^\d]/g, '');
        if (num.length > 3)
            num = num.replace(/\B(?=(?:\d{3})+(?!\d))/g, ',');
        return num;
    }
}


// pic 
$('.pic_count').val('0');

picnumber = 1;
$(document).on("click", ".add_pic > input", function () {

    picnumber++;
    picHolder = "<div>  <input type='file' id='pic" + picnumber + "' name='pic" + picnumber + "' onchange='setpic(this);' multiple='multiple' /> ";
    picHolder += "<label for='pic" + picnumber + "'> انتخاب عکس  </label> ";
    picHolder += "<a class='del_pic' title='حذف'>X</a> </div>";

    $(".user_pic").append(picHolder);


})
$(document).on("click", ".user_pic > div > .del_pic", function () {
    $(this).parent("div").remove();
})

function setpic(input) {

    $('.pic_count').val('1');

    if (input.files && input.files[0] && input.files.length == 1) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(input).parent("div").children('img').remove();

            $(input).parent("div").append('<img src="' + e.target.result + '" />');

        };
        reader.readAsDataURL(input.files[0]);

    } else if (input.files.length > 1) {
        $(input).parent("div").children('img').remove();

        for (i = 0 ; i < input.files.length; i++) {
            var reader = new FileReader();
            reader.onload = function (e) {

                $(input).parent("div").append('<img src="' + e.target.result + '" />');

            };
            reader.readAsDataURL(input.files[i]);
        }
    }


}


// File 

$(document).on("click", ".add_file > input", function () {


    fileHolder = "<div>  <input type='file'  /> ";
    fileHolder += "<label > انتخاب فایل  </label>  <span>:</span> ";
    fileHolder += "<input type='text'  placeholder='عنوان فایل '  />";
    fileHolder += "<a class='del_file' title='حذف'>X</a> </div>";

    $(".file_holder").append(fileHolder);

    for (i = 1 ; i <= $(".file_holder > div").length ; i++) {
        $(".file_holder > div:nth-child(" + i + ")").children("input[type=file]").attr("name", "FileSrc" + i);
        $(".file_holder > div:nth-child(" + i + ")").children("input[type=file]").attr("id", "FileSrc" + i);
        $(".file_holder > div:nth-child(" + i + ")").children("label").attr("for", "FileSrc" + i);
        $(".file_holder > div:nth-child(" + i + ")").children("input[type=text]").attr("name", "FileN" + i);
        $(".file_holder > div:nth-child(" + i + ")").children("input[type=text]").attr("id", "FileN" + i);

    }

})
$(document).on("click", ".file_holder > div > .del_file", function () {
    $(this).parent("div").remove();

    for (i = 1 ; i <= $(".file_holder > div").length ; i++) {
        $(".file_holder > div:nth-child(" + i + ")").children("input[type=file]").attr("name", "FileSrc" + i);
        $(".file_holder > div:nth-child(" + i + ")").children("input[type=file]").attr("id", "FileSrc" + i);
        $(".file_holder > div:nth-child(" + i + ")").children("label").attr("for", "FileSrc" + i);
        $(".file_holder > div:nth-child(" + i + ")").children("input[type=text]").attr("name", "FileN" + i);
        $(".file_holder > div:nth-child(" + i + ")").children("input[type=text]").attr("id", "FileN" + i);

    }
})



// propertice

$(document).on("click", ".add_proper > input", function () {


    proHolder = "<div> <section class='dic_holder'> <input type='text' placeholder='عنوان' /> ";
    proHolder += "<ul class='search_list'>  </ul>  </section>  <span>:</span> ";
    proHolder += "<input type='text'  placeholder='توضیحات '  /> <input type='hidden'/>";
    proHolder += "<a class='del_file' title='حذف'>X</a> </div>";

    $(".pro_holder").append(proHolder);

    for (i = 1 ; i <= $(".pro_holder > div").length ; i++) {
        $(".pro_holder > div:nth-child(" + i + ")").children("input[type=hidden]").attr("name", "proId" + i);
        $(".pro_holder > div:nth-child(" + i + ")").children("input[type=text]").attr("name", "proExpain" + i);

    }
    
    $('.pro_count').val($(".pro_holder > div").length);
})
$(document).on("click", ".pro_holder > div > .del_file", function () {
    $(this).parent("div").remove();

    for (i = 1 ; i <= $(".pro_holder > div").length ; i++) {
        $(".pro_holder > div:nth-child(" + i + ")").children("input[type=hidden]").attr("name", "proId" + i);
        $(".pro_holder > div:nth-child(" + i + ")").children("input[type=text]").attr("name", "proExpain" + i);


    }
    $('.pro_count').val($(".pro_holder > div").length);

})

$(document).on("click", ".search_list > li > a", function () {
   
    pro_id = $(this).data("id");
    pro_name = $(this).html();
    $(this).parent().parent().parent().parent().children("input[type=hidden]").val(pro_id);
    $(this).parent().parent().parent().children("input[type=text]").val(pro_name);
    $(this).parent().parent().css({'display' : 'none'});
})


$(".pr-date").each(function () {

    var times = $(this).html().split("/");
    var year = times[2].split(" ");
    var prdate = toJalaali(parseInt(year[0]), parseInt(times[0]), parseInt(times[1]));
    $(this).html(prdate.jy + "/" + prdate.jm + "/" + prdate.jd);

});
