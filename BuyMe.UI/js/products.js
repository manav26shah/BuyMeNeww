function getProducts() {
  console.log("In get products");
  fetch("https://localhost:44308/api/products?limit=20&page=1", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  }).then((res) => {
    if (res.status == 200) {
      res.json().then((data) => {
        var productDiv = document.getElementById("products");
        var products = data.products
          .map((product) => {
            var orderAmount = "";
            for (i = 0; i < product.maxOrderQuantity; i++) {
              orderAmount = orderAmount + `<option> ${i + 1} </option>`;
            }
            return `<div class="col mb-5"> 
                <div class="card shadow h-100"> <img class="card-img-top embed-responsive-item" src="${
                  product.image
                }" alt="..."/> 
                    <div class="card-body p-2">
                        <div class="text-center fw-light"> 
                            <h5 class="product-name text-wrap fw-normal">${
                              product.name
                            }</h5 > 
                            <i class="fa fa-inr"></i> ${product.mrpAmount.toLocaleString(
                              "en-IN"
                            )}
                        </div>
                    </div> 
                <div class="card-footer justify m-auto bg-transparent text-center border-top-0">
                <div class="input-group mb-3">
                <div class="input-group-prepend">
                    <button class="btn btn-dark mx-1 add-to-cart" id="${
                      product.productId
                    }" type="button">Add to Cart</button>
                </div>
                <div class="form-group mx-1">
                    <select class="form-control" id="select-product-${
                      product.productId
                    }">
                    ${orderAmount}
                    </select>
                </div>
                </div>
            </div>
            </div>
        </div>`;
          })
          .join("");
        productDiv.insertAdjacentHTML("afterbegin", products);

        document.querySelectorAll(".add-to-cart").forEach((i) =>
          i.addEventListener("click", () => {
            var select = document.getElementById(`select-product-${i.id}`);
            var value = select.options[select.selectedIndex].value;
            addItemToCart(i.id, value);
          })
        );
      });
    }
  });
}

function addItemToCart(productId, count) {
  var authToken = localStorage.getItem("authToken");
  fetch("https://localhost:44308/api/cart", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${authToken}`,
    },
    body: JSON.stringify({
      ProductId: parseInt(productId),
      Count: parseInt(count),
    }),
  }).then((res) => {
    if (res.status == 200) {
      alert("Item successfully added to cart");
    } else {
      alert("Some error occurred. Try again later.");
    }
  });
}



document.getElementById("searchProduct").addEventListener("click", searchProducts);

function searchProducts()
{
  var query = document.getElementById("search").value;
  var authToken=localStorage.getItem("authToken");
  console.log(query);
  if(query == null || query == "")
  {
    console.log("Hii");
    getProducts();
  }
  else{
    fetch("https://localhost:44308/api/Products/search/" + query, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${authToken}`,
      },
    }).then((res) => {
      if (res.status == 200) {
        res.json().then((data) => {
          var productDiv = document.getElementById("searchProducts");
          var products = data.products
            .map((product) => {
              var orderAmount = "";
              for (i = 0; i < product.maxOrderQuantity; i++) {
                orderAmount = orderAmount + `<option> ${i + 1} </option>`;
              }
              return `<div class="col mb-5"> 
                  <div class="card shadow h-100"> <img class="card-img-top embed-responsive-item" src="${
                    product.image
                  }" alt="..."/> 
                      <div class="card-body p-2">
                          <div class="text-center fw-light"> 
                              <h5 class="product-name text-wrap fw-normal">${
                                product.name
                              }</h5 > 
                              <i class="fa fa-inr"></i> ${product.mrpAmount.toLocaleString(
                                "en-IN"
                              )}
                          </div>
                      </div> 
                  <div class="card-footer justify m-auto bg-transparent text-center border-top-0">
                  <div class="input-group mb-3">
                  <div class="input-group-prepend">
                      <button class="btn btn-dark mx-1 add-to-cart" id="${
                        product.productId
                      }" type="button">Add to Cart</button>
                  </div>
                  <div class="form-group mx-1">
                      <select class="form-control" id="select-product-${
                        product.productId
                      }">
                      ${orderAmount}
                      </select>
                  </div>
                  </div>
              </div>
              </div>
          </div>`;
            })
            .join("");
          productDiv.insertAdjacentHTML("afterbegin", products);

          document.querySelectorAll(".add-to-cart").forEach((i) =>
            i.addEventListener("click", () => {
              var select = document.getElementById(`select-product-${i.id}`);
              var value = select.options[select.selectedIndex].value;
              addItemToCart(i.id, value);
            })
          );
        });
      }
    });
  }
}