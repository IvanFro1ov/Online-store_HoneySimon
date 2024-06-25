function checkAuthAndRedirect() {
  const userId = localStorage.getItem("userId");
  const authToken = localStorage.getItem("authToken");

  if (userId && authToken) {
    // Если пользователь авторизован, перенаправляем на страницу профиля
    window.location.href = "../profile/profile.html";
  } else {
    // Если пользователь не авторизован, перенаправляем на страницу авторизации
    window.location.href = "../authorization/authorization.html";
  }
}

// Привязка проверки авторизации к клику на ссылку "Профиль"
document.addEventListener("DOMContentLoaded", function () {
  document
    .getElementById("profileLink")
    .addEventListener("click", function (event) {
      event.preventDefault(); 
      checkAuthAndRedirect();
    });

  document
    .getElementById("profileTextLink")
    .addEventListener("click", function (event) {
      event.preventDefault(); 
      checkAuthAndRedirect();
    });

  document
    .getElementById("mobileProfileLink")
    .addEventListener("click", function (event) {
      event.preventDefault(); 
      checkAuthAndRedirect();
    });
});
