const footer = document.getElementById("footer");
const input_email_login = document.querySelector("#user_email");
const input_password = document.querySelector("#user_pass");


switch (true) {
    case document.URL.indexOf("hyper_stone") > 0:
        footer.style.display = "none";
        break;
    case document.URL.indexOf("confirm_sucess")         > 0:
    case document.URL.indexOf("forgot-pass")            > 0:
    case document.URL.indexOf("email_send")             > 0:
    case document.URL.indexOf("newmailcode")            > 0:
    case document.URL.indexOf("privatearea")            > 0:
    case document.URL.indexOf("05_download")            > 0:
    case document.URL.indexOf("01_project")             > 0:
    case document.URL.indexOf("reset_pass")             > 0:
    case document.URL.indexOf("alter_pass_success")     > 0:
    case document.URL.indexOf("registration")           > 0:
    case document.URL.indexOf("privatearea")            > 0:
    case document.URL.indexOf("login")                  > 0:
        footer.classList.add("fixed-bottom");
    
}

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
