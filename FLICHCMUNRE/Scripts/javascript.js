const addBtn = document.getElementById("addBtn");
const addForm = document.getElementById("addForm");
function ToggleBtn(){
    if (addForm.classList.contains("hide")) {
      addForm.classList.remove("hide");
      addBtn.value="Ẩn Form"
   }
   else {
      addForm.classList.add("hide");
      addBtn.value="Thêm Mới"
   }
}
addBtn.addEventListener("click", ToggleBtn);



