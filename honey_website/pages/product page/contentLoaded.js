document.addEventListener("DOMContentLoaded", function () {
  const productId = localStorage.getItem("selectedProductId");
  const productTypeId = localStorage.getItem("selectedProductTypeId");
  const authToken = localStorage.getItem("authToken");

  // Функция для получения пути к изображению на основе shortDesc
  function getImageSrc(shortDesc) {
    switch (shortDesc) {
      case "3 литра":
        return "../../img/3_liters.jpg";
      case "1 кг":
        return "../../img/1_kg.jpg";
      default:
        return "../../img/kubotainer33.jpg";
    }
  }

  if (productId && productTypeId) {
    fetch(
      `http://localhost:8080/http://localhost:5277/api/product/${productTypeId}?id=${productId}`
    )
      .then((response) => {
        if (!response.ok) {
          throw new Error("Ошибка при загрузке данных о продукте");
        }
        return response.json();
      })
      .then((productData) => {
        // Заполняем информацию о продукте на странице
        document.getElementById("productName").textContent = productData.name;
        document.getElementById(
          "productPrice"
        ).textContent = `${productData.price} руб.`;
        document.getElementById("productDescription").textContent =
          productData.description;
        document.getElementById(
          "productStock"
        ).textContent = `Осталось на складе: ${productData.quantityOfStock}`;

        // Устанавливаем правильное изображение на основе shortDesc
        const productImage = document.getElementById("productImage");
        productImage.src = getImageSrc(productData.shortDesc);

        // Инициализируем счетчик
        let count = 0;
        const decrementButton = document.getElementById("decrement");
        const incrementButton = document.getElementById("increment");
        const countElement = document.getElementById("count");

        function updateCount() {
          countElement.textContent = count;
        }

        decrementButton.addEventListener("click", () => {
          count = Math.max(0, count - 1);
          updateCount();
        });

        incrementButton.addEventListener("click", () => {
          if (count < productData.quantityOfStock) {
            count += 1;
            updateCount();
          } else {
            alert("Вы не можете выбрать больше товара, чем есть на складе");
          }
        });

        updateCount();

        // Настройка кнопки "Добавить в корзину"
        const addToCartButton = document.getElementById("addToCartButton");
        addToCartButton.addEventListener("click", function (event) {
          event.preventDefault();

          // Проверяем авторизацию пользователя
          if (!authToken) {
            alert("Необходимо войти в систему для добавления товара в корзину");
            return;
          }

          if (count === 0) {
            alert("Пожалуйста, выберите количество товара");
            return;
          }

          // Запрос на получение корзины пользователя
          fetch("http://localhost:8080/http://localhost:5277/api/shopcart", {
            headers: {
              Authorization: `Bearer ${authToken}`,
              "Content-Type": "application/json",
            },
          })
            .then((response) => {
              if (!response.ok) {
                throw new Error("Ошибка при получении корзины");
              }
              return response.json();
            })
            .then(() => {
              const promises = [];

              for (let i = 0; i < count; i++) {
                promises.push(
                  fetch(
                    "http://localhost:8080/http://localhost:5277/api/shopcart/add",
                    {
                      method: "PUT",
                      headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${authToken}`,
                      },
                      body: JSON.stringify(productId), // Отправляем только ID продукта
                    }
                  )
                );
              }

              return Promise.all(promises);
            })
            .then((responses) => {
              const allOk = responses.every((response) => response.ok);

              if (allOk) {
                alert("Товары добавлены в корзину");
              } else {
                throw new Error("Ошибка при добавлении товаров в корзину");
              }
            })
            .catch((error) => {
              console.error(
                "Ошибка при выполнении операции с корзиной:",
                error
              );
              alert(
                "Ошибка при выполнении операции с корзиной: " + error.message
              );
            });
        });
      })
      .catch((error) => {
        console.error("Ошибка при загрузке данных о продукте:", error);
        alert("Ошибка при загрузке данных о продукте: " + error.message);
      });
  } else {
    console.error("ID продукта или тип продукта не найдены в localStorage");
    alert("ID продукта или тип продукта не найдены в localStorage");
  }
});
