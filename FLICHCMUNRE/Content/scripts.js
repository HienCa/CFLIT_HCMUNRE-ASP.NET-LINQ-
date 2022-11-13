const addBtn = document.getElementById("addBtn");
const addForm = document.getElementById("addForm");
function ToggleBtn() {
    if (addForm.classList.contains("hide")) {
        addForm.classList.remove("hide");
        addBtn.value = "Ẩn Form"
    }
    else {
        addForm.classList.add("hide");
        addBtn.value = "Thêm Mới"
    }
}
addBtn.addEventListener("click", ToggleBtn);



const boxchat = document.querySelector("#box");
const mess = document.querySelector("#mess");
const exit = document.querySelector("#exit");
function checkClick() {
    if (boxchat.classList.contains("active")) {
        boxchat.classList.remove("active");
    } else {
        boxchat.classList.add("active");
    }
}
mess.addEventListener("click", checkClick);
exit.addEventListener("click", checkClick);
