const menuIcon = document.querySelector(".menu");
const closeButton = document.querySelector(".close-button");
const mobileNavigationPanel = document.querySelector(
  ".mobile-navigation-panel"
);

menuIcon.addEventListener("click", function () {
  mobileNavigationPanel.classList.add("active");
});

closeButton.addEventListener("click", function () {
  mobileNavigationPanel.classList.remove("active");
});
