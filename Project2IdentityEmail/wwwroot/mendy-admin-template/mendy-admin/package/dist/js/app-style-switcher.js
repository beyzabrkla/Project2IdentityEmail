$(function () {
    "use strict";

    //****************************
    /* Left header Theme Change function Start */
    //****************************
    function handlelogobg() {
        $('.theme-color .theme-item .theme-link').on("click", function () {
            var logobgskin = $(this).attr("data-logobg");
            $('.topbar .top-navbar .navbar-header').attr("data-logobg", logobgskin);
        });
    };
    handlelogobg();

    //****************************
    /* Top navbar Theme Change function Start */
    //****************************
    function handlenavbarbg() {
        if ($('#main-wrapper').attr('data-navbarbg') == 'skin3') {
            // do this
            $(".topbar .navbar").addClass('navbar-light');
            $(".topbar .navbar").removeClass('navbar-dark');
        } else {
            // do that
        }
        $('.theme-color .theme-item .theme-link').on("click", function () {
            var navbarbgskin = $(this).attr("data-navbarbg");
            $('#main-wrapper').attr("data-navbarbg", navbarbgskin);
            $('.topbar .navbar-collapse').attr("data-navbarbg", navbarbgskin);
            if ($('#main-wrapper').attr('data-navbarbg') == 'skin3') {
                // do this
                $(".topbar .navbar").addClass('navbar-light');
                $(".topbar .navbar").removeClass('navbar-dark');
            } else {
                // do that
                $(".topbar .navbar").removeClass('navbar-light');
                $(".topbar .navbar").addClass('navbar-dark');
            }
        });
    };
    handlenavbarbg();

    //****************************
    /* Manage sidebar bg color */
    //****************************
    function handlesidebarbg() {
        $('.theme-color .theme-item .theme-link').on("click", function () {
            var sidebarbgskin = $(this).attr("data-sidebarbg");
            $('.left-sidebar').attr("data-sidebarbg", sidebarbgskin);
        });
    };
    handlesidebarbg();

    //****************************
    /* sidebar position */
    //****************************
    function handlesidebarposition() {
        $('#sidebar-position').change(function () {
            if ($(this).is(":checked")) {
                $('#main-wrapper').attr("data-sidebar-position", 'fixed');
                $('.topbar .top-navbar .navbar-header').attr("data-navheader", 'fixed');
            } else {
                $('#main-wrapper').attr("data-sidebar-position", 'absolute');
                $('.topbar .top-navbar .navbar-header').attr("data-navheader", 'relative');
            }
        });

    };
    handlesidebarposition();

    //****************************
    /* Header position */
    //****************************
    function handleheaderposition() {
        $('#header-position').change(function () {
            if ($(this).is(":checked")) {
                $('#main-wrapper').attr("data-header-position", 'fixed');
            } else {
                $('#main-wrapper').attr("data-header-position", 'relative');
            }
        });
    };
    handleheaderposition();
});