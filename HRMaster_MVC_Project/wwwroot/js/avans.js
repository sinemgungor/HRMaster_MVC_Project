document.addEventListener('DOMContentLoaded', function () {
    // TempData'dan maa� bilgisini alma
    var tempDataElement = document.getElementById('tempDataContainer');
    var salary = parseFloat(tempDataElement.innerText.trim()); // innerText ile veriyi alma

    var amountInput = document.getElementById('amountInput');
    var submitButton = document.getElementById('submitButton');
    var warningSpan = document.getElementById('amountWarning');

    submitButton.addEventListener('click', function (event) {
        var amount = parseFloat(amountInput.value);

        // Miktar�n ge�erli bir say� olup olmad���n� kontrol et
        if (isNaN(amount) || amount <= 0) {
            warningSpan.innerText = 'Gecerli bir miktar giriniz.';
            warningSpan.style.display = 'block'; // Uyar�y� g�ster
            event.preventDefault(); // Formu g�ndermeyi durdur
            return;
        }

        // Maa��n �� kat�ndan fazla olup olmad���n� kontrol et
        if (amount > 3 * salary) {
            warningSpan.innerText = 'Miktar maasin uc katindan fazla olamaz.Maasiniz: ' + salary;
            warningSpan.style.display = 'block'; // Uyar�y� g�ster
            event.preventDefault(); // Formu g�ndermeyi durdur
        } else {
            warningSpan.style.display = 'none'; // Uyar�y� gizle
        }
    });
});