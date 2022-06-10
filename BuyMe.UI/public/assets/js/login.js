document.getElementById("login-button").addEventListener("click", login);

function login() {
  var email = document.getElementById("email").value;
  var password = document.getElementById("password").value;
  console.log("In Login");

  fetch("https://localhost:44308/api/Authentication/Login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email: email,
      password: password,
    }),
  }).then((res) => {
    if (res.status == 200) {
      res.json().then((data) => {
        var authToken = data.data.token;
        console.log(authToken);
        localStorage.setItem("authToken", authToken);
        window.location = "index.html";
      });
    } else {
      res.json().then((data) => {
        document.getElementById("error-text").classList.add("error-text-show");
        document
          .getElementById("error-text")
          .classList.remove("error-text-hide");
        document.getElementById("error-text").textContent = data.message[0];
      });
    }
  });
}
