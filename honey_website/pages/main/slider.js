$(document).ready(function () {
  var $slider = $(".feedbacks-list");

  $slider.slick({
    infinite: true,
    dots: true,
    draggable: false,
    swipe: true,
    touchThreshold: 10,
    slidesToScroll: 1,
    slidesToShow: 3,
    prevArrow:
      '<img src="../../img/стрелка-влево.png" class="slick-prev" alt="Previous">',
    nextArrow:
      '<img src="../../img/стрелка-вправо.png" class="slick-next" alt="Next">',
  });

  function updateSlidesToShow() {
    var screenWidth = $(window).width();
    var slidesToShow = 3;

    if (screenWidth <= 1024) {
      slidesToShow = 2;
    }

    if (screenWidth <= 768) {
      slidesToShow = 1;
    }

    $slider.slick("slickSetOption", "slidesToShow", slidesToShow, true);
  }

  $(window).resize(updateSlidesToShow);
});
