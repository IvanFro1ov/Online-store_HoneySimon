async function handleLogin(event) {
  event.preventDefault();

  const form = document.getElementById("loginForm");
  const formData = new FormData(form);
  const data = {
    emailOrUserName: formData.get("emailOrUserName"),
    password: formData.get("password"),
  };

  try {
    const response = await fetch(
      "http://localhost:8080/http://localhost:5277/api/auth/login",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      }
    );

    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }

    const result = await response.json();
    console.log("Login successful:", result);
    alert("Login successful!");

    if (result.id && result.token) {
      const userId = result.id;
      const authToken = result.token.token; // Извлекаем строку токена из объекта

      console.log("AuthToken after login:", authToken); // Проверка токена после логина

      console.log("Saving userId:", userId);
      console.log("Saving authToken:", authToken);

      // Сохранение ID и токена в localStorage
      localStorage.setItem("userId", userId);
      localStorage.setItem("authToken", authToken);

      // Перенаправление на страницу профиля после успешного логина
      window.location.href = "../profile/profile.html";
    } else {
      throw new Error("Invalid response structure");
    }
  } catch (error) {
    console.error("There was a problem with the login:", error);
    alert("Login failed: " + error.message);
  }
}
