document.getElementById("register-button").addEventListener("click", register);
function register() {
    // get all the values
    var firstname = document.getElementById("firstname").value;
    var lastname = document.getElementById("lastname").value;
    var email = document.getElementById("email").value;
    var phone = document.getElementById("phone").value;
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("confirm-password").value;

    // validations
    if (confirmPassword != password) {
        swal("Register Error !", "Password and confirm password do not match", "error");
        return;
    }
    // call API
    fetch('https://localhost:44332/api/user', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            "firstName": firstname,
            "lastName": lastname,
            "phoneNumber": phone,
            "emailId": email,
            "password": password
        })
    }).then(res => {
        if (res.status == 200) {
            swal("Registraion success !","Click on login to continue with login","success");
          }
        else{
            res.json().then(data=>{
                swal("Error",data.message[0],"error");
            })
        }
        });
}
