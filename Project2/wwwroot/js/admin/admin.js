localStorage.setItem("duyme", 1);
var a = localStorage.getItem("duyme");

$("nav .fa-bars").click(function () {
    if (a == 0) {
        $(".esassol").removeClass("main");
        $(".esassol ul li span").slideToggle();
        $(".esassol ul li").removeClass("main2");
        $(".esassag").removeClass("main3");
        a = 1;
    }
    else {
        $(".esassol").addClass("main");
        $(".esassol ul li span").slideToggle();
        $(".esassol ul li").addClass("main2");
        $(".esassag").addClass("main3");
        a = 0;
    }
    console.log(a)
})

$('.giris').click(function () {
    $('.giris1').toggle();
})


function openSidebar() {
    $(".esassol").addClass("main");
    $(".esassol ul li span").slideToggle();
    $(".esassol ul li").addClass("main2");
    $(".esassag").addClass("main3");
}

//$(window).on("load", function () {
//    if ($(location).attr("pathname").toLowerCase().includes("elantesdiqet"))
//        openSidebar();
//})