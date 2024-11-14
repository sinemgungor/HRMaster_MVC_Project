document.addEventListener('DOMContentLoaded', function () {
    const dateInputs = document.querySelectorAll('input[type="date"]');
    dateInputs.forEach(input => {
        let dateValue = input.value;
        if (dateValue === '0001-01-01') {
            input.value = ''; // Boş tarih değeri göstermek için
        }
    });
});