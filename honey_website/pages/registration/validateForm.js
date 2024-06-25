document.addEventListener("DOMContentLoaded", function () {
  const form = document.querySelector("form");

  form.addEventListener("submit", async function (event) {
    event.preventDefault(); // Предотвращаем стандартное поведение отправки формы

    if (!validateForm(event)) {
      return; // Если валидация не прошла, прекращаем выполнение
    }

    // Собираем данные из формы
    const formData = new FormData(form);
    const data = {
      firstname: formData.get("firstname"),
      name: formData.get("name"),
      lastname: formData.get("lastname"),
      age: parseInt(formData.get("age"), 10),
      email: formData.get("email"),
      userName: formData.get("userName"),
      password: formData.get("password"),
    };

    try {
      const response = await fetch(
        "http://localhost:8080/http://localhost:5277/api/auth/register",
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
      console.log("Registration successful:", result);
      alert("Registration successful!");
      form.reset();
    } catch (error) {
      console.error("There was a problem with the registration:", error);
      alert("Registration failed: " + error.message);
    }
  });
});

function validateForm(event) {
  var ageInput = document.getElementById("age");
  var age = parseInt(ageInput.value, 10);

  if (isNaN(age) || age < 1 || age > 110) {
    alert("Возраст должен быть в диапазоне от 1 до 110.");
    event.preventDefault();
    return false;
  }
  return true;
}

function validateAgeInput() {
  var ageInput = document.getElementById("age");
  if (ageInput.value.length > 3) {
    ageInput.value = ageInput.value.slice(0, 3);
  }
}
