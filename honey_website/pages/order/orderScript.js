document.addEventListener("DOMContentLoaded", function () {
  const userId = localStorage.getItem("userId");
  const orderId = localStorage.getItem("orderId"); // Идентификатор заказа
  const authToken = localStorage.getItem("authToken");

  // Функция для получения данных пользователя
  function getUserData(userId) {
    return fetch(
      `http://localhost:8080/http://localhost:5277/api/user/${userId}`,
      {
        method: "GET",
        headers: {
          accept: "text/plain",
          Authorization: `Bearer ${authToken}`,
        },
      }
    )
      .then((response) => response.json())
      .then((data) => {
        document.getElementById(
          "name"
        ).textContent = `Имя: ${data.firstname} ${data.lastname}`;
        document.getElementById("email").textContent = `E-mail: ${data.email}`;
      })
      .catch((error) => {
        console.error("Ошибка получения данных пользователя:", error);
      });
  }

  // Получить данные пользователя при загрузке страницы
  if (userId) {
    getUserData(userId);
  } else {
    console.error("User ID не найден в localStorage");
  }

  // Сохранить способ доставки в localStorage
  document.querySelectorAll('input[name="shipping"]').forEach((input) => {
    input.addEventListener("change", function () {
      localStorage.setItem("shippingMethod", this.value);
    });
  });

  // Проверить заполненность адреса и управлять кнопками
  const addressInput = document.getElementById("address");
  const confirmButton = document.getElementById("confirmBtn");
  const cancelButton = document.getElementById("cancelBtn");
  const orderForm = document.getElementById("orderForm");

  function toggleConfirmButton() {
    confirmButton.disabled = addressInput.value.trim() === "";
  }

  addressInput.addEventListener("input", toggleConfirmButton);
  toggleConfirmButton(); // Изначальная проверка при загрузке страницы

  // Перенаправление при нажатии на кнопку отмены и отправка запроса
  cancelButton.addEventListener("click", function () {
    const orderId = localStorage.getItem("orderId");

    if (orderId && authToken) {
      fetch(
        `http://localhost:8080/http://localhost:5277/api/orders/${orderId}`,
        {
          method: "PUT",
          headers: {
            accept: "*/*",
            Authorization: `Bearer ${authToken}`,
          },
        }
      )
        .then((response) => {
          if (!response.ok) {
            throw new Error("Ошибка отмены заказа");
          }
          return response.json();
        })
        .then(() => {
          window.location.href = "../basket/basket.html";
        })
        .catch((error) => {
          console.error("Ошибка при отправке запроса на отмену заказа:", error);
        });
    } else {
      console.error("Order ID или Auth Token не найдены в localStorage");
    }
  });

  // Валидация формы перед отправкой
  orderForm.addEventListener("submit", function (event) {
    if (!orderForm.checkValidity()) {
      event.preventDefault();
      event.stopPropagation();
      alert("Пожалуйста, заполните все обязательные поля.");
    } else {
      event.preventDefault();
      // Прочие действия при успешной валидации формы
      window.location.href = "../thanks/thanks.html";
    }
    orderForm.classList.add("was-validated");
  });
});
