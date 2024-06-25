function sendMail() {
  let parms = {
    from_name: document.getElementById("from_name").value,
    from_email: document.getElementById("from_email").value,
    message: document.getElementById("message").value,
  };

  emailjs.send("service_gnbusol", "template_tfsewmq", parms).then(
    function (response) {
      console.log("SUCCESS!", response.status, response.text);
      alert("Ваше сообщение отправлено!");
    },
    function (error) {
      console.error("FAILED...", error);
      alert("Ошибка при отправке сообщения. Попробуйте еще раз.");
    }
  );
}
