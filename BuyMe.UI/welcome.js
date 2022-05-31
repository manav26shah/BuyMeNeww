
function checkIfLoggedin(){
var authToken=localStorage.getItem("authToken")
if(authToken){
    fetch('https://localhost:44332/api/user/getdetails',{
        method:'GET',
        headers:{
            "Authorization": "Bearer " + localStorage.getItem('authToken')
        },
    
    }).then(res=>{
        if(res.status==200){
            res.json().then(data=>{
              document.getElementById("name").innerText="Welcome, " + data.firstName + " " + data.lastName;
            })
        }else{
            swal("Error occured","","error");
        }
    })
}
else{
    window.location="index.html";
}
}

document.getElementById("btn-logout").addEventListener("click",logout);

function logout(){
    localStorage.removeItem("authToken");
    window.location="index.html";
}
