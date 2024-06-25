function init() {
  let myMap = new ymaps.Map("map", {
    center: [57.66620404039572, 40.76415049999997],
    zoom: 14,
  });

  let placemark = new ymaps.Placemark(
    [57.66620404039572, 40.76415049999997],
    {},
    {}
  );
  myMap.geoObjects.add(placemark);
}

ymaps.ready(init);
