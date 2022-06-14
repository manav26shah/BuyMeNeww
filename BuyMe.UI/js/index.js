function checkIfLoggedin() {
  var authToken = localStorage.getItem("authToken");
  if (authToken) {
    fetch("https://localhost:44308/api/Authentication/details", {
      method: "GET",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("authToken"),
      },
    }).then((res) => {
      //console.log("In response");
      console.log(res);
      if (res.status == 200) {
        res.json().then((data) => {
          document.getElementById("welcome-text").innerText =
            "Welcome, " + data.firstName;
        });
        getProducts();
      } else if (res.status == 401) {
        logout();
      } else if (res.status == null) {
        window.location = "login.html";
      } else {
        alert("Error occured");
      }
    });
  } else if(res.status == 401){
    logout();
  } else {
    window.location = "login.html";
  }
}

document.getElementById("btn-logout").addEventListener("click", logout);

function logout() {
  localStorage.removeItem("authToken");
  window.location = "login.html";
}
