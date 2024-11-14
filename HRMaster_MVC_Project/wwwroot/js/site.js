function filterTable() {
    const searchInput = document.querySelector('input[name="table_search"]');
    const table = document.querySelector('.table-hover tbody');
    const searchValue = searchInput.value.toLowerCase();
    const rows = table.getElementsByTagName("tr");

    Array.from(rows).forEach(function (row) {
        const nameCell = row.getElementsByTagName("td")[1]; 
        const titleCell = row.getElementsByTagName("td")[2]; 

        const nameText = nameCell.textContent.toLowerCase();
        const titleText = titleCell.textContent.toLowerCase();

        if (nameText.includes(searchValue) || titleText.includes(searchValue)) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });
}