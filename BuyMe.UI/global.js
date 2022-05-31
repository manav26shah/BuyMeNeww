function checkIfLoggedIn(){
    var authToken=localStorage.getItem("authToken");
    if(authToken){
        window.location="welcome.html";
    }
}