$(document).ready(function () {
    $(".dropdown").hover(
        function () {
            $('.dropdown-menu', this).not('.in .dropdown-menu').stop(true, true).slideDown("400");
            $(this).toggleClass('open');
        },
        function () {
            $('.dropdown-menu', this).not('.in .dropdown-menu').stop(true, true).slideUp("400");
            $(this).toggleClass('open');
        }
    );
});

$(document).ready(function () {
    // executes when HTML-Document is loaded and DOM is ready

    // breakpoint and up  
    $(window).resize(function () {
        if ($(window).width() >= 980) {

            // when you hover a toggle show its dropdown menu
            $(".navbar .dropdown-toggle").hover(function () {
                $(this).parent().toggleClass("show");
                $(this).parent().find(".dropdown-menu").toggleClass("show");
            });

            // hide the menu when the mouse leaves the dropdown
            $(".navbar .dropdown-menu").mouseleave(function () {
                $(this).removeClass("show");
            });

            // do something here
        }
    });

});

//$('.slider').slick({
//    dots: true,
//    infinite: false,
//    speed: 300,
//    slidesToShow: 4,
//    slidesToScroll: 4,
//    autoplay: true,
//    autoplaySpeed: 2000,
//    responsive: [
//        {
//            breakpoint: 1024,
//            settings: {
//                slidesToShow: 3,
//                slidesToScroll: 3,
//                infinite: true,
//                dots: true
//            }
//        },
//        {
//            breakpoint: 600,
//            settings: {
//                slidesToShow: 2,
//                slidesToScroll: 2
//            }
//        },
//        {
//            breakpoint: 480,
//            settings: {
//                slidesToShow: 1,
//                slidesToScroll: 1
//            }
//        }
//        // You can unslick at a given breakpoint now by adding:
//        // settings: "unslick"
//        // instead of a settings object
//    ]
//});

function UpdateTotalPrice() {
    $.ajax({
        type: 'GET',
        url: "Cart/UpdateTotal",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            if (msg.d == "$0.00") {
                location.reload();
                return;
            }

            $('#totalPrice').text(msg.d);
        }
    })
}

function Change(el, type, pid) {
    var data = {
        'type': type,
        'pId': pid
    };
    $.ajax({
        type: 'POST',
        url: "Cart/QuanityChange",
        data: "{ 'type': " + type + ", 'pId': " + pid + "}",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            if (msg.d == 0) {
                el.parentNode.parentNode.remove();
            } else {
                $(el).siblings('span')[0].innerHTML = msg.d
            }
            UpdateTotalPrice()
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            debugger;
        }
    });
}

$(function () {

    //Optional: turn the chache off
    $.ajaxSetup({ cache: false });

    $('.btnCreate').click(function () {
        $('#dialogContent').load(this.href, function () {
            $('#dialogDiv').modal({
                backdrop: 'static',
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
});
function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#dialogDiv').modal('hide');
                    // Refresh:
                    // location.reload();
                } else {
                    $('#dialogContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}