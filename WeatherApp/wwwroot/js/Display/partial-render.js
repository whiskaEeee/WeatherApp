document.addEventListener("DOMContentLoaded", function () {
    setupAjaxFilter();
    setupAjaxPagination();
});

function setupAjaxFilter() {
    document.getElementById("filterForm").addEventListener("submit", function (event) {
        event.preventDefault();

        let selectedMonths = getSelectedValues("month-checkbox");
        let selectedYears = getSelectedValues("year-checkbox");

        document.getElementById("selectedMonths").value = selectedMonths.join(",");
        document.getElementById("selectedYears").value = selectedYears.join(",");

        let formData = new FormData(this);
        let queryString = new URLSearchParams(formData).toString();

        fetch(this.action + "?" + queryString, {
            method: "GET",
            headers: { "X-Requested-With": "XMLHttpRequest" }
        })
            .then(response => response.text())
            .then(html => {
                document.getElementById("weatherReportsContainer").innerHTML = html;
                setupAjaxPagination();
            })
            .catch(error => console.error("Ошибка:", error));
    });
}

function setupAjaxPagination() {
    document.querySelectorAll("#weatherReportsContainer .pagination a").forEach(link => {
        link.addEventListener("click", function (event) {
            event.preventDefault();

            let url = new URL(this.href);
            let params = new URLSearchParams(url.search);

            let formData = new FormData(document.getElementById("filterForm"));
            for (let [key, value] of formData.entries()) {
                params.set(key, value);
            }

            fetch(url.pathname + "?" + params.toString(), {
                method: "GET",
                headers: { "X-Requested-With": "XMLHttpRequest" }
            })
                .then(response => response.text())
                .then(html => {
                    document.getElementById("weatherReportsContainer").innerHTML = html;
                    setupAjaxPagination();
                })
                .catch(error => console.error("Ошибка:", error));
        });
    });
}

function getSelectedValues(className) {
    return Array.from(document.querySelectorAll("." + className + ":checked"))
        .map(checkbox => checkbox.value);
}
