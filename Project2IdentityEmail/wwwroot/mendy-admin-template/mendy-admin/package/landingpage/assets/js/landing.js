$(function() {
    $('a.nav-link, .dm-btn').on('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top - 10
        }, 1000);
        event.preventDefault();
    });

});