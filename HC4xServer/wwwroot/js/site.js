const input_email_login = document.querySelector("#user_email");
const input_password = document.querySelector("#user_pass");

input_email_login.addEventListener("click", () => {
    const message = document.getElementById("message");
    if (message != null) {
        message.innerHTML = "";
    }
})
input_password.addEventListener("click", () => {
    const message = document.getElementById("message");
    if (message != null) {
        message.innerHTML = "";
    }
})

//input file - foto do perfil

let photo = document.getElementById("perfil-image");
let file = document.getElementById("file-image");
let button = document.getElementById("button-input-file");

photo.addEventListener("click", () =>{
    file.click();
});

button.addEventListener("click", () =>{
    file.click();
});

file.addEventListener("change", (event) =>{
    if (event.target.files.length > 0){
        var src = URL.createObjectURL(event.target.files[0]);
        photo.src = src;
    }
});
