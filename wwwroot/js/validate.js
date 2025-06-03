document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const emailInput = document.querySelector("#email");
    const passwordInput = document.querySelector("#password");

    form.addEventListener("submit", function (e) {
        let isValid = true;

        // Remove old error messages
        document.querySelectorAll(".validation-error").forEach(el => el.remove());

        // Email validation
        const emailRegex = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
        if (!emailRegex.test(emailInput.value.trim())) {
            showError(emailInput, "Please enter a valid email.");
            isValid = false;
        }

        // Password required
        if (passwordInput.value.trim().length === 0) {
            showError(passwordInput, "Password is required.");
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault(); // prevent form from submitting
        }
    });

    function showError(input, message) {
        const error = document.createElement("div");
        error.className = "validation-error";
        error.style.color = "red";
        error.style.fontSize = "0.9em";
        error.style.marginTop = "4px";
        error.innerText = message;
        input.parentElement.appendChild(error);
    }
});
