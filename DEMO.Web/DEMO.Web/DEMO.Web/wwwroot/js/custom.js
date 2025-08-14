$(document).ready(function(){
    $(".toggle-switch").click(function(){
      $(".drpd-box").toggleClass("open-drpd");
    });
    $(".otp-sent, .otp-close").click(function(){
      $(".otp-popup").toggleClass("open-otp-popup");
    });
    $(".profile-icon").click(function(){
      $(".profile-drpd").toggleClass("open-drpd");
    });
    $(".qr-openner").click(function(){
      $(".qr-code-img").toggleClass("open-drpd");
    });
  
    $(".menu-btn").click(function(){
      $(".slide_nav").toggleClass("nav-toggle");
    }); 
    $(".menu-btn").click(function(){
      $(".header").toggleClass("h-padd");
    }); 
    $(".menu-btn").click(function(){
      $(".slide_nav .logo-desktop").toggleClass("logo-toggle");
    }); 
    $(".menu-btn").click(function(){
      $(".slide_nav .logo-mobile").toggleClass("logo-mobile-show");
    }); 
    $(".menu-btn").click(function(){
      $(".page_container").toggleClass("open-container");
    }); 
    $(".menu-btn").click(function(){
      $(".menu-opened").toggleClass("me-cls");
    }); 
    $(".menu-btn").click(function(){
      $(".menu-closed").toggleClass("me-opd");
    }); 
    $(".menu-btn").click(function(){
      $(".menu-name").toggleClass("m-n-none");
    }); 
    $(".resp-menu-btn,.resp-menu-closed ").click(function(){
      $(".slide_nav").toggleClass("open-slider-nav");
    });
    $(".resp-menu-btn,.resp-menu-closed ").click(function(){
      $(".responsive-overlay").toggleClass("open-overlay");
    });
    $(".responsive-overlay").click(function(){
      $(".slide_nav").toggleClass("open-slider-nav");
    });
    $(".responsive-overlay ").click(function(){
      $(".responsive-overlay").toggleClass("open-overlay");
    });
    $(".menu-btn").click(function(){
      $(".inner-navs").toggleClass("inner-navs-close");
    });
    $(".dropdown-toggle_1").click(function () {
        $(".dropdown-menu_1").toggleClass("dropdown-show");
    });
    $(".dropdown-toggle_2").click(function () {
        $(".dropdown-menu_2").toggleClass("dropdown-show");
    });
     
  });
