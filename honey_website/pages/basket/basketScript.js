document.addEventListener("DOMContentLoaded", function () {
  const cartItemsContainer = document.querySelector(".purchases");
  const footer = document.querySelector(".main-footer");
  const authToken = localStorage.getItem("authToken");

  if (!authToken) {
    alert("Необходимо войти в систему для просмотра корзины");
    return;
  }

  // Функция для получения данных корзины с сервера
  function fetchCartItems() {
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
      .then((data) => {
        const processedData = processCartData(data.products);
        displayCartItems(processedData);
      })
      .catch((error) => {
        console.error("Ошибка при получении данных корзины:", error);
        alert("Ошибка при получении данных корзины: " + error.message);
      });
  }

  // Функция для обработки данных корзины
  function processCartData(products) {
    const productMap = new Map();

    products.forEach((product) => {
      if (productMap.has(product.id)) {
        const existingProduct = productMap.get(product.id);
        existingProduct.quantity += 1;
      } else {
        productMap.set(product.id, { ...product, quantity: 1 });
      }
    });

    return Array.from(productMap.values());
  }

  // Функция для отображения товаров в корзине
  function displayCartItems(cart) {
    cartItemsContainer.innerHTML = "";

    let totalPrice = 0;

    if (cart.length === 0) {
      const emptyCartMessage = document.createElement("div");
      emptyCartMessage.textContent = "Ваша корзина пуста";
      emptyCartMessage.classList.add("empty-cart-message");
      cartItemsContainer.appendChild(emptyCartMessage);

      footer.style.marginTop = "580px";
      return;
    }

    const tableHeader = document.createElement("div");
    tableHeader.classList.add("table-header");
    tableHeader.innerHTML = `
          <div class="productHeading">НАИМЕНОВАНИЕ</div>
          <div class="priceHeading">ЦЕНА</div>
          <div class="quantityHeading">КОЛИЧЕСТВО</div>
      `;
    cartItemsContainer.appendChild(tableHeader);

    cart.forEach((product) => {
      const { id, name, price, quantity } = product;

      const cartItemElement = document.createElement("div");
      cartItemElement.classList.add("item");

      const productNameElement = document.createElement("div");
      productNameElement.textContent = name;
      productNameElement.classList.add("product-name");
      cartItemElement.appendChild(productNameElement);

      const priceElement = document.createElement("div");
      priceElement.textContent = `${price} руб.`;
      priceElement.classList.add("price");
      cartItemElement.appendChild(priceElement);

      const quantityControls = document.createElement("div");
      quantityControls.classList.add("quantity-controls");

      const decreaseButton = document.createElement("button");
      decreaseButton.textContent = "-";
      decreaseButton.classList.add("decrease-button");
      decreaseButton.addEventListener("click", function () {
        decreaseCartItemQuantity(id);
      });
      quantityControls.appendChild(decreaseButton);

      const quantityElement = document.createElement("div");
      quantityElement.textContent = quantity;
      quantityElement.classList.add("quantity");
      quantityControls.appendChild(quantityElement);

      const increaseButton = document.createElement("button");
      increaseButton.textContent = "+";
      increaseButton.classList.add("increase-button");
      increaseButton.addEventListener("click", function () {
        increaseCartItemQuantity(id);
      });
      quantityControls.appendChild(increaseButton);

      cartItemElement.appendChild(quantityControls);

      const deleteButton = document.createElement("button");
      deleteButton.textContent = "×";
      deleteButton.classList.add("delete-button");
      deleteButton.addEventListener("click", function () {
        deleteCartItemCompletely(id, quantity);
      });
      cartItemElement.appendChild(deleteButton);

      cartItemsContainer.appendChild(cartItemElement);

      const productTotalPrice = parseFloat(price) * quantity;
      totalPrice += productTotalPrice;
    });

    const totalPriceElement = document.createElement("div");
    totalPriceElement.textContent = `ИТОГО: ${totalPrice} руб.`;
    totalPriceElement.classList.add("total-price");
    cartItemsContainer.appendChild(totalPriceElement);

    const placeOrderButton = document.createElement("a");
    placeOrderButton.href = "#";
    placeOrderButton.textContent = "Оформить заказ";
    placeOrderButton.classList.add("placeOrder");
    placeOrderButton.addEventListener("click", function (event) {
      event.preventDefault();
      placeOrder();
    });
    cartItemsContainer.appendChild(placeOrderButton);

    footer.style.marginTop = "200px";
  }

  function increaseCartItemQuantity(productId) {
    fetch("http://localhost:8080/http://localhost:5277/api/shopcart/add", {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${authToken}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(productId),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Ошибка при увеличении количества товара");
        }
        return response.json();
      })
      .then(() => {
        fetchCartItems();
      })
      .catch((error) => {
        console.error("Ошибка при увеличении количества товара:", error);
        alert("Ошибка при увеличении количества товара: " + error.message);
      });
  }

  function decreaseCartItemQuantity(productId) {
    fetch("http://localhost:8080/http://localhost:5277/api/shopcart/remove", {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${authToken}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(productId),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Ошибка при уменьшении количества товара");
        }
        return response.json();
      })
      .then(() => {
        fetchCartItems();
      })
      .catch((error) => {
        console.error("Ошибка при уменьшении количества товара:", error);
        alert("Ошибка при уменьшении количества товара: " + error.message);
      });
  }

  function deleteCartItemCompletely(productId, quantity) {
    const deleteRequests = [];
    for (let i = 0; i < quantity; i++) {
      deleteRequests.push(
        fetch(
          "http://localhost:8080/http://localhost:5277/api/shopcart/remove",
          {
            method: "PUT",
            headers: {
              Authorization: `Bearer ${authToken}`,
              "Content-Type": "application/json",
            },
            body: JSON.stringify(productId),
          }
        )
      );
    }

    Promise.all(deleteRequests)
      .then((responses) => {
        responses.forEach((response) => {
          if (!response.ok) {
            throw new Error("Ошибка при удалении товара");
          }
        });
        fetchCartItems();
      })
      .catch((error) => {
        console.error("Ошибка при полном удалении товара:", error);
        alert("Ошибка при полном удалении товара: " + error.message);
      });
  }

  // Функция для оформления заказа
  function placeOrder() {
    fetch("http://localhost:8080/http://localhost:5277/api/orders", {
      method: "POST",
      headers: {
        accept: "*/*",
        Authorization: `Bearer ${authToken}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify({}),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Ошибка при оформлении заказа");
        }
        return response.json();
      })
      .then((data) => {
        // Сохраняем id заказа в localStorage
        localStorage.setItem("orderId", data.id);
        // Перенаправляем на страницу заказа
        window.location.href = "../order/order.html";
      })
      .catch((error) => {
        console.error("Ошибка при оформлении заказа:", error);
        alert("Ошибка при оформлении заказа: " + error.message);
      });
  }

  fetchCartItems();
});
