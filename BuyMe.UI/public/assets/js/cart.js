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
        var cartItemsDiv = document.getElementById("cart-items");
        var cartItems = data.data.cartItems
          .map((item) => {
            return `<div class="card mb-3" >
            <div class="card-body">
              <div class="d-flex justify-content-between">
                <div class="d-flex flex-row align-items-center">
                  <div>
                    <img
                      src="${item.image}"
                      class="img-fluid rounded-3"
                      alt="Shopping item"
                      style="width: 65px; height: 65px; object-fit: contain"
                    />
                  </div>
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
                  <a href="#!" style="color: #cecece"
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
        // document.querySelectorAll(".add-to-cart").forEach((i) =>
        //   i.addEventListener("click", () => {
        //     var select = document.getElementById(`select-product-${i.id}`);
        //     var value = select.options[select.selectedIndex].value;
        //     addItemToCart(i.id, value);
        //   })
        // );
      });
    }
  });
}

getCartItems();
