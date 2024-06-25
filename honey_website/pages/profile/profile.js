document.addEventListener("DOMContentLoaded", async () => {
  const userNameElement = document.getElementById("userName");
  const userEmailElement = document.getElementById("userEmail");

  const userId = localStorage.getItem("userId");
  const authToken = localStorage.getItem("authToken");
  const orderId = localStorage.getItem("orderId");
  const deliveryMethod = localStorage.getItem("shippingMethod");

  if (userId && authToken) {
    try {
      const userResponse = await fetch(
        `http://localhost:8080/http://localhost:5277/api/user/${userId}`,
        {
          method: "GET",
          headers: {
            Authorization: "Bearer " + authToken,
            Accept: "application/json",
          },
        }
      );

      if (!userResponse.ok) {
        throw new Error(
          "Network response was not ok " + userResponse.statusText
        );
      }

      const userData = await userResponse.json();
      console.log("User data retrieved:", userData);

      if (userData.userName && userData.email) {
        userNameElement.textContent = userData.userName;
        userEmailElement.textContent = userData.email;
      } else {
        throw new Error("Incomplete user data received from server");
      }

      if (orderId) {
        const orderResponse = await fetch(
          `http://localhost:8080/http://localhost:5277/api/orders/${orderId}`,
          {
            method: "GET",
            headers: {
              Authorization: "Bearer " + authToken,
              Accept: "application/json",
            },
          }
        );

        if (!orderResponse.ok) {
          throw new Error(
            "Network response was not ok " + orderResponse.statusText
          );
        }

        const orderData = await orderResponse.json();
        console.log("Order data retrieved:", orderData);

        displayOrder(orderData);
      } else {
        console.error("Order ID not found in localStorage");
        alert("Order ID not found in localStorage");
      }
    } catch (error) {
      console.error(
        "There was a problem retrieving the user data or order:",
        error
      );
      alert("Failed to retrieve user data or order: " + error.message);
    }
  } else {
    console.error("User ID or token not found in localStorage");
    alert("User ID or token not found in localStorage");
  }

  function displayOrder(order) {
    const ordersContainer = document.querySelector(".order div:last-child");

    const orderElement = document.createElement("ul");
    orderElement.className = "service-info";

    const orderIdElement = document.createElement("li");
    orderIdElement.textContent = order.id.slice(0, 8);

    const orderDateElement = document.createElement("li");
    orderDateElement.textContent = new Date(
      order.dateCreated
    ).toLocaleDateString();

    const orderStatusElement = document.createElement("li");
    const statusImage = document.createElement("img");
    statusImage.src = "../../img/link.svg";
    statusImage.addEventListener("click", () => {
      openDeliveryMethodSite();
    });
    orderStatusElement.appendChild(statusImage);

    const orderTotalElement = document.createElement("li");
    const totalAmount = order.products.reduce(
      (sum, product) => sum + product.price,
      0
    );
    orderTotalElement.textContent = totalAmount + " руб.";

    orderElement.appendChild(orderIdElement);
    orderElement.appendChild(orderDateElement);
    orderElement.appendChild(orderStatusElement);
    orderElement.appendChild(orderTotalElement);

    ordersContainer.appendChild(orderElement);
  }

  function openDeliveryMethodSite() {
    let deliveryUrl;

    switch (deliveryMethod) {
      case "russian_post":
        deliveryUrl = "https://www.pochta.ru/";
        break;
      case "cdek":
        deliveryUrl = "https://www.cdek.ru";
        break;
      case "boxberry":
        deliveryUrl = "https://boxberry.ru/";
        break;
      default:
        alert("Unknown delivery method: " + deliveryMethod); // Показываем неизвестный метод в алерте
        return;
    }

    window.open(deliveryUrl, "_blank");
  }
});
