//function to update profile image
function updateProfileImage(event) {
    const image = document.getElementById("profile-img");
    image.src = URL.createObjectURL(event.target.files[0]);
}

//function to validate form
function validateForm(event) {
    const fullName = document.getElementById('full-name').value;
    const email = document.getElementById('email').value;
    const address = document.getElementById('address').value;

    if (fullName === "" || email === "" || address === "") {
        alert("Please fill in all required fields.");
        event.preventDefault();
    }
}

//triggering all the function while loading the page
window.onload = function() {
    document.getElementById('profile-picture').addEventListener('change', updateProfileImage);
    document.querySelector('form').addEventListener('submit', validateForm);
}