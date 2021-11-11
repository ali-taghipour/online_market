
// Site Slide
SetNavSlide();
var slideLoop = null;
$(document).on("click", ".slide-nav > li", function () {
    var navNumber = $(this).index() + 1;
    SetSlide(navNumber);

    clearInterval(slideLoop);
    slideLoop = setInterval('SliderLoop()', 6000);

})

function SetSlide(i) {
    $('.slide-nav').find("li").removeClass("selected");
    $('.slide-nav').find("li:nth-child(" + i + ")").addClass("selected");

    $('.slider').children("div").removeClass("show");
    $('.slider').children("div:nth-child(" + i + ")").addClass("show");
}

function SetNavSlide() {
    var slideCount = $('.slider > div').length;
    for (i = 0; i < slideCount; i++) {
        var liNode = $("<li>");
        $('.slide-nav').append(liNode);

    }
    $('.slide-nav > li:nth-child(1)').addClass("selected");
}

slideLoop = setInterval('SliderLoop()', 6000);

function SliderLoop() {
    var slideNumber = $('.slide-nav > li.selected').index() + 1;
    if (slideNumber == $('.slider > div').length)
        slideNumber = 1;
    else
        slideNumber = slideNumber + 1;

    SetSlide(slideNumber);
}
//...