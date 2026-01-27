// Admin Panel settings
$.fn.AdminSettings = function (settings) {
    var myid = this.attr("id");

    // General option for vertical header 
    var defaults = {
        Layout: 'vertical', // 
        LogoBg: 'skin1', // You can change the Value to be skin1/skin2/skin3/skin4/skin5/skin6 
        NavbarBg: 'skin6', // You can change the Value to be skin1/skin2/skin3/skin4/skin5/skin6 
        SidebarType: 'full', // You can change it full / mini-sidebar
        SidebarColor: 'skin1', // You can change the Value to be skin1/skin2/skin3/skin4/skin5/skin6
        SidebarPosition: false, // it can be true / false
        HeaderPosition: false, // it can be true / false
    };

    var settings = $.extend({}, defaults, settings);

    // Attribute functions 
    var AdminSettings = {
        // Settings INIT
        AdminSettingsInit: function () {
            AdminSettings.ManageThemeLayout();
            AdminSettings.ManageThemeBackground();
            AdminSettings.ManageSidebarType();
            AdminSettings.ManageSidebarColor();
            AdminSettings.ManageSidebarPosition();
        },

        //****************************
        // ManageThemeLayout functions
        //****************************
        ManageThemeLayout: function () {
            switch (settings.Layout) {
                case 'horizontal':
                    $('#' + myid).attr("data-layout", "horizontal");
                    break;
                case 'vertical':
                    $('#' + myid).attr("data-layout", "vertical");
                    $('.scroll-sidebar').perfectScrollbar({});
                    break;
                default:
            }
        },

        //****************************
        // ManageSidebarType functions 
        //****************************
        ManageThemeBackground: function () {
            // Logo bg attribute
            function setlogobg() {
                var lbg = settings.LogoBg;
                if (lbg != undefined && lbg != "") {
                    $('#' + myid + ' .topbar .top-navbar .navbar-header').attr("data-logobg", lbg);
                } else {
                    $('#' + myid + ' .topbar .top-navbar .navbar-header').attr("data-logobg", "skin1");
                }
            };
            setlogobg();
            // Navbar bg attribute
            function setnavbarbg() {
                var nbg = settings.NavbarBg;
                if (nbg != undefined && nbg != "") {
                    $('#' + myid + ' .topbar .navbar-collapse').attr("data-navbarbg", nbg);
                    $('#' + myid + ' .topbar').attr("data-navbarbg", nbg);
                    $('#' + myid).attr("data-navbarbg", nbg);
                } else {
                    $('#' + myid + ' .topbar .navbar-collapse').attr("data-navbarbg", "skin1");
                    $('#' + myid + ' .topbar').attr("data-navbarbg", "skin1");
                    $('#' + myid).attr("data-navbarbg", "skin1");
                }
            };
            setnavbarbg();
        },

        //****************************
        // ManageThemeLayout functions
        //****************************
        ManageSidebarType: function () {
            switch (settings.SidebarType) {
                //****************************
                // If the sidebar type has full
                //****************************     
                case 'full':
                    $('#' + myid).attr("data-sidebartype", "full");
                    //****************************
                    /* This is for the mini-sidebar if width is less then 1170*/
                    //**************************** 
                    var setsidebartype = function () {
                        var width = (window.innerWidth > 0) ? window.innerWidth : this.screen.width;
                        if (width < 1170) {
                            $("#main-wrapper").attr("data-sidebartype", "mini-sidebar");
                        } else {
                            $("#main-wrapper").attr("data-sidebartype", "full");
                        }
                    };
                    $(window).ready(setsidebartype);
                    $(window).on("resize", setsidebartype);
                    //****************************
                    /* This is for sidebartoggler*/
                    //****************************
                    $('.sidebartoggler').on("click", function () {
                        $("#main-wrapper").toggleClass("mini-sidebar");
                        $(".sidebartoggler i").toggleClass("mdi-toggle-switch-off");
                        if ($("#main-wrapper").hasClass("mini-sidebar")) {
                            $(".sidebartoggler").prop("checked", !0);
                            $("#main-wrapper").attr("data-sidebartype", "mini-sidebar");
                        } else {
                            $(".sidebartoggler").prop("checked", !1);
                            $("#main-wrapper").attr("data-sidebartype", "full");
                        }
                    });
                    break;
                case 'mini-sidebar':
                    $('#' + myid).attr("data-sidebartype", "mini-sidebar");
                    //****************************
                    /* This is for sidebartoggler*/
                    //****************************
                    $('.sidebartoggler').on("click", function () {
                        $("#main-wrapper").toggleClass("mini-sidebar");
                        $(".sidebartoggler i").toggleClass("mdi-toggle-switch-off");
                        if ($("#main-wrapper").hasClass("mini-sidebar")) {
                            $(".sidebartoggler").prop("checked", !0);
                            $("#main-wrapper").attr("data-sidebartype", "full");
                        } else {
                            $(".sidebartoggler").prop("checked", !1);
                            $("#main-wrapper").attr("data-sidebartype", "mini-sidebar");
                        }
                    });
                    break;
                default:
            }
        },

        //****************************
        // ManageSidebarColor functions 
        //****************************
        ManageSidebarColor: function () {
            // Logo bg attribute
            function setsidebarbg() {
                var sbg = settings.SidebarColor;
                if (sbg != undefined && sbg != "") {
                    $('#' + myid + ' .left-sidebar').attr("data-sidebarbg", sbg);
                } else {
                    $('#' + myid + ' .left-sidebar').attr("data-sidebarbg", "skin1");
                }
            };
            setsidebarbg();
        },

        //****************************
        // ManageSidebarPosition functions
        //****************************
        ManageSidebarPosition: function () {
            var sidebarposition = settings.SidebarPosition;
            var headerposition = settings.HeaderPosition;
            switch (settings.Layout) {
                case 'vertical':
                    if (sidebarposition == true) {
                        $('#' + myid).attr("data-sidebar-position", 'fixed');
                        $("#sidebar-position").prop("checked", !0);
                    } else {
                        $('#' + myid).attr("data-sidebar-position", 'absolute');
                        $("#sidebar-position").prop("checked", !1);
                    }
                    if (headerposition == true) {
                        $('#' + myid).attr("data-header-position", 'fixed');
                        $("#header-position").prop("checked", !0);
                    } else {
                        $('#' + myid).attr("data-header-position", 'relative');
                        $("#header-position").prop("checked", !1);
                    }
                    break;
                default:
            }
        }
    };
    AdminSettings.AdminSettingsInit();
};