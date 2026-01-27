$(function () {
    'use strict';
    $('#main-wrapper').AdminSettings({
        Layout: 'horizontal',
        LogoBg: 'skin2', // You can change the Value to be skin1/skin2/skin3
        NavbarBg: 'skin2', // You can change the Value to be skin1/skin2/skin3
        SidebarType: 'full', // You can change it full / mini-sidebar
        SidebarColor: 'skin3', // You can change the Value to be skin1/skin2/skin3
        SidebarPosition: true, // it can be true / false ( true means Fixed and false means absolute )
        HeaderPosition: true // it can be true / false ( true means Fixed and false means absolute )
    });
});