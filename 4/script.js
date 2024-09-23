function updateProfileImage(event) {
    const image = document.getElementById("profile-img");
    image.src = URL.createObjectURL(event.target.files[0]);
}

function validateForm(event) {
    const fullName = document.getElementById('full-name').value;
    const email = document.getElementById('email').value;
    const address = document.getElementById('address').value;

    if (fullName === "" || email === "" || address === "") {
        alert("Please fill in all required fields.");
        event.preventDefault();
    }
}

window.onload = function() {
    document.getElementById('profile-picture').addEventListener('change', updateProfileImage);
    document.querySelector('form').addEventListener('submit', validateForm);
}