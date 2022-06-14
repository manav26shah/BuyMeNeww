function GetOrders() {
    var authToken = localStorage.getItem("authToken");

    fetch("https://localhost:44308/api/Order", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${authToken}`,
        },  
    }).then((res) => {
        if(res.status == 200)
        {
            res.json().then((data) => {
                document.getElementById("order-date").innerHTML =
                data.orderItems[0].orderDate;
        document.getElementById("total-amount").innerHTML =
          "Rs. " + data.orderItems[0].orderAmount.toLocaleString("en-IN");
            });
        }
    });
} 

GetOrders();

document.getElementById("btn-logout").addEventListener("click", logout);

function logout() {
  localStorage.removeItem("authToken");
  window.location = "login.html";
}