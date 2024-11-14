
function showApproveModal(id) {
    $('#approveConfirmButton').off('click').on('click', function () {
        $.ajax({
            url: `${approveUrl}/${id}`,
            type: 'POST',
            success: function (result) {
                location.reload(); 
            },
            error: function () {
                alert('Onaylama işlemi başarısız!');
            }
        });
    });
    $('#approveModal').modal('show');
}

function showRejectModal(id) {
    $('#rejectConfirmButton').off('click').on('click', function () {
        $.ajax({
            url: `${rejectUrl}/${id}`,
            type: 'POST',
            success: function (result) {
                location.reload(); 
            },
            error: function () {
                alert('Reddetme işlemi başarısız!');
            }
        });
    });
    $('#rejectModal').modal('show');
}


function showCancelModal(id) {
    $('#cancelConfirmButton').off('click').on('click', function () {
        $.ajax({
            url: `${cancelUrl}/${id}`,
            type: 'POST',
            success: function (result) {
                location.reload(); 
            },
            error: function () {
                alert('İptal işlemi başarısız!');
            }
        });
    });
    $('#cancelModal').modal('show');
}

function normalizeString(str) {
    return str.toLowerCase()
    return str.normalize('NFD').replace(/[\u0300-\u036f]/g, "")
        .replace(/ı/g, 'i')
        .replace(/İ/g, 'i')
        .replace(/ş/g, 's')
        .replace(/Ş/g, 's')
        .replace(/ç/g, 'c')
        .replace(/Ç/g, 'c')
        .replace(/ğ/g, 'g')
        .replace(/Ğ/g, 'g')
        .replace(/ü/g, 'u')
        .replace(/Ü/g, 'u')
        .replace(/ö/g, 'o')
        .replace(/Ö/g, 'o')
        .toLowerCase();
}

function filterTable() {
    var searchValue = normalizeString(document.getElementById('tableSearch').value);
    var table = document.getElementById('advanceRequestTable');
    var rows = table.getElementsByTagName('tr');

    for (var i = 1; i < rows.length; i++) { 
        var advanceTypeCell = rows[i].getElementsByClassName('type')[0];
        var approvalStatusCell = rows[i].getElementsByClassName('approval-status')[0];
        var advanceType = advanceTypeCell ? advanceTypeCell.textContent.toLowerCase() : "";
        var approvalStatus = approvalStatusCell ? normalizeString(approvalStatusCell.textContent) : "";


        if (advanceType.includes(searchValue) || approvalStatus.includes(searchValue)) {
            rows[i].style.display = '';
        } else {
            rows[i].style.display = 'none';
        }
    }
}



function filterTable2() {
    const searchInput = document.querySelector('input[name="table_search"]');
    const searchValue = searchInput.value.toLowerCase();
    const table = document.querySelector('.table-hover tbody');
    const rows = table.getElementsByTagName("tr");

    Array.from(rows).forEach(row => {
        const cells = row.getElementsByTagName("td");

       
        if (cells.length > 0) {
            const nameCell = cells[0]; 
            const nameText = nameCell.textContent.toLowerCase();

          
            if (nameText.includes(searchValue)) {
                row.style.display = "";
            } else {
                row.style.display = "none";
            }
        }
    });
}

function filterTable3() {
    const searchInput = document.querySelector('input[name="table_search"]');
    const searchValue = searchInput.value.toLowerCase();
    const table = document.querySelector('.table-hover tbody');
    const rows = table.getElementsByTagName("tr");

    Array.from(rows).forEach(row => {
        const cells = row.getElementsByTagName("td");

       
        if (cells.length > 0) {
            const nameCell = cells[1];
            const nameText = nameCell.textContent.toLowerCase();

           
            if (nameText.includes(searchValue)) {
                row.style.display = "";
            } else {
                row.style.display = "none";
            }
        }
    });
}