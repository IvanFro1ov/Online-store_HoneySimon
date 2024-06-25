$(".slider").slick({
  infinite: true,
  dots: true,
  draggable: false,
  swipe: true,
  centerMode: true,
  centerPadding: "0",
  variableWidth: true,
  touchThreshold: 10,
  slidesToShow: 1,
  slidesToScroll: 1,
  prevArrow:
    '<img src="../../img/стрелка-влево.png" class="slick-prev" alt="Previous">',
  nextArrow:
    '<img src="../../img/стрелка-вправо.png" class="slick-next" alt="Next">',
});
