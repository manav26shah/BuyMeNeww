document.getElementById("register-button").addEventListener("click", register);

function register() {
  // get all the values
  var firstname = document.getElementById("fName").value;
  var lastname = document.getElementById("lName").value;
  var email = document.getElementById("email").value;
  var phone = document.getElementById("phone").value;
  var password = document.getElementById("password").value;
  var address = document.getElementById("address").value;

  // call API
  fetch("https://localhost:44308/api/Authentication/Register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email: email,
      password: password,
      firstName: firstname,
      lastName: lastname,
      phoneNumber: phone,
      address: address,
    }),
  }).then((res) => {
    if (res.status == 200) {
      res.json().then((data) => {
        console.log(data);
      });
      // alert("Registraion success!");
      window.location = "login.html";
    } else if (res.status == 417) {
      res.json().then((data) => {
        console.log(data.message[0]);
        document.getElementById("error-text").classList.add("error-text-show");
        document
          .getElementById("error-text")
          .classList.remove("error-text-hide");
        document.getElementById("error-text").textContent = data.message[0];
      });
    } else {
      res.json().then((data) => {
        console.log(data);
        document.getElementById("error-text").classList.add("error-text-show");
        document
          .getElementById("error-text")
          .classList.remove("error-text-hide");
        document.getElementById("error-text").textContent =
          "Some error Occurred!";
      });
    }
  });
}
