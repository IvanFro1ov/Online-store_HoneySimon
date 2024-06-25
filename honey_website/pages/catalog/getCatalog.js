document.addEventListener("DOMContentLoaded", function () {
  const catalogList = document.querySelector(".catalog-list");
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

  // Функция для создания карточки товара
  function createProductCard(product) {
    const productCard = document.createElement("div");
    productCard.classList.add("catalog-item");

    const imageSrc = getImageSrc(product.shortDesc); // Получаем изображение на основе shortDesc

    productCard.innerHTML = `
      <a href="../product page/product_page.html" class="product" data-id="${product.id}" data-product-type-id="${product.productTypeId}">
        <img src="${imageSrc}" width="200" height="200" alt="Продукт">
        <p>${product.name}</p>
      </a>
      <p class="price">${product.price} руб.</p>
      <a href="#" class="inBasket" data-id="${product.id}" data-product-type-id="${product.productTypeId}">В корзину</a>
    `;

    // Добавляем обработчик клика на ссылку продукта
    productCard
      .querySelector(".product")
      .addEventListener("click", function (event) {
        event.preventDefault();
        const productId = this.getAttribute("data-id");
        const productTypeId = this.getAttribute("data-product-type-id");

        // Сохраняем ID продукта и его тип в localStorage
        localStorage.setItem("selectedProductId", productId);
        localStorage.setItem("selectedProductTypeId", productTypeId);

        // Переходим на страницу продукта
        window.location.href = this.href;
      });

    // Добавляем обработчик клика на кнопку "В корзину"
    productCard
      .querySelector(".inBasket")
      .addEventListener("click", function (event) {
        event.preventDefault();
        const productId = this.getAttribute("data-id"); // Используем атрибут data-id

        // Проверяем авторизацию пользователя
        if (!authToken) {
          alert("Необходимо войти в систему для добавления товара в корзину");
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
          .then((cart) => {
            // Проверим, что корзина была успешно получена
            console.log("Корзина:", cart);

            // Добавляем товар в корзину
            return fetch(
              "http://localhost:8080/http://localhost:5277/api/shopcart/add",
              {
                method: "PUT",
                headers: {
                  "Content-Type": "application/json",
                  Authorization: `Bearer ${authToken}`,
                },
                body: JSON.stringify(productId), // Отправляем только ID продукта
              }
            );
          })
          .then((response) => {
            if (response.ok) {
              alert("Товар добавлен в корзину");
            } else {
              return response.json().then((error) => {
                throw new Error(
                  error.message || "Ошибка при добавлении товара в корзину"
                );
              });
            }
          })
          .catch((error) => {
            console.error("Ошибка при выполнении операции с корзиной:", error);
            alert(
              "Ошибка при выполнении операции с корзиной: " + error.message
            );
          });
      });

    return productCard;
  }

  // Функция для загрузки данных продуктов
  async function loadProducts() {
    try {
      const response = await fetch(
        "http://localhost:8080/http://localhost:5277/api/product?skip=0&take=9"
      );
      const data = await response.json();

      if (data && data.data) {
        // Сортируем данные по убыванию цены
        const sortedData = data.data.sort((a, b) => b.price - a.price);

        // Создаем карточки продуктов и добавляем их в каталог
        sortedData.forEach((product) => {
          const productCard = createProductCard(product);
          catalogList.appendChild(productCard);
        });
      } else {
        console.error("Ошибка в формате данных:", data);
      }
    } catch (error) {
      console.error("Ошибка при загрузке продуктов:", error);
    }
  }

  loadProducts();
});
