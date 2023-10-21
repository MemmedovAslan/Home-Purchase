$('.giris').click(function () {
    $('.giris1').toggle();
})
$('.etrafli').click(function () {
    $('.elave').slideToggle();
    $('.etrafli i').toggleClass("etraflii")
})


//window.onload = function () {
//    let elementClicked = false;

//    $(".darkmode").click(function () {
//        elementClicked = true;
//    });

//    if (elementClicked) {
//        $(this).addClass("darkmode1");
//        $(".circle").addClass("circle1");
//        $(".dark").addClass("dark1");
//        $(".darkk").addClass("darkk1");
//    }
//    else {
//        $(this).removeClass("darkmode1");
//        $(".circle").removeClass("circle1");
//        $(".dark").removeClass("dark1");
//        $(".darkk").removeClass("darkk1");
//    }
//}

$(".darkmode").click(function () {
    $(this).toggleClass("darkmode1");
    $(".circle").toggleClass("circle1");
    $(".dark").toggleClass("dark1");
    $(".darkk").toggleClass("darkk1");
})

//window.onload = function () {
//    $('.' + window.location.href.split('/')[window.location.href.split('/').length - 1]).addClass("active");
//}


$("#seher").change(function () {
    $('#rayon option:gt(0)').hide(); /*gt(0) o demekdiki 0 dan boyuk olanlar*/
    $('#rayon').val('')
    $(`#rayon option[seherid="${$("#seher").val()}"]`).show()
    //$("#rayon option").show();
    //$("#rayon option").filter(attr(SeherId) != (this).val()).hide();
})

