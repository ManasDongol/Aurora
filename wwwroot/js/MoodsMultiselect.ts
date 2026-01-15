const dropdown = document.getElementById("moodDropdown");
const checkboxes = dropdown.querySelectorAll("input[type='checkbox']");
const selectedText = document.getElementById("selectedText");

function toggleDropdown() {
    dropdown.classList.toggle("open");
}