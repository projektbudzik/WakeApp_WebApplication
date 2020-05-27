function footerAlign() {
    $('footer').css('display', 'block');
    $('footer').css('height', 'auto');
    var footerHeight = $('footer').outerHeight();
    $('body').css('padding-bottom', footerHeight);
    $('footer').css('height', footerHeight);
}


$(document).ready(function () {
    footerAlign();
});

$(window).resize(function () {
    footerAlign();
});


$(window).resize(function () {
    $('body').css('padding-top', parseInt($('#main-navbar').css("height")) + 10);
});

$(window).load(function () {
    $('body').css('padding-top', parseInt($('#main-navbar').css("height")) + 10);
});