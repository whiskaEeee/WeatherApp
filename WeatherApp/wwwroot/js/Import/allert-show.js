function showAlert(message) {
    if (!message) return;

    const successMessage = "успешно обработан";

    let alertClass = message.includes(successMessage) ? "alert-success" : "alert-danger";

    let alertDiv = document.createElement("div");
    alertDiv.className = `alert ${alertClass} alert-dismissible fade show mt-2`;
    alertDiv.role = "alert";
    alertDiv.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Закрыть"></button>
        `;

    document.getElementById("alert-container").appendChild(alertDiv);

    setTimeout(() => {
        alertDiv.classList.remove("show");
        alertDiv.classList.add("fade");
        setTimeout(() => alertDiv.remove(), 500);
    }, 3000);
}