//Function to set the minimum date.
export function setMinDate() {
    const today = new Date();
    const minDate = today.toISOString().split('T')[0]; // Format to YYYY-MM-DD
    const dateInput = document.getElementById("task-date");
    dateInput.setAttribute("min", minDate);
    dateInput.value = minDate;
}
