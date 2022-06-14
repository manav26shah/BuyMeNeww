function getCartItems() {
  var authToken = localStorage.getItem("authToken");

  fetch("https://localhost:44308/api/cart", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${authToken}`,
    },
  }).then((res) => {
    if (res.status == 200) {
      res.json().then((data) => {
        if(data.data.cartItems == null || data.data.cartItems == ""){
          alert("Cart is Empty");
          window.location = "index.html";
        }
        var cartItemsDiv = document.getElementById("cart-items");
        var cartItems = data.data.cartItems
          .map((item) => {
            return `<div class="card mb-3" >
            <div class="card-body">
              <div class="d-flex justify-content-between">
                <div class="d-flex flex-row align-items-center">
                 
                  <div class="ms-3">
                    <h5>${item.name}</h5>
                  </div>
                </div>
                <div class="d-flex flex-row align-items-center">
                  <div style="width: 50px">
                    <h5 class="fw-normal mb-0">${item.count}</h5>
                  </div>
                  <div style="width: 120px">
                    <h5 class="mb-0"><i class="fa fa-inr"></i> ${item.mrpAmount.toLocaleString(
                      "en-IN"
                    )}</h5>
                  </div>
                  <a href="#" onclick="removeProduct(${item.productId})"
                    ><i class="fas fa-trash-alt"></i
                  ></a>
                </div>
              </div>
            </div>
          </div>`;
          })
          .join("");
        cartItemsDiv.insertAdjacentHTML("afterbegin", cartItems);
        document.getElementById("total-quantity").innerHTML =
          data.data.numberOfProducts;
        document.getElementById("total-amount").innerHTML =
          "Rs. " + data.data.totalAmount.toLocaleString("en-IN");
        console.log(data.data.totalAmount.toLocaleString("en-IN"));
        document.getElementById("checkout-amount").innerHTML =
          "Rs. " + data.data.totalAmount.toLocaleString("en-IN");
      });
    }
  });
}

getCartItems();

document.getElementById("btn-logout").addEventListener("click", logout);

function logout() {
  localStorage.removeItem("authToken");
  window.location = "login.html";
}

function removeProduct(id)
{
  var authToken = localStorage.getItem("authToken");
  
  console.log(id); 
  fetch("https://localhost:44308/api/Cart/" + id, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${authToken}`,
    },
  }).then((res) => {
    if (res.status == 200) {
      getCartItems();
    }
    else
    {
      alert("Something went wrong!");
    }
  });
}